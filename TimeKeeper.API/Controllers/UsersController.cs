using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TimeKeeper.DTO.Models;
using TimeKeeper.DAL;
using TimeKeeper.Domain;
using TimeKeeper.DTO.Factory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace TimeKeeper.API.Controllers
{
    //[Authorize(Roles ="admin,user")]
    [Authorize(AuthenticationSchemes = "TokenAuthentication")]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : BaseController
    {
        public UsersController(TimeKeeperContext context) : base(context) { }

        //[HttpGet]
        //public IActionResult Get()
        //{
        //    var currentUser = HttpContext.User as ClaimsPrincipal;
        //    List<string> claims = new List<string>();
        //    foreach (Claim claim in currentUser.Claims) claims.Add(claim.Value);
        //    var users = Unit.Users.Get().ToList();
        //    return Ok(new { claims, users });
        //}
        ////[AllowAnonymous]
        ////[HttpPost]
        ////public IActionResult Login([FromBody] User user)
        ////{
        ////    User control = Unit.Users.Get(x => x.Username == user.Username && x.Password == user.Password).FirstOrDefault();
        ////    if (control == null) return NotFound();
        ////    byte[] bytes = Encoding.ASCII.GetBytes($"{control.Username}:{control.Password}");
        ////    string base64 = Convert.ToBase64String(bytes);
        ////    return Ok(new {
        ////        control.Id,
        ////        control.Name,
        ////        control.Role,
        ////        base64
        ////    });
        ////}
        //[HttpGet]
        //[Route("/login")]
        //[Authorize]
        //public IActionResult Login()
        //{
        //    if (User.Identity.IsAuthenticated)
        //    {
        //        var accessToken = HttpContext.GetTokenAsync(OpenIdConnectParameterNames.AccessToken).Result;
        //        var response = new
        //        {
        //            Id = User.Claims.FirstOrDefault(c => c.Type == "sub").Value.ToString(),
        //            Name = User.Claims.FirstOrDefault(c => c.Type == "given_name").Value.ToString(),
        //            Role = User.Claims.FirstOrDefault(c => c.Type == "role").Value.ToString(),
        //            //Team = Unit.Teams.Get(x => x.TeamMembers.Contains()).ToList(),
        //            accessToken
        //        };
        //        return Ok(response);
        //    }
        //    else
        //    {
        //        return NotFound();
        //    }
        //}

        //[AllowAnonymous]
        //[Route("/api/logout")]
        //[HttpGet]
        //public async Task Logout()
        //{
        //    if (User.Identity.IsAuthenticated)
        //    {
        //        await HttpContext.SignOutAsync("Cookies");
        //        await HttpContext.SignOutAsync("oidc");
        //    }
        //}
        [HttpGet("password")]
        public IActionResult GetUsersAndUpdate()
        {
            try
            {
                var query = Unit.Users.Get();
                foreach(User user in query)
                {
                    user.Password = user.Username.HashWith(user.Password);
                    Unit.Context.Entry(user).CurrentValues.SetValues(user);
                }
                Unit.Save();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var query = Unit.Users.Get();
                return Ok(query.Select(x => x.Create()).ToList().OrderBy(x => x.Name));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                User user = Unit.Users.Get(id);
                if (user == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(user.Create());
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpPost]
        public IActionResult Post([FromBody] User user)
        {
            try
            {
                Unit.Users.Insert(user);
                Unit.Save();
                return Ok(user.Create());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpPut("{id}")]
        public IActionResult Put([FromBody] User user, int id)
        {
            try
            {
                Unit.Users.Update(user, id);
                Unit.Save();
                return Ok(user.Create());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                Unit.Users.Delete(id);
                Unit.Save();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("/login")]
        public IActionResult Login([FromBody] LoginModel model)
        {
            try
            {
                User user = Access.Check(model.Username, model.Password);
                if (user != null)
                {
                    string token = Access.GetToken(user);
                    return Ok(new { UserModel = user.Create(), token });
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}