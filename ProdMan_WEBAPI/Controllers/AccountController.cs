using DataAccess.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Model;
using ProdMan_WEBAPI.Helpers;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ProdMan_WEBAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly SignInManager<ApplicationUser> signinManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManage;

        private readonly APISettings apiSettings;

        public AccountController(SignInManager<ApplicationUser> signinManager
            ,UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManage,
            IOptions<APISettings> settings
            )
        {
            this.signinManager = signinManager;
            this.userManager = userManager;
            this.roleManage = roleManage;
            apiSettings = settings.Value;
        }



        [HttpPost]
        public async Task<IActionResult> Login(AuthenticationRequestDTO authReq)
        {

            if (authReq == null || !ModelState.IsValid) return Unauthorized();

            var signInResult = await signinManager.PasswordSignInAsync(authReq.Username, authReq.Password,false,false);

            if (!signInResult.Succeeded)
            {
                var uresp = new AuthenticateResponseDTO() { IsAuthenticationSuccess = false, ErrorMesssage = "Wrong username or password" };
                return Unauthorized(uresp);
            }

            var appUser = await userManager.FindByNameAsync(authReq.Username);

            if (appUser == null) return Unauthorized();

            //Create Token
            var claims = await GetClaims(appUser);
            var credential = GetSigningCredentials();

            var tokenDescriptor = new JwtSecurityToken(
                issuer:apiSettings.ValidIssuer,
                audience:apiSettings.ValidAudience,
                claims:claims,
                signingCredentials:credential,
                expires:System.DateTime.Now.AddDays(7)
                
                );
            var token = new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);

            var response = new AuthenticateResponseDTO()
            {
                IsAuthenticationSuccess = true,
                Token = token,
                UserDTO = new UserDTO()
                {
                    Email = appUser.Email,
                    UserName = appUser.UserName,
                    PhoneNo = appUser.PhoneNumber
                }
            };
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserRequestDTO req)
        {

            UserRegistrationResponseDTO response = new UserRegistrationResponseDTO(); ;
            if(ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    Name = req.Name,
                    PhoneNumber = req.PhoneNo,
                    UserName = req.UserName,
                    Email = req.Email
                };
                var regResult = await userManager.CreateAsync(user,req.Password);

                if(regResult.Succeeded)
                {
                    try { 
                        var storedUser = await userManager.FindByEmailAsync(req.Email);
                        var roleReg = await userManager.AddToRoleAsync(storedUser, "Customer");
                        response.IsRegistrationSuccessful = true;
                        return StatusCode(201,response);
                    }catch(Exception ex)
                    {
                        response.IsRegistrationSuccessful = false;
                        response.ErrorMessages.Add(ex.Message);
                    }
                }
                response.ErrorMessages = regResult.Errors.Select(e => e.Description).ToList();
                response.IsRegistrationSuccessful = false;
                return BadRequest(response);

            }
            else
            {
                response.ErrorMessages.Add("Model is incomplete");
                response.IsRegistrationSuccessful = false;
                return BadRequest(response);
            }
        }

        private SigningCredentials GetSigningCredentials()
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(apiSettings.SecretKey));
            var credential = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            return credential;

        }

       private async Task<List<Claim>> GetClaims(ApplicationUser user)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(ClaimTypes.Name,user.UserName),
                new Claim("Id",user.Id),

            };
            var roles = await userManager.GetRolesAsync(user);
            foreach(var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            return claims;

        }
    }
}
