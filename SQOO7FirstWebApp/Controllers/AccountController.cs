using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SQOO7FirstWebApp.Models;
using SQOO7FirstWebApp.ViewModels;

namespace SQOO7FirstWebApp.Controllers
{
    
    public class AccountController : Controller
    {
        private readonly UserManager<Employee> _userManager;

        public SignInManager<Employee> _signManager { get; }

        private readonly IWebHostEnvironment _webHost;

        public AccountController(UserManager<Employee> userManager, 
            SignInManager<Employee> signInManager, IWebHostEnvironment webHost)
        {
            _userManager = userManager;
            _signManager = signInManager;
            _webHost = webHost;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            var model = new RegisterViewModel();
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            // check if email already exist
            var user = await _userManager.FindByEmailAsync(model.Email);
            if(user != null)
            {
                ModelState.AddModelError("", "Email already exists");
                return View(model);
            }

            var photoUrl = model.Gender == 'm' ? "user1.jpg" : (model.Gender == 'f' ? "user2,jpg" : "");

            // map user to add
            var userToAdd = new Employee
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                UserName = model.Email,
                Gender = model.Gender.ToString(),
                Photo = photoUrl
            };

            // add to db
            var result = await _userManager.CreateAsync(userToAdd, model.Password);
            if(!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                    return View(model);
                }
            }

            if (model.IsAdmin)
            {
                await _userManager.AddToRoleAsync(userToAdd, "Admin");
            }
            else
            {
                await _userManager.AddToRoleAsync(userToAdd, "Regular");
            }
            //await _userManager.AddClaimAsync(userToAdd, new Claim("CanComment", "true"));


            // generate email confirmation token
            var emailConfirmationToken = await _userManager
                .GenerateEmailConfirmationTokenAsync(userToAdd);
            // generate confirmation url
            var emailConfirmationLink = Url.Action("RegistrationConfirmation", "Account", 
                new { userId = userToAdd.Id, token = emailConfirmationToken }, Request.Scheme);

            return RedirectToAction("RegistrationConfirmationPage", 
                new { str = $"{userToAdd.FirstName} {userToAdd.LastName}", 
                    link = emailConfirmationLink });

            //var str1 = new String("dfkdlkjgd");
            //return RedirectToAction("AccountConfirmation", new { str = $"{userToAdd.FirstName} {userToAdd.LastName}" });

        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult RegistrationConfirmationPage(string str, string link)
        {
            ViewBag.Username = str;
            ViewBag.Link = link;
            return View();
        }


        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> RegistrationConfirmation(string userId, string token)
        {
            if(!string.IsNullOrWhiteSpace(userId) && !string.IsNullOrWhiteSpace(token))
            {
                var user = await _userManager.FindByIdAsync(userId);
                var result = await _userManager.ConfirmEmailAsync(user, token);
                if (!result.Succeeded)
                {
                    ViewBag.ErrorMessage = "Couldn't confirm account";
                    return View();
                }

                //ViewBag.UserName = $"{user.FirstName} {user.LastName}";
                return RedirectToAction("AccountConfirmation", 
                new { str = $"{user.FirstName} {user.LastName}" });
            }

            ViewBag.ErrorMessage = "Invalid credentials";
            return View();
        }


        [HttpGet]
        [AllowAnonymous]
        public IActionResult AccountConfirmation(string str)
        {
            ViewBag.UserName = str;
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;

            if (!ModelState.IsValid)
                return View(model);

            //ViewData["returnUrl"] = returnUrl;

            var user = await _userManager.FindByEmailAsync(model.Email);
            if(user == null)
            {
                ModelState.AddModelError("", "Invalid credentials");
                return View(model);
            }

            if(!await _userManager.IsEmailConfirmedAsync(user))
            {
                ModelState.AddModelError("", "Email not onfirmed yet!");
                return View(model);
            }

            // logged-in
            var result = await _signManager.PasswordSignInAsync(user, model.Password, 
                isPersistent: model.RememberMe, false); // account lockedout is set

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Invalid credentials");
                return View(model);
            }

            if (!string.IsNullOrWhiteSpace(returnUrl))
                return Redirect(returnUrl);

            return RedirectToAction("Index", "Home");
        }


        // log me out
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }


        // forgot password
        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
                return View(model);

            var generatedPasswordToken = await _userManager.GeneratePasswordResetTokenAsync(user);
            var link = Url.Action("ResetPassword", "Account", new { email = user.Email, token = generatedPasswordToken });

            // Todo: send a mail with the link

            return RedirectToAction("ConfirmPasswordReset", "Account", new { link });
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ConfirmPasswordReset(string link)
        {
            ViewBag.Link = link;
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPassword(string email, string token)
        {
            if(string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(token))
            {
                ViewBag.ErrorMessage = "Missing credentials";
            }
            var model = new RestPasswordViewModel
            {
                Email = email,
                token = token
            };
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword(RestPasswordViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
                return View(model);

            var result = await _userManager
                .ResetPasswordAsync(user, model.token, model.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                    ModelState.AddModelError("", error.Description);

                return View(model);
            }

            return RedirectToAction("ResetPasswordConfirmationPage");
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPasswordConfirmationPage()
        {
            return View();
        }


        // upload file
        [HttpGet]
        //[Authorize(Policy = "AdminRolePolicy")]
        //[Authorize(Roles ="manager, editor")]
        //[Authorize(Policy = "CanEditPolicy")]
        //[Authorize(Policy = "AdminOrManagerRoleAndCanEdit")]
        public async Task<IActionResult> Profile()
        {
            var loggedInUserId = _userManager.GetUserId(User);
            var user = await _userManager.FindByIdAsync(loggedInUserId);

            var model = new ProfileViewModel
            {
                FullName = $"{user.FirstName} {user.LastName}",
                Email = user.Email,
                Photo = user.Photo,
                FileUploadModel = new FileUploadViewModel()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> UploadFile(ProfileViewModel model)
        {
            if(model.FileUploadModel.Photo == null)
            {
                ModelState.AddModelError("", "No picture selected!");
            }
            
            var photoName = "";
            //IHostingEnvironment
            var folderName = Path.Combine(_webHost.WebRootPath, "images");
            photoName = Guid.NewGuid().ToString() + "_" + model.FileUploadModel.Photo.FileName;

            var fullPath = Path.Combine(folderName, photoName);

            using (var fs = new FileStream(fullPath, FileMode.Create))
            {
                model.FileUploadModel.Photo.CopyTo(fs);
            }

            var f = new FileInfo(fullPath);

            if (f.Exists)
            {

                var user = await _userManager.FindByEmailAsync(model.Email);
                if(user != null)
                {
                    user.Photo = photoName;

                    var result = await _userManager.UpdateAsync(user);
                    if (!result.Succeeded)
                    {
                        ModelState.AddModelError("", "Failed to upload!");
                        return View(model);
                    }

                    return RedirectToAction("Profile");
                }

            }

            ViewBag.ErrorMessage = "File upload failed!";
            return RedirectToAction("Profile");

        }

        [HttpGet]
        public IActionResult AccessDenied(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }
    }
}
