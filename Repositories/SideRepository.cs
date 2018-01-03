using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using burgershack_c.Models;
using Dapper;

namespace burgershack_c.Repositories
{
    public class SideRepository
    {
        private string _connectionString;

        public SideRepository()
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

        public IEnumerable<Side> GetAll()
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<Side>("SELECT * FROM Sides");
            }
        }

        public Side GetById(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.QueryFirstOrDefault<Side>($"SELECT FROM Sides WHERE id = {id}");
            }
        }

        public Side Add(Side side)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                int id = dbConnection.Execute($@"
                INSERT INTO Sides (Name, Description, Price)
                VALUES ({side.Name}, {side.Description}, {side.Price});
                SELECT CAST(SCOPE_IDENTITY() as int)", side);
                side.Id = id;
                return side;
            }
        }

        public Side GetOneByIdAndUpdate(int id, Side side)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.QueryFirstOrDefault<Side>($@"
                UPDATE Sides SET  
                    Name = {side.Name},
                    Description = {side.Description},
                    Price = {side.Price}
                WHERE Id = {id}
                ");
            }
        }

    }
}