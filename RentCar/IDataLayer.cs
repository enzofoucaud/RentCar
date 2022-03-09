using System.Collections.Generic;

namespace RentCar
{
    public interface IDataLayer
    {
        List<Client> Clients { get; }
        List<Car> Cars { get; }
        List<Booking> Bookings { get; }
    }
}
