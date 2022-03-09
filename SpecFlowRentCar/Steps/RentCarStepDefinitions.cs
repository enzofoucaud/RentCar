using TechTalk.SpecFlow;
using FluentAssertions;
using RentCar;
using RentCar.Fake;
using System;
using System.Globalization;

namespace RentCar.Steps
{
    [Binding]
    public sealed class RentCarStepDefinitions
    {

        // For additional details on SpecFlow step definitions see https://go.specflow.org/doc-stepdef
        private readonly ScenarioContext _scenarioContext;
        // Error return
        private string _lastErrorMessage;
        // Client
        private string _email;
        private string _firstname;
        private string _lastname;
        private string _password;
        private DateTime _birthday;
        private string _driver_license_id;
        private string _driver_license_date;
        // Car
        private string _matriculation;
        private string _brand;
        private string _model;
        private string _color;
        private double _form_price;
        private double _cleaning_price;
        private double _km_price;
        private int _horsepower;
        //Booking
        private DateTime _start_date;
        private DateTime _end_date;
        private double _estimated_distance;
        private double _estimated_price;
        // Location
        private Location _target;
        private FakeDataLayer _fakeDataLayer;

        public RentCarStepDefinitions(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
            this._fakeDataLayer = new FakeDataLayer();
            this._target = new Location(this._fakeDataLayer);
        }

        #region Background
        [Given(@"following existing clients")]
        public void GivenFollowingExistingClients(Table table)
        {
            foreach (TableRow row in table.Rows)
            {
                // A MODIFIER CAR LES INFORMATIONS NE SONT PAS DANS L'ORDRE
                this._fakeDataLayer.Clients.Add(new Client(row[0], row[1], row[2], row[3], DateTime.Parse(row[4], new CultureInfo("fr-FR")), row[5], row[6]));
            }
        }

        [Given(@"following existing cars")]
        public void GivenFollowingExistingCars(Table table)
        {
            foreach (TableRow row in table.Rows)
            {
                // A MODIFIER CAR LES INFORMATIONS NE SONT PAS RENTRE
                this._fakeDataLayer.Cars.Add(new Car(row[0], row[1], row[2], row[3], Convert.ToDouble(row[4]), Convert.ToDouble(row[5]), Convert.ToDouble(row[6]), Convert.ToInt32(row[7])));
            }
        }

        [Given(@"following existing bookings")]
        public void GivenFollowingExistingBookings(Table table)
        {
            foreach (TableRow row in table.Rows)
            {
                // A MODIFIER CAR LES INFORMATIONS NE SONT PAS RENTRE
                this._fakeDataLayer.Bookings.Add(new Booking(row[0], row[1], DateTime.Parse(row[2], new CultureInfo("fr-FR")), DateTime.Parse(row[3], new CultureInfo("fr-FR")), Convert.ToDouble(row[4]), Convert.ToDouble(row[5]), Convert.ToDouble(row[6]), Convert.ToBoolean(row[7])));
            }
        }

        #endregion

        [Given("my email address is (.*)")]
        public void GivenMyEmailAddressIs(string email)
        {
            this._email = email;
        }

        [Given("my password is (.*)")]
        public void GivenMyPasswordIs(string password)
        {
            this._password = password;
        }

        [Given("my lastname is (.*)")]
        public void GivenMyLastnameIs(string lastname)
        {
            this._lastname = lastname;
        }

        [Given("my firstname is (.*)")]
        public void GivenMyFirstnameIs(string firstname)
        {
            this._firstname = firstname;
        }

        [Given("my birthday is (.*)")]
        public void GivenMyBirthdayIs(DateTime birthday)
        {
            this._birthday = birthday;
        }

        [Given(@"my license ID is (.*)")]
        public void GivenMyLicenseIDIs(string driver_license_id)
        {
            this._driver_license_id = driver_license_id;
        }

        [Given(@"my license obtention date is (.*)")]
        public void GivenMyLicenseObtentionDateIs(string driver_license_date)
        {
            this._driver_license_date = driver_license_date;
        }

