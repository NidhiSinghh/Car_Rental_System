using Car_Rental_System.Exceptions;
using Car_Rental_System.Model;
using Car_Rental_System.Repository;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Car_Rental_System.Service
{
    internal class CarLeaseService : ICarLeaseService
    {

        ICarLeaseRepository icarLeaseRepository;

        public CarLeaseService()
        {
            icarLeaseRepository = new CarLeaseRepository();
        }

        //--------------------------------------------------------------Lease management---------------------------------------------------
        public void returnCarInfo()
        {
            try
            {
                Console.WriteLine("Enter lease id for which Lease details you want");
                int enteredLeaseId = int.Parse(Console.ReadLine());
                Lease returnedLeaseInfo = icarLeaseRepository.returnCarInfo(enteredLeaseId);
                Console.WriteLine(returnedLeaseInfo);
            }
            catch (LeaseNotFoundException ex)
            {
                Console.WriteLine($"{ex.Message}");
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Database error: {ex.Message}");

            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");

            }
        }


        #region Active Lease
        public void ListActiveLease()
        {
            try
            {
                List<Lease> activeLease = icarLeaseRepository.ListActiveLeases();
                foreach (Lease lease in activeLease)
                {
                    Console.WriteLine(lease);
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Database error: {ex.Message}");

            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");

            }

        }

        #endregion

        #region Lease History
        public void listLeaseHistory()
        {
            try
            {
                List<Lease> leaseDetails = icarLeaseRepository.listLeaseHistory();
                foreach (Lease lease in leaseDetails)
                {
                    Console.WriteLine(lease);
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Database error: {ex.Message}");

            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");

            }
        }
        #endregion


        #region Create Lease
        public void createLease()
        {
            try
            {
                Console.WriteLine("Enter vehicle ID:");
                int vehicleId = int.Parse(Console.ReadLine());
                List<Vehicle> foundVehicle = icarLeaseRepository.findCarById(vehicleId);
                //check whether vehicle exists and whether that vehicle is avialable for lease
                if (foundVehicle != null && foundVehicle[0].Status == "Available")
                {
                    Console.WriteLine("Enter customer ID:");
                    int customerId = int.Parse(Console.ReadLine());
                    Customer foundCustomer = icarLeaseRepository.findCustomerById(customerId);
                    //checks if customer exists
                    if (foundCustomer != null)
                    {

                        Console.WriteLine("Enter start date (yyyy-MM-dd):");
                        DateTime startDate = DateTime.Parse(Console.ReadLine());

                        Console.WriteLine("Enter end date (yyyy-MM-dd):");
                        DateTime endDate = DateTime.Parse(Console.ReadLine());

                        Console.WriteLine("Enter lease type (Monthly or Daily):");
                        string leaseType = Console.ReadLine();

                        Lease newLease = new Lease()
                        {
                            VehicleId = vehicleId,
                            CustomerId = customerId,
                            StartDate = startDate,
                            EndDate = endDate,
                            LeaseType = leaseType
                        };

                        int addLeaseStatus = icarLeaseRepository.createLease(newLease);

                        if (addLeaseStatus > 0)
                        {
                            foundVehicle[0].Status = "Not available";
                            Console.WriteLine("Lease added successfully.");
                        }
                        else
                        {
                            Console.WriteLine("Lease not added.");
                        }
                    }
                    else
                    {
                        throw new CustomerNotFoundException($"Customer with ID {customerId} not found.");
                    }
                }
                //handles condition when vehicle is not found
                else if (foundVehicle == null || foundVehicle.Count == 0)
                {
                    throw new CarNotFoundException($"Vehicle with ID {vehicleId} not found.");
                }
                //handles condition when vehicle is found but it not available for lease
                else
                {
                    Console.WriteLine("Selected vehicle is not available for lease.");
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Database error: {ex.Message}");
            }
            catch (CarNotFoundException ex)
            {
                Console.WriteLine($"CarNotFoundException: {ex.Message}");
            }
            catch (CustomerNotFoundException ex)
            {
                Console.WriteLine($"CustomerNotFoundException: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
            }
        }


        #endregion

        //-------------------------------------------------------------------Car management---------------------------------------------------

        #region Add vehicle
        public void AddVehicle()
        {
            try
            {
                Console.WriteLine("Enter make of the vehicle");
                string make = Console.ReadLine();
                Console.WriteLine("Enter modelof the vehicle");
                string model = Console.ReadLine();
                Console.WriteLine("Enter year of making of the vehicle");
                string year = Console.ReadLine();
                Console.WriteLine("Enter daily rate of the vehicle");
                double dailyRate = Double.Parse(Console.ReadLine());
                Console.WriteLine("Enter status of the vehicle");
                string status = Console.ReadLine();
                Console.WriteLine("Enter engine capacity of the vehicle");
                int e_capacity = int.Parse(Console.ReadLine());
                Console.WriteLine("Enter passenger capacity of the vehicle");
                int p_capacity = int.Parse(Console.ReadLine());
                Vehicle addVehicle = new Vehicle() { Make = make, Model = model, Year = year, DailyRate = dailyRate, Status = status, PassengerCapacity = p_capacity, EngineCapacity = e_capacity };
                int addVehicleStatus = icarLeaseRepository.AddVehicle(addVehicle);
                if (addVehicleStatus > 0)
                {
                    Console.WriteLine("Vehicle Added SucessFully");
                }
                else
                {
                    Console.WriteLine("Vehicle Addition Failed");
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Database error: {ex.Message}");

            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");

            }
        }
        #endregion

        #region Find Vehicle by Id
        public void findCarById()
        {
            try
            {
                Console.WriteLine("Enter vehicle id");
                int findCarId = int.Parse(Console.ReadLine());
                List<Vehicle> resultList = icarLeaseRepository.findCarById(findCarId);
                foreach (Vehicle vehicle in resultList)
                {
                    Console.WriteLine(vehicle);
                }
            }

            catch (CarNotFoundException ex)
            {
                Console.WriteLine($"CarNotFoundException:{ex.Message}");
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Database error: {ex.Message}");

            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");

            }



        }
        #endregion

        #region Get all vehicles
        public void GetAllVehicle()
        {
            try
            {
                List<Vehicle> allProducts = icarLeaseRepository.GetAllVehicle();
                foreach (var item in allProducts)
                {
                    Console.WriteLine(item);
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Database error: {ex.Message}");

            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");

            }
        }
        #endregion
        #region List Available vehicles
        public void listAvailableCars()
        {
            try
            {
                List<Vehicle> allAvailable = icarLeaseRepository.listAvailableCars();
                foreach (Vehicle vehicle in allAvailable)
                {
                    Console.WriteLine(vehicle);
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Database error: {ex.Message}");

            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");

            }
        }

        #endregion
        #region List rented vehicles
        public void listRentedCars()
        {
            try
            {
                List<Vehicle> rented = icarLeaseRepository.listRentedCars();
                foreach (Vehicle vehicle in rented)
                {
                    Console.WriteLine(vehicle);
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Database error: {ex.Message}");

            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");

            }
        }
        #endregion
        #region Remove Vehicle
        public void RemoveVehicle()
        {
            try
            {
                Console.WriteLine("Enter vehicle id");
                int delId = int.Parse(Console.ReadLine());
                int delVehicleStatus = icarLeaseRepository.RemoveVehicle(delId);
                if (delVehicleStatus != 0)
                {
                    Console.WriteLine("Vehicle has been deleted succesfully");
                }
            }
            catch (CarNotFoundException ex)
            {
                Console.WriteLine($"CarNotFoundException:{ex.Message}");
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Database error: {ex.Message}");

            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");

            }


        }
        #endregion
        //------------------------------------------------------------Customer Management------------------------------------
        #region Find customer by Id
        public void findCustomerById()
        {
            try
            {
                Console.WriteLine("Enter customer id");
                int custId = int.Parse(Console.ReadLine());
                Customer foundCustomer = icarLeaseRepository.findCustomerById(custId);
                Console.WriteLine(foundCustomer);
            }
            catch (CustomerNotFoundException ex)
            {
                Console.WriteLine($"{ex.Message}");
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Database error: {ex.Message}");

            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");

            }
        }
        #endregion

        #region List All Customers
        public void listAllCustomers()
        {
            try
            {
                List<Customer> allCustomers = icarLeaseRepository.listAllCustomers();
                foreach (var customer in allCustomers)
                {
                    Console.WriteLine(customer);
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Database error: {ex.Message}");

            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");

            }
        }
        #endregion

        #region Add customer
        public void AddCustomer()
        {
            try
            {
                Console.WriteLine("Enter first name");
                string fname = Console.ReadLine();
                Console.WriteLine("Enter last name");
                string lname = Console.ReadLine();
                Console.WriteLine("Enter email");
                string email = Console.ReadLine();
                Console.WriteLine("Enter phone number");
                string pno = Console.ReadLine();

                Customer addCust = new Customer() { FirstName = fname, LastName = lname, Email = email, Phone = pno };
                int addCustStatus = icarLeaseRepository.AddCustomer(addCust);
                if (addCustStatus != 0)
                {
                    Console.WriteLine("Customer added sucessfully");
                }
                else
                {
                    Console.WriteLine("Adding customer failed");
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Database error: {ex.Message}");

            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");

            }

        }
        #endregion

        #region Remove Customer
        public void RemoveCustomer()
        {
            try
            {
                Console.WriteLine("Enter customer id");
                int delCustId = int.Parse(Console.ReadLine());
                int delCustStatus = icarLeaseRepository.RemoveCustomer(delCustId);
                if (delCustStatus != 0)
                {
                    Console.WriteLine("Customer has been deleted succesfully");
                }
                else
                {
                    Console.WriteLine("Customer not deleted");
                }
            }
            catch (CarNotFoundException ex)
            {
                Console.WriteLine($"CarNotFoundException: {ex.Message}");
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Database error: {ex.Message}");

            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");

            }

        }
        #endregion
        //--------------------------------------Payment Management------------------------------------

        #region Record Payment
        public void recordPayment()
        {
            try
            {
                Console.WriteLine("Enter the lease id to record the payment");
                int lId = int.Parse(Console.ReadLine());

                Lease l = icarLeaseRepository.returnCarInfo(lId);
                if (l != null)
                {
                    Console.WriteLine("Enter the payment date ");
                    DateTime payDate = DateTime.Parse(Console.ReadLine());
                    Console.WriteLine("Enter payment amount");
                    double amt = double.Parse(Console.ReadLine());

                    Payment payment = new Payment()
                    {
                        LeaseId = lId,
                        PaymentDate = payDate,
                        Amount = amt
                    };
                    int payStatus = icarLeaseRepository.recordPayment(payment);

                    if (payStatus != 0)
                    {
                        Console.WriteLine("Payment recorded successfully");
                    }
                    else
                    {
                        Console.WriteLine("Adding payment failed");
                    }
                }
                else
                {
                    throw new LeaseNotFoundException($"Lease with Id:{lId} not found");
                }


            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Database error: {ex.Message}");

            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");

            }



        }
        #endregion
    }
}
