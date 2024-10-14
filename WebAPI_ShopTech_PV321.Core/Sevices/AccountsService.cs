using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using WebAPI_ShopTech_PV321.Core.DTOs.User;

using WEBAPI_ShopTech_PV321.Core.Interfaces;

namespace WEBAPI_ShopTech_PV321.Core.Sevices
{
    public class AccountsService : IAccountsService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AccountsService(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
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

        public async Task Login(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email!);

            if (user == null)
            {
                throw new Exception("User not found");
            }

            var res = await _userManager.CheckPasswordAsync(user, loginDto.Password!);

            if (res)
            {
                throw new Exception("Wrong password");
            }

            await _signInManager.SignInAsync(user, true);
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
