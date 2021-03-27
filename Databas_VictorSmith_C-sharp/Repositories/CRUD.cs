using Databas_VictorSmith_C_sharp.Models;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using System.Windows;

namespace Databas_VictorSmith_C_sharp.Repositories
{

    public static class CRUD
    {
        readonly private static string connectionString = ConfigurationManager.ConnectionStrings["dblocal"].ConnectionString;

        #region CREATE
        public static void AddObserver(string firstName, string lastName)
        {
            string stmt = "INSERT INTO observer (firstname, lastname) VALUES (@primaryName, @secondaryName)";
            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                using (var command = new NpgsqlCommand(stmt, conn))
                {
                    command.Parameters.Add(new NpgsqlParameter("primaryName", firstName));
                    command.Parameters.Add(new NpgsqlParameter("secondaryName", lastName));
                    command.ExecuteNonQuery();
                }
                conn.Close();
            }

        }

        public static void AddCountry(string countryName)
        {
            string stmt = "INSERT INTO country (country) VALUES (@countryName)";
            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                using (var command = new NpgsqlCommand(stmt, conn))
                {
                    command.Parameters.Add(new NpgsqlParameter("countryName", countryName));
                    command.ExecuteNonQuery();
                }
                conn.Close();
            }
        }

        public static void AddArea(string areaName, int country_id)
        {
            string stmt = "INSERT INTO area (name, country_id) VALUES (@areaName, @countryInput)";
            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                using (var command = new NpgsqlCommand(stmt, conn))
                {
                    command.Parameters.Add(new NpgsqlParameter("areaName", areaName));
                    command.Parameters.Add(new NpgsqlParameter("countryInput", country_id));
                    command.ExecuteNonQuery();
                }
                conn.Close();
            }
        }

        public static void AddGeolocation(double latitude, double longitude, int area_id)
        {
            string stmt = "INSERT INTO geolocation (latitude, longitude, area_id) VALUES (@longitudeInput, @latitudeInput, @areaInput)";
            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                using (var command = new NpgsqlCommand(stmt, conn))
                {
                    command.Parameters.Add(new NpgsqlParameter("latitudeInput", latitude));
                    command.Parameters.Add(new NpgsqlParameter("longitudeInput", longitude));
                    command.Parameters.Add(new NpgsqlParameter("areaInput", area_id));
                    command.ExecuteNonQuery();
                }
                conn.Close();
            }
        }

        public static void AddObservation(Observer obs, int geolocation_id)
        {
                string stmt = "INSERT INTO observation (observer_id, geolocation_id) VALUES (@observerIdInput, @geolocationIdInput)";
                using (var conn = new NpgsqlConnection(connectionString))
                {
                    conn.Open();
                    using (var command = new NpgsqlCommand(stmt, conn))
                    {
                        command.Parameters.Add(new NpgsqlParameter("observerIdInput", obs.Id));
                        command.Parameters.Add(new NpgsqlParameter("geolocationIdInput", geolocation_id));
                        command.ExecuteNonQuery();
                    }
                    conn.Close();
                }
            }

        public static void AddMeasurement()
        {
            //TODO
            string stmt = "INSERT INTO x VALUES y";
            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                using (var command = new NpgsqlCommand(stmt, conn))
                {
                    command.ExecuteNonQuery();
                }
                conn.Close();
            }
        }
        #endregion

        #region READ
        public static Observer GetObserver(Observer observer)
        {
            // Guard against ob==null after deleting an observer.
            if (observer != null)
            {
                System.Diagnostics.Trace.WriteLine($"CRUD:GetObserver");
                string stmt = "SELECT id, firstname, lastname FROM observer WHERE id = @observerId";
                using (var conn = new NpgsqlConnection(connectionString))
                {
                    Observer obs = new Observer();
                    conn.Open();
                    using (var command = new NpgsqlCommand(stmt, conn))
                    {
                        try
                        {
                            command.Parameters.Add(new NpgsqlParameter("observerId", observer.Id));
                            using (var reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    Observer o;
                                    o = new Observer()
                                    {
                                        Id = (int)reader["id"],
                                        FirstName = (string)reader["firstname"],
                                        LastName = (string)reader["lastname"]
                                    };
                                    obs = o;
                                };
                            }
                        }
                        catch (NullReferenceException)
                        {
                            GetObserverList();
                        }

                    }
                    conn.Close();
                    return obs;
                }
            }
            else
            {
                return null;
            }
        }
        public static List<Observer> GetObserverList()
        {
            System.Diagnostics.Trace.WriteLine($"CRUD:GetObserverList");
            string stmt = "SELECT id, firstname, lastname FROM observer ORDER BY lastname";
            using (var conn = new NpgsqlConnection(connectionString))
            {
                Observer obs;
                List<Observer> observers = new List<Observer>();
                conn.Open();
                //FIXME 2 using not good?
                using (var command = new NpgsqlCommand(stmt, conn))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            obs = new Observer()
                            {
                                Id = (int)reader["id"],
                                FirstName = (string)reader["firstname"],
                                LastName = (string)reader["lastname"]
                            };
                            observers.Add(obs);
                        };
                    }
                }
                conn.Close();
                return observers;
            }
        }
        
        public static List<Observation> UpdateObservationList(Observer observer)
        {
            System.Diagnostics.Trace.WriteLine($"CRUD:UpdateObservationList");
            string stmt = "SELECT id, date, geolocation_id FROM observation WHERE observer_id = @observerId ORDER BY id";
            using (var conn = new NpgsqlConnection(connectionString))
            {
                Observation obs;
                List<Observation> observations = new List<Observation>();
                conn.Open();
                using (var command = new NpgsqlCommand(stmt, conn))
                {
                    command.Parameters.Add(new NpgsqlParameter("observerId", observer.Id));
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            obs = new Observation()
                            {
                                Id = (int)reader["id"],
                                Date = (DateTime)reader["date"],
                                //Observer_Id = (int)reader["observer_id"],
                                Geolocation_Id = (int)reader["geolocation_id"],
                            };
                            observations.Add(obs);
                        };
                    }
                }
                conn.Close();
                return observations;
            }
        }
        #endregion

        #region UPDATE

        #endregion

        #region DELETE
        public static string DeleteObserver(Observer obs)
        {
            string stmt = "DELETE FROM observer WHERE id=" + obs.Id;
            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                using (var command = new NpgsqlCommand(stmt, conn))

                {
                    try
                    {
                        using var reader = command.ExecuteReader();
                    }
                    catch (NpgsqlException ex)
                    {
                        if (ex.ToString().Contains("23503"))
                        {
                            //FIXME after this box was shown there is a null error for some reason
                            return (MessageBox.Show("Observatören du försöker ta bort har gjort observationer som måste raderas först.").ToString());
                        }
                    }
                }
                conn.Close();
            }
            return MessageBox.Show("Observatören är nu borttagen.").ToString();
        }
        #endregion
    }
}