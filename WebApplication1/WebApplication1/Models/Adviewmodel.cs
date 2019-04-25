using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Adviewmodel
    {


        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public string ProductImage { get; set; }
        public string ProductDescription { get; set; }
        public int ProductPrice { get; set; }
       
        public Nullable<int> ProdctFK_admin { get; set; }
        public Nullable<int> ProdctFK_customer { get; set; }
        public Nullable<int> ProdctFK_category { get; set; }
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        //public string UserName { get; set; }
        //public string UserImage { get; set; }
        //public string ContactNumber { get; set; }

    }
    }

