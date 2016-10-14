using InventoryV3.Models;
using InventoryV3.ViewModels;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace InventoryV3.Controllers.InventoryDB
{

    public class ItemsController : Controller
    {
        private InventoryDBv3Entities _db = new InventoryDBv3Entities();

        // GET: Items
        public ActionResult Index()
        {
            //var items = _db.Items.Include(i => i.Brand1).Include(i => i.Category1).Include(i => i.Connectivity1).Include(i => i.Status1).Include(i => i.Supplier1);
            return View(/*items.ToList()*/);
        }

       protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }

        /*!!!!!!!!!!!!!!!!!!!!!!!MANUAL IMPLEMENTATION BELOW!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!*/


        //=======MONITOR SECTION ===============
        public ActionResult Monitor()
        {

            var items = _db.Items
                .Include(i => i.Brand1)
                .Include(i => i.Connectivity1)
                .Include(i => i.Status1)
                .Include(i => i.Supplier1)
                .Where(i => i.Category1.ID.Equals(1));
            return View(items.ToList());
        }

        public ActionResult CreateMonitor()
        {
            var viewModel = new MonitorViewModel
            {
                Brands = _db.Brands.ToList(),
                Suppliers = _db.Suppliers.ToList()
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateMonitor(MonitorViewModel monitorViewModel)
        {
            if (ModelState.IsValid)
            {
                var postData = new Item
                {
                    SerialNumber = monitorViewModel.SerialNumber,
                    ItemModel = monitorViewModel.ModelNumber,
                    ScreenSize = monitorViewModel.ScreenSize,
                    WarrantyExpire = monitorViewModel.GetDateTime(),
                    Brand = monitorViewModel.BrandID,
                    Supplier = monitorViewModel.SupplierID,
                    Status = 1,
                    Category = 1
                };

                _db.Items.Add(postData);
                _db.SaveChanges();

                return RedirectToAction("Monitor");
            }

            var viewModel = new MonitorViewModel
            {
                Brands = _db.Brands.ToList(),
                Suppliers = _db.Suppliers.ToList()
            };

            return View(viewModel);
        }

        public ActionResult EditMonitor(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = _db.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            ViewBag.Brand = new SelectList(_db.Brands, "ID", "BrandName", item.Brand);
            ViewBag.Status = new SelectList(_db.Status, "ID", "StatusName", item.Status);
            ViewBag.Supplier = new SelectList(_db.Suppliers, "ID", "SupplierName", item.Supplier);
            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditMonitor([Bind(Include = "ID,SerialNumber,ModelNumber,ScreenSize,WarrantyExpire,Brand,Supplier,Status")] Item item)
        {
            //Status should be grayed-out in view or remove it from view and manually assigned here

            if (ModelState.IsValid)
            {
                item.Category = 1;
                _db.Entry(item).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Monitor");
            }
            ViewBag.Brand = new SelectList(_db.Brands, "ID", "BrandName", item.Brand);
            ViewBag.Status = new SelectList(_db.Status, "ID", "StatusName", item.Status);
            ViewBag.Supplier = new SelectList(_db.Suppliers, "ID", "SupplierName", item.Supplier);
            return View(item);
        }

        public ActionResult MonitorDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = _db.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        public ActionResult DeleteMonitor(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = _db.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        [HttpPost, ActionName("DeleteMonitor")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteMonitorConfirmed(int id)
        {
            Item item = _db.Items.Find(id);
            _db.Items.Remove(item);
            _db.SaveChanges();
            return RedirectToAction("Monitor");
        }
        //=======MONITOR SECTION ENDS===============


        //-------SYSTEM UNIT SECTION----------------
        public ActionResult SystemUnit()
        {
            var items = _db.Items
                .Include(i => i.Brand1)
                .Include(i => i.Connectivity1)
                .Include(i => i.Status1)
                .Include(i => i.Supplier1)
                .Where(i => i.Category1.ID.Equals(2));
            return View(items.ToList());
        }

        public ActionResult CreateSystemUnit()
        {
            var viewModel = new SystemUnitViewModel
            {
                Brands = _db.Brands.ToList(),
                Suppliers = _db.Suppliers.ToList(),
                Statuses = _db.Status.ToList()
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateSystemUnit(SystemUnitViewModel sysUnitViewModel)
        {
            if (ModelState.IsValid)
            {
                var postData = new Item
                {
                    SerialNumber = sysUnitViewModel.SerialNumber,
                    ItemModel = sysUnitViewModel.ModelNumber,
                    Processor = sysUnitViewModel.Processor,
                    HDD = sysUnitViewModel.HDD,
                    Memory = sysUnitViewModel.Memory,
                    OperatingSystem = sysUnitViewModel.OperatingSystem,
                    WarrantyExpire = sysUnitViewModel.GetDateTime(),
                    Brand = sysUnitViewModel.BrandID,
                    Supplier = sysUnitViewModel.SupplierID,
                    Status = sysUnitViewModel.StatusID,
                    Category = 2
                };

                _db.Items.Add(postData);
                _db.SaveChanges();

                return RedirectToAction("SystemUnit");
            }

            var viewModel = new SystemUnitViewModel
            {
                Brands = _db.Brands.ToList(),
                Suppliers = _db.Suppliers.ToList(),
                Statuses = _db.Status.ToList()
            };

            return View(viewModel);
        }

        public ActionResult SystemUnitDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = _db.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        public ActionResult EditSystemUnit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = _db.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            ViewBag.Brand = new SelectList(_db.Brands, "ID", "BrandName", item.Brand);
            ViewBag.Status = new SelectList(_db.Status, "ID", "StatusName", item.Status);
            ViewBag.Supplier = new SelectList(_db.Suppliers, "ID", "SupplierName", item.Supplier);
            return View(item);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditSystemUnit([Bind(Include = "ID,SerialNumber,ModelNumber,Processor,HDD,Memory,OperatingSystem,WarrantyExpire,Brand,Supplier,Status")] Item item)
        {
            if (ModelState.IsValid)
            {
                item.Category = 2;
                _db.Entry(item).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("SystemUnit");
            }
            ViewBag.Brand = new SelectList(_db.Brands, "ID", "BrandName", item.Brand);
            ViewBag.Status = new SelectList(_db.Status, "ID", "StatusName", item.Status);
            ViewBag.Supplier = new SelectList(_db.Suppliers, "ID", "SupplierName", item.Supplier);
            return View(item);
        }

        public ActionResult DeleteSystemUnit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = _db.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        [HttpPost, ActionName("DeleteSystemUnit")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteSystemUnitConfirmed(int id)
        {
            Item item = _db.Items.Find(id);
            _db.Items.Remove(item);
            _db.SaveChanges();
            return RedirectToAction("SystemUnit");
        }
        //-------SYSTEM UNIT SECTION ENDS-----------

        //*******MOUSE SECTION**********************
        public ActionResult Mouse()
        {
            var items = _db.Items
                .Include(i => i.Brand1)
                .Include(i => i.Connectivity1)
                .Include(i => i.Status1)
                .Include(i => i.Supplier1)
                .Where(i => i.Category1.ID.Equals(3));
            return View(items.ToList());
        }

        public ActionResult CreateMouse()
        {
            var viewModel = new MouseViewModel
            {
                Connectivities = _db.Connectivities.ToList(),
                Brands = _db.Brands.ToList(),
                Statuses = _db.Status.ToList(),
                Suppliers = _db.Suppliers.ToList()
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateMouse(MouseViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var postData = new Item
                {
                    SerialNumber = viewModel.SerialNumber,
                    ItemModel = viewModel.ModelNumber,
                    Connectivity = viewModel.ConnectivityID,
                    Brand = viewModel.BrandID,
                    Status = viewModel.StatusID,
                    Supplier = viewModel.SupplierID,
                    WarrantyExpire = viewModel.GetDateTime(),
                    Category = 3
                };

                _db.Items.Add(postData);
                _db.SaveChanges();

                return RedirectToAction("Mouse");
            }

            var model = new MouseViewModel
            {
                Connectivities = _db.Connectivities.ToList(),
                Brands = _db.Brands.ToList(),
                Statuses = _db.Status.ToList(),
                Suppliers = _db.Suppliers.ToList()
            };

            return View(model);
        }

        public ActionResult MouseDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = _db.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        public ActionResult EditMouse(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = _db.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            ViewBag.Connectivity = new SelectList(_db.Connectivities, "ID", "ConnectivityName", item.Connectivity);
            ViewBag.Brand = new SelectList(_db.Brands, "ID", "BrandName", item.Brand);
            ViewBag.Status = new SelectList(_db.Status, "ID", "StatusName", item.Status);
            ViewBag.Supplier = new SelectList(_db.Suppliers, "ID", "SupplierName", item.Supplier);
            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditMouse([Bind(Include = "ID,SerialNumber,ModelNumber,Connectivity,WarrantyExpire,Brand,Supplier,Status")] Item item)
        {
            if (ModelState.IsValid)
            {
                item.Category = 3;
                _db.Entry(item).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Mouse");
            }
            ViewBag.Connectivity = new SelectList(_db.Connectivities, "ID", "ConnectivityName", item.Connectivity);
            ViewBag.Brand = new SelectList(_db.Brands, "ID", "BrandName", item.Brand);
            ViewBag.Status = new SelectList(_db.Status, "ID", "StatusName", item.Status);
            ViewBag.Supplier = new SelectList(_db.Suppliers, "ID", "SupplierName", item.Supplier);
            return View(item);
        }

        public ActionResult DeleteMouse(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = _db.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        [HttpPost, ActionName("DeleteMouse")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteMouseConfirmed(int id)
        {
            Item item = _db.Items.Find(id);
            _db.Items.Remove(item);
            _db.SaveChanges();
            return RedirectToAction("Mouse");
        }
        //*******MOUSE SECTION ENDS**********************


        //############KEYBOARD SECTION###################
        public ActionResult Keyboard()
        {
            var items = _db.Items
                .Include(i => i.Brand1)
                .Include(i => i.Connectivity1)
                .Include(i => i.Status1)
                .Include(i => i.Supplier1)
                .Where(i => i.Category1.ID.Equals(4));
            return View(items.ToList());
        }

        public ActionResult CreateKeyboard()
        {
            var viewModel = new KeyboardViewModel
            {
                Connectivities = _db.Connectivities.ToList(),
                Brands = _db.Brands.ToList(),
                Statuses = _db.Status.ToList(),
                Suppliers = _db.Suppliers.ToList()
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateKeyboard(KeyboardViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var postData = new Item
                {
                    SerialNumber = viewModel.SerialNumber,
                    ItemModel = viewModel.ModelNumber,
                    Connectivity = viewModel.ConnectivityID,
                    Brand = viewModel.BrandID,
                    Status = viewModel.StatusID,
                    Supplier = viewModel.SupplierID,
                    WarrantyExpire = viewModel.GetDateTime(),
                    Category = 4
                };

                _db.Items.Add(postData);
                _db.SaveChanges();

                return RedirectToAction("Keyboard");
            }

            var model = new KeyboardViewModel
            {
                Connectivities = _db.Connectivities.ToList(),
                Brands = _db.Brands.ToList(),
                Statuses = _db.Status.ToList(),
                Suppliers = _db.Suppliers.ToList()
            };

            return View(model);
        }

        public ActionResult KeyboardDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = _db.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        public ActionResult EditKeyboard(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = _db.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            ViewBag.Connectivity = new SelectList(_db.Connectivities, "ID", "ConnectivityName", item.Connectivity);
            ViewBag.Brand = new SelectList(_db.Brands, "ID", "BrandName", item.Brand);
            ViewBag.Status = new SelectList(_db.Status, "ID", "StatusName", item.Status);
            ViewBag.Supplier = new SelectList(_db.Suppliers, "ID", "SupplierName", item.Supplier);
            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditKeyboard([Bind(Include = "ID,SerialNumber,ModelNumber,Connectivity,WarrantyExpire,Brand,Supplier,Status")] Item item)
        {
            if (ModelState.IsValid)
            {
                item.Category = 4;
                _db.Entry(item).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Keyboard");
            }
            ViewBag.Connectivity = new SelectList(_db.Connectivities, "ID", "ConnectivityName", item.Connectivity);
            ViewBag.Brand = new SelectList(_db.Brands, "ID", "BrandName", item.Brand);
            ViewBag.Status = new SelectList(_db.Status, "ID", "StatusName", item.Status);
            ViewBag.Supplier = new SelectList(_db.Suppliers, "ID", "SupplierName", item.Supplier);
            return View(item);
        }

        public ActionResult DeleteKeyboard(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = _db.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        [HttpPost, ActionName("DeleteKeyboard")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteKeyboardConfirmed(int id)
        {
            Item item = _db.Items.Find(id);
            _db.Items.Remove(item);
            _db.SaveChanges();
            return RedirectToAction("Keyboard");
        }
    }
}
