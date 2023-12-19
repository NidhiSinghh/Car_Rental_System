using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Car_Rental_System.Model
{
    public class Lease
    {
        public int LeaseId { get; set; }
        public int VehicleId {  get; set; }
        public int CustomerId {  get; set; }
        public DateTime StartDate {  get; set; }
        public DateTime EndDate { get; set; }
        public string LeaseType { get; set; }


        //public Lease()
        //{

        //}
     
        //public Lease(int leaseId,int vehicleId,int customerId,DateTime startDate,DateTime endDate,string leseType)
        //{
        //    LeaseId= leaseId;
        //    VehicleId= vehicleId;
        //    CustomerId= customerId;
        //    StartDate= startDate;
        //    EndDate= endDate;
        //    LeaseType = leseType;
        //}
        public override string ToString()
        {
            return $"Id::{LeaseId}\t Vehicle Id::{VehicleId}\t CustomerId:{CustomerId} \t Start date:{StartDate.ToString("dd/MM/yyyy")} \t End date:{EndDate.ToString("dd/MM/yyyy")} \t Lease Type:{LeaseType}";
        }

    }
}
