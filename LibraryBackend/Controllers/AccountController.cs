using LibraryBackend.DTO.Authentication;
using LibraryBackend.Utilities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LibraryBackend.Controllers
{
    [Route("api/accounts")]
    public class AccountController : ControllerBase
    {
        private readonly IConfiguration Configuration;
        private readonly UserManager<ApplicationIdentityUser> UserManager;
        private readonly SignInManager<ApplicationIdentityUser> SignInManager;

        public AccountController(IConfiguration configuration, UserManager<ApplicationIdentityUser> userManager, 
            SignInManager<ApplicationIdentityUser> signInManager)
        {
            this.Configuration = configuration;
            this.UserManager = userManager;
            this.SignInManager = signInManager;
        }

        [HttpPost]
        [Route("register")]
        public async Task<ActionResult<AuthenticationResponse>> RegisterAccount(UserCredentials userCredentials)
        {
            var user = new ApplicationIdentityUser 
            { 
                UserName = userCredentials.Email, 
                Email = userCredentials.Email,
                Name = userCredentials.Name,
                LastName = userCredentials.LastName,
                EmployeeKey = userCredentials.EmployeeKey,
            };

            var result = await UserManager.CreateAsync(user, userCredentials.Password);

            if (result.Succeeded)
            { 
                return BuildToken(userCredentials);
            }
            else
            {
                return BadRequest(result.Errors)
;
            }
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<AuthenticationResponse>> Login(LoginCredentials loginCredentials)
        {
            var result = await SignInManager.PasswordSignInAsync(loginCredentials.Email, loginCredentials.Password, 
                isPersistent: false, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                return await LoginToken(loginCredentials);
            } 
            else
            {
                return BadRequest("Hubo un error con las credenciales");
            }
        }

        [HttpPost]
        [Route("make-admin")]
        public async Task<ActionResult> MakeAdmin(MakeAdminDTO makeAdminDTO)
        {
            var user = await UserManager.FindByEmailAsync(makeAdminDTO.Email);
            await UserManager.AddClaimAsync(user, new Claim("isAdmin", "true"));
            return Ok("Se agregó cuenta de Administrador");
        }

        [HttpPost]
        [Route("remove-admin")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "isAdmin")]
        public async Task<ActionResult> RemoveAdmin(MakeAdminDTO makeAdminDTO)
        {
            var user = await UserManager.FindByEmailAsync(makeAdminDTO.Email);
            await UserManager.RemoveClaimAsync(user, new Claim("isAdmin", "true"));
            return Ok("Se removió el permiso de Administrador");
        }

        private AuthenticationResponse BuildToken(UserCredentials userCredentials)
        {
            var claims = new List<Claim>()
            {
                new Claim("email", userCredentials.Email),
                new Claim("name", userCredentials.Name),
                new Claim("lastName", userCredentials.LastName),
                new Claim("employeeKey", userCredentials.EmployeeKey),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["jwtkey"]));
            var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.UtcNow.AddHours(8);

            var securityToken = new JwtSecurityToken(
                issuer: null, audience: null, claims, notBefore: null, 
                expires, signingCredentials);

            return new AuthenticationResponse()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(securityToken),
            };
        }

        private async Task<AuthenticationResponse> LoginToken(LoginCredentials loginCredentials)
        {
            var user = await UserManager.FindByEmailAsync(loginCredentials.Email);
            
            var claims = new List<Claim>()
            {
                new Claim("email", user.Email),
                new Claim("employeeKey", user.EmployeeKey),
            };

            var claimsDB = await UserManager.GetClaimsAsync(user);
            claims.AddRange(claimsDB);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["jwtkey"]));
            var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.UtcNow.AddHours(8);

            var securityToken = new JwtSecurityToken(
                issuer: null, audience: null, claims, notBefore: null,
                expires, signingCredentials);

            return new AuthenticationResponse()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(securityToken),
                Expiration = expires,
            };
        }
    }
}
