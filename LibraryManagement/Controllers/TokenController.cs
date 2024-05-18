using LibraryManagement.Data;
using LibraryManagement.Models;
using LibraryManagement.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LibraryManagement.Controllers
{
    [Route("api/token")]
    [ApiController]
    public class TokenController : Controller
    {
        private readonly IUserRepository _userRepository;
        public IConfiguration _configuration { get; set; }

        public TokenController(IConfiguration configuration, IUserRepository userRepository)
        {
            _configuration = configuration;
            _userRepository = userRepository;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("Register")]
        public async Task<ActionResult<UserInfo>> Register(UserInfo user)
        {
            if(user != null)
            {
                var _user = _userRepository.GetUserByNameAndPassword(user.Name, user.Password);
                if (_user == null)
                {
                    _userRepository.AddUser(user);
                    return await Task.FromResult(user);
                }
                return BadRequest("User already exists!!");
            }
            else
            {
                return BadRequest("Fields are missing");
            }
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Post(UserInfo user)
        {
            if (user != null && user.Name != null && user.Password != null)
            {
                var _user = _userRepository.GetUserByNameAndPassword(user.Name, user.Password);

                if (_user != null)
                {
                    var claims = new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim("UserId", user.Id.ToString()),
                        new Claim("DisplayName", user.Name),
                        new Claim(ClaimTypes.Role, user.Role),
                    };
                  
                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(
                        _configuration["Jwt:Issuer"],
                        _configuration["Jwt:Audience"],
                        claims,
                        expires: DateTime.UtcNow.AddMinutes(10),
                        signingCredentials: signIn);

                    return Ok(new JwtSecurityTokenHandler().WriteToken(token));
                }
                else
                {
                    return BadRequest("Invalid credentials");
                }
            }
            else
            {
                return BadRequest("Fields are missing");
            }

        }


    }
}