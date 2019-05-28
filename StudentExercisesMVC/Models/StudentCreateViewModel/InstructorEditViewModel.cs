using Microsoft.AspNetCore.Mvc.Rendering;
using StudentExercisesMVC.Respositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentExercisesMVC.Models.StudentCreateViewModel
{
    public class InstructorEditViewModel
    {
        // A single instructor
        public Instructor Instructor { get; set; }

        // All cohorts
        public List<SelectListItem> Cohorts;

        public InstructorEditViewModel() { }
        public InstructorEditViewModel(int id)
        {
            Instructor = InstructorRepository.GetInstructor(id);
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
