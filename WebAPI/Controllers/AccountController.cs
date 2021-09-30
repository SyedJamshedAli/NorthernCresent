using Domain.Entities;
using Domain.Interfaces;
using Domain.Models;
using Infrastructure.DatabaseContext;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        //private readonly SignInManager<ApplicationUser> signInManager;
        //private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private IConfiguration _configuration;
        private IRepository<ApplicationUser> _eventRepository;
        private IRepository<tblUserRole> _repository;

        private IUser _User;
        public AccountController(/*UserManager<ApplicationUser> userManager,*/ RoleManager<IdentityRole> roleManager, IConfiguration configuration, /*SignInManager<ApplicationUser> signInManager,*/ IRepository<ApplicationUser> eventRepository, IUser user, IRepository<tblUserRole> repository)
        {
           // this.userManager = userManager;
            this.roleManager = roleManager;
            _configuration = configuration;
           // this.signInManager = signInManager;
            _eventRepository = eventRepository;
            _User = user;
            _repository=repository;
        }
        [HttpPost]

        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {
            //var users = await userManager.FindByEmailAsync(model.Username);
            //if (users != null && await userManager.CheckPasswordAsync(users, model.Password))
            //{
            //    return Ok(new Responce { Status = "Success", Message = "Login Successfully!" });
            //}
            return StatusCode(StatusCodes.Status500InternalServerError, new Responce { Status = "Error", Message = "User creation failed! Please check user details and try again." });

            //var user = await userManager.FindByNameAsync(model.Username);

            //if (user != null && await userManager.CheckPasswordAsync(user, model.Password))
            //{
            //    var userRoles = await userManager.GetRolesAsync(user);

            //    var authClaims = new List<Claim>
            //    {
            //        new Claim("UserID", user.Id.ToString()),
            //        new Claim(ClaimTypes.Name, user.UserName),
            //        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            //    };

            //    foreach (var userRole in userRoles)
            //    {
            //        authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            //    }

            //    var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            //    var token = new JwtSecurityToken(
            //        issuer: _configuration["JWT:ValidIssuer"],
            //        audience: _configuration["JWT:ValidAudience"],
            //        expires: DateTime.Now.AddHours(3),
            //        claims: authClaims,
            //        signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            //        );

            //    return Ok(new
            //    {
            //        token = new JwtSecurityTokenHandler().WriteToken(token),
            //        expiration = token.ValidTo
            //    });
            //}
            //return Unauthorized();
        }
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] SignUpViewModel model)
        {
            //var userExists = await userManager.FindByNameAsync(model.Email);
            //if (userExists != null)
            //    return StatusCode(StatusCodes.Status500InternalServerError, new Responce { Status = "Error", Message = "User already exists!" });

            ApplicationUser user = new ApplicationUser()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Discipline=model.Discipline,
                JobTitle=model.JobTitle,
                Phone=model.Phone,
                App=model.App,
                companyID=model.companyID,
                emailID=model.emailID
                

            };
            //var result = await userManager.CreateAsync(user, model.Password);
            //if (result.Succeeded)
            //{
            //    var userRoleExists = await roleManager.RoleExistsAsync(UserRoles.User);   // returning false
            //    if (userRoleExists)
            //    {
            //        await userManager.AddToRoleAsync(user, UserRoles.User);
            //        return StatusCode(StatusCodes.Status500InternalServerError, new Responce { Status = "Error", Message = "User creation failed! Please check user details and try again." });
            //    }
            //}
            //else
            //{
            //    return Ok(new Responce { Status = "error", Message = result.ToString() });
            //}

            return Ok(new Responce { Status = "Success", Message = "User created successfully!" });
        }
        [HttpGet]
        [Route("AssignRole")]
        public UserRoleViewModel AssignRole()
        {
            
            var Role = _User.GetRole();
            var getdata = _User.Getuserdata();
           // var getdata = _User.GetRole();

            return new UserRoleViewModel()
            {
                roles = Role.roles,
                GetuserAllDatas = getdata

            };
           
        }
        [HttpPost]
        [Route("SaveAssignRole")]
        public tblUserRole SaveAssignRole(tblUserRole userRole)
        {
            try
            {
                if (userRole.ID != 0)
                {
                    _repository.Update(userRole);
                }
                else
                {
                    userRole.IsDeleted = false;
                    _repository.Insert(userRole);
                }
                return userRole;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        //[HttpGet]
        //public UserRoleViewModel GetUserRole(long id)
        //{
        //   return _User.GetById(id);
        //   // return userRole;
        //}
        [HttpGet]
        [Route("GetUserRole/{id}")]
        public UserRoleViewModel GetUserRole(long id)
        {
            return _User.GetById(id);
           
        }


        [HttpGet]
        [Route("companyUserRole/{email}")]
        public UserRoleViewModel CompanyUserRole(string email)
        {
            return _User.CompanyUserRole(email);

        }

        [HttpGet("DeleteUser/{id}")]
        public void DeleteUser(long id)
        {
            var data = _repository.GetById((int)id);
            data.IsDeleted = true;
             _repository.Update(data);
        }
    }
}
