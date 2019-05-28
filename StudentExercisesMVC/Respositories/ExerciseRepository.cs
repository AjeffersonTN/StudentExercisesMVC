using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using StudentExercisesMVC.Models;

namespace StudentExercisesMVC.Respositories
{
    public class ExerciseRepository
    {
        private static IConfiguration _config;

        public static void SetConfig(IConfiguration configuration)
        {
            _config = configuration;
        }

        public static SqlConnection Connection
        {
            get
            {
                return new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            }
        }
        public static List<Exercise> GetAllExercises(string orderBy)
        {
            string sql = @"
                        SELECT e.Id,
                            e.Title,
                            e.CodeLanguage
                        FROM Exercise e
                    ";

            if (orderBy != null)
            {
                sql += $"ORDER BY s.{orderBy}";
            }

            // GET: Exercise
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = sql;
                    SqlDataReader reader = cmd.ExecuteReader();

                    List<Exercise> exercises = new List<Exercise>();
                    while (reader.Read())
                    {
                        Exercise exercise = new Exercise
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Title = reader.GetString(reader.GetOrdinal("Title")),
                            CodeLanguage = reader.GetString(reader.GetOrdinal("CodeLanguage"))

                        };

                        exercises.Add(exercise);
                    }

                    reader.Close();

                    return exercises; //return list because you ar adding each exercise to the list
                }
            }
        }

        // GET: Exercise/Details/5
        public static Exercise GetExercise(int id)
        {
            //Exercise exercise = null;
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
                            CodeLanguage = reader.GetString(reader.GetOrdinal("CodeLanguage")),
                        };
                    }

                    reader.Close();

                    return exercise; //return only the Exercise object beecause your are getting one Exercise
                }
            }
        }
        // GET: Exercise/Create
        public static Exercise CreateExercise(Exercise exercise)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO Exercise (Title, CodeLanguage)         
                                         OUTPUT INSERTED.Id                                                       
                                         VALUES (@CodeLanguage, @Title)";
                    cmd.Parameters.Add(new SqlParameter("@Title", exercise.Title));
                    cmd.Parameters.Add(new SqlParameter("@CodeLanguage", exercise.CodeLanguage));
                    int newId = (int)cmd.ExecuteScalar();
                    exercise.Id = newId;
                    return exercise;
                }
            }
        }

        // GET: Exercise/Edit/5
        public static void EditExercise(Exercise exercise)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"UPDATE Exercise
                            SET Title = @Title, 
                                CodeLanguage = @CodeLanguage
                            WHERE Id = @ExerciseId
                        ";
                    cmd.Parameters.Add(new SqlParameter("@ExerciseId", exercise.Id));
                    cmd.Parameters.Add(new SqlParameter("@Title", exercise.Title));
                    cmd.Parameters.Add(new SqlParameter("@CodeLanguage", exercise.CodeLanguage));
                    cmd.Parameters.Add(new SqlParameter("@id", exercise.Id));

                    cmd.ExecuteNonQuery();
                }
            }
        }


        // GET: Exercise/Delete/5
        public static void DeleteExercise(int id)
        {

            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"DELETE FROM Exercise WHERE Id = @ExerciseId";                                    
                       
                    cmd.Parameters.Add(new SqlParameter("@ExerciseId", id));

                    cmd.ExecuteNonQuery();
                    return;
                }
            }


        }

    }
}