using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using CrMVCApp.Models;
using System.Web.Mvc;

using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class MyCustomerController : Controller
    {
        // GET: MyCustomer
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ShowCustomerList()
        {
            CrMVCApp.Models.CartEntities db = new CrMVCApp.Models.CartEntities();
            //CrMVCApp.Models.Customer c;
            var c = (from b in db.Customers select b).ToList();

            Customer rpt = new Customer();
            rpt.Load();
            rpt.SetDataSource(c);
            Stream s = rpt.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            return File(s, "application/pdf");
        }

    }
}