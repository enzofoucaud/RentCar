using RentCar;
using System;
using System.Collections.Generic;
using System.Text;

namespace RentCar.Fake
{
    class FakeDataLayer : IDataLayer
    {
        public List<Client> Clients { get; set; }
        public List<Car> Cars { get; set; }
        public List<Booking> Bookings { get; set; }

        public FakeDataLayer()
        {
            this.Clients = new List<Client>();
            this.Cars = new List<Car>();
            this.Bookings = new List<Booking>();
        }
    }
}
