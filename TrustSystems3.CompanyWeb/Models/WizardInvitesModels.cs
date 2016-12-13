using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TrustSystems3.CompanyWeb.Models
{
    public class WizardAddInvitesModel
    {
        public string InvitationsData { get; set; }
    }

    public class WizardSendfromInvitesModel
    {
        [Required]
        public string SendFrom { get; set; }
        [Display(Name = "Sender email address")]
        [Required(ErrorMessage = "The email address is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string ReplyTo { get; set; }
    }
}