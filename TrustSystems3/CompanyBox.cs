//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TrustSystems3
{
    using System;
    using System.Collections.Generic;
    
    public partial class CompanyBox
    {
        public int Id { get; set; }
        public CompanyBoxType BoxType { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public string Details { get; set; }
        public int CompaniesId { get; set; }
        public string Image { get; set; }
    
        public virtual Companies Companies { get; set; }
    }
}
