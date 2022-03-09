using System;
using System.Text.RegularExpressions;

namespace RentCar
{
    public class Car
    {
        private string matriculation;
        private string brand;
        private string model;
        private string color;
        private double form_price;
        private double cleaning_price;
        private double km_price;
        private int horsepower;

        public string Matriculation { get => matriculation; set => matriculation = value; }
        public string Brand { get => brand; set => brand = value; }
        public string Model { get => model; set => model = value; }
        public string Color { get => color; set => color = value; }
        public double Form_price { get => form_price; set => form_price = value; }
        public double Cleaning_price { get => cleaning_price; set => cleaning_price = value; }
        public double KM_price { get => km_price; set => km_price = value; }
        public int Horsepower { get => horsepower; set => horsepower = value; }

        public Car(string _matriculation, string _brand, string _model, string _color, double _form_price, double _cleaning_price, double _km_price, int _horsepower)
        {
            this.Matriculation = _matriculation;
            this.Brand = _brand;
            this.Model = _model;
            this.Color = _color;
            this.Form_price = _form_price;
            this.Cleaning_price = _cleaning_price;
            this.KM_price = _km_price;
            this.Horsepower = _horsepower;
        }

        public bool isGoodMatriculation()
        {
            Regex regex = new Regex("^[A-Z]{2}[-]{1}[0-9]{3}[-]{1}[A-Za-z]{2}");
            Match match = regex.Match(this.matriculation);
            if (match.Success)
            {
                return true;
            }
            return false;
        }
    }
}
