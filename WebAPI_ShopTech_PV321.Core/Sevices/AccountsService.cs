using Microsoft.AspNetCore.Identity;
using WebAPI_ShopTech_PV321.Core.DTOs.User;
using WebAPI_ShopTech_PV321.Core.Interfaces;

namespace WebAPI_ShopTech_PV321.Core.Sevices
{
    public class AccountsService : IAccountsService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        public AccountsService(UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager)
        {
            _userManager= userManager;
            _signInManager= signInManager;
        }
        public async Task<IdentityUser> Get(string id)
        {
            var user= await _userManager.FindByIdAsync(id);
            if (user == null) {
                throw new Exception("User not found by id");
            }
            return user;
        }

        public Task<string> Login(LoginDto loginDto)
        {
            throw new NotImplementedException();
        }

        public Task Logout()
        {
            throw new NotImplementedException();
        }

        public Task Register(RegisterDto registerDto)
        {
            throw new NotImplementedException();
        }
    }
}
