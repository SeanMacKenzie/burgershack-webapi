using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using burgershack_c.Models;
using Dapper;

namespace burgershack_c.Repositories
{
    public class DrinkRepository
    {
        private string _connectionString;

        public DrinkRepository()
        {
            _connectionString = @"Server=DESKTOP-P9K6TD5,49172;Database=burgershack;User Id=student;Password=student";
        }

        public IDbConnection Connection
        {
            get
            {
                return new SqlConnection(_connectionString);
            }
        }

        // Find One Find Many add update delete

        public IEnumerable<Drink> GetAll()
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<Drink>("SELECT * FROM Drinks");
            }
        }

        public Drink GetById(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.QueryFirstOrDefault<Drink>($"SELECT FROM Drinks WHERE id = {id}");
            }
        }

        public Drink Add(Drink drink)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                int id = dbConnection.Execute($@"
                INSERT INTO Drinks (Name, Description, Price)
                VALUES ({drink.Name}, {drink.Description}, {drink.Price});
                SELECT CAST(SCOPE_IDENTITY() as int)", drink);
                drink.Id = id;
                return drink;
            }
        }

        public Drink GetOneByIdAndUpdate(int id, Drink drink)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.QueryFirstOrDefault<Drink>($@"
                UPDATE Drinks SET  
                    Name = {drink.Name},
                    Description = {drink.Description},
                    Price = {drink.Price}
                WHERE Id = {id}
                ");
            }
        }

    }
}