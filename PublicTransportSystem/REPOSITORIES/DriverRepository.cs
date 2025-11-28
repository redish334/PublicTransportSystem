using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

public class DriverRepository : IDriverRepository
{
    private readonly IDbConnection _connection;
    private readonly IDbTransaction _transaction;

    public DriverRepository(IDbConnection connection, IDbTransaction transaction)
    {
        _connection = connection;
        _transaction = transaction;
    }

    public IEnumerable<Driver> GetAll()
    {
        using (var cmd = new SqlCommand("SELECT * FROM Driver WHERE is_deleted = 0",
                                       (SqlConnection)_connection,
                                       (SqlTransaction)_transaction))
        {
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    yield return new Driver
                    {
                        DriverId = (int)reader["driver_id"],
                        FirstName = reader["first_name"].ToString(),
                        LastName = reader["last_name"].ToString(),
                        Phone = reader["phone"].ToString(),
                        LicenseNumber = reader["license_number"].ToString()
                    };
                }
            }
        }
    }

    public void Add(string f, string l, string p, string lic)
    {
        using (var cmd = new SqlCommand("AddDriver",
                                       (SqlConnection)_connection,
                                       (SqlTransaction)_transaction))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@first_name", f);
            cmd.Parameters.AddWithValue("@last_name", l);
            cmd.Parameters.AddWithValue("@phone", p);
            cmd.Parameters.AddWithValue("@license_number", lic);
            cmd.Parameters.AddWithValue("@updated_by", "system");
            cmd.ExecuteNonQuery();
        }
    }

    public void SoftDelete(int id)
    {
        using (var cmd = new SqlCommand("SoftDeleteDriver",
                                       (SqlConnection)_connection,
                                       (SqlTransaction)_transaction))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@driver_id", id);
            cmd.Parameters.AddWithValue("@updated_by", "system");
            cmd.ExecuteNonQuery();
        }
    }

}