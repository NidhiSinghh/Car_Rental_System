--year ->manufacture_Year,status-> vehicle_Status ,type->lease_type

CREATE DATABASE CAR_RENTAL_SYSTEM
USE CAR_RENTAL_SYSTEM

CREATE TABLE VEHICLES
(
vehicle_Id INT IDENTITY PRIMARY KEY,
make VARCHAR(30) NOT NULL,
model  VARCHAR(30) NOT NULL,
manufacture_Year INT NOT NULL,
daily_Rate FLOAT NOT NULL,
vehicle_Status VARCHAR(25) NOT NULL,
passenger_Capacity INT NOT NULL,
engine_Capacity    INT NOT NULL,
)

CREATE TABLE CUSTOMERS
(
customer_Id INT IDENTITY PRIMARY KEY,
first_Name VARCHAR(20) NOT NULL,
last_Name VARCHAR(20) ,
email VARCHAR(30) NOT NULL,
phone_Number VARCHAR(10) NOT NULL,

)
CREATE TABLE LEASE
(
lease_ID INT IDENTITY PRIMARY KEY,
vehicle_ID INT NOT NULL,
customer_ID INT NOT NULL,
start_Date DATE NOT NULL,
end_Date  DATE NOT NULL,
lease_type VARCHAR(30) NOT NULL
FOREIGN KEY(vehicle_ID) REFERENCES VEHICLES(vehicle_ID),
FOREIGN KEY(customer_ID) REFERENCES CUSTOMERS(customer_Id)
)
CREATE TABLE PAYMENTS
(
payment_ID INT IDENTITY PRIMARY KEY,
lease_ID INT NOT NULL,
payment_Date DATE NOT NULL,
amount FLOAT NOT NULL
FOREIGN KEY(lease_ID) REFERENCES LEASE(lease_ID)
)

INSERT INTO VEHICLES(make, model,manufacture_Year, daily_Rate, vehicle_Status, passenger_Capacity, engine_Capacity)
VALUES
  ('Toyota', 'Camry', 2019, 60.00, 'available', 5, NULL),
  ('Honda', 'Civic', 2020, 55.00, 'available', 5, NULL),
  ('Ford', 'Escape', 2018, 70.00, 'available', 5, NULL),
  ('Chevrolet', 'Malibu', 2021, 65.00, 'available', 5, NULL),
  ('Nissan', 'Altima', 2017, 50.00, 'notAvailable', 5, NULL),
  ('Harley-Davidson', 'Sportster', 2022, 100.00, 'available', NULL, 1200),
  ('BMW', 'R1250GS', 2021, 120.00, 'available', NULL, 1250),
  ('Tesla', 'Model 3', 2023, 150.00, 'available', 5, NULL),
  ('Ford', 'Mustang', 2019, 80.00, 'notAvailable', 4, NULL),
  ('Audi', 'Q5', 2020, 90.00, 'available', 5, NULL),
  ('Kawasaki', 'Ninja 300', 2022, 80.00, 'available', NULL, 296),
  ('Ducati', 'Monster 821', 2021, 100.00, 'available', NULL, 821),
  ('Yamaha', 'YZF-R6', 2023, 90.00, 'available', NULL, 599),
  ('Honda', 'CBR650R', 2020, 85.00, 'available', NULL, 649),
  ('Harley-Davidson', 'Softail Slim', 2022, 120.00, 'available', NULL, 1868),
  ('Suzuki', 'GSX-R750', 2021, 95.00, 'available', NULL, 750);

