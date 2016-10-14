using InventoryV3.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InventoryV3.ViewModels
{
    public class AssignmentViewModel
    {
        public IEnumerable<Employee> Employees { get; set; }
        [Required]
        public int EmployeeID { get; set; }

        public IEnumerable<Item> SerialNumbers { get; set; }
        [Required]
        public int SerialNumber { get; set; }

        //date will be implemented in the controller 
    }
}