using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using TimeKeeper.DAL;
using TimeKeeper.Domain;

namespace TimeKeeper.API.Controllers
{
    //[Authorize(Roles ="admin,user")]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : BaseController
    {
        public UsersController(TimeKeeperContext context) : base(context) { }

        [HttpGet]
        public IActionResult Get()
        {
            var currentUser = HttpContext.User as ClaimsPrincipal;
            List<string> claims = new List<string>();
            foreach (Claim claim in currentUser.Claims) claims.Add(claim.Value);
            var users = Unit.Users.Get().ToList();
            return Ok(new { claims, users});
        }
        //[AllowAnonymous]
        //[HttpPost]
        //public IActionResult Login([FromBody] User user)
        //{
        //    User control = Unit.Users.Get(x => x.Username == user.Username && x.Password == user.Password).FirstOrDefault();
        //    if (control == null) return NotFound();
        //    byte[] bytes = Encoding.ASCII.GetBytes($"{control.Username}:{control.Password}");
        //    string base64 = Convert.ToBase64String(bytes);
        //    return Ok(new {
        //        control.Id,
        //        control.Name,
        //        control.Role,
        //        base64
        //    });
        //}
        [HttpGet]
        [Route("/login")]
        [Authorize]
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                var accessToken = HttpContext.GetTokenAsync(OpenIdConnectParameterNames.AccessToken).Result;
                var response = new
                {
                    Id = User.Claims.FirstOrDefault(c => c.Type == "sub").Value.ToString(),
                    Name = User.Claims.FirstOrDefault(c => c.Type == "given_name").Value.ToString(),
                    Role = User.Claims.FirstOrDefault(c => c.Type == "role").Value.ToString(),
                    accessToken
                };
                return Ok(response);
            }
            else
            {
                return NotFound();
            }
        }

        [AllowAnonymous]
        [Route("/api/logout")]
        [HttpGet]
        public async Task Logout()
        {
            if(User.Identity.IsAuthenticated)
            {
                await HttpContext.SignOutAsync("Cookies");
                await HttpContext.SignOutAsync("oidc");
            }            
        }
    }
}