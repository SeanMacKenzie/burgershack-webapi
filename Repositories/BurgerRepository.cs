using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using burgershack_c.Models;
using Dapper;
using MySql.Data.MySqlClient;

namespace burgershack_c.Repositories
{
    public class BurgerRepository
    {
        private readonly IDbConnection _db;

        public BurgerRepository(IDbConnection db)
        {
            _db = db;
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
                int id = dbConnection.ExecuteScalar<int>("INSERT INTO Burgers (Name, Description, Price)"
                 + " VALUES (@Name, @Description, @Price) SELECT LAST_INSERT_ID()", burger);
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
