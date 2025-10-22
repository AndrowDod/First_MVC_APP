using AutoMapper;
using First_MVC_App.PLL.Interfaces;
using First_MVC_APP.DAL.Data.Models;
using First_MVC_APP.PL.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace First_MVC_APP.PL.Controllers
{
    [Authorize]
    public class DepartmentController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public DepartmentController(IMapper mapper,IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            
        }

        // main page
        public IActionResult Index()
        {
            var departments = _unitOfWork.DepartmentRepository.GetAll();
            var MappedDept = _mapper.Map<IEnumerable<Department>, IEnumerable<DepartmentViewModel>>(departments);
            return View(MappedDept);
        }

        #region Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(DepartmentViewModel departmentVM)
        {
            if (ModelState.IsValid)
            {
                var mappedDept = _mapper.Map<DepartmentViewModel, Department>(departmentVM);
                _unitOfWork.DepartmentRepository.Add(mappedDept);
                _unitOfWork.Complete();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(departmentVM);
            }
        }
        #endregion

        #region Details
        public IActionResult Details(int? id, String viewName = "Details")
        {
            if (!id.HasValue)
                return BadRequest();
            var department = _unitOfWork.DepartmentRepository.GetById(id.Value);
            _unitOfWork.Complete();

            if (department is null)
                return NotFound();
            var mappedDept = _mapper.Map<Department, DepartmentViewModel>(department);

            return View(viewName, mappedDept);
        }
        #endregion

        #region Edit
        public IActionResult Edit(int? id)
        {
            return Details(id, "Edit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int id, DepartmentViewModel departmentVM)
        {
            if (id != departmentVM.Id || departmentVM is null)
                return BadRequest();

            if (ModelState.IsValid)
            {
                try
                {
                    var mappedDept = _mapper.Map<DepartmentViewModel, Department>(departmentVM);
                    _unitOfWork.DepartmentRepository.Update(mappedDept);
                    _unitOfWork.Complete();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {

                    ModelState.AddModelError(String.Empty, ex.Message);
                }
            }
            return View(departmentVM);

        }
        #endregion

        #region Delete
        public IActionResult Delete(int? id)
        {
            return Details(id, "Delete");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete([FromRoute] int id, DepartmentViewModel departmentVM)
        {
            if (id != departmentVM.Id || departmentVM is null)
                return BadRequest();

                try
                {
                var mappedDept = _mapper.Map<DepartmentViewModel, Department>(departmentVM);
                _unitOfWork.DepartmentRepository.Delete(mappedDept);
                _unitOfWork.Complete();
                return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {

                    ModelState.AddModelError(String.Empty, ex.Message);
                }
            
            return View(departmentVM);
        } 
        #endregion
    }
}
