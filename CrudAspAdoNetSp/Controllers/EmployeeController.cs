﻿using CrudAspAdoNetSp.DAL;
using CrudAspAdoNetSp.Models;
using Microsoft.AspNetCore.Mvc;

namespace CrudAspAdoNetSp.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly Employee_DAL _dal;

        public EmployeeController(Employee_DAL dal)
        {
            _dal = dal;
        }

        [HttpGet]
        public IActionResult Index()
        {
            List<Employee> employees = new List<Employee>();
            try
            {
                employees = _dal.GetAll();
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
            }
            return View(employees);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Employee model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    TempData["errorMessage"] = "Los datos del modelo no son válidos.";
                }
                bool result = _dal.Insert(model);

                if (!result)
                {
                    TempData["errorMessage"] = "No se guardaron los datos.";
                    return View();
                }

                TempData["successMessage"] = "Empleado registrado correctamente.";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            try
            {
                Employee employee = _dal.GetById(id);
                
                if ( employee.Id == 0)
                {
                    TempData["errorMessage"] = "Empleado no encontrado.";
                    return RedirectToAction("Index");
                }
                return View(employee);
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
        }

        [HttpPost]
        public IActionResult Edit(Employee model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    TempData["errorMessage"] = "Los datos del modelo no son válidos.";
                    return View();
                }
                bool result = _dal.Update(model);

                if (!result)
                {
                    TempData["errorMessage"] = "No se actualizaron los datos.";
                    return View();
                }
                TempData["successMessage"] = "Empleado actualizado correctamente.";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            try
            {
                Employee employee = _dal.GetById(id);

                if (employee.Id == 0)
                {
                    TempData["errorMessage"] = "Empleado no encontrado.";
                    return RedirectToAction("Index");
                }
                return View(employee);
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(Employee model)
        {
            try
            {
                bool result = _dal.Delete(model.Id);

                if (!result)
                {
                    TempData["errorMessage"] = "No se eliminaron los datos.";
                    return View();
                }
                TempData["successMessage"] = "Empleado eliminado correctamente.";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
        }
    }
}
