
using InventoryV3.Models;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace InventoryV3.Controllers.InventoryDB
{
    public class AssignmentsController : Controller
    {
        private readonly InventoryDBv3Entities _db = new InventoryDBv3Entities();

        // GET: Assignments
        public ActionResult Index()
        {
            var assignments = _db.Assignments.Include(a => a.Employee1).Include(a => a.Item);
            return View(assignments.ToList());
        }

        // GET: Assignments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Assignment assignment = _db.Assignments.Find(id);
            if (assignment == null)
            {
                return HttpNotFound();
            }
            return View(assignment);
        }

        // GET: Assignments/Create
        public ActionResult Create()
        {
            ViewBag.Employee = new SelectList(_db.Employees, "ID", "EmployeeFname");
            ViewBag.SerialNumber = new SelectList(_db.Items, "ID", "SerialNumber");
            return View();
        }

        // POST: Assignments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Employee,SerialNumber,AssignDate")] Assignment assignment)
        {
            if (ModelState.IsValid)
            {
                _db.Assignments.Add(assignment);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Employee = new SelectList(_db.Employees, "ID", "EmployeeFname", assignment.Employee);
            ViewBag.SerialNumber = new SelectList(_db.Items, "ID", "SerialNumber", assignment.SerialNumber);
            return View(assignment);
        }

        // GET: Assignments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Assignment assignment = _db.Assignments.Find(id);
            if (assignment == null)
            {
                return HttpNotFound();
            }
            ViewBag.Employee = new SelectList(_db.Employees, "ID", "EmployeeFname", assignment.Employee);
            ViewBag.SerialNumber = new SelectList(_db.Items, "ID", "SerialNumber", assignment.SerialNumber);
            return View(assignment);
        }

        // POST: Assignments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Employee,SerialNumber,AssignDate")] Assignment assignment)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(assignment).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Employee = new SelectList(_db.Employees, "ID", "EmployeeFname", assignment.Employee);
            ViewBag.SerialNumber = new SelectList(_db.Items, "ID", "SerialNumber", assignment.SerialNumber);
            return View(assignment);
        }

        // GET: Assignments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Assignment assignment = _db.Assignments.Find(id);
            if (assignment == null)
            {
                return HttpNotFound();
            }
            return View(assignment);
        }

        // POST: Assignments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Assignment assignment = _db.Assignments.Find(id);
            _db.Assignments.Remove(assignment);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
