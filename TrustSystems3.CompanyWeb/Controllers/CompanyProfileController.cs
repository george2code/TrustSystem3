using System;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using TrustSystems3.CompanyWeb.Models;
using TrustSystems3.Repository;
using System.IO;
using System.Threading.Tasks;
using System.Web;


namespace TrustSystems3.CompanyWeb.Controllers
{
    [Authorize]
    public class CompanyProfileController : BaseController
    {
        private CompanyRepository _companyRepository = null;
        private RootCategoryRepository _rootCategoryRepository = null;
        private CategoryRepository _categoryRepository = null;
        private CompanyBoxRepository _companyBoxRepository = null;

        private Companies _currentCompany;

        private Companies CurrentCompany {
            get {
                if (_currentCompany == null) {
                    _currentCompany = _companyRepository.GetAll().FirstOrDefault(c => c.Id == GetCurrentCompanyId);
                }
                return _currentCompany;
            }
        }

        public CompanyProfileController()
        {
            _companyRepository = new CompanyRepository(DbUnitOfWork);
            _companyBoxRepository = new CompanyBoxRepository(DbUnitOfWork);
            _rootCategoryRepository = new RootCategoryRepository(DbUnitOfWork);
            _categoryRepository = new CategoryRepository(DbUnitOfWork);
        }


        // GET: CompanyProfile
        public ActionResult Information() {
            var company = _companyRepository.GetByCompanyId(GetCurrentCompanyId);
            CompanyInformationViewModel model = new CompanyInformationViewModel {
                Slug = company.Slug,
                Website = company.Website,
                Name = company.Name,
                Email = company.Email,
                PhoneNumber = company.PhoneNumber,
                StreetAddress = company.Address,
                Zip = company.Zip,
                Description = company.Description
            };

            if (company.Country.HasValue) {
                model.SelectedCountry = (CountryType)company.Country.Value;
            }
            
            return View(model);
        }

        //
        // POST: /CompanyProfile/Information
        [HttpPost]
        public async Task<ActionResult> Information(CompanyInformationViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Company name
                var company = _companyRepository.GetByCompanyId(GetCurrentCompanyId);
                if (company == null)
                {
                    ModelState.AddModelError(string.Empty, "Company not found!");
                    return View(model);
                }


                company.Slug = model.Slug;
                company.Website = model.Website;
                company.Name = model.Name;
                company.Email = model.Email;
                company.PhoneNumber = model.PhoneNumber;
                company.Address = model.StreetAddress;
                company.Zip = model.Zip;
                company.Country = (model.Country.HasValue) ? (int) model.Country.Value : (int?) null;
                company.Description = model.Description;

                _companyRepository.Update(company);
                return RedirectToAction("Information", "CompanyProfile");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }




        #region Categories

        public ActionResult Categories()
        {
            return View(new CompanyCategoriesViewModel
            {
                CurrentCategories = CurrentCompany.Categories,
                RootCategories = _rootCategoryRepository.GetAll()
                
            });
        }

        [HttpPost]
        public JsonResult GetSubCategories(int rootId)
        {
            string result = string.Empty;
            var rootCategory = _rootCategoryRepository.GetAll().FirstOrDefault(r => r.Id == rootId);

            if (rootCategory != null)
            {
                StringBuilder sb = new StringBuilder();
                foreach (var category in rootCategory.Category)
                {
                    if (CurrentCompany.Categories.Contains(category))
                        continue;

                    sb.AppendFormat("<li itemid=\"{0}\">", category.Id);
                    sb.AppendFormat("<span aria-hidden=\"true\" class=\"glyphicon glyphicon-check\"></span>");
                    sb.AppendFormat("<span class=\"glyphicon-class\">{0}</span>", category.Name);
                    sb.Append("</li>");
                }

                result = sb.ToString();
            }

            return Json(new {html = result});
        }

        [HttpPost]
        public JsonResult UpdateCompanyCategories(int categoryId)
        {
            var category = _categoryRepository.GetById(categoryId);
            if (category != null)
            {
                CurrentCompany.Categories.Add(category);
                _companyRepository.Update(CurrentCompany);
            }

            return null;
        }

