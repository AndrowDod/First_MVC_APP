using AutoMapper;
using First_MVC_APP.DAL.Data.Models;
using First_MVC_APP.PL.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace First_MVC_APP.PL.Controllers
{
    [Authorize]

    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public UserController(UserManager<ApplicationUser> userManager , IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        #region Index

        public async Task<IActionResult> Index(string email)
        {
            var UsersVM = new List<UserViewModel>();
            if (string.IsNullOrEmpty(email))
            {
                var users = await _userManager.Users.ToListAsync();

                foreach (var user in users)
                {
                    var role = await _userManager.GetRolesAsync(user);

                    UsersVM.Add(new UserViewModel
                    {
                        Id = user.Id,
                        FName = user.FName,
                        LName = user.LName,
                        Email = user.Email,
                        PhoneNumber = user.PhoneNumber,
                        Roles = role,
                    });
                }

                return View(UsersVM);
            }
            else
            {
                var user = await _userManager.FindByEmailAsync(email);
                if (user is not null)
                {
                    UsersVM.Add(new UserViewModel()
                    {
                        Id = user.Id,
                        FName = user.FName,
                        LName = user.LName,
                        Email = user.Email,
                        PhoneNumber = user.PhoneNumber,
                        Roles = _userManager.GetRolesAsync(user).Result
                    });
                    return View(UsersVM);
                }
                return View(UsersVM);
            }

        }

        #endregion

        #region Details
        public async Task<IActionResult> Details(String id, String viewName = "Details")
        {
            if (String.IsNullOrEmpty(id))
                return BadRequest();
            var user = await _userManager.FindByIdAsync(id);

            if (user is null)
                return NotFound();

            var mappedUser = _mapper.Map<ApplicationUser, UserViewModel>(user);

            return View(viewName, mappedUser);
        }

        #endregion

        #region Edit

        public Task<IActionResult> Edit(String id)
        {
            return Details(id, "Edit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] String id, UserViewModel userVM)
        {
            if (id != userVM.Id || userVM is null)
                return BadRequest();

            if (ModelState.IsValid)
            {
                try
                {
                    var user = await _userManager.FindByIdAsync(userVM.Id);
                    var updatedUser = _mapper.Map<UserViewModel, ApplicationUser>(userVM, user);
                    var result = await _userManager.UpdateAsync(updatedUser);
                    if (result.Succeeded)
                        return RedirectToAction(nameof(Index));

                    foreach (var error in result.Errors)
                        ModelState.AddModelError(String.Empty, error.Description);
                     
                   
                }
                catch (Exception ex)
                {

                    ModelState.AddModelError(String.Empty, ex.Message);
                }
            }
            return View(userVM);

        }

        #endregion

        #region Delete
        public Task<IActionResult> Delete(String id)
        {
            return Details(id, "Delete");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromRoute] String id, UserViewModel userVM)
        {
            if (id != userVM.Id || userVM is null)
                return BadRequest();

            try
            {
                var user = await _userManager.FindByIdAsync(userVM.Id);
                var mappedUser = _mapper.Map<UserViewModel, ApplicationUser>(userVM, user);
                var result = await _userManager.DeleteAsync(mappedUser);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {

                ModelState.AddModelError(String.Empty, ex.Message);
            }

            return View(userVM);
        }
        #endregion


    }
}
