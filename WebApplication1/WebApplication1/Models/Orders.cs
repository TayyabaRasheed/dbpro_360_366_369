using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Orders
    {
        public int OrderID { get; set; }
        public int fk_Product_ID { get; set; }
        public int fk_invoice_ID { get; set; }
        public Nullable<System.DateTime> OrderDate { get; set; }
        public int OrderQuantity { get; set; }
        public int UnitPrice { get; set; }
        public float Bill { get; set; }
       
    }
}