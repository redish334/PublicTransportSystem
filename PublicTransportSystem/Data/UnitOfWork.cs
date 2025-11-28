using System;
using System.Data;

public class UnitOfWork : IDisposable
{
    private readonly IDbConnection _conn;
    private readonly IDbTransaction _txn;

    public IDriverRepository Drivers { get; }
    public ITripRepository Trips { get; }

    public UnitOfWork(IDbConnectionFactory factory)
    {
        _conn = factory.CreateConnection();
        _conn.Open();
        _txn = _conn.BeginTransaction();

        Drivers = new DriverRepository(_conn, _txn);
        Trips = new TripRepository(_conn, _txn);
    }

    public void Commit()
    {
        _txn.Commit();
        _conn.Close();
    }

    public void Dispose() => _conn.Dispose();
}
