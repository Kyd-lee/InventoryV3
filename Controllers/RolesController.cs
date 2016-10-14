using InventoryV3.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace InventoryV3.Controllers
{
    public class RolesController : Controller
    {

        private readonly ApplicationDbContext _context = new ApplicationDbContext();

        //[HttpPost]
        //[ValidateAntiForgeryToken]

        // GET: Roles
        //public ActionResult Index()
        //{
        //    var roles = _context.Roles.ToList();

        //    return View();
        //}

         [HttpGet]
         
        public ActionResult RolesCreate()
        {
            try
            {
                _context.Roles.Add(new IdentityRole()
                {

                    //Name = collection["RoleName"]

                });
                _context.SaveChanges();
                ViewBag.ResultMessage = "Role create successfuly";
                return RedirectToAction("RolesIndex", "Roles");

            }
            catch
            {

                return View();
            }

            //return View();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult RolesCreate(FormCollection collection)
        {
            try
            {
                _context.Roles.Add(new IdentityRole()
                {

                    Name = collection["RoleName"]

                });
                _context.SaveChanges();
                ViewBag.ResultMessage = "Role create successfuly";
                return RedirectToAction("RolesIndex", "Roles");

            }
            catch
            {

                return View();
            }

        }


        public ActionResult RolesIndex()
        {
            //var roles = _context.Roles.OrderBy(r => r.Name).ToList().Select(rr => new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();
            var roles = _context.Roles.ToList();

            //ViewBag.Roles = roles;
            return View(roles);


        }


        public ActionResult Delete(string roleName)
        {
            var thisRole = _context.Roles.FirstOrDefault(r => r.Name.Equals(roleName, StringComparison.CurrentCultureIgnoreCase));
            _context.Roles.Remove(thisRole);

            _context.SaveChanges();
            return RedirectToAction("RolesIndex");
        }


        public ActionResult Edit(string roleName)
        {
            var thisRole = _context.Roles.FirstOrDefault(r => r.Name.Equals(roleName, StringComparison.CurrentCultureIgnoreCase));

            return View(thisRole);
        }

        //
        // POST: /Roles/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(IdentityRole role)
        {
            try
            {
                _context.Entry(role).State = System.Data.Entity.EntityState.Modified;
                _context.SaveChanges();

                return RedirectToAction("RolesIndex");
            }
            catch
            {
                return View();
            }
        }


        public ActionResult ManageRoles()
        {
            List<string> roles;
            List<string> users;

            using (var context = new ApplicationDbContext())
            {
                var roleStore = new RoleStore<IdentityRole>(context);
                var roleManager = new RoleManager<IdentityRole>(roleStore);

                var userStore = new UserStore<ApplicationUser>(context);
                var userManager = new UserManager<ApplicationUser>(userStore);

                users = (from u in userManager.Users select u.UserName).ToList();
                roles = (from r in roleManager.Roles select r.Name).ToList();
            }

            ViewBag.Roles = new SelectList(roles);
            ViewBag.Users = new SelectList(users);

            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ManageRoles(string roleName, string userName)
        {
            List<string> roles;
            List<string> users;

            using (var context = new ApplicationDbContext())
            {
                var roleStore = new RoleStore<IdentityRole>(context);
                var roleManager = new RoleManager<IdentityRole>(roleStore);

                var userStore = new UserStore<ApplicationUser>(context);
                var userManager = new UserManager<ApplicationUser>(userStore);

                users = (from u in userManager.Users select u.UserName).ToList();

                var user = userManager.FindByName(userName);
                if (user == null)
                    throw new Exception("User not found!");

                var role = roleManager.FindByName(roleName);
                if (role == null)
                    throw new Exception("Role not found!");

                if (userManager.IsInRole(user.Id, role.Name))
                {
                    ViewBag.ResultMessage = "This user already has the role specified !";
                }
                else
                {
                    userManager.AddToRole(user.Id, role.Name);
                    context.SaveChanges();

                    ViewBag.ResultMessage = "User added to the role succesfully !";
                }

                roles = (from r in roleManager.Roles select r.Name).ToList();
            }

            ViewBag.Roles = new SelectList(roles);
            ViewBag.Users = new SelectList(users);
            return View();
        }

        public ActionResult addUser(string RoleName)
        {
            var thisRole = _context.Roles.FirstOrDefault(r => r.Name.Equals(RoleName, StringComparison.CurrentCultureIgnoreCase));
            _context.Roles.Add(thisRole);

            _context.SaveChanges();
            return RedirectToAction("addUser");
        }


        [HttpGet]
        //public ActionResult DrawChart()
        //{
        //    var employees = new List<Employee>();


        //    string connectionString = @"Data Source=ROOT\MSSQLSERVER2012;Initial Catalog=InventoryDBv3;Integrated Security=True;Connect Timeout=15;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        //    //string dbContext = _context.ToString();

        //    using (var con = new SqlConnection(connectionString))
        //    {
        //        con.Open();

        //        using (var command = new SqlCommand("SELECT EmployeeFname, Department FROM Employee", con))

        //        using (var reader = command.ExecuteReader())
        //        {
        //            while (reader.Read())
        //            {
        //                employees.Add(new Employee { EmployeeFname = reader.GetString(0), Department = reader.GetInt32(2) });
        //            }
        //        }
        //    }

        //    return View(employees);
        //}

        public ActionResult UserRoles()
        {
            //to be implemented

            var userId = User.Identity.GetUserId();

            return View();
        }
    }
}