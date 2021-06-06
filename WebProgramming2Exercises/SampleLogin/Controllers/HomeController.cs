using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SampleLogin.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Login(string Username, string Password)
        {
            bool isValid = true;

            ViewData["Username"] = Username;
            ViewData["Password"] = Password;

            if (String.IsNullOrEmpty(Username))
            {
                ViewData["UsernameError"] = "This field is Required!";
                isValid = false;
            }
              

            if (String.IsNullOrEmpty(Password))
            {
                ViewData["PasswordError"] = "This field is Required!";
                isValid = false;
            }
            
            if(isValid == false)
            {
                return View();
            }
                

            if (Username == "user" && Password == "pass")
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewData["AuthenticationError"] = "Invalid username or password!";
                return View();
            }
        }

        [HttpGet]
        public IActionResult CheckboxesTest()
        {
            return View();           
        }

        [HttpPost]
        public string CheckboxesTest(bool isAdmin, int[] groups)
        {

            string result = "";

            result += "Is Admin: " + isAdmin + "\n";

            for (int i = 0; i < groups.Length; i++)
            {
                result += "Group: " + groups[i] + "\n";
            }

            return result;
        }
    }
}
