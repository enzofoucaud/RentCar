using System;
using System.Collections.Generic;
using System.Text;

namespace RentCar
{
    public class Booking
    {
        private string client_email;
        private string car_matriculation;
        private DateTime start_date;
        private DateTime end_date;
        private double estimated_distance;
        private double estimated_price;
        private double final_price;
        private bool car_returned;

        public string Client_email { get => client_email; set => client_email = value; }
        public string Car_matriculation { get => car_matriculation; set => car_matriculation = value; }
        public DateTime Start_date { get => start_date; set => start_date = value; }
        public DateTime End_date { get => end_date; set => end_date = value; }
        public double Estimated_distance { get => estimated_distance; set => estimated_distance = value; }
        public double Estimated_price { get => estimated_price; set => estimated_price = value; }
        public double Final_price { get => final_price; set => final_price = value; }
        public bool Car_returned { get => car_returned; set => car_returned = value; }

        public Booking(string client_email, string car_matriculation, DateTime start_date, DateTime end_date, double estimated_distance)
        {
            Client_email = client_email;
            Car_matriculation = car_matriculation;
            Start_date = start_date;
            End_date = end_date;
            Estimated_distance = estimated_distance;
            Car_returned = false;
        }

        public Booking(string client_email, string car_matriculation, DateTime start_date, DateTime end_date, double estimated_distance, double estimated_price, double final_price, bool car_returned)
        {
            Client_email = client_email;
            Car_matriculation = car_matriculation;
            Start_date = start_date;
            End_date = end_date;
            Estimated_distance = estimated_distance;
            Estimated_price = estimated_price;
            Final_price = final_price;
            Car_returned = car_returned;
        }

        public bool areDatesCorrect()
        {
            int result = DateTime.Compare(Start_date, End_date);
            return result < 0;  
        }

        
    }
}
