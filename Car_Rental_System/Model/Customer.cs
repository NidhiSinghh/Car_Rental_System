using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car_Rental_System.Model
{
    public class Customer
    {

        public int CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        //public Customer() { }

        //public Customer(int id, string firstName, string lastName, string email, string phone)
        //{
        //    CustomerId = id;
        //    FirstName = firstName;
        //    LastName = lastName;
        //    Email = email;

        //    Phone = phone;

        //}
        public override string ToString()
        {
            return $"Id::{CustomerId}\t first Name::{FirstName}\t last Name:{LastName} \t Email:{Email} \t Phone:{Phone}";
        }

    }

}
