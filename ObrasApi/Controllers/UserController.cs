﻿namespace Obras.Api.Controllers
{
    using AutoMapper;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Microsoft.IdentityModel.Tokens;
    using Obras.Api.Models;
    using Obras.Business.CompanyDomain.Models;
    using Obras.Business.CompanyDomain.Services;

    using Obras.Business.SharedDomain.Helpers;
    using Obras.Data;
    using Obras.Data.Entities;
    using System;
    using System.Collections.Generic;
    using System.IdentityModel.Tokens.Jwt;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
    public class UserController : ControllerBase
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly ICompanyService _companyService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserController(IConfiguration configuration, 
            SignInManager<User> signInManager,
            RoleManager<IdentityRole> roleManager,
            IHttpContextAccessor httpContextAccessor, 
            ICompanyService companyService,
            IMapper mapper,
            UserManager<User> userManager)
        {
            _configuration = configuration;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _companyService = companyService;
            _userManager = userManager;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] LoginDetails model)
        {
            // Check user exist in system or not
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return NotFound();
            }

            // Perform login operation
            var signInResult = await _signInManager.CheckPasswordSignInAsync(user, model.Password, true);
            if (signInResult.Succeeded)
            {
                // Obtain token
                TokenDetails token = await GetJwtSecurityTokenAsync(user);
                return Ok(token);
            }
            else
            {
                return BadRequest(new { message = "Username or password is incorrect" });
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteUser([FromQuery] string id)
        {
            try
            {
                var existingUserDetails = await _userManager.FindByIdAsync(id);
                if (existingUserDetails != null)
                {
                    await _userManager.DeleteAsync(existingUserDetails);
                }
                else
                {
                    return BadRequest(new { message = "User not found" });
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest(new { message = e.Message });
            }

            return Ok("User has been deleted");
        }

        [AllowAnonymous]
        [HttpPost("create-user")]
        public async Task<IActionResult> CreateUser([FromBody] UserModel model)
        {
            #region Users

            var userDetails =  new User {
                        Email = model.Email,
                        UserName = model.UserName,
                        PhoneNumber = model.PhoneNumber,
                        CompanyId = model.CompanyId,
                        NormalizedUserName = model.UserName.ToUpper(),
                        NormalizedEmail = model.Email.ToLower(),
                        EmailConfirmed = true,
                        PhoneNumberConfirmed = false,
                        TwoFactorEnabled = false,
                        LockoutEnabled = false,
                        AccessFailedCount = 0
            };

            try
            {
                var existingUserDetails = await _userManager.FindByEmailAsync(model.Email);
                if (existingUserDetails == null)
                {
                    if (model.CompanyId != null) {
                        var companyExist = await _companyService.GetCompanyId((int) model.CompanyId);
                        if (companyExist == null) {
                            return BadRequest(new { message = "Empresa não encontrada!" });
                        } 
                    }
                    var resultUser = await _userManager.CreateAsync(userDetails);
                    if (resultUser.Succeeded)
                    {
                        var resultPassword = await _userManager.AddPasswordAsync(userDetails, model.Password);
                        if (resultPassword.Succeeded)
                        {
                            try
                            {
                                await _userManager.AddToRoleAsync(userDetails, "Customer");
                            } catch (Exception e)
                            {
                                await _userManager.DeleteAsync(userDetails);
                                return BadRequest(new { message = "Verifique nome da Roles" });
                            }
                        }
                        else
                        {
                            await _userManager.DeleteAsync(userDetails);
                            return BadRequest(new { message = resultPassword.Errors });
                        }
                    } else
                    {
                        await _userManager.DeleteAsync(userDetails);
                        return BadRequest(new { message = resultUser.Errors });
                    }
                } else
                {
                    return BadRequest(new { message = "Email already used" });
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest(new { message = e.Message });
            }

            #endregion

            return Ok("User has been created");
        }

        [AllowAnonymous]
        [HttpPost("create-default-users")]
        public async Task<IActionResult> CreateDefaultUsers()
        {
            #region Roles

            var rolesDetails = new List<string>
            {
                    Constants.Roles.Customer,
                    Constants.Roles.Engineer,
                    Constants.Roles.Admin
            };

            foreach (string roleName in rolesDetails)
            {
                if (!await _roleManager.RoleExistsAsync(roleName))
                {
                    await _roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            #endregion

            #region Users

            var userDetails = new Dictionary<string, User>{
                {
                    Constants.Roles.Admin,
                    new User { 
                        Email = "admin@demo.com", 
                        UserName = "AdminUser", 
                        EmailConfirmed = true, 
                        PhoneNumberConfirmed = false,
                        TwoFactorEnabled = false,
                        LockoutEnabled = false,
                        AccessFailedCount = 0
                    }
                }
            };

            try
            {

                foreach (var details in userDetails)
                {
                    var existingUserDetails = await _userManager.FindByEmailAsync(details.Value.Email);
                    if (existingUserDetails == null)
                    {
                        await _userManager.CreateAsync(details.Value);
                        await _userManager.AddPasswordAsync(details.Value, "Password");
                        await _userManager.AddToRoleAsync(details.Value, details.Key);
                    }
                }
            } catch (Exception e)
            {
                Console.WriteLine(e);
            }

            #endregion

            return Ok("Default User has been created");
        }

        [HttpGet("me")]
        public async Task<IActionResult> GetUser()
        {
            var userId = User?.Identities?.FirstOrDefault()?.Claims?.Where(a => a.Type == "sub")?.FirstOrDefault()?.Value;
            if (userId == null) return Unauthorized();

            var user = await _userManager.FindByIdAsync(userId);

            var rolesOfUser = await _userManager.GetRolesAsync(user);

            var company = await _companyService.GetCompanyId(user.CompanyId != null ? (int) user.CompanyId : 0);

            var companyModel = _mapper.Map<CompanyModel>(company);

            #region Users

            var userDetails = new UserDetails
            {
                Email = user.Email,
                UserName = user.UserName,
                PhoneNumber = user.PhoneNumber,
                Roles = rolesOfUser.FirstOrDefault(),
                Company = companyModel
            };

            #endregion

            return Ok(userDetails);
        }


        private async Task<TokenDetails> GetJwtSecurityTokenAsync(User user)
        {
            var keyInBytes = System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("JwtIssuerOptions:SecretKey").Value);
            SigningCredentials credentials = new SigningCredentials(new SymmetricSecurityKey(keyInBytes), SecurityAlgorithms.HmacSha256);
            DateTime tokenExpireOn = DateTime.Now.AddDays(3);

            var rolesOfUser = await _userManager.GetRolesAsync(user);
            var tokenClaims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim(ClaimTypes.NameIdentifier, user.Id)
            };

            // Adicionar todas as roles do usuário às claims
            foreach (var role in rolesOfUser)
            {
                tokenClaims.Add(new Claim(ClaimTypes.Role, role));
            }

            // Make JWT token
            JwtSecurityToken token = new JwtSecurityToken(
                issuer: _configuration.GetSection("JwtIssuerOptions:Issuer").Value,
                audience: _configuration.GetSection("JwtIssuerOptions:Audience").Value,
                claims: tokenClaims,
                expires: tokenExpireOn,
                signingCredentials: credentials
            );

            // Return it
            TokenDetails TokenDetails = new TokenDetails
            {
                UserId = user.Id,
                Name = user.UserName,
                Email = user.Email,
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                ExpireOn = tokenExpireOn,
            };

            // Set current user details for busines & common library
            var currentUser = await _userManager.FindByEmailAsync(user.Email);

            // Add new claim details
            var existingClaims = await _userManager.GetClaimsAsync(currentUser);
            await _userManager.RemoveClaimsAsync(currentUser, existingClaims);
            await _userManager.AddClaimsAsync(currentUser, tokenClaims);

            return TokenDetails;
        }
    }
}
