using System.Collections.Generic;
public interface ITripRepository
{
    IEnumerable<Trip> GetTripsByRoute(int routeId);
}
