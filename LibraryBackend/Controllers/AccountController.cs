using AutoMapper;
using LibraryBackend.DTO.Authentication;
using LibraryBackend.Utilities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        private readonly IMapper mapper;

        public AccountController(IConfiguration configuration, UserManager<ApplicationIdentityUser> userManager,
            SignInManager<ApplicationIdentityUser> signInManager, IMapper mapper)
        {
            this.Configuration = configuration;
            this.UserManager = userManager;
            this.SignInManager = signInManager;
            this.mapper = mapper;
        }

        [HttpPost]
        [Route("register")]
        public async Task<ActionResult<AuthenticationResponse>> RegisterAccount([FromBody] UserCredentials userCredentials)
        {
            var userExist = await UserManager.FindByEmailAsync(userCredentials.Email);

            if (userExist != null) return BadRequest("El usuario ya está registrado");

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
                return BadRequest(result.Errors);
            }
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<AuthenticationResponse>> Login([FromBody] LoginCredentials loginCredentials)
        {
            var result = await SignInManager.PasswordSignInAsync
(
                loginCredentials.Email, 
                loginCredentials.Password,
                isPersistent: false, 
                lockoutOnFailure: false);

            if (result.Succeeded)
            {
                return await LoginToken(loginCredentials);
            }
            else
            {
                return BadRequest("Hubo un error con las credenciales");
            }
        }

        [HttpPatch]
        [Route("update-password")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> UpdatePassword([FromBody] UpdatePasswordDTO newPassword)
        {
            var emailClaim = HttpContext.User.Claims.FirstOrDefault(claim => claim.Type == "email");
            if (emailClaim == null)
            {
                return BadRequest("No se encontró la cuenta del usuario.");
            }
            var email = emailClaim.Value;
            var user = await UserManager.FindByEmailAsync(email);
            if (user == null)
            {
                return NotFound("Usuario no encontrado");
            }

            // Generar el hash de la nueva contraseña y actualizarla en el usuario
            var newPasswordHash = UserManager.PasswordHasher.HashPassword(user, newPassword.Password);
            user.PasswordHash = newPasswordHash;

            // Actualizar el usuario en la base de datos
            var result = await UserManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return NoContent();
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }

        [HttpPost]
        [Route("make-admin")]
        public async Task<ActionResult> MakeAdmin([FromBody] MakeAdminDTO makeAdminDTO)
        {
            if (string.IsNullOrEmpty(makeAdminDTO.Email))
            {
                return BadRequest("El correo electrónico no puede estar vacío");
            }

            var user = await UserManager.FindByEmailAsync(makeAdminDTO.Email);

            if(user == null) { return BadRequest("No se encontró al usuario"); };

            await UserManager.AddClaimAsync(user, new Claim("isAdmin", "true"));
            return NoContent();
        }

        [HttpPost]
        [Route("remove-admin")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "isAdmin")]
        public async Task<ActionResult> RemoveAdmin([FromBody] MakeAdminDTO makeAdminDTO)
        {
            if (string.IsNullOrEmpty(makeAdminDTO.Email))
            {
                return BadRequest("El correo electrónico no puede estar vacío");
            }

            var user = await UserManager.FindByEmailAsync(makeAdminDTO.Email);

            if (user == null) { return BadRequest("No se encontró al usuario"); };

            await UserManager.RemoveClaimAsync(user, new Claim("isAdmin", "true"));
            return NoContent();
        }

        [HttpGet]
        // Deberá ir protegida
        [Route("get-accounts")]
        public async Task<ActionResult<List<AccountDataDTO>>> GetAccounts()
        {
            var accounts = await UserManager.Users.ToListAsync();
            return mapper.Map<List<AccountDataDTO>>(accounts);
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

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT_KEY"]));
            var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.UtcNow.AddHours(8).ToLocalTime();

            var securityToken = new JwtSecurityToken(
                issuer: null, audience: null, claims, notBefore: null,
                expires, signingCredentials);

            return new AuthenticationResponse()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(securityToken),
                Expiration = expires,
            };
        }

        private async Task<AuthenticationResponse> LoginToken(LoginCredentials loginCredentials)
        {
            var user = await UserManager.FindByEmailAsync(loginCredentials.Email);

            var claims = new List<Claim>()
            {
                new Claim("email", user.Email),
                new Claim("name", user.Name),
                new Claim("lastName", user.LastName),
                new Claim("employeeKey", user.EmployeeKey),
            };

            var claimsDB = await UserManager.GetClaimsAsync(user);
            claims.AddRange(claimsDB);

            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT_KEY"]));
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
