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
    public class CohortRepository
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
        public static List<Cohort> GetAllCohorts()
        {
           string sql   = @"
                        SELECT c.Id,
                            c.Designation                                                        
                        FROM Cohort c
                    ";
           
        // GET: Cohort
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = sql;
                    SqlDataReader reader = cmd.ExecuteReader();

                    List<Cohort> cohorts = new List<Cohort>();
                    while (reader.Read())
                    {
                        Cohort cohort = new Cohort
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Designation = reader.GetString(reader.GetOrdinal("Designation"))

                        };

                        cohorts.Add(cohort);
                    }

                    reader.Close();

                    return cohorts; //return list because you ar adding each cohort to the list
                }
            }
        }

        // GET: Cohort/Details/5
        public static Cohort GetCohort(int id)
        {
            //Cohort cohort = null;
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                            SELECT c.Id,
                            c.Designation                                                        
                            FROM Cohort c
                            WHERE c.Id = @CohortId
                        ";
                    cmd.Parameters.Add(new SqlParameter("@CohortId", id));

                    SqlDataReader reader = cmd.ExecuteReader();

                    Cohort cohort = null;
                    if (reader.Read())
                    {
                        cohort = new Cohort
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Designation = reader.GetString(reader.GetOrdinal("Designation"))
                        };
                    }

                    reader.Close();

                    return cohort; //return only the cohort object beecause your are getting one cohort
                }
            }
        }
        // GET: Cohort/Create
        public static Cohort CreateCohort(Cohort cohort)
        { 
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO Cohort (Designation)         
                                         OUTPUT INSERTED.Id                                                       
                                         VALUES (@designation)";
                    cmd.Parameters.Add(new SqlParameter("@designation", cohort.Designation));

                    int newId = (int)cmd.ExecuteScalar();
                    cohort.Id = newId;
                    return cohort;
                }
            }
        }

        // GET: Cohort/Edit/5
        public static void EditCohort(Cohort cohort)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"UPDATE Cohort
                            SET Designation = @designation                                                   
                            WHERE Id = @CohortId
                        ";
                    cmd.Parameters.Add(new SqlParameter("@CohortId", cohort.Id));
                    cmd.Parameters.Add(new SqlParameter("@designation", cohort.Designation));
                    cmd.Parameters.Add(new SqlParameter("@id", cohort.Id));

                    cmd.ExecuteNonQuery();
                }
            }
        }


        // GET: Cohort/Delete/5
        public static void DeleteCohort(int id)
        {
            
                using (SqlConnection conn = Connection)
                {
                    conn.Open();
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = @"
                            DELETE                                                        
                            FROM Cohort c
                            WHERE c.Id = @CohortId
                        ";
                        cmd.Parameters.Add(new SqlParameter("@CohortId", id));

                        int rowsAffected = cmd.ExecuteNonQuery();
                        
                        return;
                    }
                }

           
        }

    }
}