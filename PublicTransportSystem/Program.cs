using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublicTransportSystem
{
    class Program
    {
        static void Main()
        {
            string cs = @"Server=DESKTOP-9U2MBLB\SQLEXPRESS;Database=PublicTransport1;Trusted_Connection=True;";
            var uow = new UnitOfWork(new SqlConnectionFactory(cs));

            Console.WriteLine("=== All drivers ===");
            foreach (var d in uow.Drivers.GetAll())
                Console.WriteLine($"{d.DriverId}. {d.FirstName} {d.LastName}");

            Console.WriteLine("\n=== Add new driver ===");
            uow.Drivers.Add("Roman", "Dudko", "380991112233", "L4444");

            Console.WriteLine("\n=== Trips for route 1 ===");
            foreach (var t in uow.Trips.GetTripsByRoute(1))
                Console.WriteLine($"{t.TripId}: {t.Departure} → {t.Arrival}");

            uow.Commit();
            Console.WriteLine("\nCOMPLETED");
        }
    }

}
