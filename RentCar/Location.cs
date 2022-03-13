using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace RentCar
{
    public class Location
    {
        private IDataLayer _dataLayer;
        private bool userConnected;
        public bool UserConnected { get => userConnected; set => userConnected = value; }


        public Location()
        {
            this._dataLayer = new DataLayer();
        }

        public Location(IDataLayer dataLayer)
        {
            this._dataLayer = dataLayer;
        }


        // Client
        public string insertClient(Client client)
        {
            // Check if information missing
            if (client.GetType().GetProperties().All(p => p.GetValue(client) != null) == false)
            {
                return "Missing information";
            }
            // Check if user exist or not and create it if not
            Client result_mail = this._dataLayer.Clients.SingleOrDefault(_ => _.Email == client.Email);
            Client result_license_id = this._dataLayer.Clients.SingleOrDefault(_ => _.Driver_license_id == client.Driver_license_id);
            if (result_mail == null && result_license_id == null)
            {
                this._dataLayer.Clients.Add(client);
                return "The account is created";
            }
            else if (result_mail != null)
            {
                return "The user already exists, try with another email";
            } else
            {
                return "The license ID already exists";
            }    
        }

        public string connectClient(string email, string password)
        {
            Client result = this._dataLayer.Clients.SingleOrDefault(_ => _.Email == email);
            if (result == null)
            {
                this.UserConnected = false;
                return "Your email or password is incorrect";
            }
            else
            {
                if (result.Password == password)
                {
                    this.UserConnected = true;
                    return "You are connected";
                }
                else
                {
                    this.UserConnected = false;
                    return "Your email or password is incorrect";
                }
            }
        }

        // Car
        public string insertCar(Car car)
        {
            // Check if matriculation is good
            if (!car.isGoodMatriculation())
            {
                return "Matriculation is not common";
            }
            // Check if missing information
            if (car.GetType().GetProperties().All(p => p.GetValue(car) != null) == false)
            {
                return "Missing information";
            }
            // Matriculation verification
            Car result = this._dataLayer.Cars.SingleOrDefault(_ => _.Matriculation == car.Matriculation);
            if (result == null)
            {
                this._dataLayer.Cars.Add(car);
                return "The car has been created";
            }
            else
            {
                return "The car already exists, try with another matriculation";
            }
        }

        public string insertBooking(Booking booking)
        {
            if (!booking.areDatesCorrect())
            {
                return "The choosen dates are incorrect";
            }

            List<Booking> _bookings_matriculation = this._dataLayer.Bookings.FindAll(_booking => _booking.Car_matriculation == booking.Car_matriculation);
            foreach (var _booking in _bookings_matriculation)
            {
                if (areDatesOverlapping(booking.Start_date, booking.End_date, _booking.Start_date, _booking.End_date))
                {
                    return "The car is already booked on these dates";
                }
            }

            List<Booking> _bookings_mail = this._dataLayer.Bookings.FindAll(_booking => _booking.Client_email == booking.Client_email);
            foreach (var _booking in _bookings_mail)
            {
                if (areDatesOverlapping(booking.Start_date, booking.End_date, _booking.Start_date, _booking.End_date))
                {
                    return "You have already booked a car on these dates";
                }
            }

            if(this.clientAge(booking) < 18)
            {
                return "You cannot book this car because you are under 18";
            } else if (this.clientAge(booking) < 21 && this.carHorsepower(booking) >= 8)
            {
                return "You cannot book this car with 8 or more horsepower because you are under 21";
            } else if (this.clientAge(booking) < 25 && this.carHorsepower(booking) >= 13)
            {
                return "You cannot book this car with 13 or more horsepower because you are under 25";
            }

            this._dataLayer.Bookings.Add(booking);
            this.estimatePrice(booking);
            return "Congratulations, your booking is confirmed";
        }

        public bool areDatesOverlapping(DateTime start_date_new_booking, DateTime end_date_new_booking , DateTime start_date_existing_booking, DateTime end_date_existing_booking)
        {
            return start_date_new_booking < end_date_existing_booking && start_date_existing_booking < end_date_new_booking;
        }

        public double estimatePrice(Booking booking)
        {
            Car result = this._dataLayer.Cars.SingleOrDefault(_ => _.Matriculation == booking.Car_matriculation);
            booking.Estimated_price = result.Form_price + result.Cleaning_price + (result.KM_price * booking.Estimated_distance);
            return booking.Estimated_price;
        }


        public int clientAge(Booking booking)
        {
            Client client = this._dataLayer.Clients.SingleOrDefault(_ => _.Email == booking.Client_email);
            DateTime now = DateTime.Now;
            DateTime birthdate = client.Birthday;

            int age = now.Year - birthdate.Year;

            if (now.Month < birthdate.Month || (now.Month == birthdate.Month && now.Day < birthdate.Day))
            {
                age--;
            }

            return age;
        }

        public int carHorsepower(Booking booking)
        {
            Car car = this._dataLayer.Cars.SingleOrDefault(_ => _.Matriculation == booking.Car_matriculation);

            return car.Horsepower;
        }
    }
}
