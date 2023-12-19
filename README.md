# Car-Rental-System

## Overview

This is a simple Car Rental System implemented in C#. The system allows users to manage customers, vehicles, leases, and payments.

## Project Structure

The project follows an object-oriented design and is organized into the following packages:

- **model:** Contains entity classes representing real-world entities (e.g., Customer, Vehicle, Lease).
- **repository:** Provides the data access layer with interfaces and their implementations for database interactions.
- **exception:** Defines custom exceptions for error handling.
- **util:** Includes utility classes for database connection and property handling.
- **main:** Contains the MainModule class for demonstrating system functionalities in a menu-driven application.

## Key Functionalities

1. **Customer Management:**
   - Add new customers, update customer information, and retrieve customer details.

2. **Car Management:**
   - Add new cars, update car availability, and retrieve car information.

3. **Lease Management:**
   - Create daily or monthly leases for customers.
   - Calculate the total cost of a lease based on the type (Daily or Monthly) and the number of days or months.

4. **Payment Handling:**
   - Record payments for leases.
   - Retrieve payment history for a customer.
   

## Database Schema

The application connects to a SQL database with the following tables:

1. **Vehicle Table:**
   - vehicleID (Primary Key)
   - make
   - model
   - year
   - dailyRate
   - status (available, notAvailable)
   - passengerCapacity
   - engineCapacity

2. **Customer Table:**
   - customerID (Primary Key)
   - firstName
   - lastName
   - email
   - phoneNumber

3. **Lease Table:**
   - leaseID (Primary Key)
   - vehicleID (Foreign Key referencing Vehicle Table)
   - customerID (Foreign Key referencing Customer Table)
   - startDate
   - endDate
   - type (to distinguish between DailyLease and MonthlyLease)

4. **Payment Table:**
   - paymentID (Primary Key)
   - leaseID (Foreign Key referencing Lease Table)
   - paymentDate
   - amount

## Getting Started

1. Clone the repository: `git clone https://github.com/NidhiSinghh/Car_Rental_System.git`
2. Set up your SQL database and update connection details in `DBUtil` class and appsettings.json file.

## Packages Used

The Car Rental System project utilizes the following key packages:

1. **Microsoft.Extensions.Configuration**
2. **Microsoft.Extensions.Configuration.Abstractions**
3. **Microsoft.Extensions.Configuration.FileExtensions**
4. **Microsoft.Extensions.Configuration.Json**
5. **System.Data.SqlClient**
   
## Running the Application

Compile and run the `Program.cs` class to start the menu-driven application.

## Unit Testing

Unit test cases are essential to ensure the correctness and reliability of the system. Use NUnit syntax for writing test cases.

## Contributors

- Nidhi Singh



