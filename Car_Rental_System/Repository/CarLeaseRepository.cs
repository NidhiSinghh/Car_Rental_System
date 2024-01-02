


using Car_Rental_System.Exceptions;
using Car_Rental_System.Utility;
using Car_Rental_System.Model;
using Microsoft.VisualBasic;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Reflection;
using System.Text;

namespace Car_Rental_System.Repository
{
    public class CarLeaseRepository : ICarLeaseRepository
    {


        //Sql Connection and Sql Command
        public string connectionString;
        SqlCommand cmd = null;


        public CarLeaseRepository()
        {
            connectionString = DbUtil.GetConnectionString();
            cmd = new SqlCommand();



        }


        //--------------------------------------------------------------Lease management---------------------------------------------------


        #region Create Lease

        public int createLease(Lease lease1)
        {
            //try
            //{
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    cmd.Parameters.Clear();

                    cmd.CommandText = "insert into LEASE values (@vid, @cid, @sdate, @edate, @ltype)";
                    cmd.Parameters.AddWithValue("vid", lease1.VehicleId);
                    cmd.Parameters.AddWithValue("@cid", lease1.CustomerId);
                    cmd.Parameters.AddWithValue("@sdate", lease1.StartDate);
                    cmd.Parameters.AddWithValue("@edate", lease1.EndDate);
                    cmd.Parameters.AddWithValue("@ltype", lease1.LeaseType);
                    cmd.Connection = sqlConnection;
                    sqlConnection.Open();

                    int createLeaseStatus = cmd.ExecuteNonQuery();
                //return createLeaseStatus;

                // Update the vehicle status to 'Not available'
                cmd.Parameters.Clear();
                cmd.CommandText = "UPDATE VEHICLES SET vehicle_Status = 'Not available' WHERE vehicle_Id = @vehicleId";
                cmd.Parameters.AddWithValue("@vehicleId", lease1.VehicleId);

                cmd.ExecuteNonQuery();
                return createLeaseStatus;

                }
            //}
            //catch (SqlException ex)
            //{
                
            //    Console.WriteLine($"Database error: {ex.Message}");
                
            //    throw;
            //}
            //catch (Exception ex)
            //{
            //    // Handle other unexpected exceptions
            //    Console.WriteLine($"An unexpected error occurred: {ex.Message}");
            //    throw;
            //}
        }
        #endregion

