//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace InventoryV3.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Payment
    {
        public int ID { get; set; }
        public string PaymentNumber { get; set; }
        public string PaymentDetails { get; set; }
        public System.DateTime PaymentDate { get; set; }
        public Nullable<int> Invoice { get; set; }
        public Nullable<int> Supplier { get; set; }
    
        public virtual Invoice Invoice1 { get; set; }
        public virtual Supplier Supplier1 { get; set; }
    }
}
