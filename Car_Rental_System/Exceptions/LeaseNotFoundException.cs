using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car_Rental_System.Exceptions
{
    public class LeaseNotFoundException : ApplicationException
{
        public LeaseNotFoundException() { }
        public LeaseNotFoundException(string message) : base(message) { }

}
}
