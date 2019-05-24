using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using StudentExercisesMVC.Models;

namespace StudentExercisesMVC.Controllers
{
    public class ExercisesController : Controller
    {
        private readonly IConfiguration _config;

        public ExercisesController(IConfiguration config)
        {
            _config = config;
        }

        public SqlConnection Connection
        {
            get
            {
                return new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            }
        }
        // GET: Exercises
        public ActionResult Index()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT e.Id,
                            e.Title,
                            e.CodeLanguage                            
                        FROM Exercise e
                    ";
                    SqlDataReader reader = cmd.ExecuteReader();

                    List<Exercise> exercises = new List<Exercise>();
                    while (reader.Read())
                    {
                        Exercise exercise = new Exercise
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Title = reader.GetString(reader.GetOrdinal("Title")),
                            CodeLanguage = reader.GetString(reader.GetOrdinal("CodeLanguage")),                                                       
                        };

                        exercises.Add(exercise);
                    }

                    reader.Close();

                    return View(exercises);
                }
            }
        }

        // GET: Exercises/Details/5
        public ActionResult Details(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                            SELECT e.Id,
                            e.Title,
                            e.CodeLanguage                            
                            FROM Exercise e
                            WHERE e.Id = @ExerciseId
                        ";
                    cmd.Parameters.Add(new SqlParameter("@ExerciseId", id));

                    SqlDataReader reader = cmd.ExecuteReader();

                    Exercise exercise = null;
                    if (reader.Read())
                    {
                        exercise = new Exercise
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Title = reader.GetString(reader.GetOrdinal("Title")),
                            CodeLanguage = reader.GetString(reader.GetOrdinal("CodeLanguage"))
                        };
                    }

                    reader.Close();

                    return View(exercise);
                }
            }
        }
    

        // GET: Exercises/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Exercises/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([FromForm] Exercise model)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO Exercise (Title, CodeLanguage)         
                                         OUTPUT INSERTED.Id                                                       
                                         VALUES (@title, @codeLanguage)";
                    cmd.Parameters.Add(new SqlParameter("@title", model.Title));
                    cmd.Parameters.Add(new SqlParameter("@codeLanguage", model.CodeLanguage));
                   
                    int newId = (int)cmd.ExecuteScalar();
                    return RedirectToAction(nameof(Index));
                }
            }
        }

        // GET: Exercises/Edit/5
        public ActionResult Edit(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                            SELECT e.Id,
                            e.Title,
                            e.CodeLanguage                            
                            FROM Exercise e
                            WHERE e.Id = @ExerciseId
                        ";
                    cmd.Parameters.Add(new SqlParameter("@ExerciseId", id));

                    SqlDataReader reader = cmd.ExecuteReader();

                    Exercise exercise = null;
                    if (reader.Read())
                    {
                        exercise = new Exercise
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Title = reader.GetString(reader.GetOrdinal("Title")),
                            CodeLanguage = reader.GetString(reader.GetOrdinal("CodeLanguage"))
                        };
                    }

                    reader.Close();

                    return View(exercise);
                }
            }
        }

        // POST: Exercises/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Exercise exercise)
        {            
            {
               
                using (SqlConnection conn = Connection)
                {
                    conn.Open();
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = @"UPDATE Exercise
                                                SET Title = @title,
                                                    CodeLanguage = @codeLanguage
                                                WHERE Id = @id";
                        cmd.Parameters.Add(new SqlParameter("@title", exercise.Title));
                        cmd.Parameters.Add(new SqlParameter("@codeLanguage", exercise.CodeLanguage));
                        cmd.Parameters.Add(new SqlParameter("@id", id));

                        int rowsAffected = cmd.ExecuteNonQuery();
                            
                            
                        return RedirectToAction(nameof(Index));
                    }
                           
                }                
              
            }
        }

        // GET: Exercises/Delete/5
        public ActionResult Delete(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                            SELECT e.Id,
                            e.Title,
                            e.CodeLanguage                            
                            FROM Exercise e
                            WHERE e.Id = @ExerciseId
                        ";
                    cmd.Parameters.Add(new SqlParameter("@ExerciseId", id));

                    SqlDataReader reader = cmd.ExecuteReader();

                    Exercise exercise = null;
                    if (reader.Read())
                    {
                        exercise = new Exercise
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Title = reader.GetString(reader.GetOrdinal("Title")),
                            CodeLanguage = reader.GetString(reader.GetOrdinal("CodeLanguage"))
                        };
                    }

                    reader.Close();

                    return View(exercise);
                }
            }
        }

        // POST: Exercises/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Exercise model)
        {
            // TODO: Add delete logic here
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"DELETE FROM Exercise WHERE Id = @id";
                    cmd.Parameters.Add(new SqlParameter("@id", id));

                    int rowsAffected = cmd.ExecuteNonQuery();

                    return RedirectToAction(nameof(Index));
                }
            }
        }
    }
}