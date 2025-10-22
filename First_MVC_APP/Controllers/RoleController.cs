using AutoMapper;
using First_MVC_APP.DAL.Data.Models;
using First_MVC_APP.PL.Hellpers;
using First_MVC_APP.PL.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace First_MVC_APP.PL.Controllers
{
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;

        public RoleController(RoleManager<IdentityRole> roleManager , IMapper mapper)
        {
            _roleManager = roleManager;
            _mapper = mapper;
        }

        #region Index

        public async Task<IActionResult> Index(String searchInput)
        {
            var AllRoles = Enumerable.Empty<RoleViewModel>();

            if (String.IsNullOrEmpty(searchInput))
            {
                AllRoles = await _roleManager.Roles
                    .Select(role => _mapper.Map<IdentityRole, RoleViewModel>(role))
                    .ToListAsync();
            }
            else
            {
                AllRoles = await _roleManager.Roles
                    .Where(r => r.Name.ToLower().Contains(searchInput.ToLower()))
                    .Select(role => _mapper.Map<IdentityRole, RoleViewModel>(role))
                    .ToListAsync();
            }

            return View(AllRoles);
        }
        #endregion

        #region Create

        public IActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(RoleViewModel roleVm)
        {

            if (ModelState.IsValid)
            {
                var role = _mapper.Map<RoleViewModel, IdentityRole>(roleVm);
                var result = await _roleManager.CreateAsync(role);

                if (result.Succeeded)
                return RedirectToAction(nameof(Index));
                 
            }

                return View(roleVm);
        }
        #endregion

        #region Details

        public async Task<IActionResult> Details(string id, String viewName = "Details")
        {
            if (String.IsNullOrEmpty(id))
                return BadRequest();

            var role = await _roleManager.FindByIdAsync(id);

            if (role is null)
                return NotFound();

            var mappedRole = _mapper.Map<IdentityRole, RoleViewModel>(role);

            return View(viewName, mappedRole);
        }
        #endregion

        #region Edit
        public Task<IActionResult> Edit(String id)
        {
            return Details(id, "Edit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] String id, RoleViewModel roleVM)
        {
            if (id != roleVM.Id || roleVM is null)
                return BadRequest();

            if (ModelState.IsValid)
            {
                try
                {
                    var Role = await _roleManager.FindByIdAsync(id);
                    Role.Name = roleVM.RoleName;
                    var result = await _roleManager.UpdateAsync(Role);
                    if (result.Succeeded)
                    return RedirectToAction(nameof(Index));
                    ModelState.AddModelError(String.Empty, "There is Error");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(String.Empty, ex.Message);
                }
            }
            return View(roleVM);

        }
        #endregion

        #region Delete
        public Task<IActionResult> Delete(string id)
        {
            return Details(id, "Delete");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteAsync([FromRoute] string id, RoleViewModel DeletedRole)
        {
            if (id != DeletedRole.Id || DeletedRole is null)
                return BadRequest();

            try
            {
                var Role = await _roleManager.FindByIdAsync(id);
                _mapper.Map(DeletedRole, Role);
                var result = await _roleManager.DeleteAsync(Role);
                if (result.Succeeded)
                    return RedirectToAction(nameof(Index));
                ModelState.AddModelError(String.Empty, "There is Error");
            }
            catch (Exception ex)
            {

                ModelState.AddModelError(String.Empty, ex.Message);
            }

            return View(DeletedRole);
        }
        #endregion
    }
}