        [HttpPost]
        public JsonResult RemoveCategoryFromCompany(int categoryId)
        {
            string result = string.Empty;

            var category = _categoryRepository.GetById(categoryId);
            if (category != null)
            {
                CurrentCompany.Categories.Remove(category);
                _companyRepository.Update(CurrentCompany);

                StringBuilder sb = new StringBuilder();
                int index = 1;
                foreach (var cat in CurrentCompany.Categories)
                {
                    sb.Append("<tr>");
                    sb.AppendFormat("<th scope=\"row\">{0}</th>", index);
                    sb.AppendFormat("<td>{0}</td>", cat.Name);
                    sb.AppendFormat("<td><input type=\"button\" value=\"Remove this category\" class=\"btn btn-danger\" itemid=\"{0}\"></td>", cat.Id);
                    sb.Append("</tr>");

                    index++;
                }

                result = sb.ToString();
            }

            return Json(new { html = result, count = CurrentCompany.Categories.Count });
        }

        #endregion


        public ActionResult Promotion()
        {
            var model = new CompanyBoxViewModel();
            var box = _companyBoxRepository.GetAll().FirstOrDefault(c => c.BoxType == CompanyBoxType.Promotion && c.CompaniesId == GetCurrentCompanyId);
            if (box != null)
            {
                model.Id = box.Id;
                model.Title = box.Title;
                model.Message = box.Message;
                model.Details = box.Details;
                model.ImagePath = box.Image;
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Promotion(CompanyBoxViewModel model)
        {
            var imageTypes = new string[]
            {
                "image/gif",
                "image/jpeg",
                "image/pjpeg",
                "image/png"
            };

//            if (model.ImageUpload == null || model.ImageUpload.ContentLength == 0)
//            {
//                ModelState.AddModelError("ImageUpload", "This field is required");
//            }
//            else if (!imageTypes.Contains(model.ImageUpload.ContentType))
//            {
//                ModelState.AddModelError("ImageUpload", "Please choose either a GIF, JPG or PNG image.");
//            }

            if (ModelState.IsValid)
            {
                var box = _companyBoxRepository.GetAll().FirstOrDefault(c => c.BoxType == CompanyBoxType.Promotion && c.CompaniesId == GetCurrentCompanyId);
                if (box != null)
                {
                    // Update box
                    box.Title = model.Title;
                    box.Message = model.Message;
                    box.Details = model.Details;
                    var newImagePath = GetImage(model.ImageUpload);
                    if (newImagePath != null)
                    box.Image = newImagePath;

                    // Update entity
                    _companyBoxRepository.Update(box);
                }
                else
                {
                    // New box
                    box = new CompanyBox
                    {
                        CompaniesId = CurrentCompany.Id,
                        Title = model.Title,
                        Message = model.Message,
                        Details = model.Details,
                        BoxType = CompanyBoxType.Promotion,
                        Image = GetImage(model.ImageUpload)
                    };

                    // Insert entity
                    _companyBoxRepository.Insert(box);
                }
                
                
                return RedirectToAction("Promotion");
            }

            return View(model);
        }


        private string GetImage(HttpPostedFileBase imageUpload)
        {
            if (imageUpload != null && imageUpload.ContentLength > 0)
            {
                var uploadDir = string.Format("~/uploads/companybox/{0}", CurrentCompany.Name);

                // create directory if not exists
                var path = Server.MapPath(uploadDir);
                var directory = new DirectoryInfo(path);
                if (directory.Exists == false)
                {
                    directory.Create();
                }

                var imagePath = Path.Combine(Server.MapPath(uploadDir), imageUpload.FileName);
                // Check if file exists, - don't upload again
                if (!System.IO.File.Exists(imagePath))
                {
                    imageUpload.SaveAs(imagePath);
                }

                return string.Format("~/uploads/companybox/{0}/{1}", CurrentCompany.Name, imageUpload.FileName);
            }

            return null;
        }

        [HttpPost]
        public ActionResult RemoveBoxImage(int boxId)
        {
            var box = _companyBoxRepository.SingleOrDefault(boxId);
            if (box != null)
            {
                box.Image = null;
                _companyBoxRepository.Update(box);
            }

            return RedirectToAction("Promotion");
        }


        public ActionResult Guarantee()
        {
            var model = new CompanyBoxViewModel();
            var box = _companyBoxRepository.GetAll().FirstOrDefault(c => c.BoxType == CompanyBoxType.Guarantee && c.CompaniesId == GetCurrentCompanyId);
            if (box != null) {
                model.Id = box.Id;
                model.Title = box.Title;
                model.Message = box.Message;
                model.Details = box.Details;
                model.ImagePath = box.Image;
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Guarantee(CompanyBoxViewModel model)
        {
            var imageTypes = new string[]
            {
                "image/gif",
                "image/jpeg",
                "image/pjpeg",
                "image/png"
            };

            //            if (model.ImageUpload == null || model.ImageUpload.ContentLength == 0)
            //            {
            //                ModelState.AddModelError("ImageUpload", "This field is required");
            //            }
            //            else if (!imageTypes.Contains(model.ImageUpload.ContentType))
            //            {
            //                ModelState.AddModelError("ImageUpload", "Please choose either a GIF, JPG or PNG image.");
            //            }

            if (ModelState.IsValid)
            {
                var box = _companyBoxRepository.GetAll().FirstOrDefault(c => c.BoxType == CompanyBoxType.Guarantee && c.CompaniesId == GetCurrentCompanyId);
                if (box != null) {
                    // Update box
                    box.Title = model.Title;
                    box.Message = model.Message;
                    box.Details = model.Details;
                    var newImagePath = GetImage(model.ImageUpload);
                    if (newImagePath != null)
                        box.Image = newImagePath;

                    // Update entity
                    _companyBoxRepository.Update(box);
                }
                else
                {
                    // New box
                    box = new CompanyBox {
                        CompaniesId = CurrentCompany.Id,
                        Title = model.Title,
                        Message = model.Message,
                        Details = model.Details,
                        BoxType = CompanyBoxType.Guarantee,
                        Image = GetImage(model.ImageUpload)
                    };

                    // Insert entity
                    _companyBoxRepository.Insert(box);
                }


                return RedirectToAction("Guarantee");
            }

            return View(model);
        }



        public ActionResult Facebook()
        {
            var model = new CompanyBoxViewModel();
            var box = _companyBoxRepository.GetAll().FirstOrDefault(c => c.BoxType == CompanyBoxType.Facebook && c.CompaniesId == GetCurrentCompanyId);
            if (box != null) {
                model.Id = box.Id;
                model.Message = box.Message;
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Facebook(CompanyBoxViewModel model)
        {
            var box = _companyBoxRepository.GetAll().FirstOrDefault(c => c.BoxType == CompanyBoxType.Facebook && c.CompaniesId == GetCurrentCompanyId);
            if (box != null) {
                if (!String.IsNullOrEmpty(model.Message)) {
                    // Update box
                    box.Message = model.Message;
                    // Update entity
                    _companyBoxRepository.Update(box);
                }
                else {
                    _companyBoxRepository.Delete(box);
                }
            }
            else {
                if (!String.IsNullOrEmpty(model.Message)) {
                    // New box
                    box = new CompanyBox {
                        CompaniesId = GetCurrentCompanyId,
                        Title = "Facebook Like Box",
                        Message = model.Message,
                        Details = String.Empty,
                        BoxType = CompanyBoxType.Facebook
                    };

                    // Insert entity
                    _companyBoxRepository.Insert(box);
                }
            }

            return RedirectToAction("Facebook");
        }



        public ActionResult ReviewSettings()
        {
            var company = _companyRepository.GetAll().FirstOrDefault(c => c.Id == GetCurrentCompanyId);
            if (company != null) {
                var model = new ReviewSettingsViewModel {
                    IsCodeRequired = company.IsOrderRequired ? 1 : 0,
                    Answers = new[] {
                        new SelectListItem {Text = "True", Value = "1"},
                        new SelectListItem {Text = "False", Value = "0"}
                    }
                };

                return View(model);
            }
            
            return RedirectToAction("Information", "CompanyProfile");
        }

        [HttpPost]
        public ActionResult ReviewSettings(ReviewSettingsViewModel model)
        {
            model.Answers = new[] {
                new SelectListItem {Text = "True", Value = "1"},
                new SelectListItem {Text = "False", Value = "0"}
            };

            var company = _companyRepository.GetAll().FirstOrDefault(c => c.Id == GetCurrentCompanyId);
            if (company != null) {
                company.IsOrderRequired = (model.IsCodeRequired > 0);
                _companyRepository.Update(company);
            }

            return View(model);
        }
    }
}