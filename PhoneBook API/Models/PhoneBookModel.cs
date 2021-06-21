using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhoneBook_API.Models
{
    public class PhoneBookModel
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Mobile { get; set; }
        public string UserEmail { get; set; }

    }
}