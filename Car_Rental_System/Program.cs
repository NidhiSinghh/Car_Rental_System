
using Car_Rental_System.Service;
using Car_Rental_System.Model;
using Car_Rental_System.Repository;

ICarLeaseService icarLeaseService = new CarLeaseService();


Console.WriteLine("                                                   Welcome to Car Rental System                                                 ");
while (true)
{
    Console.WriteLine("");
    Console.WriteLine("");
    //Console.WriteLine("");
    //Console.WriteLine("");
    Console.WriteLine("");
    Console.WriteLine("-------------------------------------------------------------MENU-----------------------------------------------------------------");
    Console.WriteLine("");
    Console.WriteLine("-------------------------------------------------------------------Car management---------------------------------------------------");
    Console.WriteLine("1. GetAllVehicles");
    Console.WriteLine("2. Add Vehicle");
    Console.WriteLine("3.Get List of available cars");
    Console.WriteLine("4.Get List of rented cars");
    Console.WriteLine("5.Remove car");
    Console.WriteLine("6.Get car by id");
    Console.WriteLine("--------------------------------------------------------------Customer Management-------------------------");
    Console.WriteLine("7.Get a list of all customers");
    Console.WriteLine("8.Add customer");
    Console.WriteLine("9.Get customer by id");
    Console.WriteLine("10.Remove customer");
    Console.WriteLine("");
    Console.WriteLine("--------------------------------------------------------------Lease Management-------------------------");
    Console.WriteLine("11.Create Lease");
    Console.WriteLine("12. List Lease Histrory");
    Console.WriteLine("13. List active lease");
    Console.WriteLine("14. Return Car(lease info) for a lease id");
    Console.WriteLine("--------------------------------------------------------Payment Management-------------------------");
    Console.WriteLine("15.Record payment");
    Console.WriteLine("");
    try
    {
        Console.WriteLine("Enter your choice");
        int choice = int.Parse(Console.ReadLine());

        switch (choice)
        {
            case 1:
                icarLeaseService.GetAllVehicle();
                break;
            case 2:
                icarLeaseService.AddVehicle();
                break;
            case 3:
                icarLeaseService.listAvailableCars();
                break;
            case 4:
                icarLeaseService.listRentedCars();
                break;
            case 5:
                icarLeaseService.RemoveVehicle();
                break;
            case 6:
                icarLeaseService.findCarById();
                break;
            case 7:
                icarLeaseService.listAllCustomers();
                break;
            case 8:
                icarLeaseService.AddCustomer();
                break;
            case 9:
                icarLeaseService.findCustomerById();
                break;

            case 10:
                icarLeaseService.RemoveCustomer();
                break;
            case 11:
                icarLeaseService.createLease();
                break;
            case 12:
                icarLeaseService.listLeaseHistory();
                break;
            case 13:
                icarLeaseService.ListActiveLease();
                break;
            case 14:
                icarLeaseService.returnCarInfo();
                break;
           
            case 15:
                icarLeaseService.recordPayment();
                break;
            default:
                Console.WriteLine("Enter correct choice");
                break;


        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"An unexpected error occurred: {ex.Message}");
    }

}


