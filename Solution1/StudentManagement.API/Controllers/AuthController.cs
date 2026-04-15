using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentManagement.Application.Common;
using StudentManagement.Application.DTOs;
using StudentManagement.Infrastructure.JWT;

namespace StudentManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly JwtService _jwt;

        public AuthController(JwtService jwt)
        {
            _jwt = jwt;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDto dto)
        {
            if (dto.Username == "admin" && dto.Password == "admin123")
            {
                var token = _jwt.GenerateToken(dto.Username);

                return Ok(ApiResponse<string>.SuccessResponse(token, "Login successful"));
            }

            return Unauthorized(ApiResponse<string>.Failure("Invalid credentials"));
        }
    }
}
