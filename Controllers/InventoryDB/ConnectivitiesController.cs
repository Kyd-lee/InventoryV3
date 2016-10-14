using InventoryV3.Models;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace InventoryV3.Controllers.InventoryDB
{
    public class ConnectivitiesController : Controller
    {
        private InventoryDBv3Entities _db = new InventoryDBv3Entities();

        // GET: Connectivities
        public ActionResult Index()
        {
            return View(_db.Connectivities.ToList());
        }

        // GET: Connectivities/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Connectivity connectivity = _db.Connectivities.Find(id);
            if (connectivity == null)
            {
                return HttpNotFound();
            }
            return View(connectivity);
        }

        // GET: Connectivities/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Connectivities/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,ConnectivityName")] Connectivity connectivity)
        {
            if (ModelState.IsValid)
            {
                _db.Connectivities.Add(connectivity);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(connectivity);
        }

        // GET: Connectivities/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Connectivity connectivity = _db.Connectivities.Find(id);
            if (connectivity == null)
            {
                return HttpNotFound();
            }
            return View(connectivity);
        }

        // POST: Connectivities/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,ConnectivityName")] Connectivity connectivity)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(connectivity).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(connectivity);
        }

        // GET: Connectivities/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Connectivity connectivity = _db.Connectivities.Find(id);
            if (connectivity == null)
            {
                return HttpNotFound();
            }
            return View(connectivity);
        }

        // POST: Connectivities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Connectivity connectivity = _db.Connectivities.Find(id);
            _db.Connectivities.Remove(connectivity);
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
