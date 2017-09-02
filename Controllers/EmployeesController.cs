using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using _397_FinalProject.Models;

namespace _397_FinalProject.Controllers
{
    public class EmployeesController : Controller
    {
        private EmployeeDBContext db = new EmployeeDBContext();

        // GET: Employees
        public ActionResult Index(string field, string searchValue, string sortBy)
        {
            var items = new List<string>();
            items.Add("Last Name");
            items.Add("First Name");
            items.Add("Department");
            items.Add("Location");
            ViewBag.field = new SelectList(items);
            var employees = from s in db.Employees select s;
            if (!String.IsNullOrEmpty(searchValue))
            {
                if (field.Equals("Last Name")) {
                    employees = employees.Where(s => s.LastName.Contains(searchValue));
                }
                else if (field.Equals("First Name"))
                {
                    employees = employees.Where(s => s.FirstName.Contains(searchValue));
                }
                else if (field.Equals("Department"))
                {
                    employees = employees.Where(s => s.Department.Contains(searchValue));
                }
                else if (field.Equals("Location"))
                {
                    employees = employees.Where(s => s.Location.Contains(searchValue));
                }
            }

            if (!String.IsNullOrEmpty(sortBy))
            {
                if (sortBy.Equals("LastName"))
                {
                    employees = from e in db.Employees orderby e.LastName select e;
                }
                else if (sortBy.Equals("FirstName")){
                    employees = from e in db.Employees orderby e.FirstName select e;
                }
                else if (sortBy.Equals("Department"))
                {
                    employees = from e in db.Employees orderby e.Department select e;
                }
                else if (sortBy.Equals("Performance"))
                {
                    employees = from e in db.Employees orderby e.Performance select e;
                }
            }
            return View(employees);
        }

        public ActionResult SearchEmployee(string names)
        {
            var items = new List<string>();
            var name = from e in db.Employees select (e.LastName + "," + e.FirstName);
            items.AddRange(name);
            ViewBag.names = new SelectList(items);

            string lName = "";
            string fName = "";
            if (!String.IsNullOrEmpty(names)) {
                lName = names.Substring(0, names.IndexOf(","));
                fName = names.Substring(names.IndexOf(",") + 1);
                var qry = from e in db.Employees where e.FirstName == fName && e.LastName == lName select e.EmployeeId;
                List<int> ids = new List<int>();
                ids.AddRange(qry);
                return RedirectToAction("Edit/" + ids.ElementAt(0));
            }

            return View();
        }

        // GET: Employees/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // GET: Employees/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EmployeeId,LastName,FirstName,Salary,Gender,Department,Location,Performance")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Employees.Add(employee);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(employee);
        }

        // GET: Employees/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EmployeeId,LastName,FirstName,Salary,Gender,Department,Location,Performance")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(employee);
        }

        // GET: Employees/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Employee employee = db.Employees.Find(id);
            db.Employees.Remove(employee);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