INSERT INTO CUSTOMERS(first_Name, last_Name, email, phone_Number)
VALUES
  ('John', 'Doe', 'john.doe@example.com', '1234567890'),
  ('Jane', 'Smith', 'jane.smith@example.com', '9876543210'),
  ('Bob', 'Johnson', 'bob.johnson@example.com', '5551234567'),
  ('Alice', 'Williams', 'alice.williams@example.com', '4567890123'),
  ('Charlie', 'Brown', 'charlie.brown@example.com', '7890123456'),
  ('Eva', 'Taylor', 'eva.taylor@example.com', '2345678901'),
  ('Sam', 'Miller', 'sam.miller@example.com', '6789012345'),
  ('Olivia', 'Clark', 'olivia.clark@example.com', '8901234567'),
  ('Daniel', 'Lee', 'daniel.lee@example.com', '3456789012'),
  ('Sophia', 'Wang', 'sophia.wang@example.com', '5678901234');

INSERT INTO LEASE(vehicle_ID, customer_ID, start_Date, end_Date, lease_type)
VALUES
  (1, 1, '2023-01-01', '2023-01-05', 'DailyLease'),
  (2, 2, '2023-02-10', '2023-02-15', 'DailyLease'),
  (3, 3, '2023-03-15', '2023-04-15', 'MonthlyLease'),
  (4, 4, '2023-05-01', '2023-06-01', 'MonthlyLease'),
  (5, 5, '2023-07-01', '2023-07-05', 'DailyLease'),
  (6, 6, '2023-08-10', '2023-08-17', 'DailyLease'),
  (7, 7, '2023-09-15', '2023-10-15', 'MonthlyLease'), 
  (12, 2, '2024-04-10', '2024-04-15', 'DailyLease'),
  (13, 3, '2024-05-15', '2024-06-15', 'MonthlyLease'),
  (14, 4, '2024-07-01', '2024-08-01', 'MonthlyLease');

   --(8, 8, '2023-11-01', '2023-11-15', 'DailyLease'),
  --(9, 9, '2023-12-01', '2024-01-01', 'MonthlyLease'),
  --(10, 10, '2024-02-15', '2024-02-20', 'DailyLease'),

-- Payment Table
INSERT INTO PAYMENTS(lease_ID, payment_Date, amount)
VALUES
  (2, '2023-02-15', 55.00),
  (3, '2023-03-30', 70.00),
  (4, '2023-06-05', 65.00),
  (5, '2023-07-05', 50.00),
  (6, '2023-08-17', 100.00),
  (8, '2023-11-15', 150.00),
  (9, '2024-01-05', 90.00),
  (10, '2024-02-20', 80.00);
select * from payments
--truncate table payments;
    --(7, '2023-10-01', 120.00),
use car_rental_System


select * from VEHICLES;
--------------------------------------------------------------------------------------
--Car Management

--1. void addCar(Car car);
--add a vehicle when you are given a vehcile name
--('Aprilia', 'Tuono V4 1100', 2021, 115.00, 'available', NULL, 1077),
DECLARE  @make VARCHAR(30), @model VARCHAR(30),@year INT, @status VARCHAR(30)
SELECT @make='Aprilia', @model='Tuono V4 1100',@year=2021, @status='Available';
INSERT INTO VEHICLES(make, model,manufacture_Year,vehicle_Status)
VALUES (@make,@model,@year,@status);

--2. void removeCar(int carID);
--remove a vehicle
DECLARE @vehicle_id INT
SET @vehicle_id =17
DELETE FROM VEHICLES WHERE vehicle_id=@vehicle_id;

--3. List<Car> listAvailableCars();
--3.1  the one with status marked as available
	SELECT vehicle_Id,make+' ' +model as vehicle_Name FROM VEHICLES WHERE vehicle_Status='available';
----3.2  list available(non-rented) cars which are not on lease(rent) 
--	SELECT v.vehicle_Id,v.make+' '+v.model as non_rented_vehicles
--	FROM VEHICLES v LEFT JOIN LEASE l ON v.vehicle_Id=l.vehicle_ID
--	WHERE l.vehicle_ID IS NULL

--4. List<Car> listRentedCars
--list rented cars 
SELECT v.vehicle_Id,v.make+' '+v.model as rented_vehicles
FROM VEHICLES v JOIN LEASE l ON v.vehicle_Id=l.vehicle_ID

