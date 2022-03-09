using System;
using System.Collections.Generic;
using System.Text;

namespace RentCar
{
    public class Client
    {
        private string email;
        private string password;
        private string lastname;
        private string firstname;
        private DateTime birthday;
        private string driver_license_id;
        private string driver_license_date;
        

        public string Email { get => email; set => email = value; }
        public string Password { get => password; set => password = value; }
        public string Lastname { get => lastname; set => lastname = value; }
        public string Firstname { get => firstname; set => firstname = value; }
        public DateTime Birthday { get => birthday; set => birthday = value; }
        public string Driver_license_id { get => driver_license_id; set => driver_license_id = value; }
        public string Driver_license_date { get => driver_license_date; set => driver_license_date = value; }
        

        public Client(string _email, string _password, string _lastname, string _firstname, DateTime _birthday, string _driver_license_id, string _driver_license_date)
        {
            this.Email = _email;
            this.Password = _password;
            this.Lastname = _lastname;
            this.Firstname = _firstname;
            this.Birthday = _birthday;
            this.Driver_license_id = _driver_license_id;
            this.Driver_license_date = _driver_license_date;
        }
    }
}
