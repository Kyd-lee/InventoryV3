using InventoryV3.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InventoryV3.ViewModels
{
    public class MonitorViewModel
    {
        [Required]
        [StringLength(20)]
        [Display(Name = "Serial Number")]
        public string SerialNumber { get; set; }

        [Required]
        [StringLength(20)]
        [Display(Name = "Model Number")]
        public string ModelNumber { get; set; }

        [Required]
        [StringLength(10)]
        [Display(Name = "Screen Size")]
        public string ScreenSize { get; set; }

        public IEnumerable<Brand> Brands { get; set; }
        [Required]
        [Display(Name = "Brand")]
        public int BrandID { get; set; }

        [Required]
        [Display(Name = "Date")]
        public string ExpieryDate { get; set; }

        public IEnumerable<Supplier> Suppliers { get; set; }
        [Required]
        [Display(Name = "Supplier")]
        public int SupplierID { get; set; }

        public DateTime GetDateTime()
        {
            return DateTime.Parse($"{ExpieryDate}");
        }

    }
}