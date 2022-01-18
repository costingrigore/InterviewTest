using InterviewTest.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using System.Collections.Generic;


namespace InterviewTest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ListController : ControllerBase
    {
        public ListController()
        {
        }

        /*
         * List API methods goe here
         * */

        // Used to insert a new Employee into the database
        [HttpPost]
        public bool Insert(string name, string value)
        {
            var connectionStringBuilder = new SqliteConnectionStringBuilder() { DataSource = "./SqliteDB.db" };
            using (var connection = new SqliteConnection(connectionStringBuilder.ConnectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    var insertqueryCmd = connection.CreateCommand();
                    insertqueryCmd.CommandText = @$"INSERT INTO Employees (Name, Value) VALUES ('{name}', '{value}');";
                    insertqueryCmd.ExecuteNonQuery();
                    transaction.Commit();
                }            
            }
            return true;
        }

        //Used to Update the details of an Employee
        [HttpPut]
        public bool Update(string originalName, string originalValue, string newName, string newValue)
        {
            var connectionStringBuilder = new SqliteConnectionStringBuilder() { DataSource = "./SqliteDB.db" };
            using (var connection = new SqliteConnection(connectionStringBuilder.ConnectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    var updatequeryCmd = connection.CreateCommand();
                    updatequeryCmd.CommandText = @$"
                        UPDATE Employess
                        SET Name = '{newName}', Value= '{newValue}'
                        WHERE Name = '{originalName}' AND Value='{originalValue}';";
                    updatequeryCmd.ExecuteNonQuery();
                    transaction.Commit();
                }
            }
            return true;
        }

        //Used to delete an Employee from the database
        [HttpDelete]
        public bool Delete(string name, string value)
        {
            var connectionStringBuilder = new SqliteConnectionStringBuilder() { DataSource = "./SqliteDB.db" };
            using (var connection = new SqliteConnection(connectionStringBuilder.ConnectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    var deletequeryCmd = connection.CreateCommand();
                    deletequeryCmd.CommandText = @$"
                        DELETE FROM Employees WHERE Name='{name}' AND Value='{value}'";
                    deletequeryCmd.ExecuteNonQuery();
                    transaction.Commit();
                }
            }
            return true;
        }

        //Used to get the value of an employee of the database (Extra functionality)
        [HttpGet]
        public int GetValue(string name, string value)
        {
            int newValue = 0;

            var connectionStringBuilder = new SqliteConnectionStringBuilder() { DataSource = "./SqliteDB.db" };
            using (var connection = new SqliteConnection(connectionStringBuilder.ConnectionString))
            {
                connection.Open();

                var queryCmd = connection.CreateCommand();
                queryCmd.CommandText = @$"SELECT Value FROM Employees WHERE Name='{name}' AND Value='{value}'";
                using (var reader = queryCmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        newValue = reader.GetInt32(1);
                    }
                }
            }
            return newValue;
        }

        //Used to Update the rows starting by E
        [HttpPut]
        public bool UpdateRowsByNameStartingByE()
        {
            var connectionStringBuilder = new SqliteConnectionStringBuilder() { DataSource = "./SqliteDB.db" };
            using (var connection = new SqliteConnection(connectionStringBuilder.ConnectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    var updatequeryCmd = connection.CreateCommand();
                    updatequeryCmd.CommandText = @$"
                        UPDATE Employees
                        SET Value = Value + 1
                        WHERE Name LIKE 'E%';";
                    updatequeryCmd.ExecuteNonQuery();
                    transaction.Commit();
                }
            }
            return true;
        }

        //Used to Update the rows starting by G
        [HttpPut]
        public bool UpdateRowsByNameStartingByG()
        {
            var connectionStringBuilder = new SqliteConnectionStringBuilder() { DataSource = "./SqliteDB.db" };
            using (var connection = new SqliteConnection(connectionStringBuilder.ConnectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    var updatequeryCmd = connection.CreateCommand();
                    updatequeryCmd.CommandText = @$"
                        UPDATE Employees
                        SET Value = Value + 10
                        WHERE Name LIKE 'G%';";
                    updatequeryCmd.ExecuteNonQuery();
                    transaction.Commit();
                }
            }
            return true;
        }

        //Used to Update the rows that do not start neither by E nor G
        [HttpPut]
        public bool UpdateRowsByNameNotStartingByEAndG()
        {
            var connectionStringBuilder = new SqliteConnectionStringBuilder() { DataSource = "./SqliteDB.db" };
            using (var connection = new SqliteConnection(connectionStringBuilder.ConnectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    var updatequeryCmd = connection.CreateCommand();
                    updatequeryCmd.CommandText = @$"
                        UPDATE Employees
                        SET Value = Value + 100
                        WHERE Name NOT LIKE 'G%' AND Name NOT LIKE 'E%';";
                    updatequeryCmd.ExecuteNonQuery();
                    transaction.Commit();
                }
            }
            return true;
        }

        //Used to get the sum of all Values for all Names that begin with A, B or C but only present the data where the summed values are greater than or equal to 11171
        [HttpGet]
        public int GetSumOfABC()
        {
            var sum = 0;
            var connectionStringBuilder = new SqliteConnectionStringBuilder() { DataSource = "./SqliteDB.db" };
            using (var connection = new SqliteConnection(connectionStringBuilder.ConnectionString))
            {
                connection.Open();

                var queryCmd = connection.CreateCommand();
                queryCmd.CommandText = @"
                        SELECT SUM(Value)
                        FROM Employees WHERE Value >= 11171 AND Name LIKE 'A%' AND Name LIKE 'B%' AND Name LIKE 'C%';";
                using (var reader = queryCmd.ExecuteReader())
                {
                    sum = reader.GetInt32(1);
                }
            }
            return sum;
        }
    }
}
