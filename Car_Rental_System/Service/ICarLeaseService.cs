using Car_Rental_System.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car_Rental_System.Service
{
    internal interface ICarLeaseService
    {
        //--------------------------------------------------------------Car management---------------------------------------------------
        void GetAllVehicle();
        void listAvailableCars();
        void listRentedCars();
        void AddVehicle();
        void RemoveVehicle();
        void findCarById();


        //------------------------------Customer Management-------------------------
        void AddCustomer();
        void findCustomerById();
        void RemoveCustomer();

        void listAllCustomers();

        //------------------------------Lease Management-------------------------
        void createLease();
        void listLeaseHistory();
        void ListActiveLease();
        void returnCarInfo();
        void RemoveLease();
       
        //------------------------------Payment Management-------------------------
        void recordPayment();

        void GetAllPayments();
    }
}
