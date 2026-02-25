//using Microsoft.AspNetCore.Mvc;
//using Microsoft.IdentityModel.Tokens;
//using System.IdentityModel.Tokens.Jwt;
//using System.Security.Claims;
//using System.Text;
//using StuApi.Models;

//namespace StuApi.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class AuthController : ControllerBase
//    {
//        private readonly IConfiguration _config;

//        public AuthController(IConfiguration config)
//        {
//            _config = config;
//        }

//        [HttpPost("login")]
//        public IActionResult Login([FromBody] LoginModel login)
//        {
//            if (login.Username == "admin" && login.Password == "123")
//            {
//                var token = GenerateToken(login.Username);
//                return Ok(new { token });
//            }

//            return Unauthorized("Invalid credentials");
//        }

//        private string GenerateToken(string username)
//        {
//            var claims = new[]
//            {
//                new Claim(ClaimTypes.Name, username)
//            };

//            var key = new SymmetricSecurityKey(
//                Encoding.UTF8.GetBytes(_config["Jwt:Key"]));

//            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

//            var token = new JwtSecurityToken(
//                issuer: _config["Jwt:Issuer"],
//                audience: _config["Jwt:Audience"],
//                claims: claims,
//                expires: DateTime.Now.AddMinutes(
//                    Convert.ToDouble(_config["Jwt:DurationInMinutes"])),
//                signingCredentials: creds);

//            return new JwtSecurityTokenHandler().WriteToken(token);
//        }
//    }
//}



using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StuApi.Data;
using StuApi.Models;

namespace StuApi.Controllers
{
    // This controller handles user authentication (signup and login) using a simple email and password mechanism.
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AuthController(AppDbContext context)
        {
            _context = context;
        }

        // SIGNUP
        [HttpPost("signup")]
        public async Task<IActionResult> Signup(User user)
        {
            if (await _context.Users.AnyAsync(u => u.Email == user.Email))
                return BadRequest("User already exists");

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok("Signup Successful");
        }

        // LOGIN
        [HttpPost("login")]
        public async Task<IActionResult> Login(User loginUser)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u =>
                    u.Email == loginUser.Email &&
                    u.Password == loginUser.Password);

            if (user == null)
                return Unauthorized("Invalid Credentials");

            return Ok("Login Successful");
        }
    }
}