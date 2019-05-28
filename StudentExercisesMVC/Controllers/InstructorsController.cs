using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using StudentExercisesMVC.Models;
using StudentExercisesMVC.Models.StudentCreateViewModel;
using StudentExercisesMVC.Models.ViewModels;
using StudentExercisesMVC.Respositories;

namespace StudentExercisesMVC.Controllers
{
    public class InstructorsController : Controller
    {
       
        // GET: Instructors
        public ActionResult Index(string _orderBy, string _sortDirection)
        {
            string currentSort = "";

            if (_sortDirection == null)
            {
                ViewData["sortDirection"] = "desc";
                currentSort = "asc";

            }
            else if (_sortDirection == "asc")
            {
                ViewData["sortDirection"] = "desc";
                currentSort = "asc";

            }
            else if (_sortDirection == "desc")
            {
                ViewData["sortDirection"] = "asc";
                currentSort = "desc";
            }

            var students = InstructorRepository.GetInstructors(_orderBy, currentSort);

            return View(students);
        }
        

            // GET: Instructors/Details/5
            public ActionResult Details(int id)
            {
                var instructor = InstructorRepository.GetInstructor(id);
                return View(instructor);
            }
        // GET: Instructors/Create
        public ActionResult Create()
        {
            InstructorCreateViewModel model = new InstructorCreateViewModel();
            return View(model);
        }

        // POST: Instructors/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([FromForm] InstructorCreateViewModel model)
        {
            var instructor = InstructorRepository.CreateInstructor(model.Instructor);
            return View(model);
        }

        // GET: Instructors/Edit/5
        public ActionResult Edit(int id)
        {
            var model = new InstructorEditViewModel(id);
            return View(model);
        }

        // POST: Instructors/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, InstructorEditViewModel model)
        {
            try
            {
                model.Instructor.Id = id;
                InstructorRepository.EditInstructor(model.Instructor);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                return View(model);
            }

        }
      

        // GET: Instructors/Delete/5
        public ActionResult DeleteConfirm(int id)
        {
            var instructor = InstructorRepository.GetInstructor(id);
            return View(instructor);
        }

        // POST: Instructors/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete([FromForm] int id)
        {
            InstructorRepository.DeleteInstructor(id);
            return RedirectToAction(nameof(Index));
        }
    }
}