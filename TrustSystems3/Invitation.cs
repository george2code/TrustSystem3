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
    
    public partial class Invitation
    {
        public int Id { get; set; }
        public int CompaniesId { get; set; }
        public string Email { get; set; }
        public string SenderName { get; set; }
        public string FromName { get; set; }
        public string FromEmail { get; set; }
        public InivationStatusType Status { get; set; }
        public System.DateTime CreatedAt { get; set; }
        public Nullable<System.DateTime> SentAt { get; set; }
        public string ReferenceId { get; set; }
        public Nullable<System.DateTime> DeliveryDate { get; set; }
    
        public virtual Companies Companies { get; set; }
    }
}
