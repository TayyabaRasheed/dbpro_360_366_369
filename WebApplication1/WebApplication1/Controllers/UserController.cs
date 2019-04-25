using PagedList;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class UserController : Controller
    {

        CartEntities db = new CartEntities();
        // GET: User
        public ActionResult Index(int ?page)
        {
            int pagesize = 9, pageindex = 1;
            pageindex = page.HasValue ? Convert.ToInt32(page) : 1;
            var list = db.Categories.Where(x => x.CategoryStatus == 1).OrderByDescending(x => x.CategoryID).ToList();
            IPagedList<Category> stu = list.ToPagedList(pageindex, pagesize);


            return View(stu);
        }
        public ActionResult SignUp()
        {

            return View();
        }

        [HttpPost]
        public ActionResult SignUp(Customer uvm, HttpPostedFileBase imgfile)
        {
            try
            {
                string path = uploadimgfile(imgfile);
                if (path.Equals("-1"))
                {
                    ViewBag.error = "Image could not be uploaded....";
                }
                else
                {
                    Customer u = new Customer();
                    u.UserName = uvm.UserName;
                    u.EmailAddress = uvm.EmailAddress;
                    u.Password = uvm.Password;
                    u.HomeAdress = uvm.HomeAdress;
                    u.UserImage = path;
                    u.ContactNumber = uvm.ContactNumber;
                    u.Added_On = System.DateTime.Now;
                    db.Customers.Add(u);
                    db.SaveChanges();
                    return RedirectToAction("login");

                }
            }
            catch
            {

                return View();
            }
            return View();
        } //method......................... end.....................
        [HttpGet]
        public ActionResult login()
        {
            return View();
        }


        [HttpPost]
        public ActionResult login(Customer avm)
        {
            Customer ad = db.Customers.Where(x => x.EmailAddress == avm.EmailAddress && x.Password == avm.Password).SingleOrDefault();
            if (ad != null)
            {

                Session["CustomerID"] = ad.CustomerID.ToString();
                return RedirectToAction("Index");

            }
            else
            {
                ViewBag.error = "Invalid username or password";

            }

            return View();
        }


        public ActionResult AllProducts(int? page)
        {
            int pagesize = 9, pageindex = 1;
            pageindex = page.HasValue ? Convert.ToInt32(page) : 1;
            var list = db.Products.Where(x => x.ProductStatus == 1).OrderByDescending(x => x.ProductID).ToList();
            IPagedList<Product> stu = list.ToPagedList(pageindex, pagesize);


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


        public ActionResult ProductDetails(int? id)
        {
            Adviewmodel ad = new Adviewmodel();
            Product p = db.Products.Where(x => x.ProductID == id).SingleOrDefault();
            ad.ProductID = p.ProductID;
            ad.ProductName = p.ProductName;
            ad.ProductImage = p.ProductImage;
            ad.ProductPrice = p.ProductPrice;
            Category cat = db.Categories.Where(x => x.CategoryID == p.ProdctFK_category).SingleOrDefault();
            ad.CategoryName = cat.CategoryName;
            Customer u = db.Customers.Where(x => x.CustomerID == p.ProdctFK_customer).SingleOrDefault();
            //ad.UserName = u.UserName;
            //ad.UserImage = u.UserImage;
            //ad.ContactNumber = u.ContactNumber;

            return View(ad);
        }


        public ActionResult Signout()
        {
            Session.RemoveAll();
            Session.Abandon();

            return RedirectToAction("Index");
        }
    }
}