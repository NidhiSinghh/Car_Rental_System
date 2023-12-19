using Car_Rental_System.Exceptions;
using Car_Rental_System.Repository;

namespace Car_Rental_System.Tests
{
    public class Tests
    {
        private const string connectionString = "Server=DESKTOP-SRQUN8T;Database=CAR_RENTAL_SYSTEM;Trusted_Connection=True";

        [Test]
        public void TestToGetAllVehicles()
        {
            //Arrange or Assign
            CarLeaseRepository carleaseRepository = new CarLeaseRepository();
            carleaseRepository.connectionString = connectionString;

            //Act
            var allVehicles = carleaseRepository.GetAllVehicle();

            //Assert
            //Assert.IsNotNull(allVehicles);

            Assert.GreaterOrEqual(allVehicles.Count, 0);
        }

        [Test]
        public void TestToAddVehicle()
        {
            //assign
            CarLeaseRepository carleaseRepository = new CarLeaseRepository();
            carleaseRepository.connectionString = connectionString;
            //act
            int addVehicleStatus = carleaseRepository.AddVehicle(new Car_Rental_System.Model.Vehicle
            {
                Make = "Lexus",
                Model = "RX",
                Year = "2012",
                DailyRate = 400.0,
                EngineCapacity = 2000,
                PassengerCapacity = 3,
                Status = "Available"
            });
            //assert
            Assert.That(addVehicleStatus, Is.EqualTo(1));

        }

        [Test]
        public void TestToCreateLease()
        {
            //assign
            CarLeaseRepository carleaseRepository = new CarLeaseRepository();
            carleaseRepository.connectionString = connectionString;
            //act
            int addLeaseStatus = carleaseRepository.createLease(new Car_Rental_System.Model.Lease
            {
                VehicleId = 15,
                CustomerId = 5,
                StartDate = new DateTime(2023, 12, 17),
                EndDate = new DateTime(2024, 04, 17),
                LeaseType = "Daily"
            });
            //assert
            Assert.That(addLeaseStatus, Is.EqualTo(1));

        }



        [Test]
        public void TestToLeaseRetrievalById()
        {
            //assign
            CarLeaseRepository carleaseRepository = new CarLeaseRepository();
            carleaseRepository.connectionString = connectionString;
            //act

            var leaseInfo = carleaseRepository.returnCarInfo(1);
            //    //Assert
            //Assert.IsNotNull(leaseInfo);
            Assert.That(leaseInfo.LeaseId, Is.EqualTo(1));



        }

        [Test]
        public void TestToCarException()
        {
            //assign
            CarLeaseRepository carleaseRepository = new CarLeaseRepository();
            carleaseRepository.connectionString = connectionString;
            //act


            Assert.Throws<CarNotFoundException>(() => carleaseRepository.findCarById(99));

        }
        [Test]
        public void TestToCustomerException()
        {
            //assign
            CarLeaseRepository carleaseRepository = new CarLeaseRepository();
            carleaseRepository.connectionString = connectionString;
            //act


            Assert.Throws<CustomerNotFoundException>(() => carleaseRepository.findCustomerById(99));

        }

        [Test]
        public void TestToLeaseException()
        {
            //assign
            CarLeaseRepository carleaseRepository = new CarLeaseRepository();
            carleaseRepository.connectionString = connectionString;
            //act


            Assert.Throws<LeaseNotFoundException>(() => carleaseRepository.returnCarInfo(99));

        }






    }
}
