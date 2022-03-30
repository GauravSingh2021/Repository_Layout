using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Repository.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUsers iu;
        public HomeController()
        {
            iu = new UsersRepository("Nothing");
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Login(string Email, string Password)
        {
            if (ModelState.IsValid)
            {
                bool result = iu.Verify(Email, Password);

                if (result == true)
                {
                    return View("LoginSuccess");
                }
                else
                {
                    ViewBag.message = "Login Failed";
                    return View("Index");
                }
            }
            else
            {
                return View();
            }
        }

        public IActionResult Registration()
        {
            return View();
        }
        public IActionResult Register(Users u)
        {
            ValidationError ve = new ValidationError();

            ve = Validation(u);

            if(ve.retval == true)
            {
                bool x = iu.Register(u);
                if(x==true)
                {
                    TempData["message"] = "User have been registered successfully.";
                    return RedirectToAction("Registration");
                }
                else
                {
                    ViewBag.message = "Unspecified error in data insertion.";
                    return View("Registration", u);
                }           
            }
            else
            {
                ViewBag.message = ve.retmsg;
                return View("Registration", u);
            }
        }
        public ValidationError Validation(Users u)
         {
            ValidationError ve = new ValidationError();
            if(u.FullName==null||u.Email==null||u.Password==null||u.ConfirmPassword==null)
            {
                ve.retval = false;
                ve.retmsg = "Input can not be blank";

                return ve;
            }
            else if(iu.FindDuplicate(u.Email)==false)
            {
                ve.retval = false;
                ve.retmsg = "You have already registered with this Email.";

                return ve;
            }
            else if(u.Password.Length<=4 || u.Password.Length > 8)
            {
                ve.retval = false;
                ve.retmsg = "Password must be more than 4 and less than 8 characters.";

                return ve;
            }
            else if(!u.Password.Equals(u.ConfirmPassword))
            {
                ve.retval = false;
                ve.retmsg = "Password and Confirm Password do not match.";

                return ve;
            }
            else
            {
                ve.retval = true;
                ve.retmsg = "null";

                return ve;
            }
        }
    }
}
