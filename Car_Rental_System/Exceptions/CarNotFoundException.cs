using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car_Rental_System.Exceptions
{
    public class CarNotFoundException: ApplicationException
    {
        public CarNotFoundException() { }
        public CarNotFoundException(string message) : base(message)
        {
            //Console.WriteLine("exception found");

        }
    }
}
