using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SmartTask.Application.DTOs.Auth;
using SmartTask.Infrastructure.Identity;

namespace SmartTask.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;
        public AuthController(UserManager<ApplicationUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(
        RegisterRequest request)
        {

            var existingUser =
                await _userManager.FindByEmailAsync(request.Email);


            if (existingUser != null)
            {
                return BadRequest(
                    "Email already exists");
            }


            var user = new ApplicationUser
            {
                FullName = request.FullName,

                Email = request.Email,

                UserName = request.Email
            };


            var result =
            await _userManager.CreateAsync(
                user,
                request.Password);



            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }


            return Ok(
                new
                {
                    message = "Registration successful"
                });

        }





        [HttpPost("login")]
        public async Task<IActionResult> Login(
            LoginRequest request)
        {

            var user =
            await _userManager
            .FindByEmailAsync(request.Email);



            if (user == null)
            {
                return Unauthorized();
            }


            var valid =
            await _userManager
            .CheckPasswordAsync(
                user,
                request.Password);



            if (!valid)
            {
                return Unauthorized();
            }


            var token =
            GenerateToken(user);



            return Ok(new AuthResponse
            {

                Token = token,

                Email = user.Email!,

                FullName = user.FullName

            });

        }





        private string GenerateToken(
            ApplicationUser user)
        {


            var claims = new[]
            {

            new Claim(
            JwtRegisteredClaimNames.Sub,
            user.Id),


            new Claim(
            JwtRegisteredClaimNames.Email,
            user.Email!),


            new Claim(
            "name",
            user.FullName)

        };



            var key =
            new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(
                _configuration["Jwt:Key"]!));



            var credentials =
            new SigningCredentials(
                key,
                SecurityAlgorithms.HmacSha256);



            var token =
            new JwtSecurityToken(

                issuer:
                _configuration["Jwt:Issuer"],


                audience:
                _configuration["Jwt:Audience"],


                claims: claims,


                expires:
                DateTime.Now.AddMinutes(60),


                signingCredentials:
                credentials

            );



            return new JwtSecurityTokenHandler()
            .WriteToken(token);

        }

    }
}