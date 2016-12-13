using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;


namespace TrustSystems3.CompanyWeb.Models
{
    public class EmailTemplateViewModel
    {
        [Required]
        [Range(7, 90, ErrorMessage = "Delay должен быть от 7 до 90")]
        public int Delay { get; set; }
        [Required]
        [StringLength(250, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 4)]
        public String Subject { get; set; }
        [Required]
        [Display(Name = "Template")]
        [UIHint("tinymce_jquery_full"), AllowHtml]
        public String Body { get; set; }
    }
}