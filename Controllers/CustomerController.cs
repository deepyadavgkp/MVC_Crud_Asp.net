using MVC_Demo.Models;
using MVC_Demo.Repo;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_Demo.Controllers
{
    public class CustomerController : Controller
    {
       
        dbcontext db = new dbcontext();
        public ActionResult Index()
        {

            return View();
        }

        public ActionResult GetAllEmpDetails()
        {
            try
            {
               
                ModelState.Clear();
                var pdlist = db.GetAllEmployees();
                if (pdlist.Count == 0)
                {
                    TempData["InfoMessage"] = "Currently data is unavailable";
                }
                return View(pdlist);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View();
            }
        }

        public ActionResult AddEmployee()
        {
            return View();
        }

        [HttpPost]
       
        public ActionResult AddEmployee(CustomerModel obj, HttpPostedFileBase productImage)
        {
            try
            {
                bool isInserted = false;
                if (ModelState.IsValid)
                {
                    if (productImage != null && productImage.ContentLength > 0)
                    {
                        var fileName = Guid.NewGuid().ToString().Replace("-", "") + "_" + Path.GetFileName(productImage.FileName);
                        var path = Path.Combine(Server.MapPath("~/Content/images/"), fileName);

                        productImage.SaveAs(path);
                        obj.Photo = fileName;
                    }
                    else
                    {
                        ModelState.AddModelError("Photo", "Please upload a photo.");
                        return View(obj);
                    }

                    isInserted = db.AddEmployee(obj);
                    if (isInserted)
                    {
                        TempData["Success"] = "Data is inserted successfully";
                    }
                    else
                    {
                        TempData["Error"] = "Data is unable to save";
                    }
                }

                return View(obj);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View(obj);
            }
        }

        public ActionResult EditEmpDetails(int id)
        {
            try
            {
               
                var product = db.GetAllEmployees().Find(obj => obj.ID == id);
                if (product == null)
                {
                    TempData["InfoMessage"] = "Products not available with id " + id.ToString();
                    return RedirectToAction("GetAllEmpDetails");
                }
                return View(product);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View();
            }
        }

        [HttpPost]
        public ActionResult EditEmpDetails( CustomerModel obj)
        {
            try
            {
                bool isUpdate = false;
               
                if (ModelState.IsValid)
                {
                    isUpdate = db.UpdateEmployee(obj);
                    
                    if (isUpdate)
                    {
                        TempData["UpdateMessage"] = "Products details Updated successfuly";
                    }

                    else
                    {
                        TempData["Error"] = "Products is unable to Update";
                    }
                }

                return View();
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View();
            }
        }

        public ActionResult DeleteEmp(int id)
        {
            try
            {
               
               
               
                if (db.DeleteEmployee(id))
                {
                    //TempData["InfoDelete"] = "Products not available with id " + id.ToString();
                    //return RedirectToAction("Index");
                    ViewBag.AlertMsg = "Employee details deleted successfully";
                }
                return RedirectToAction("GetAllEmpDetails");
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return View();
            }
        }

    }
}