using InventoryV3.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InventoryV3.ViewModels
{
    public class SystemUnitViewModel
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
        [StringLength(12)]
        [Display(Name = "Processor")]
        public string Processor { get; set; }

        [Required]
        [StringLength(12)]
        [Display(Name = "HDD Size")]
        public string HDD { get; set; }

        [Required]
        [StringLength(12)]
        [Display(Name = "Memory Size")]
        public string Memory { get; set; }

        [Required]
        [StringLength(20)]
        [Display(Name = "Operating System")]
        public string OperatingSystem { get; set; }

        [Required]
        [Display(Name = "Warranty Expire Date")]
        public string ExpireDate { get; set; }

        public IEnumerable<Brand> Brands { get; set; }
        [Required]
        [Display(Name = "Brand")]
        public int BrandID { get; set; }
        public IEnumerable<Supplier> Suppliers { get; set; }
        [Required]
        [Display(Name = "Supplier")]
        public int SupplierID { get; set; }

        public IEnumerable<Status> Statuses { get; set; }
        [Required]
        [Display(Name = "Status")]
        public int StatusID { get; set; }

        public DateTime GetDateTime()
        {
            return DateTime.Parse($"{ExpireDate}");
        }
    }
}