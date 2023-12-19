using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Car_Rental_System.Model
{
    public class Vehicle
    {       
        public int VehicleId { get; set; }
        public string Make {  get; set; }
        public string Model {  get; set; }
        public string Year {  get; set; }
        public double DailyRate {  get; set; }
        public string Status {  get; set; }
        public int? PassengerCapacity {  get; set; }
        public int? EngineCapacity {  get; set; }

        //public Vehicle() { }

        //public Vehicle(int id,string make,string model,string year,double dailyRate,string status,int passCapacity,int engineCapacity) 
        //{ 
        //    VehicleId = id;
        //    Make = make;
        //    Model = model;
        //    Year = year;
        //    DailyRate = dailyRate;
        //    Status = status;
        //    PassengerCapacity = passCapacity;
        //    EngineCapacity = engineCapacity;

        //}
        public override string ToString()
        {
            //return $"Id:{VehicleId}";
            return $"Id:{VehicleId}\tMake:{Make}\tModel:{Model}\tDailyRate={DailyRate}\tYear:{Year}\tStatus:{Status}\tPassengerCapacity:{PassengerCapacity}\tEngine Capacity:{EngineCapacity}";
      }
    }
}
