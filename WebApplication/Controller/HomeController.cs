using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace WebApplication.Controllers
{
    public class HomeController : Controller
    {
        private string apiUrl;
        private IConfiguration _configuration;
        public HomeController(IConfiguration configuration)
        {
            _configuration = configuration;
            apiUrl = _configuration["ApiUrl"].ToString();
        }
        public async  Task<IActionResult> Index()
        {

            var email = User.Identity.Name;
            // UserRoleViewModel user = new UserRoleViewModel();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(apiUrl + "Account/companyUserRole/" + email))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        var user = JsonConvert.DeserializeObject<UserRoleViewModel>(apiResponse);
                        HttpContext.Session.SetString("UserRole", user.RoleName);
                        
                        return View();
                    }
                    else
                        ViewBag.StatusCode = response.StatusCode;
                }
            }


          
           // ViewBag.Role = "Admin";
            return View();
        }
       // [AllowAnonymous]
        public IActionResult MainPage()
        {
            var email = User.Identity.Name;
            if (email != null)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
      
    }
}
