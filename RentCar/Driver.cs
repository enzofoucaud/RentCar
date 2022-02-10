using System;
using System.Collections.Generic;
using System.Text;

namespace RentCar
{
    class Driver
    {
        public string lastname;
        public string firstname;
        public string birthday;
        public string driver_licence_date;
        public string driver_licence_id;

        public Driver(string _lastname, string _firstname)
        {
            this.lastname = _lastname;
            this.firstname = _firstname;
        }
    }
}
