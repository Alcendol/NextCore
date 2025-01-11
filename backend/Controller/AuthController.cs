using auth.Data;
using auth.Dtos;
using auth.Helpers;
using auth.Models;
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
        public IActionResult Register(RegisterDto dto) {
            var user = new User
            {
                userId = dto.userId, // Nanti isinya pake NIK, jangan generate
                firstName = dto.firstName,
                lastName = dto.lastName,
                userEmail = dto.userEmail,
                userPhone = dto.userPhone,
                password = BCrypt.Net.BCrypt.HashPassword(dto.password),
                imageKtp = dto.imageKtp,
                role = dto.role,
            };

            return Created("success", _repository.Create(user));
        }

        [HttpPost("login")]
        public IActionResult Login(LoginDto dto) {
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
                    message = "succes"
                }
            );
        }

        [HttpGet("user")]
        public IActionResult User()
        {
            try {
                var jwt = Request.Cookies["jwt"];

                var token = _jwtService.Verify(jwt);

                int userId = int.Parse(token.Issuer);

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