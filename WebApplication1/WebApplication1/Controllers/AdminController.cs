﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using PagedList;
using CrystalDecisions.CrystalReports.Engine;


namespace WebApplication1.Controllers
{
    public class AdminController : Controller
    {
        DB17Entities1 db = new DB17Entities1();
        // GET: Admin


        public ActionResult CustomerOrder()
        {
            //ViewBag.ListCustomers = db.Customers.ToList();
            return View();
        }



        public ActionResult CustomerInfo()
        {
            ViewBag.ListCustomers = db.Customers.ToList();
            return View();
        }
        public ActionResult Export()
        {
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/Reports/CustomerReport.rpt")));
            rd.SetDataSource(db.Customers.Select(p => new
            {
                CustomerID = p.CustomerID,
                UserName = p.UserName,
                EmailAddress = p.EmailAddress,
                HomeAdress = p.HomeAdress,
                ContactNumber = p.ContactNumber
            }).ToList());
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            Stream stream = rd.ExportToStream
                (CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            return File(stream, "application/pdf", "ListCustomers.pdf");
        }


        public ActionResult CategoryInfo()
        {
            ViewBag.ListCategory = db.Categories.ToList();
            return View();
        }

        public ActionResult Export4()
        {

            ReportDocument rd3 = new ReportDocument();
            rd3.Load(Path.Combine(Server.MapPath("~/Reports/CategoryReport.rpt")));
            //rd1.SetDataSource(db.tbl_order.Select(p => new
            rd3.SetDataSource(db.Categories.Select(p => new
            {
                CategoryID = p.CategoryID,
                CategoryName = p.CategoryName



            }).ToList());
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            Stream stream = rd3.ExportToStream
                (CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            return File(stream, "application/pdf", "ListCategory.pdf");





        }


        public ActionResult OrderInfo()
        {
            ViewBag.ListOrder = db.CustomerOrder().ToList();
            return View();
        }

        public ActionResult Export1()
        {

            ReportDocument rd1 = new ReportDocument();
            rd1.Load(Path.Combine(Server.MapPath("~/Reports/CustomerReport1.rpt")));

            rd1.SetDataSource(db.CustomerOrder().Select(p => new
            {
                OrderID = p.o_id,
                CustomerName = p.UserName,
                product = p.ProductName,
                Quantity = p.o_qty,
                OrderDate = p.orderdate


            }).ToList());
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            Stream stream = rd1.ExportToStream
                (CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            return File(stream, "application/pdf", "ListOrders.pdf");





        }



        public ActionResult DailySale()
        {
            ViewBag.ListReport = db.DailyReport(System.DateTime.Now).ToList();
            return View();
        }

        public ActionResult Export5()
        {

            ReportDocument rd1 = new ReportDocument();
            rd1.Load(Path.Combine(Server.MapPath("~/Reports/DailySaleReport.rpt")));

            rd1.SetDataSource(db.DailyReport(System.DateTime.Now).Select(p => new
            {
                OrderID = p.o_id,
                CustomerName = p.UserName,
                product = p.ProductName,
                Quantity = p.o_qty,
                OrderDate = p.orderdate


            }).ToList());
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            Stream stream = rd1.ExportToStream
                (CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            return File(stream, "application/pdf", "ListReport.pdf");





        }









        public ActionResult ProductInfo()
        {
            ViewBag.ListProduct = db.ProductReport().ToList();
            return View();
        }

        public ActionResult Export2()
        {

            ReportDocument rd2 = new ReportDocument();
            rd2.Load(Path.Combine(Server.MapPath("~/Reports/PRODUCTS.rpt")));
            //rd1.SetDataSource(db.tbl_order.Select(p => new
			/*
            rd2.SetDataSource(db.ProductReport().Select(p => new
            {
                ProductID = p.ProductID,
                ProductName = p.ProductName,
                ProductDescription = p.ProductDescription,
                CategoryName = p.CategoryName,
                ProductPrice = p.ProductPrice



            }).ToList());
			*/
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            Stream stream = rd2.ExportToStream
                (CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            return File(stream, "application/pdf", "ListProduct.pdf");





        }

        


        [HttpGet]
        public ActionResult login()
        {
            return View();
        }


        [HttpPost]
        public ActionResult login(Admin avm)
        {
            Admin ad = db.Admins.Where(x => x.UserName == avm.UserName && x.Password == avm.Password).SingleOrDefault();
            if (ad != null)
            {

                Session["AdminID"] = ad.AdminID.ToString();
                return RedirectToAction("Create");

            }
            else
            {
                ViewBag.error = "Invalid username or password";

            }

            return View();
        }


        public ActionResult Create()
        {
            if (Session["AdminID"] == null)
            {
                return RedirectToAction("login");
            }
            return View();
        }


        [HttpPost]
        public ActionResult Create(Category cvm, HttpPostedFileBase imgfile)
        {
            string path = uploadimgfile(imgfile);
            if (path.Equals("-1"))
            {
                ViewBag.error = "Image could not be uploaded....";
            }
            else
            {
                Category cat = new Category();
                cat.CategoryName = cvm.CategoryName;
                cat.CategoryImage = path;
                cat.CategoryStatus = 1;
                cat.CategoryFK = Convert.ToInt32(Session["AdminID"].ToString());
                db.Categories.Add(cat);
                db.SaveChanges();
                return RedirectToAction("ViewCategory");
            }

            return View();
        } //end,,,,,,,,,,,,,,,,,,,



        public ActionResult ViewCategory(int? page)
        {
            int pagesize = 9, pageindex = 1;
            pageindex = page.HasValue ? Convert.ToInt32(page) : 1;
            var list = db.Categories.Where(x => x.CategoryStatus == 1).OrderByDescending(x => x.CategoryID).ToList();
            IPagedList<Category> stu = list.ToPagedList(pageindex, pagesize);


            return View(stu);
        }




        public string uploadimgfile(HttpPostedFileBase file)
        {
            Random r = new Random();
            string path = "-1";
            int random = r.Next();
            if (file != null && file.ContentLength > 0)
            {
                string extension = Path.GetExtension(file.FileName);
                if (extension.ToLower().Equals(".jpg") || extension.ToLower().Equals(".jpeg") || extension.ToLower().Equals(".png"))
                {
                    try
                    {

                        path = Path.Combine(Server.MapPath("~/Content/upload"), random + Path.GetFileName(file.FileName));
                        file.SaveAs(path);
                        path = "~/Content/upload/" + random + Path.GetFileName(file.FileName);

                        //    ViewBag.Message = "File uploaded successfully";
                    }
                    catch (Exception ex)
                    {
                        path = "-1";
                    }
                }
                else
                {
                    Response.Write("<script>alert('Only jpg ,jpeg or png formats are acceptable....'); </script>");
                }
            }

            else
            {
                Response.Write("<script>alert('Please select a file'); </script>");
                path = "-1";
            }



            return path;
        }


        [HttpGet]
        public ActionResult AddProduct()
        {
            List<Category> li = db.Categories.ToList();
            ViewBag.categorylist = new SelectList(li, "CategoryID", "CategoryName");

            return View();
        }

        [HttpPost]
        public ActionResult AddProduct(Product pvm, HttpPostedFileBase imgfile)
        {
            List<Product> li = db.Products.ToList();
            ViewBag.categorylist = new SelectList(li, "CategoryID", "CategoryName");


            string path = uploadimgfile(imgfile);
            if (path.Equals("-1"))
            {
                ViewBag.error = "Image could not be uploaded....";
            }
            else
            {
                Product p = new Product();
                p.ProductName = pvm.ProductName;
                p.ProductPrice = pvm.ProductPrice;
                p.ProductImage = path;
                p.ProductStatus = 1;
                p.ProdctFK_category = pvm.ProdctFK_category;
                p.ProductDescription = pvm.ProductDescription;
                p.ProdctFK_admin = Convert.ToInt32(Session["AdminID"].ToString());
                db.Products.Add(p);
                db.SaveChanges();
                Response.Redirect("AddProduct");
                
            }
            List<Category> list = db.Categories.ToList();
            ViewBag.categorylist = new SelectList(list, "CategoryID", "CategoryName");
            return View();
        }


        public ActionResult ShowProduct(int? id, int? page)
        {
            int pagesize = 9, pageindex = 1;
            pageindex = page.HasValue ? Convert.ToInt32(page) : 1;
            var list = db.Products.Where(x => x.ProdctFK_category == id).OrderByDescending(x => x.ProductID).ToList();
            IPagedList<Product> stu = list.ToPagedList(pageindex, pagesize);


            return View(stu);


        }

        public ActionResult AllProducts(int? page)
        {
            int pagesize = 9, pageindex = 1;
            pageindex = page.HasValue ? Convert.ToInt32(page) : 1;
            var list = db.Products.Where(x => x.ProductStatus == 1).OrderByDescending(x => x.ProductID).ToList();
            IPagedList<Product> stu = list.ToPagedList(pageindex, pagesize);


            return View(stu);
        }

        public ActionResult Delete(int? id)
        {
            DB17Entities1 db = new DB17Entities1();
            Product p = db.Products.Where(x => x.ProductID == id).SingleOrDefault();
            db.Products.Remove(p);
            db.SaveChanges();
            return View("Create");
        }
        public ActionResult DeleteCategory(int? id)
        {
            DB17Entities1 db = new DB17Entities1();
            Category p = db.Categories.Where(x => x.CategoryID == id).SingleOrDefault();
            db.Categories.Remove(p);
            db.SaveChanges();
            return View("Create");
        }
        public ActionResult RegisteredCustomers()
        {
            DB17Entities1 db = new DB17Entities1();

            List<Customer> list = db.Customers.ToList();
            List<customers> viewList = new List<customers>();

            foreach (Customer obj in db.Customers)
            {

                customers p = new customers();
                p.CustomerID = obj.CustomerID;
                p.UserName = obj.UserName;
                p.EmailAddress = obj.EmailAddress;
                p.Password = obj.Password;
                p.HomeAdress = obj.HomeAdress;
                p.DateOfBirth = obj.DateOfBirth;
                p.ContactNumber = obj.ContactNumber;
                p.UserImage = obj.UserImage;





                viewList.Add(p);


            }
            List<customers> view = new List<customers>();
            return View(viewList);


        }

        //public ActionResult AllCustomers()
        //{
        //    BakerySystemEntities db = new BakerySystemEntities();

        //    List<Customer> list = db.Customers.ToList();
        //    List<Customers> viewList = new List<Customers>();

        //    foreach (Customer obj in db.Customers)
        //    {

        //        Customers p = new Customers();
        //        p.CustomerID = obj.CustomerID;
        //        p.UserName = obj.UserName;
        //        p.EmailAddress = obj.EmailAddress;
        //        p.Password = obj.Password;
        //        p.HomeAdress = obj.HomeAdress;
        //        p.DateOfBirth = obj.DateOfBirth;
        //        p.ContactNumber = obj.ContactNumber;
        //        p.UserImage = obj.UserImage;





        //        viewList.Add(p);


        //    }
           
        //    return View(viewList);


        //}

        public ActionResult DeleteCustomer(int? id)
        {

            DB17Entities1 db = new DB17Entities1();
                Customer p = db.Customers.Where(x => x.CustomerID == id).SingleOrDefault();
                db.Customers.Remove(p);
                db.SaveChanges();
                return View("RegisteredCustomers");
           

        }


        //public ActionResult DeleteAllCustomer(int? id)
        //{

        //    BakerySystemEntities db = new BakerySystemEntities();
        //    Customer p = db.Customers.Where(x => x.CustomerID == id).SingleOrDefault();
        //    db.Customers.Remove(p);
        //    db.SaveChanges();
        //    return View("RegisteredCustomers");


        //}

        public ActionResult Signout()
        {
            Session.RemoveAll();
            Session.Abandon();

            return RedirectToAction("Index","User");
        }

    }
}