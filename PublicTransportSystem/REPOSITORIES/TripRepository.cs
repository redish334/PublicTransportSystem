using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System;

public class TripRepository : ITripRepository
{
    private readonly IDbConnection _connection;
    private readonly IDbTransaction _transaction;

    public TripRepository(IDbConnection connection, IDbTransaction transaction)
    {
        _connection = connection;
        _transaction = transaction;
    }

    public IEnumerable<Trip> GetTripsByRoute(int routeId)
    {
        using (var cmd = new SqlCommand("GetTripsByRoute",
                                       (SqlConnection)_connection,
                                       (SqlTransaction)_transaction))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@route_id", routeId);

            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    yield return new Trip
                    {
                        TripId = (int)reader["trip_id"],
                        RouteName = reader["route_id"].ToString(),
                        Departure = (DateTime)reader["departure_time"],
                        Arrival = (DateTime)reader["arrival_time"]
                    };
                }
            }
        }
    }
}