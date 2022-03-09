Feature: RentCar
![Calculator](https://specflow.org/wp-content/uploads/2020/09/calculator.png)
Simple calculator for adding **two** numbers

Link to a feature: [Calculator](SpecFlowRentCar/Features/Calculator.feature)
***Further read***: **[Learn more about how to generate Living Documentation](https://docs.specflow.org/projects/specflow-livingdoc/en/latest/LivingDocGenerator/Generating-Documentation.html)**

Background: 
	Given following existing clients
	| Email               | Password | Firstname | Lastname   | Birthday   | License_ID | License_date |
	| foucauden@gmail.com | coolcar  | Enzo      | Foucaud    | 05/10/1999 | 158963257  | 01/05/2019   |
	| yanis@gmail.com     | supercar | Yanis     | Parmentier | 03/03/1998 | 158745990  | 05/03/2019   |

	Given following existing cars
	| Matriculation | Brand   | Model     | Color  | Form_price | Cleaning_price | KM_price | Horsepower |
	| DV-231-FR     | Peugeot | 208       | Grey   | 10         | 20             | 3        | 5          |
	| DR-232-FR     | Renault | Clio      | Yellow | 10         | 20             | 3        | 5          |
	| DG-233-FR     | Nissan  | GTR       | Purple | 100        | 100            | 8        | 10         |
	| DB-234-FR     | Ferrari | LaFerrari | Red    | 300        | 500            | 20       | 30         |

	Given following existing bookings
	| Client_email    | Car_matriculation | Start_date | End_date   | Estimated_distance | Estimated_price | Final_price | Car_returned  |
	| yanis@gmail.com | DV-233-FR         | 02/11/2022 | 11/11/2022 | 100                | 1000            | 0            | false        |
	| yanis@gmail.com | DV-234-FR         | 07/03/2022 | 13/03/2022 | 100                | 2800            | 0            | false        |

# Général
Scenario: Client account creation - Account registered
	Given my email address is p.dupont@mail.com
	And my firstname is Pierre
	And my lastname is Dupont
	And my password is passwd10
	And my birthday is 10/10/1910
	And my license ID is 185679258
	And my license obtention date is 13/02/2021
	When I try to create an account
	Then the message is The account is created

Scenario: Client account creation - Email already exists
	Given my email address is yanis@gmail.com
	And my firstname is Yanis
	And my lastname is Parm
	And my password is passwd10
	And my birthday is 10/12/2000
	And my license ID is 185679259
	And my license obtention date is 13/12/2021
	When I try to create an account
	Then the message is The user already exists, try with another email

Scenario: Client account creation - License ID already exists
	Given my email address is newclient@mail.com
	And my firstname is Yanis
	And my lastname is Parm
	And my password is passwd10
	And my birthday is 10/12/2000
	And my license ID is 158963257
	And my license obtention date is 22/06/2021
	When I try to create an account
	Then the message is The license ID already exists

Scenario: Client account creation - Missing information
	Given my email address is p.dupont@mail.com
	And my firstname is Pierre
	And my password is passwd10
	And my birthday is 10/10/1910
	And my license ID is 185679258
	And my license obtention date is 13/02/2021
	When I try to create an account
	Then the message is Missing information

Scenario: Client account connection - Etablished
	Given my email address is yanis@gmail.com
	And my password is supercar
	When I try to connect
	Then the connection is established
	And the message is You are connected

Scenario: Client account connection - Not etablished because email is incorrect
	Given my email address is yan@gmail.com
	And my password is supercar
	When I try to connect
	Then the connection is not established
	And the message is Your email or password is incorrect

Scenario: Client account connection - Not etablished because password is incorrect
	Given my email address is yanis@gmail.com
	And my password is car
	When I try to connect
	Then the connection is not established
	And the message is Your email or password is incorrect

# Car
Scenario: Add car in our park - Authorized
	Given The car brand is Renault
	And The car model is Clio
	And The car matriculation is DV-237-JK
	And The car horsepower is 4
	And The car color is Yellow
	And The form car price is 10
	And The cleaning price is 25
	And The km price is 3
	When I try to add new car in our park
	Then the message is The car has been created

Scenario: Add car in our park - Not authorized because missing information
	Given The car brand is Renault
	And The car model is Clio
	And The car matriculation is DR-231-FR
	When I try to add new car in our park
	Then the message is Missing information

Scenario: Add car in our park - Not authorized because matriculation is used
	Given The car brand is Renault
	And The car model is Clio
	And The car matriculation is DV-231-FR
	And The car horsepower is 4
	And The car color is Yellow
	And The form car price is 10
	And The cleaning price is 25
	And The km price is 3
	When I try to add new car in our park
	Then the message is The car already exists, try with another matriculation

Scenario: Add car in our park - Not authorized because matriculation is not common
	Given The car brand is XXX
	And The car model is XXX
	And The car matriculation is 22-FRR-22
	And The car horsepower is 0
	And The car color is XXX
	And The form car price is 0
	And The cleaning price is 0
	And The km price is 0
	When I try to add new car in our park
	Then the message is Matriculation is not common

# Booking 
Scenario: A client is booking a car 
	Given my email address is foucauden@gmail.com
	And the car I want to book is DV-231-FR
	And the booking start date is 10/04/2022
	And the booking end date is 20/04/2022
	And my estimation for the travelling distance is 100 km
	When I send the booking form
	Then the estimated price is 330 euros
	And the message is Congratulations, your booking is confirmed


Scenario: A client is booking a car - Incorrect dates (end date is sooner then start date)
	Given my email address is foucauden@gmail.com
	And the car I want to book is DV-234-FR
	And the booking start date is 10/04/2022
	And the booking end date is 05/04/2022
	And my estimation for the travelling distance is 100 km
	When I send the booking form
	Then the message is The choosen dates are incorrect

Scenario: A client is booking a car - Car is already booked on this dates
	Given my email address is foucauden@gmail.com
	And the car I want to book is DV-234-FR
	And the booking start date is 08/03/2022
	And the booking end date is 15/03/2022
	And my estimation for the travelling distance is 100 km
	When I send the booking form
	Then the message is The car is already booked on these dates

Scenario: A client is booking a car - Client has already booked a car on this dates
	Given my email address is yanis@gmail.com
	And the car I want to book is DV-231-FR
	And the booking start date is 03/11/2022
	And the booking end date is 15/11/2022
	And my estimation for the travelling distance is 100 km
	When I send the booking form
	Then the message is You have already booked a car on these dates

