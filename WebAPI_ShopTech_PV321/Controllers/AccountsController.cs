using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI_ShopTech_PV321.Core.Interfaces;
using WebAPI_ShopTech_PV321.Core.Sevices;

namespace WebAPI_ShopTech_PV321.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountsService _accountsService;
        public AccountsController(IAccountsService accountsService)
        {
            _accountsService = accountsService;
        }

        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            var product = _accountsService.Get(id);
            return Ok(product);
        }


    }
}
