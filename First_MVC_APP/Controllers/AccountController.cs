using First_MVC_APP.Controllers;
using First_MVC_APP.DAL.Data.Models;
using First_MVC_APP.PL.Hellpers;
using First_MVC_APP.PL.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace First_MVC_APP.PL.Controllers
{
    
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(UserManager<ApplicationUser> userManager , SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        #region SignUp

        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(model.UserName);
                if (user is null)
                {
                    user = new ApplicationUser
                    {
                        UserName = model.UserName,
                        FName = model.FName,
                        LName = model.LName,
                        Email = model.Email,
                        IsAgree = model.IsAgree,
                        PhoneNumber = model.PhoneNumber
                    };
                    var State = await _userManager.CreateAsync(user, model.Password);

                    if (State.Succeeded)
                        return RedirectToAction(nameof(SignIn));

                    foreach (var error in State.Errors)
                        ModelState.AddModelError(string.Empty, error.Description);
                }
                else
                    ModelState.AddModelError(string.Empty, "User Already Exists");

            }
            return View(model);
        }

        #endregion

        #region SignIn
        public IActionResult SignIn()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> SignIn(SignInViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user is not null)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);

                    if (result.Succeeded)
                        return RedirectToAction(nameof(HomeController.Index), "Home");
                }
                ModelState.AddModelError(string.Empty, "Invalid Login");
            }
            return View(model);
        }
        #endregion

        #region SignOut

        public new async Task<IActionResult> SignOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(SignIn));
        }

        #endregion

        #region ForgetPassword

        public IActionResult ForgetPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SendResetPasswordUrl(ForgetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);

                if (user is not null)
                {
                    var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                    var resetPasswordUrl = Url.Action("ResetPassword", "Account", new { email = model.Email, token = token} , "https" ,"localhost:44371" );

                    var email = new Email()
                    {
                        Subject = "Reset Password",
                        Recipints = model.Email,
                        Body = resetPasswordUrl,
                    };
                    EmailSetting.SendEmail(email);
                    return RedirectToAction(nameof(CheckYourInbox));
                }
                ModelState.AddModelError(string.Empty, "Email not found");
            }
            return View(model);
        }


        public IActionResult CheckYourInbox()
        {
            return View();
        }


        #endregion

        #region Reset Password
        public IActionResult ResetPassword(string email , string token)
        {
            TempData["email"] = email;
            TempData["token"] = token;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            String email = TempData["email"] as string;
            String token = TempData["token"] as string;
            if (ModelState.IsValid)
            {
            var user = await _userManager.FindByEmailAsync(email);
            var result = await _userManager.ResetPasswordAsync(user, token, model.NewPassword);

                if (result.Succeeded)
                    return RedirectToAction(nameof(SignIn));

                foreach (var error in result.Errors)
                    ModelState.AddModelError(string.Empty, error.Description);

                ModelState.AddModelError(string.Empty, "Invalid Request");
            }
            return View(model);
        }
        #endregion

    }
}