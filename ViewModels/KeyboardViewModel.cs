using InventoryV3.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InventoryV3.ViewModels
{
    public class KeyboardViewModel
    {
        [Required]
        [StringLength(12)]
        [Display(Name = "Serial Number")]
        public string SerialNumber { get; set; }

        [Required]
        [StringLength(12)]
        [Display(Name = "Model Number")]
        public string ModelNumber { get; set; }

        [Required]
        public string ExpireDate { get; set; }

        public IEnumerable<Connectivity> Connectivities { get; set; }
        [Required]
        [Display(Name = "Connectivity")]
        public int ConnectivityID { get; set; }

        public IEnumerable<Brand> Brands { get; set; }
        [Required]
        [Display(Name = "Brand")]
        public int BrandID { get; set; }

        public IEnumerable<Status> Statuses { get; set; }
        [Required]
        [Display(Name = "Status")]
        public int StatusID { get; set; }

        public IEnumerable<Supplier> Suppliers { get; set; }
        [Required]
        [Display(Name = "Supplier")]
        public int SupplierID { get; set; }

        public DateTime GetDateTime()
        {
            return DateTime.Parse($"{ExpireDate}");
        }
    }
}