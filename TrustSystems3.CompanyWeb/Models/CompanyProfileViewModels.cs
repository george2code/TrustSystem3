using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;
using Localization;

namespace TrustSystems3.CompanyWeb.Models
{
    public class ReviewSettingsViewModel
    {
        public int IsCodeRequired { get; set; }
        public IEnumerable<SelectListItem> Answers { get; set; }
    }


    public class CompanyInformationViewModel
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "CIDisplayName", ResourceType = typeof(Company))] 
        public String Slug { get; set; }
        [Required]
        [Display(Name = "CIWebsite", ResourceType = typeof(Company))] 
        public String Website { get; set; }


        [Required]
        [Display(Name = "CICompanyName", ResourceType = typeof(Company))]
        public String Name { get; set; }
        [Required]
        [EmailAddress]
        [Display(Name = "CIEmail", ResourceType = typeof(Company))]
        public String Email { get; set; }


        [DataType(DataType.PhoneNumber)]
        [Required]
        [StringLength(32)]
        public string PhoneNumber { get; set; }
        [Display(Name = "CIStreetAddress", ResourceType = typeof(Company))]
        public String StreetAddress { get; set; }
        [Display(Name = "CIZip", ResourceType = typeof(Company))]
        public String Zip { get; set; }


        [Display(Name = "CICountry", ResourceType = typeof(Company))]
        public CountryType? Country { get; set; }
        public CountryType? SelectedCountry { get; set; }

        [Display(Name = "CIDescription", ResourceType = typeof(Company))]
        public String Description { get; set; }
    }


    public class CompanyFacebookLikeViewModel
    {
        [Display(Name = "FBLabel", ResourceType = typeof(Company))]
        public String FacebookUrl { get; set; }
        
    }


    public class CompanyCategoriesViewModel
    {
        public ICollection<Category> CurrentCategories { get; set; }
        public IEnumerable<RootCategory> RootCategories { get; set; }
    }


    public class CompanyBoxViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "BoxTitle", ResourceType = typeof(Company))]
        public string Title { get; set; }

        [Required]
        [Display(Name = "BoxHeadline", ResourceType = typeof(Company))]
        public string Message { get; set; }

        [Required]
        [Display(Name = "BoxMessageDetails", ResourceType = typeof(Company))]
        public string Details { get; set; }

        public string ImagePath { get; set; }

        [DataType(DataType.Upload)]
        public HttpPostedFileBase ImageUpload { get; set; }
    }
}