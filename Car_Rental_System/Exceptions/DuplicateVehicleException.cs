using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car_Rental_System.Exceptions
{
    internal class DuplicateVehicleException:ApplicationException
    {
        public DuplicateVehicleException()
        {

        }
        public DuplicateVehicleException(string message):base(message) { }
    }
}
