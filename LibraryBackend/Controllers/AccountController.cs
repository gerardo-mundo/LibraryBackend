using LibraryBackend.DTO.Authentication;
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
        private readonly UserManager<IdentityUser> UserManager;
        private readonly SignInManager<IdentityUser> SignInManager;

        public AccountController(IConfiguration configuration, UserManager<IdentityUser> userManager, 
            SignInManager<IdentityUser> signInManager)
        {
            this.Configuration = configuration;
            this.UserManager = userManager;
            this.SignInManager = signInManager;
        }

        [HttpPost]
        [Route("register")]
        public async Task<ActionResult<AuthenticationResponse>> RegisterAccount(UserCredentials userCredentials)
        {
            var user = new IdentityUser { UserName = userCredentials.Email, Email = userCredentials.Email };
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
        public async Task<ActionResult<AuthenticationResponse>> Login(UserCredentials userCredentials)
        {
            var result = await SignInManager.PasswordSignInAsync(userCredentials.Email, userCredentials.Password, 
                isPersistent: false, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                return BuildToken(userCredentials);
            } 
            else
            {
                return BadRequest("Hubo un error con las credenciales");
            }
        }

        private AuthenticationResponse BuildToken(UserCredentials userCredentials)
        {
            var claims = new List<Claim>()
            {
                new Claim("email", userCredentials.Email)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["jwtkey"]));
            var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.UtcNow.AddDays(3);

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
