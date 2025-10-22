using AutoMapper;
using First_MVC_App.PLL.Interfaces;
using First_MVC_APP.DAL.Data.Models;
using First_MVC_APP.PL.Hellpers;
using First_MVC_APP.PL.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace First_MVC_APP.PL.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EmployeeController(IMapper mapper ,IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        // main page
        public IActionResult Index(String searchInput)
        {
            var employees = Enumerable.Empty<Employee>();
            if (String.IsNullOrEmpty(searchInput))
                 employees = _unitOfWork.EmployeeRepository.GetAll();

            else
                employees = _unitOfWork.EmployeeRepository.SearchByName(searchInput);

            var MappedEmp = _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel> >(employees);
            return View(MappedEmp);
        }

        #region Create
      
        public IActionResult Create()
        {
      
            return View();
        }

        [HttpPost]
        public IActionResult Create(EmployeeViewModel employeeVM)
        {

            if (ModelState.IsValid)
            {
                employeeVM.ImageName = DocumentSettings.UploadFile(employeeVM.Image, "images");

                var employee = _mapper.Map<EmployeeViewModel, Employee>(employeeVM);
                _unitOfWork.EmployeeRepository.Add(employee);
                _unitOfWork.Complete();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(employeeVM);
            }
        }
        #endregion

        #region Details
        public IActionResult Details(int? id, String viewName = "Details")
        {
            if (!id.HasValue)
                return BadRequest();
            var employee = _unitOfWork.EmployeeRepository.GetById(id.Value);
            _unitOfWork.Complete();

            if (employee is null)
                return NotFound();
            var mappedEmp = _mapper.Map<Employee, EmployeeViewModel>(employee);

            return View(viewName, mappedEmp);
        }
        #endregion

        #region Edit
        public IActionResult Edit(int? id)
        {
            return Details(id, "Edit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int id, EmployeeViewModel employeeVm)
        {
            if (id != employeeVm.Id || employeeVm is null)
                return BadRequest();

            if (ModelState.IsValid)
            {
                try
                {
                    var employee = _mapper.Map<EmployeeViewModel, Employee>(employeeVm);
                    _unitOfWork.EmployeeRepository.Update(employee);
                    _unitOfWork.Complete();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {

                    ModelState.AddModelError(String.Empty, ex.Message);
                }
            }
            return View(employeeVm);

        }
        #endregion

        #region Delete
        public IActionResult Delete(int? id)
        {
            return Details(id, "Delete");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete([FromRoute] int id, EmployeeViewModel employeeVm)
        {
            if (id != employeeVm.Id || employeeVm is null)
                return BadRequest();

            try
            {
                var employee = _mapper.Map<EmployeeViewModel, Employee>(employeeVm);
                _unitOfWork.EmployeeRepository.Delete(employee);
                _unitOfWork.Complete();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {

                ModelState.AddModelError(String.Empty, ex.Message);
            }

            return View(employeeVm);
        }
        #endregion
    }
}
