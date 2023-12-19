using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car_Rental_System.Model
{
    public class Payment
    {

        public int PaymentId { get; set; }
        public int LeaseId {  get; set; }
        public DateTime PaymentDate {  get; set; }
        public double Amount {  get; set; }

        //public Payment() { }
        //public Payment(int payId,int leaseId,DateTime payDate,double amount)
        //{
        //    PaymentId = payId;
        //    LeaseId = leaseId;
        //    PaymentDate = payDate;
        //    Amount = amount;
        //}

        public override string ToString()
        {
            return $"Id::{PaymentId}\t Lease Id::{LeaseId}\t Payment date:{PaymentDate.ToString("dd/MM/yyyy")} \tAmount:{Amount}";
        }
    }
}
