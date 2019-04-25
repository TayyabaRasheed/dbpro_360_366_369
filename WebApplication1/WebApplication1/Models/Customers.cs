using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Customers
    {
        public int CustomerID { get; set; }
        public string UserName { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public string HomeAdress { get; set; }
        public Nullable<System.DateTime> DateOfBirth { get; set; }
        public string ContactNumber { get; set; }
        public string UserImage { get; set; }

    }
}