--5. Car findCarById(int carID);
--find vehicle when id is given
SELECT vehicle_Id ,make+' ' +model as vehicle_Name FROM VEHICLES WHERE vehicle_Id=6;


--list all of the vehicles
SELECT make+' ' +model as vehicle_Name FROM VEHICLES ;

--Customer Management
--1. void addCustomer(Customer customer)
--add a customer
DECLARE  @firstname VARCHAR(30), @email VARCHAR(30), @phone_Number INT
SELECT @firstname='Rohit', @email='rohit@gmail.com',@phone_Number=123456789;
INSERT INTO CUSTOMERS(first_Name,email,phone_Number)
VALUES (@firstname,@email,@phone_Number);

SELECT * FROM CUSTOMERS
--2. void removeCustomer(int customerID);
--remove a customer when customer id is given
DECLARE @delcustomerid INT
SET @delcustomerid =11
DELETE FROM CUSTOMERS WHERE customer_Id=@delcustomerid;

--3. List<Customer> listCustomers();
--list all customers
SELECT * FROM CUSTOMERS

--4. Customer findCustomerById(int customerID);
--Find a customer by id
SELECT * FROM CUSTOMERS WHERE customer_Id=11

--Lease Management
--1. Lease createLease(int customerID, int carID, Date startDate, Date endDate);
-- Create a lease
DECLARE @vehicleId INT , @customerId INT, @startDate DATE, @endDate DATE, @leasetype VARCHAR(30)
SELECT  @vehicleId=8 , @customerId=8 , @startDate='2023-10-01' , @endDate='2023-11-01' , @leasetype='MonthlyLease';
INSERT INTO LEASE(vehicle_ID, customer_ID, start_Date, end_Date, lease_type)
VALUES (@vehicleId, @customerId, @startDate, @endDate, @leasetype)

 --2. void returnCar(int leaseID);
--returnCar(int leaseID); (while returning a car,update the End DateDECLARE @returnleaseId INTSET @returnleaseId=11;UPDATE LEASESET end_date=CAST( GETDATE() AS Date ) WHERE lease_ID=@returnleaseId

--3. List<Lease> listActiveLeases();
--Give list of active leases
SELECT lease_ID as Active_lease_id FROM LEASE WHERE CAST( GETDATE() AS Date ) BETWEEN start_Date AND end_Date;
--4. List<Lease> listLeaseHistory();--list lease history
SELECT customer_ID,vehicle_ID,start_Date,end_date FROM LEASE 
ORDER BY customer_ID,start_Date DESC ,end_date DESC


--list lease history for a particular customer
SELECT customer_ID,vehicle_ID,start_Date,end_date FROM LEASE 
WHERE customer_ID=1
ORDER BY start_Date DESC ,end_date DESC 


----Find car when leaseid is given
--SELECT l.lease_ID,v.make+' ' + v.model as vehicleName
--FROM LEASE l JOIN VEHICLES v ON l.vehicle_ID=v.vehicle_Id
-- WHERE l.lease_ID=8

--Payment Handling
--1. void recordPayment(Lease lease, double amount);
--  (11, '2023-02-15', 55.00)
Declare @lease_ID int = 11 , @amount float = 55.00
INSERT INTO PAYMENTS(lease_ID, payment_Date, amount)
VALUES (@lease_ID,getdate(),@amount)


--2. List<Payment> listPayments(Lease lease);
--show payments for a lease
--SELECT * FROM PAYMENTS where lease_ID=3

select p.payment_ID ,l.customer_ID,p.payment_Date, p.Amount , l.lease_type
from Payments p join Lease l
on p.lease_ID = l.lease_ID
--------------------------------------------------------------------------------------

















--____________________________
--update customer details

--change vehivle status

--Calculate the total cost of a lease based on the type (Daily or Monthly) and the number
--of days or months.
