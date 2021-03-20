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
        private static string connectionString = ConfigurationManager.ConnectionStrings["dblocal"].ConnectionString;

        

        #region CREATE
        public static void AddObserver()
        {
            string stmt = "INSERT INTO observer (firstname, lastname) VALUES ('Hej', 'Då')";
            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                using (var command = new NpgsqlCommand(stmt, conn))
                {
                    command.ExecuteNonQuery();
                }

            }

        }

        public static void AddMeasurement()
        {
            string stmt = "INSERT INTO x VALUES y";
            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                using (var command = new NpgsqlCommand(stmt, conn))
                {
                    command.ExecuteNonQuery();
                }
            }
        }
        #endregion

        #region READ
        public static IEnumerable<Observer> GetObserver()
        {
            string stmt = "SELECT id, firstname, lastname FROM observer ORDER BY lastname";
            using (var conn = new NpgsqlConnection(connectionString))
            {
                Observer obs = null;
                List<Observer> observers = new List<Observer>();
                conn.Open();
                using (var command = new NpgsqlCommand(stmt, conn))
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
                return observers;
            }
            
        }

        #endregion

        #region UPDATE

        public static void SelectObserver(Observer activeObserver)
        {
            activeObserver = new Observer();
        }

        #endregion

        #region DELETE
        public static string DeleteObserver(Observer obs)
        {
            string stmt = "DELETE FROM observer WHERE id="+obs;
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
                           return (MessageBox.Show("Observatören du försöker ta bort har gjort observationer som måste raderas först.").ToString());
                        }
                    }
                }
            }
            return MessageBox.Show("Observatören är nu borttagen.").ToString();
        }
    }
        #endregion
}