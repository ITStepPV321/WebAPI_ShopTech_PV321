using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using WebAPI_ShopTech_PV321.Core.DTOs.User;

using WEBAPI_ShopTech_PV321.Core.Interfaces;

namespace WEBAPI_ShopTech_PV321.Core.Sevices
{
    public class AccountsService : IAccountsService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IConfiguration _configuration;


        public AccountsService(UserManager<IdentityUser> userManager,
                                SignInManager<IdentityUser> signInManager,
                                IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        public async Task<IdentityUser> Get(string id)
        {
            IdentityUser? user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                throw new Exception("User not found by id");
            }

            return user;
        }

        public async Task<string> Login(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email!);

            if (user == null)
            {
                throw new Exception("User not found");
            }

            var res = await _userManager.CheckPasswordAsync(user, loginDto.Password!);

            if (!res)
            {
                throw new Exception("Wrong password");
            }

            await _signInManager.SignInAsync(user, true);
            //create Claim 
            var claimParams = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier,user.Id),
                new Claim(ClaimTypes.Name,user.UserName),

            };

            //generate jwt-tocken
            var key_jwt = _configuration.GetSection("Jwt:Key").Value;
            var issure_jwt = _configuration.GetSection("Jwt:Issuer").Value;
            var lifetime_jwt =int.Parse( _configuration.GetSection("Jwt:Lifetime").Value);

            var secretKey_jwt = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key_jwt));
            var signinCredentials=new SigningCredentials(secretKey_jwt, SecurityAlgorithms.HmacSha256);

            var tokenOptions = new JwtSecurityToken(
                issuer: issure_jwt,
                claims: claimParams,
                expires: DateTime.Now.AddMinutes(lifetime_jwt),
                signingCredentials: signinCredentials
                ); ;


            return JsonSerializer.Serialize(new JwtSecurityTokenHandler().WriteToken(tokenOptions));
        }

        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task Register(RegisterDto registerDto)
        {
            IdentityUser user = new IdentityUser()
            {
                UserName = registerDto.Username,
                Email = registerDto.Email,
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if (!result.Succeeded)
            {
                throw new Exception($"{String.Join(",", result.Errors)}");
            }
        }
    }
}
