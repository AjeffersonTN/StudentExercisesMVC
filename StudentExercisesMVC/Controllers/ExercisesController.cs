using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using StudentExercisesMVC.Models;
using StudentExercisesMVC.Respositories;

namespace StudentExercisesMVC.Controllers
{
    public class ExercisesController : Controller
    {
        
        // GET: Exercises
        public ActionResult Index(string _orderBy)
        {

            var exercises = ExerciseRepository.GetAllExercises(_orderBy);
            return View(exercises);
                
            
        }

        // GET: Exercises/Details/5
        public ActionResult Details(int id)
        {

            var exercise = ExerciseRepository.GetExercise(id);
            return View(exercise);
             
        }
    

        // GET: Exercises/Create
        public ActionResult Create()
        {
            Exercise model = new Exercise();
            return View(model);
        }

        // POST: Exercises/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([FromForm] Exercise model)
        {
            var exercise = ExerciseRepository.CreateExercise(model);
            return RedirectToAction(nameof(Index));
        }

        // GET: Exercises/Edit/5
        public ActionResult Edit(int id)
        {
            var model = ExerciseRepository.GetExercise(id);
            return View(model);
        }

        // POST: Exercises/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Exercise exercise)
        {
            try
            {
                exercise.Id = id;
                ExerciseRepository.EditExercise(exercise);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                return View(exercise);
            }
        }

        // GET: Exercises/Delete/5
        public ActionResult Delete(int id)
        {
            var exercise = ExerciseRepository.GetExercise(id);
            return View(exercise);
        }

        // POST: Exercises/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Exercise model)
        {
            ExerciseRepository.DeleteExercise(id);
            return RedirectToAction(nameof(Index));
        }
    }
}