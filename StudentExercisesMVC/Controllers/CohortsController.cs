using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using StudentExercisesMVC.Respositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using StudentExercisesMVC.Models;
using StudentExercisesMVC.Models.ViewModels;

namespace StudentExercisesMVC.Controllers
{
    public class CohortsController : Controller
    {      
            // GET: Cohort
            public ActionResult Index(string _orderBy)
            {
                var cohorts = CohortRepository.GetAllCohorts();

                return View(cohorts);                   
                
            }

        // GET: Cohort/Details/5
        public ActionResult Details(int id)
        {
            var cohort = CohortRepository.GetCohort(id);
            return View(cohort);
        }
        
        // GET: Cohort/Create
            public ActionResult Create()
        {
            Cohort model = new Cohort();
            return View(model);
        }

        // POST: Cohort/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([FromForm] Cohort model)
        {
            var cohort = CohortRepository.CreateCohort(model);
            return RedirectToAction(nameof(Index));
        }

        // GET: Cohort/Edit/5
        public ActionResult Edit(int id)
        {

            var model = CohortRepository.GetCohort(id);
            return View(model);
                            
        }

        // POST: Cohort/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Cohort cohort)
        {
            try
            {
                cohort.Id = id;
                CohortRepository.EditCohort(cohort);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                return View(cohort);
            }
        }

        // GET: Cohort/Delete/5
        public ActionResult DeleteConfirm(int id)
        {
            var cohort = CohortRepository.GetCohort(id);
            return View(cohort);
        }

        // POST: Cohort/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete([FromForm] int id)
        {
            CohortRepository.DeleteCohort(id);
            return RedirectToAction(nameof(Index));
         
        }
    }
}