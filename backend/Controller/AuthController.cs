using NextCore.backend.Repositories;
using NextCore.backend.Dtos;
using NextCore.backend.Helpers;
using NextCore.backend.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace auth.Controllers
{
    [Route("api")]
    [ApiController]
    public class AuthController : Controller 
    {
        private readonly IUserRepository _repository;
        private readonly JwtService _jwtService;
        private const string UploadsFolder = "uploads";

        public AuthController(IUserRepository repository, JwtService jwtService) {
            _repository = repository;
            _jwtService = jwtService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromForm] RegisterDTO dto) {
            try {
                if (dto.imageKtp == null || dto.imageKtp.Length == 0)
                    return BadRequest(new { message = "KTP image is required." });

                if (string.IsNullOrWhiteSpace(dto.userId))
                    return BadRequest(new { message = "User ID (NIK) is required." });

                if (string.IsNullOrWhiteSpace(dto.userEmail) || !Regex.IsMatch(dto.userEmail, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                    return BadRequest(new { message = "Invalid email format." });

                if (string.IsNullOrWhiteSpace(dto.password) || dto.password.Length < 6)
                    return BadRequest(new { message = "Password must be at least 6 characters long." });

                string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), UploadsFolder);
                string uniqueFileName = Guid.NewGuid().ToString() + "_" + dto.imageKtp.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                // Create directory if it doesn't exist
                if (!Directory.Exists(uploadsFolder))
                    Directory.CreateDirectory(uploadsFolder);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await dto.imageKtp.CopyToAsync(fileStream);
                }

                // Create user
                var user = new User
                {
                    userId = dto.userId,
                    firstName = dto.firstName,
                    lastName = dto.lastName,
                    userEmail = dto.userEmail,
                    userPhone = dto.userPhone,
                    password = BCrypt.Net.BCrypt.HashPassword(dto.password),
                    imageKtpPath = Path.Combine(UploadsFolder, uniqueFileName),
                };

                _repository.Create(user);

                return Created("success", new { message = "User registered successfully." });
            }
            catch (Exception ex) {
                return StatusCode(500, new { message = "An error occurred while processing your request.", error = ex.Message });
            }
        }

        [HttpPost("login")]
        public IActionResult Login(LoginDTO dto) {
            try {
                var user = _repository.GetByEmail(dto.userEmail);
                if (user == null)
                    return BadRequest(new { message = "Account is not registered." });

                if (!BCrypt.Net.BCrypt.Verify(dto.password, user.password))
                    return BadRequest(new { message = "Invalid credentials." });

                var jwt = _jwtService.generate(user.userId);

                Response.Cookies.Append("jwt", jwt, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict
                });

                return Ok(new { message = "Login successful." });
            }
            catch (Exception ex) {
                return StatusCode(500, new { message = "An error occurred during login.", error = ex.Message });
            }
        }

        [HttpGet("user")]
        public IActionResult User()
        {
            try {
                var jwt = Request.Cookies["jwt"];

                if (string.IsNullOrWhiteSpace(jwt))
                    return Unauthorized(new { message = "JWT token is missing." });

                var token = _jwtService.Verify(jwt);
                string userId = token.Issuer;

                var user = _repository.GetById(userId);

                if (user == null)
                    return NotFound(new { message = "User not found." });

                return Ok(user);
            }
            catch (Exception ex) {
                return Unauthorized(new { message = "Unauthorized access.", error = ex.Message });
            }
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("jwt");
            return Ok(new { message = "Logout successful." });
        }
    }
}
