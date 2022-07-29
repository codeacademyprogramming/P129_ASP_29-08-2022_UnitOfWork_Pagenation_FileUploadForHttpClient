using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using P129FirstApi.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Text.Encodings;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;
using P129FirstApi.Interfaces;
using P129FirstApi.DTOs.UserDTOs;

namespace P129FirstApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IJWTManager _jWTManager;

        public AccountsController(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, IJWTManager jWTManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _jWTManager = jWTManager;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            AppUser appUser = await _userManager.FindByNameAsync("SuperAdmin");

            IList<string> roles = await _userManager.GetRolesAsync(appUser);

            //JwtSecurityToken DejwtSecurityToken = jwtSecurityTokenHandler.ReadToken(token) as JwtSecurityToken;

            //List<Claim> deClaims = DejwtSecurityToken.Claims.ToList();

            //return Ok(new { token, deClaims.FirstOrDefault(x=>x.Type == "EleBele").Value});

            string token = _jWTManager.GenerateToken(appUser, roles);
            string userName = _jWTManager.GetUserNameByToken(token);

            return Ok(new { token,userName});
        }

        //[HttpGet]
        //public async Task<IActionResult> Get()
        //{
        //    await _roleManager.CreateAsync(new IdentityRole { Name = "SuperAdmin" });
        //    await _roleManager.CreateAsync(new IdentityRole { Name = "Admin" });
        //    await _roleManager.CreateAsync(new IdentityRole { Name = "Member" });

        //    AppUser appUser = new AppUser
        //    {
        //        Name = "Super",
        //        SurName = "Admin",
        //        UserName = "SuperAdmin",
        //        Email = "superadmin@code.az"
        //    };

        //    IdentityResult identityResult = await _userManager.CreateAsync(appUser, "SuperAdmin123");

        //    await _userManager.AddToRoleAsync(appUser, "SuperAdmin");

        //    return Ok("Yarandi");
        //}
    }
}
