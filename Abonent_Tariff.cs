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
    
    public partial class Abonent_Tariff
    {
        public int ID { get; set; }
        public int Abonent { get; set; }
        public int Tariff { get; set; }
    
        public virtual Abonent Abonent1 { get; set; }
        public virtual Tariff Tariff1 { get; set; }
    }
}