using Infrastructure.DatabaseContext;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public class AccountController : Controller
    {
        //private SignInManager<ApplicationUser> _signInManager;
        //private UserManager<ApplicationUser> _userManager;
        private IConfiguration _configuration;
        private string apiUrl;
        public AccountController(IConfiguration configuration/*, SignInManager<ApplicationUser> signInManager*//*, UserManager<ApplicationUser> userManager*/)
        {
            _configuration = configuration;
            //_signInManager = signInManager;
            //_userManager = userManager;
            apiUrl = _configuration["ApiUrl"].ToString();
        }
        public IActionResult Login()
        {
            return View();
        }
        //[HttpPost]
        //public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var user = await _userManager.FindByNameAsync(loginViewModel.Username);
        //        if (user != null)
        //        {
        //            var result = await _signInManager.PasswordSignInAsync(loginViewModel.Username, loginViewModel.Password, true, false);
        //            if (result.Succeeded)
        //            {
        //                using (var httpClient = new HttpClient())
        //                {
        //                    StringContent content = new StringContent(JsonConvert.SerializeObject(loginViewModel), Encoding.UTF8, "application/json");

        //                    using (var response = await httpClient.PostAsync(apiUrl + "Account/Login", content))
        //                    {
        //                        if (response.IsSuccessStatusCode)
        //                        {
        //                            string apiResponse = await response.Content.ReadAsStringAsync();
        //                            var res = JObject.Parse(apiResponse);
        //                            //string data = JObject.Parse(apiResponse)["token"].ToString();
        //                            //HttpContext.Session.SetString("token", data);
        //                            //HttpContext.Session.SetString("Id", user.Id);
        //                            //return RedirectToAction("UserInfo", "Users");
        //                            var role = await _userManager.GetRolesAsync(user);
        //                            if (role.FirstOrDefault() == "User")
        //                            {
        //                                return RedirectToAction("Index", "Home");      //redirected to home dashboard here. instead of UserInfo of User

        //                            }
        //                            //else if (role.FirstOrDefault() == "Admin")
        //                            //{
        //                            //    return RedirectToAction("AddClient", "Owner");
        //                            //}
        //                            //else
        //                            //{
        //                            //    return RedirectToAction("Dashboard", "HR");

        //                            //}

        //                        }
        //                    }
        //                }
        //            }

        //        }

        //        ModelState.AddModelError("", "Email or Password Is Incorrect ");
        //    }
        //    return View(loginViewModel);
        //}
        public IActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpViewModel model)
        {
            if (model.Password != model.CPassword)
            {
                ModelState.AddModelError("CPassword", "Password Not Match ");
                return View(model);
            }
            if (ModelState.IsValid)
            {
                using (var httpClient = new HttpClient())
                {
                    StringContent content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

                    using (var response = await httpClient.PostAsync(apiUrl + "Account/register", content))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            string apiResponse = await response.Content.ReadAsStringAsync();
                            var error = JsonConvert.DeserializeObject<Responce>(apiResponse);
                            if (error.Status == "error")
                            {
                                ModelState.AddModelError("", error.Message);
                                return View(model);
                            }
                            else
                            {
                                return RedirectToAction(nameof(Login));
                            }
                          
                        }
                       
                    }
                   
                }
            }
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> AssignRole(UserRoleViewModel model)
        {
            
            if (ModelState.IsValid)
            {
                using (var httpClient = new HttpClient())
                {
                    StringContent content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

                    using (var response = await httpClient.PostAsync(apiUrl + "Account/SaveAssignRole", content))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            string apiResponse = await response.Content.ReadAsStringAsync();
                            var error = JsonConvert.DeserializeObject<Responce>(apiResponse);
                            if (error.Status == "error")
                            {
                                ModelState.AddModelError("", error.Message);
                                return View(model);
                            }
                            else
                            {
                                return RedirectToAction(nameof(AssignRole));
                            }

                        }

                    }

                }
            }
            return View(model);
        }
        [HttpGet]
        //public async Task<IActionResult> EditAssignRole(long? id)
        //{
        //    UserRoleViewModel userRole = new UserRoleViewModel();
        //    using (var httpClient = new HttpClient())
        //    {
        //        using (var response = await httpClient.GetAsync(apiUrl + "Account/GetUserRole" + id))
        //        {
        //            if (response.StatusCode == System.Net.HttpStatusCode.OK)
        //            {
        //                string apiResponse = await response.Content.ReadAsStringAsync();
        //                userRole = JsonConvert.DeserializeObject<UserRoleViewModel>(apiResponse);
        //            }
        //            else
        //                ViewBag.StatusCode = response.StatusCode;
        //        }
        //    }
        //    return RedirectToAction(nameof(AssignRole));
        //}
        public async Task<IActionResult> EditAssignRole(long? id)
        {
            UserRoleViewModel user = new UserRoleViewModel();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(apiUrl + "Account/GetUserRole/" + id))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        user = JsonConvert.DeserializeObject<UserRoleViewModel>(apiResponse);
                    }
                    else
                        ViewBag.StatusCode = response.StatusCode;
                }
            }
            return View("AssignRole", user);
        }
        public async Task<IActionResult> LogOut()
        {
            //await _signInManager.SignOutAsync();
            var user = User as ClaimsPrincipal;
            var identity = user.Identity as ClaimsIdentity;
            var name = identity.FindFirst("Name");
            //var claims = identity.Claims;
            identity.RemoveClaim(name);
            HttpContext.Session.Clear();

            return RedirectToAction("MainPage", "Home");
        }
        public IActionResult ForgetPassword()
        {
            return View();
        }
        public async Task<IActionResult> AssignRole()
        {
            UserRoleViewModel userRole = new UserRoleViewModel();
            using (var httpClient = new HttpClient())
            {
               
                using (var response = await httpClient.GetAsync(apiUrl + "Account/AssignRole"))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        userRole = JsonConvert.DeserializeObject<UserRoleViewModel>(apiResponse);
                    }
                    else
                        ViewBag.StatusCode = response.StatusCode;
                }
            }
            return View(userRole);
        }
        public async Task<IActionResult> DeleteUser(long? id)
        {
            UserRoleViewModel personalData = new UserRoleViewModel();
            using (var httpClient = new HttpClient())
            {
               using (var response = await httpClient.GetAsync(apiUrl + "Account/DeleteUser/" + id))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        personalData = JsonConvert.DeserializeObject<Models.UserRoleViewModel>(apiResponse);
                    }
                    else
                        ViewBag.StatusCode = response.StatusCode;
                }
            }
            return RedirectToAction(nameof(AssignRole));

        }
    }
}
