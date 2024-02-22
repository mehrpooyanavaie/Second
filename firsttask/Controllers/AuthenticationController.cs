//using Microsoft.AspNetCore.Http;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Mvc;
//using firsttask.Models;
//using AutoMapper;
//using System.Security.Claims;
//using System.IdentityModel.Tokens.Jwt;
//using Microsoft.IdentityModel.JsonWebTokens;
//using Microsoft.IdentityModel.Tokens;
//using System.Text;
//using firsttask.Models.Helpers;
//using Microsoft.EntityFrameworkCore;

//namespace firsttask.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class AuthenticationController : ControllerBase
//    {
//        private readonly UserManager<Models.AppliacationUser> _userManager;
//        private readonly RoleManager<IdentityRole> _roleManager;
//        private readonly Data.AppContext _appContext;
//        private readonly IConfiguration _configuration;
//        private readonly TokenValidationParameters _tokenValidationParameters;
//        private readonly IMapper _mapper;
//        private readonly Data.MyFirstContext _firstConetext;
//        public AuthenticationController(UserManager<Models.AppliacationUser> userManager,
//            RoleManager<IdentityRole> roleManager,
//            Data.AppContext appContext,
//            IConfiguration configuration,
//            IMapper mapper,
//            TokenValidationParameters tokenValidationParameters)
//        {
//            _userManager = userManager;
//            _roleManager = roleManager;
//            _appContext = appContext;
//            _configuration = configuration;
//            _mapper = mapper;
//            _tokenValidationParameters = tokenValidationParameters;
//        }
//        [HttpPost("register-user")]
//        public async Task<ActionResult> Register(RegisterViewModel registerViewModel)
//        {
//            if (!ModelState.IsValid)
//            {
//                return BadRequest();
//            }
//            var userExists = await _userManager.FindByEmailAsync(registerViewModel.Email);
//            if (userExists != null)
//            {
//                return BadRequest();//user with this email already exists
//            }
//            var newUser = _mapper.Map<AppliacationUser>(registerViewModel);
//            newUser.SecurityStamp = Guid.NewGuid().ToString();
//            var result = await _userManager.CreateAsync(newUser, registerViewModel.Password);
//            if (result.Succeeded)
//            {
//                if (registerViewModel.Role == UserRoles.Maneger)
//                    await _userManager.AddToRoleAsync(newUser, UserRoles.Maneger);
//                else if (registerViewModel.Role == UserRoles.Customer)
//                    await _userManager.AddToRoleAsync(newUser, UserRoles.Customer);
//                return Ok();
//            }
//            else
//                return BadRequest();
//            //---------------------------you must to return id
//        }
//        [HttpPost("login-user")]
//        public async Task<ActionResult> Login(LoginViewModel loginViewModel)
//        {
//            //---------------------------you must to return id
//            if (!ModelState.IsValid)
//                return BadRequest();
//            var userExists = await _userManager.FindByEmailAsync(loginViewModel.Email);
//            if (userExists != null && await _userManager.CheckPasswordAsync(userExists, loginViewModel.Password))
//            {
//                var tokenValue = await GenerateJWTTokenasync(userExists, null);
//                return Ok(tokenValue);
//            }
//            return Unauthorized();
//        }
//        [HttpPost("refresh-token")]
//        public async Task<ActionResult> RefreshToken(TokenRequestViewModel tokenRequestViewModel)
//        {
//            //---------------------------you must to return id
//            if (!ModelState.IsValid)
//                return BadRequest();
//            var result = await VerifyAndGenerateTokenAsync(tokenRequestViewModel);
//            return Ok(result);
//        }

//        private async Task<AuthResultViewModel> VerifyAndGenerateTokenAsync(TokenRequestViewModel tokenRequestViewModel)
//        {
//            var jwtTokenHandler = new JwtSecurityTokenHandler();
//            var storedToken = await _appContext.RefreshTokens.FirstOrDefaultAsync(x => x.Token == tokenRequestViewModel.RefreshToken);
//            var dbuser = await _userManager.FindByIdAsync(storedToken.UserId);
//            try
//            {
//                var tokenCheckResult = jwtTokenHandler.ValidateToken(tokenRequestViewModel.Token, _tokenValidationParameters,
//                    out var validatedToken);
//                return await GenerateJWTTokenasync(dbuser, storedToken);
//            }
//            catch (SecurityTokenExpiredException)
//            {
//                if (storedToken.DateExpire >= DateTime.UtcNow)
//                {
//                    return await GenerateJWTTokenasync(dbuser, storedToken);
//                }
//                else
//                {
//                    return await GenerateJWTTokenasync(dbuser, null);
//                }
//            }
//        }

//        private async Task<Models.AuthResultViewModel> GenerateJWTTokenasync(AppliacationUser user, RefreshToken rToken)
//        {
//            var authClaims = new List<System.Security.Claims.Claim>()
//            {
//                new Claim(ClaimTypes.Name , user.UserName),
//                new Claim(ClaimTypes.NameIdentifier, user.Id),
//                new Claim(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Email , user.Email),
//                new Claim(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Sub , user.Email),
//                new Claim(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Jti , Guid.NewGuid().ToString())
//            };
//            //add user role claims
//            var userRoles = await _userManager.GetRolesAsync(user);
//            foreach (var userRole in userRoles)
//            {
//                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
//            }
//            var authSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["JWT:Secret"]));
//            var token = new JwtSecurityToken(issuer: _configuration["JWT:Issuer"], audience: _configuration["JWT:Audience"],
//                expires: DateTime.UtcNow.AddMinutes(10)/* change to 1 hour*/, claims: authClaims,
//                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256));
//            var jwttoken = new JwtSecurityTokenHandler().WriteToken(token);
//            if (rToken != null)
//            {
//                var rTokenresponse = new Models.AuthResultViewModel()
//                {
//                    Token = jwttoken,
//                    RefreshToken = rToken.Token,
//                    ExpiresAt = token.ValidTo
//                };
//                return rTokenresponse;
//            }
//            //var x = token.Id;
//            var refreshToken = new RefreshToken()
//            {
//                Id = token.Id,
//                IsRevoked = false,
//                UserId = user.Id,
//                DateAdded = DateTime.UtcNow,
//                DateExpire = DateTime.UtcNow.AddMonths(6),
//                Token = Guid.NewGuid().ToString() + "-" + Guid.NewGuid().ToString()
//            };
//            await _appContext.RefreshTokens.AddAsync(refreshToken);
//            await _appContext.SaveChangesAsync();
//            var response = new Models.AuthResultViewModel()
//            {
//                Token = jwttoken,
//                RefreshToken = refreshToken.Token,
//                ExpiresAt = token.ValidTo
//            };
//            return response;
//        }
//    }
//}
