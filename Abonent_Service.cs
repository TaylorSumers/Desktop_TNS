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
    
    public partial class Abonent_Service
    {
        public int asID { get; set; }
        public int asAbonent { get; set; }
        public int asService { get; set; }
    
        public virtual Abonent Abonent { get; set; }
        public virtual Service Service { get; set; }
    }
}
