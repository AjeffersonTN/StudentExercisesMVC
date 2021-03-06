﻿using Microsoft.AspNetCore.Mvc.Rendering;
using StudentExercisesMVC.Respositories;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace StudentExercisesMVC.Models.ViewModels
{
    public class InstructorCreateViewModel
    {
        // A single student
        public Instructor Instructor { get; set; } = new Instructor();

        // All cohorts
        public List<SelectListItem> Cohorts;

        public InstructorCreateViewModel()
        {
            BuildCohortOptions();
        }

        public void BuildCohortOptions()
        {
            Cohorts = CohortRepository.GetAllCohorts()
                .Select(li => new SelectListItem
                {
                    Text = li.Designation,
                    Value = li.Id.ToString()
                }).ToList();

            Cohorts.Insert(0, new SelectListItem
            {
                Text = "Choose cohort...",
                Value = "0"
            });
        }
    }
}