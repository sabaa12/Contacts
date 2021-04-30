using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using TestNg.Controllers.Identity;
using TestNg.Data.Models;
using TestNg.Models.identity;

namespace TestNg.Controllers
{
    public class IdentityController : ApiController
    {
        private readonly UserManager<User> userManager;
        private readonly ApplicationSetings appSetings;
        private readonly IIdentityservice identityService;

        public IdentityController(
            UserManager<User> userManager,
            IOptions<ApplicationSetings> appSetings,
            IIdentityservice identityService)
        {
            this.userManager = userManager;
            this.appSetings = appSetings.Value;
            this.identityService = identityService;
        }
        [HttpPost]
        [Route(nameof(Register))]
        public async Task<ActionResult> Register(RegisterUSerRequestModel model)
        {
            var user = new User
            {
                Email = model.Email,
                UserName = model.username,
                PhoneNumber=model.phone,
            };
            var result = await userManager.CreateAsync(user, model.password);
            if (result.Succeeded)
            {
                return Ok("good");
            }
            return BadRequest(result.Errors);
        }
       
        [HttpPost]
        [Route(nameof(Login))]
        
        public async Task<ActionResult<tokenResponse>> Login(LoginUSerRequestModel model)
        {
            var user = await userManager.FindByNameAsync(model.username);
            if (user == null)
            {
                return Unauthorized();
            }

            var passwordValid = await userManager.CheckPasswordAsync(user, model.password);

            if (!passwordValid)
            {
                return Unauthorized(    );
            }
            var token = identityService.GenerateToken(user.Id, user.UserName, appSetings.secret);

            return new tokenResponse { token = token };
        }

    }
}
