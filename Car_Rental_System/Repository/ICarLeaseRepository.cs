using Car_Rental_System.Model;


namespace Car_Rental_System.Repository
{
    internal interface ICarLeaseRepository
    {

        //--------------------------------------------------------------Car management---------------------------------------------------
        List<Vehicle> GetAllVehicle();
        List<Vehicle> listAvailableCars();
        List<Vehicle> listRentedCars();
        int AddVehicle(Vehicle vehicle1);
        int RemoveVehicle(int removeCarId);
        List<Vehicle> findCarById(int carId);


        //------------------------------Customer Management-------------------------
        int AddCustomer(Customer customer1);
        Customer findCustomerById(int custId);
        int RemoveCustomer(int customerId);

        List<Customer> listAllCustomers();

        //-----------------------------Lease Management---------------------------
        int createLease(Lease lease1);
        List<Lease> listLeaseHistory();

        List<Lease> ListActiveLeases();


        Lease returnCarInfo(int leaseId);
        int RemoveLease(int removeLeaseId);




        //-------------------------------Payment Handling-------------------------
        int recordPayment(Payment payment1);
       List<Payment> GetAllPayments();






    }
}
