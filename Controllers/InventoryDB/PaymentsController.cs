using InventoryV3.Models;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace InventoryV3.Controllers.InventoryDB
{
    public class PaymentsController : Controller
    {
        private InventoryDBv3Entities _db = new InventoryDBv3Entities();

        // GET: Payments
        public ActionResult Index()
        {
            var payments = _db.Payments.Include(p => p.Invoice1).Include(p => p.Supplier1);
            return View(payments.ToList());
        }

        // GET: Payments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Payment payment = _db.Payments.Find(id);
            if (payment == null)
            {
                return HttpNotFound();
            }
            return View(payment);
        }

        // GET: Payments/Create
        public ActionResult Create()
        {
            ViewBag.Invoice = new SelectList(_db.Invoices, "ID", "InvoiceNumber");
            ViewBag.Supplier = new SelectList(_db.Suppliers, "ID", "SupplierName");
            return View();
        }

        // POST: Payments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,PaymentNumber,PaymentDetails,PaymentDate,Invoice,Supplier")] Payment payment)
        {
            if (ModelState.IsValid)
            {
                _db.Payments.Add(payment);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Invoice = new SelectList(_db.Invoices, "ID", "InvoiceNumber", payment.Invoice);
            ViewBag.Supplier = new SelectList(_db.Suppliers, "ID", "SupplierName", payment.Supplier);
            return View(payment);
        }

        // GET: Payments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Payment payment = _db.Payments.Find(id);
            if (payment == null)
            {
                return HttpNotFound();
            }
            ViewBag.Invoice = new SelectList(_db.Invoices, "ID", "InvoiceNumber", payment.Invoice);
            ViewBag.Supplier = new SelectList(_db.Suppliers, "ID", "SupplierName", payment.Supplier);
            return View(payment);
        }

        // POST: Payments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,PaymentNumber,PaymentDetails,PaymentDate,Invoice,Supplier")] Payment payment)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(payment).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Invoice = new SelectList(_db.Invoices, "ID", "InvoiceNumber", payment.Invoice);
            ViewBag.Supplier = new SelectList(_db.Suppliers, "ID", "SupplierName", payment.Supplier);
            return View(payment);
        }

        // GET: Payments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Payment payment = _db.Payments.Find(id);
            if (payment == null)
            {
                return HttpNotFound();
            }
            return View(payment);
        }

        // POST: Payments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Payment payment = _db.Payments.Find(id);
            _db.Payments.Remove(payment);
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
