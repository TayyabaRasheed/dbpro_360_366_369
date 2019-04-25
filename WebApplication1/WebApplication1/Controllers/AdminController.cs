using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using PagedList;

namespace WebApplication1.Controllers
{
    public class AdminController : Controller
    {
        CartEntities db = new CartEntities();
        // GET: Admin
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


        
        public ActionResult RegisteredCustomers()
        {
            CartEntities db = new CartEntities();

            List<Customer> list = db.Customers.ToList();
            List<Customers> viewList = new List<Customers>();

            foreach (Customer obj in db.Customers)
            {

                Customers p = new Customers();
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
            List<Customers> view = new List<Customers>();
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

            CartEntities db = new CartEntities();
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