        #region Return Car  parameter : int leaseID return type : Lease info
        public Lease returnCarInfo(int leaseId)
        {

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                cmd.CommandText = "select * from LEASE WHERE lease_ID=@lID";


                cmd.Connection = sqlConnection;
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@lID", leaseId);
                sqlConnection.Open();


                using (SqlDataReader reader = cmd.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        Lease leaseinfo = new Lease()
                        {
                            LeaseId = (int)reader["lease_ID"],
                            VehicleId = (int)reader["vehicle_ID"],
                            CustomerId = (int)reader["customer_ID"],
                            StartDate = (DateTime)reader["start_Date"],
                            EndDate = (DateTime)reader["end_Date"],
                            LeaseType = (string)reader["lease_Type"],

                        };
                        return leaseinfo;
                    }

                    throw new LeaseNotFoundException($"Lease id {leaseId} does not exist");

                }


            }

        }

        #endregion

        #region active lease

        public List<Lease> ListActiveLeases()
        {
            List<Lease> activeLease = new List<Lease>();
            //try
            //{
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                cmd.Parameters.Clear();
                StringBuilder sb = new StringBuilder();
                sb.Append("SELECT * FROM LEASE WHERE ");
                //sb.Append("CAST(GETDATE() AS Date ) BETWEEN start_Date AND end_Date ");
                sb.Append("CAST(GETDATE() AS Date ) BETWEEN start_Date AND end_Date ");
                //sb.Append("")
                //sb.Append("vehicle_Status='available'");
                cmd.CommandText = sb.ToString();
                cmd.Connection = sqlConnection;
                sqlConnection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Lease activeLeaseDetails = new Lease();
                    activeLeaseDetails.LeaseId = (int)reader["lease_ID"];
                    activeLeaseDetails.VehicleId = (int)reader["vehicle_ID"];
                    activeLeaseDetails.CustomerId = (int)reader["customer_ID"];
                    activeLeaseDetails.StartDate = (DateTime)reader["start_Date"];
                    activeLeaseDetails.EndDate = (DateTime)reader["end_Date"];
                    activeLeaseDetails.LeaseType = (string)reader["lease_Type"];

                    activeLease.Add(activeLeaseDetails);
                }

            }
            return activeLease;
        }

        #endregion


        #region Lease History
        public List<Lease> listLeaseHistory()
        {
            List<Lease> histLease = new List<Lease>();



            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                //cmd.CommandText = "SELECT customer_ID,vehicle_ID,start_Date,end_date FROM LEASE" +
                //    " ORDER BY customer_ID, start_Date DESC,end_date";
                cmd.Parameters.Clear();
                cmd.CommandText = "SELECT lease_ID, customer_ID, vehicle_ID, start_Date, end_date, lease_Type " +
                          "FROM LEASE " +
                          "ORDER BY customer_ID, start_Date DESC, end_date";
                cmd.Connection = sqlConnection;
                sqlConnection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Lease leaseDetails = new Lease();
                    leaseDetails.LeaseId = (int)reader["lease_ID"];
                    leaseDetails.VehicleId = (int)reader["vehicle_ID"];
                    leaseDetails.CustomerId = (int)reader["customer_ID"];
                    leaseDetails.StartDate = (DateTime)reader["start_Date"];
                    leaseDetails.EndDate = (DateTime)reader["end_Date"];
                    leaseDetails.LeaseType = (string)reader["lease_Type"];

                    histLease.Add(leaseDetails);
                }

            }
            return histLease;

        }

        #endregion

        #region Remove a Lease Record
        public int RemoveLease(int removeLeaseId)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                cmd.Parameters.Clear();
                StringBuilder sb = new StringBuilder();
                sb.Append("DELETE  from LEASE ");
                sb.Append("WHERE lease_ID=@rlease_Id");

                cmd.CommandText = sb.ToString();
                cmd.Parameters.AddWithValue("@rlease_Id", removeLeaseId);
                cmd.Connection = sqlConnection;

                sqlConnection.Open();
                int delLeaseStatus = cmd.ExecuteNonQuery();

                if (delLeaseStatus == 0)
                {
                    throw new LeaseNotFoundException($"The lease with id {removeLeaseId} does not exist");
                }

                return delLeaseStatus;
            }


        }

        #endregion


        //--------------------------------------------------------------Car management---------------------------------------------------

        #region List available cars

        public List<Vehicle> listAvailableCars()
        {
            List<Vehicle> availableVehicles = new List<Vehicle>();

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    cmd.Parameters.Clear();
                    StringBuilder sb = new StringBuilder();
                    sb.Append("select * from VEHICLES ");
                    sb.Append("WHERE vehicle_Status='available' ");
                    //sb.Append("vehicle_Status='available'");
                    cmd.CommandText = sb.ToString();
                    cmd.Connection = sqlConnection;
                    sqlConnection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Vehicle availVehicle = new Vehicle();
                        availVehicle.VehicleId = (int)reader["vehicle_Id"];
                        availVehicle.Model = (string)reader["model"];
                        availVehicle.Make = (string)reader["make"];
                        availVehicle.EngineCapacity = (int)reader["engine_Capacity"];
                        availVehicle.PassengerCapacity = (int)reader["passenger_Capacity"];
                        availVehicle.DailyRate = (double)reader["daily_Rate"];
                        availVehicle.Status = (string)reader["vehicle_Status"];
                        availableVehicles.Add(availVehicle);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
            return availableVehicles;

        }



        #endregion


        #region Rented Vehciles
        public List<Vehicle> listRentedCars()
        {
            List<Vehicle> rentedVehicles = new List<Vehicle>();

            try
            {

                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    cmd.Parameters.Clear();
                    StringBuilder sb = new StringBuilder();
                    sb.Append("select * from VEHICLES ");
                    sb.Append("WHERE vehicle_Status='Not available' ");
                    //sb.Append("vehicle_Status='available'");
                    cmd.CommandText = sb.ToString();
                    cmd.Connection = sqlConnection;
                    sqlConnection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Vehicle rentVehicle = new Vehicle();
                        rentVehicle.VehicleId = (int)reader["vehicle_Id"];
                        rentVehicle.Model = (string)reader["model"];
                        rentVehicle.Make = (string)reader["make"];
                        rentVehicle.EngineCapacity = (int)reader["engine_Capacity"];
                        rentVehicle.PassengerCapacity = (int)reader["passenger_Capacity"];
                        rentVehicle.DailyRate = (double)reader["daily_Rate"];
                        rentVehicle.Status = (string)reader["vehicle_Status"];




                        rentedVehicles.Add(rentVehicle);
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
            return rentedVehicles;
        }

        #endregion

        #region Get All Vehicles
        public List<Vehicle> GetAllVehicle()
        {
            List<Vehicle> vehicles = new List<Vehicle>();

            try
            {

                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    cmd.Parameters.Clear();
                    cmd.CommandText = "select * from VEHICLES";
                    cmd.Connection = sqlConnection;
                    sqlConnection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Vehicle vehicle = new Vehicle();
                        vehicle.VehicleId = (int)reader["vehicle_Id"];
                        vehicle.Model = (string)reader["model"];
                        vehicle.Make = (string)reader["make"];
                        vehicle.EngineCapacity = (int)reader["engine_Capacity"];
                        vehicle.PassengerCapacity = (int)reader["passenger_Capacity"];
                        vehicle.DailyRate = (double)reader["daily_Rate"];
                        vehicle.Status = (string)reader["vehicle_Status"];




                        vehicles.Add(vehicle);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return vehicles;

        }
        #endregion


        #region Add vehicle

        public int AddVehicle(Vehicle vehicle1)
        {

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                cmd.Parameters.Clear();
                cmd.CommandText = "SELECT COUNT(*) FROM VEHICLES WHERE make = @make AND model = @model";
                cmd.Parameters.AddWithValue("@make", vehicle1.Make);
                cmd.Parameters.AddWithValue("@model", vehicle1.Model);
                cmd.Connection = sqlConnection;
                sqlConnection.Open();

                int existingVehicle = (int)cmd.ExecuteScalar();
                if (existingVehicle > 0)
                {
                    throw new DuplicateVehicleException("Vehicle already exists.");
                }
                else
                {
                    cmd.Parameters.Clear();
                    cmd.CommandText = "Insert into VEHICLES values ( @make,@model,@manufacture_Year,@daily_Rate,@vehicle_Status,@passenger_Capacity,@engine_Capacity)";
                    cmd.Parameters.AddWithValue("@make", vehicle1.Make);
                    cmd.Parameters.AddWithValue("@model", vehicle1.Model);
                    cmd.Parameters.AddWithValue("@manufacture_Year", vehicle1.Year);
                    cmd.Parameters.AddWithValue("@daily_Rate", vehicle1.DailyRate);
                    cmd.Parameters.AddWithValue("@vehicle_Status", vehicle1.Status);
                    cmd.Parameters.AddWithValue("@passenger_Capacity", vehicle1.PassengerCapacity);
                    cmd.Parameters.AddWithValue("@engine_Capacity", vehicle1.EngineCapacity);
                }
                 int addVehicleStatus = cmd.ExecuteNonQuery();
                
                return addVehicleStatus;

            }



        }
        #endregion

        #region Remove Car
        public int RemoveVehicle(int removeCarId)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                cmd.Parameters.Clear();
                StringBuilder sb = new StringBuilder();
                sb.Append("DELETE  from VEHICLES ");
                sb.Append("WHERE vehicle_Id=@rvehicle_Id");

                cmd.CommandText = sb.ToString();
                cmd.Parameters.AddWithValue("@rvehicle_Id", removeCarId);
                cmd.Connection = sqlConnection;

                sqlConnection.Open();
                int delVehicleStatus = cmd.ExecuteNonQuery();

                if (delVehicleStatus == 0)
                {
                    throw new CarNotFoundException($"The car with id {removeCarId} does not exist");
                }

                return delVehicleStatus;
            }


        }

        #endregion


        #region Find car by ID
        public List<Vehicle> findCarById(int carId)
        {

            List<Vehicle> carAccToId = new List<Vehicle>();

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                cmd.Parameters.Clear();
                StringBuilder sb = new StringBuilder();
                sb.Append("SELECT * from VEHICLES ");
                sb.Append("WHERE vehicle_Id=@vId");

                cmd.CommandText = sb.ToString();
                cmd.Parameters.AddWithValue("@vId", carId);
                cmd.Connection = sqlConnection;

                sqlConnection.Open();


                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Vehicle vehicle = new Vehicle();
                    vehicle.VehicleId = (int)reader["vehicle_Id"];
                    vehicle.Model = (string)reader["model"];
                    vehicle.Make = (string)reader["make"];
                    vehicle.EngineCapacity = (int)reader["engine_Capacity"];
                    vehicle.PassengerCapacity = (int)reader["passenger_Capacity"];
                    vehicle.DailyRate = (double)reader["daily_Rate"];
                    vehicle.Status = (string)reader["vehicle_Status"];

                    carAccToId.Add(vehicle);

                }
                cmd.Parameters.Clear();


                if (carAccToId.Count() > 0)
                {
                    return carAccToId;

                }

                throw new CarNotFoundException($"Car with ID {carId} not found.");
            }

        }
        #endregion



        //------------------------------Customer Management-------------------------

        #region Add customer

        public int AddCustomer(Customer customer)
        {

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {

                cmd.Parameters.Clear();
                cmd.CommandText = "INSERT INTO CUSTOMERS values ( @first_Name,@last_Name,@email,@phone_Number)";
                cmd.Parameters.AddWithValue("@first_Name", customer.FirstName);
                cmd.Parameters.AddWithValue("@last_Name", customer.LastName);
                cmd.Parameters.AddWithValue("@email", customer.Email);
                cmd.Parameters.AddWithValue("@phone_Number", customer.Phone);

                cmd.Connection = sqlConnection;
                sqlConnection.Open();
                int addCustomertatus = cmd.ExecuteNonQuery();
                return addCustomertatus;
            }


        }


        #endregion

        #region Find Customer By id
        public Customer findCustomerById(int custId)
        {

            
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                


                StringBuilder sb = new StringBuilder();
                sb.Append("SELECT* FROM CUSTOMERS ");
                sb.Append("WHERE customer_Id = @customerId");

                cmd.Parameters.Clear();
                cmd.CommandText = sb.ToString();
                cmd.Parameters.AddWithValue("@customerId", custId);
                cmd.Connection = sqlConnection;

                sqlConnection.Open();


                SqlDataReader reader = cmd.ExecuteReader();


                while (reader.Read())
                {

                    Customer customerAccToId = new Customer
                    {

                        CustomerId = (int)reader["customer_Id"],
                        FirstName = (string)reader["first_Name"],
                        LastName = (string)reader["last_Name"],
                        Email = (string)reader["email"],
                        Phone = (string)reader["phone_Number"],
                    };
                    return customerAccToId;
                }
                throw new CustomerNotFoundException($"Customer with id {custId} not found");


            }

        }






        #endregion

        #region Remove customer
        //corresponding lease id gets deleted->corr to leaseId,paymentId gets delted
        public int RemoveCustomer(int customerId)
        {

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                cmd.Parameters.Clear();
                StringBuilder sb = new StringBuilder();
                sb.Append("DELETE  from CUSTOMERS ");
                sb.Append("WHERE customer_Id=@rcustomer_Id");
                cmd.Parameters.Clear();
                cmd.CommandText = sb.ToString();
                cmd.Parameters.AddWithValue("@rcustomer_Id", customerId);
                cmd.Connection = sqlConnection;

                sqlConnection.Open();
                int delCustomerStatus = cmd.ExecuteNonQuery();
                return delCustomerStatus;

            }
            throw new CustomerNotFoundException($"Customer with id {customerId} not found");
        }

        #endregion

        #region List all customers
        public List<Customer> listAllCustomers()
        {
            List<Customer> availableCustomers = new List<Customer>();

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    cmd.Parameters.Clear();
                    cmd.CommandText = "select * from CUSTOMERS";

                    cmd.Connection = sqlConnection;
                    sqlConnection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Customer cust = new Customer();
                        cust.CustomerId = (int)reader["customer_Id"];
                        cust.FirstName = (string)reader["first_Name"];
                        cust.LastName = (string)reader["last_Name"];
                        cust.Email = (string)reader["email"];

                        cust.Phone = (string)reader["phone_Number"];

                        availableCustomers.Add(cust);
                    }

                }


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return availableCustomers;
        }
        #endregion

        //--------------------------------------Payment Management------------------------------------

        #region Record Payment
        public int recordPayment(Payment payment1)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                cmd.Parameters.Clear();
                cmd.CommandText = "INSERT INTO PAYMENTS VALUES(@lease_ID, @payment_Date, @amount)";
                cmd.Parameters.AddWithValue("@lease_ID", payment1.LeaseId);
                cmd.Parameters.AddWithValue("@payment_Date", payment1.PaymentDate);
                cmd.Parameters.AddWithValue("@amount", payment1.Amount);

                cmd.Connection = sqlConnection;
                sqlConnection.Open();
                int payRecordStatus = cmd.ExecuteNonQuery();
                return payRecordStatus;

            }

        }

        #endregion

        #region Get all payments

        public List<Payment> GetAllPayments()
        {
            List<Payment> payments = new List<Payment>();

            try
            {

                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    cmd.Parameters.Clear();
                    cmd.CommandText = "select * from PAYMENTS";
                    cmd.Connection = sqlConnection;
                    sqlConnection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Payment payment1 = new Payment();
                        payment1.PaymentId = (int)reader["payment_Id"];
                        payment1.LeaseId = (int)reader["lease_ID"];
                        payment1.PaymentDate = (DateTime)reader["payment_Date"];
                        payment1.Amount = (double)reader["amount"];
                        payments.Add(payment1);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return payments;

        }

        #endregion

    }
};




