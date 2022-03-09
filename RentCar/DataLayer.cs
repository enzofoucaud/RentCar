using System.Collections.Generic;

namespace RentCar
{
    internal class DataLayer : IDataLayer
    {
        public List<Client> Clients { get; private set; }
        public List<Car> Cars { get; private set; }
        public List<Booking> Bookings { get; private set; }

        public DataLayer()
        {
            this.Clients = new List<Client>();
            this.Cars = new List<Car>();
            this.Bookings = new List<Booking>();
        }
    }
}
