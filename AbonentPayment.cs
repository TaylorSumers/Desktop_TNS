//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Desktop_TNS
{
    using System;
    using System.Collections.Generic;
    
    public partial class AbonentPayment
    {
        public int PaymentID { get; set; }
        public int Abonent { get; set; }
        public System.DateTime PaymentDate { get; set; }
        public decimal PaymentAmount { get; set; }
        public decimal Balance { get; set; }
        public System.DateTime BalanceDate { get; set; }
        public Nullable<decimal> Debt { get; set; }
    
        public virtual Abonent Abonent1 { get; set; }
    }
}