        [Given(@"The car brand is (.*)")]
        public void GivenTheCarBrandIs(string brand)
        {
            this._brand = brand;
        }

        [Given(@"The car model is (.*)")]
        public void GivenTheCarModelIsClio(string model)
        {
            this._model = model;
        }

        [Given(@"The car matriculation is (.*)")]
        public void GivenTheCarMatriculationIsDV_JK(string matriculation)
        {
            this._matriculation = matriculation;
        }

        [Given(@"The car horsepower is (.*)")]
        public void GivenTheCarHorsepowerIs(int horsepower)
        {
            this._horsepower = horsepower;
        }

        [Given(@"The car color is (.*)")]
        public void GivenTheCarColorIsYellow(string color)
        {
            this._color = color;
        }

        [Given(@"The form car price is (.*)")]
        public void GivenTheFormCarPriceIs(double form_price)
        {
            this._form_price = form_price;
        }

        [Given(@"The cleaning price is (.*)")]
        public void GivenTheCleaningPriceIs(double cleaning_price)
        {
            this._cleaning_price = cleaning_price;
        }

        [Given(@"The km price is (.*)")]
        public void GivenTheKmPriceIs(int km_price)
        {
            this._km_price = km_price;
        }

        [Given(@"the car I want to book is (.*)")]
        public void GivenTheCarIWantToBookIs(string matriculation)
        {
            this._matriculation = matriculation;
        }

        [Given(@"the booking start date is (.*)")]
        public void GivenTheBookingStartDateIs(string start_date)
        {
            this._start_date = DateTime.Parse(start_date, new CultureInfo("fr-FR"));
        }

        [Given(@"the booking end date is (.*)")]
        public void GivenTheBookingEndDateIs(string end_date)
        {
            this._end_date = DateTime.Parse(end_date, new CultureInfo("fr-FR"));
        }

        [Given(@"my estimation for the travelling distance is (.*) km")]
        public void GivenMyEstimationForTheTravellingDistanceIsKm(int estimated_distance)
        {
            this._estimated_distance = estimated_distance;
        }

        [When("I try to create an account")]
        public void WhenITryToCreateAnAccount()
        {
            Client client = new Client(this._email, this._password, this._lastname, this._firstname, this._birthday, this._driver_license_id, this._driver_license_date );
            this._lastErrorMessage = this._target.insertClient(client);
        }

        [When("I try to connect")]
        public void WhenITryToConnect()
        {
            this._lastErrorMessage = this._target.connectClient(this._email,this._password);
        }

        [When(@"I try to add new car in our park")]
        public void WhenITryToAddNewCarInOurPark()
        {
            Car car = new Car(this._matriculation, this._brand, this._model, this._color, this._form_price, this._cleaning_price, this._km_price, this._horsepower);
            this._lastErrorMessage = this._target.insertCar(car);
        }

        [When(@"I send the booking form")]
        public void WhenISendTheBookingForm()
        {
            Booking booking = new Booking(this._email, this._matriculation, this._start_date, this._end_date, this._estimated_distance);
            this._lastErrorMessage = this._target.insertBooking(booking);
            this._estimated_price = booking.Estimated_price;
        }

        [Then(@"the connection is not established")]
        public void ThenTheConnectionIsNotEstablished()
        {
            this._target.UserConnected.Should().BeFalse();
        }

        [Then(@"the connection is established")]
        public void ThenTheConnectionIsEstablished()
        {
            this._target.UserConnected.Should().BeTrue();
        }

        [Then(@"the message is (.*)")]
        public void ThenTheMessageIs(string errorMessage)
        {
            this._lastErrorMessage.Should().Be(errorMessage);
        }

        [Then(@"the estimated price is (.*) euros")]
        public void ThenTheEstimatedPriceIsEuros(double estimated_price)
        {
            this._estimated_price.Should().Be(estimated_price);
        }

    }
}
