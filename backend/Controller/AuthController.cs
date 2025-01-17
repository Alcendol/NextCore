using NextCore.backend.Repositories;
using NextCore.backend.Dtos;
using NextCore.backend.Helpers;
using NextCore.backend.Models;
using Microsoft.AspNetCore.Mvc;

namespace auth.Controllers
{
    [Route("api")]
    [ApiController]
    public class AuthController : Controller 
    {
        private readonly IUserRepository _repository;
        private readonly JwtService _jwtService;
        public AuthController(IUserRepository repository, JwtService jwtService) {
            _repository = repository;
            _jwtService = jwtService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromForm] RegisterDTO dto) {
            if (dto.imageKtp == null || dto.imageKtp.Length == 0)
                return BadRequest("KTP image is required.");

            // Save the uploaded file
            string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "uploads");
            string uniqueFileName = Guid.NewGuid().ToString() + "_" + dto.imageKtp.FileName;
            string filePath = Path.Combine(uploadsFolder, uniqueFileName);

            // Create directory if it doesn't exist
            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await dto.imageKtp.CopyToAsync(fileStream);
            }
            var user = new User
            {
                userId = dto.userId, // Nanti isinya pake NIK, jangan generate
                firstName = dto.firstName,
                lastName = dto.lastName,
                userEmail = dto.userEmail,
                userPhone = dto.userPhone,
                password = BCrypt.Net.BCrypt.HashPassword(dto.password),
                imageKtpPath = Path.Combine("uploads", uniqueFileName),
            };

            _repository.Create(user);

            return Created("success", new { message = "User registered successfully." });
        }

        [HttpPost("login")]
        public IActionResult Login(LoginDTO dto) {
            var user = _repository.GetByEmail(dto.userEmail);
            if (user == null) return BadRequest(new {message= "Invalid Credentials"});
            if (!BCrypt.Net.BCrypt.Verify(dto.password, user.password))
            {
                return BadRequest(new {message= "Invalid Credentials"});
            }

            var jwt = _jwtService.generate(user.userId);

            Response.Cookies.Append("jwt", jwt, new CookieOptions
            {
                HttpOnly = true
            });

            return Ok(
                new{
                    message = "success"
                }
            );
        }

        [HttpGet("user")]
        public IActionResult User()
        {
            try {
                var jwt = Request.Cookies["jwt"];

                var token = _jwtService.Verify(jwt);

                string userId = token.Issuer;

                var user = _repository.GetById(userId);

                return Ok(user);
            }
            catch (Exception _) {
                return Unauthorized();
            }
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("jwt");
            return Ok(new 
            {
                message = "success"
            });
        }
    }
}