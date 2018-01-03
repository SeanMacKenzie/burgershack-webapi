using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using burgershack_c.Models;
using Dapper;

namespace burgershack_c.Repositories
{
    public class BurgerRepository
    {
        private string _connectionString;

        public BurgerRepository()
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

        public IEnumerable<Burger> GetAll()
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<Burger>("SELECT * FROM Burgers");
            }
        }

        public Burger GetById(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.QueryFirstOrDefault<Burger>($"SELECT * FROM Burgers WHERE id = {id}");
            }
        }

        public Burger Add(Burger burger)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                int id = dbConnection.Execute("INSERT INTO Burgers (Name, Description, Price)"
                 + " VALUES (@Name, @Description, @Price) SELECT CAST(SCOPE_IDENTITY() as int)", burger);
                burger.Id = id;
                return burger;
            }
        }

         public Burger GetOneByIdAndUpdate(int id, Burger burger)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.QueryFirstOrDefault<Burger>($@"
                UPDATE Burgers SET  
                    Name = @Name,
                    Description = @Description,
                    Price = @Price
                WHERE Id = {id};
                SELECT * FROM Burgers WHERE id = {id};", burger);
            }
        }

        public void DeleteById(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                dbConnection.QueryFirstOrDefault<Burger>($@"
                    DELETE FROM Burgers
                    WHERE id = {id}");
            }

        }

    }
}
