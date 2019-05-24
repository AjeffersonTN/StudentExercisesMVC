﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StudentExercisesMVC.Models
{
    public class Student
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [Display(Name ="First Name")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Last Name")] //changes the name of the column when displayed in html
        public string LastName { get; set; }
        [Required]
        [Display(Name = "Slack")]
        public string SlackHandle { get; set; }
        [Required]
        public int CohortId { get; set; }
        
        public Cohort Cohort { get; set; }

        public List<Exercise> AssignedExercise { get; set; }
        
    }
}
