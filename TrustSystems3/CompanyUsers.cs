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
    
    public partial class CompanyUsers
    {
        public int CompanyId { get; set; }
        public string UserId { get; set; }
        public UserStateType UserState { get; set; }
        public Byte IsOwner { get; set; }
    
        public virtual Companies Companies { get; set; }
    }
}