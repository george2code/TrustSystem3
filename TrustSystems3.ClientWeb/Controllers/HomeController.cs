using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using PagedList;
using TrustSystems3.BL;
using TrustSystems3.BL.Utils;
using TrustSystems3.ClientWeb.Models;
using TrustSystems3.ClientWeb.Models.Base;
using TrustSystems3.Repository;
using TrustSystems3.Repository.Infrastructure;
using TrustSystems3.Repository.Infrastructure.Contract;
using TrustSystems3.Services;

namespace TrustSystems3.ClientWeb.Controllers
{
    public class HomeController : BaseController
    {
        private IUnitOfWork _uow = null;
        private ReviewRepository _reviewRepository = null;
        private UserRepository _userRepository = null;
        private RootCategoryRepository _rootCategoryRepository = null;
        private CategoryRepository _categoryRepository = null;
        private CompanyRepository _companyRepository = null;

        public HomeController()
        {
            _uow = new UnitOfWork();
            _reviewRepository = new ReviewRepository(_uow);
            _userRepository = new UserRepository(_uow);

            _rootCategoryRepository = new RootCategoryRepository(_uow);
            _categoryRepository = new CategoryRepository(_uow);
            _companyRepository = new CompanyRepository(_uow);
        }


        public ActionResult Index()
        {
            var model = new HomeViewModel();

            //Latest reviews
            model.LatestReviews = new List<ReviewModel>();
            var latestReviewsCount = User.Identity.IsAuthenticated ? 6 : 5;
            var list = _reviewRepository.GetAll().OrderBy(rew => Guid.NewGuid()).Take(latestReviewsCount);
            
            foreach (var item in list)
            {
                model.LatestReviews.Add(new ReviewModel
                {
                    Review = item,
                    User = _userRepository.SingleOrDefault(item.UserId),
                    UserAvatar = "/images/team/our-team01.jpg",
                    UserReviewsCount = _reviewRepository.GetAll().Count(x=>x.UserId == item.UserId)
                });
            }

            //Review of the day
            var dayReview = _reviewRepository.GetAll().OrderBy(rew => Guid.NewGuid()).First();
            model.ReviewOfTheDay = new ReviewModel
            {
                Review = dayReview,
                User = _userRepository.SingleOrDefault(dayReview.UserId),
                UserAvatar = "/images/team/our-team02.jpg"
            };

            //Categories
            model.RootCategoryList = _rootCategoryRepository.GetAll().ToList();


            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contacts()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


        /// <summary>
        /// Get value from view textbox and send to model
        /// </summary>
        /// <param name="txtValue"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Search(string txtValue, int? page)
        {
            ViewBag.SearchValue = txtValue;

            // Check length before for process
            if (txtValue.Length > 0)
            {
                var companies = _companyRepository.Search(txtValue);
                List<SearchItemModel> list = new List<SearchItemModel>();
                
                //TODO: reviews count format: 26,161 Reviews
                foreach (var company in companies)
                {
                    list.Add(new SearchItemModel
                    {
                        CompanyName = company.Name,
                        CompanySlug = company.Slug,
                        CompanyUrl = company.Website,
                        CompanyDescription = StringUtils.TruncateWithPreservation(company.Description, 240),
                        CompanyRating = _reviewRepository.GetRatingByCompanyId(company.Id),
                        CompanyReviewsCount = _reviewRepository.GetReviewsCountByCompanyId(company.Id)
                    });
                }

                int pageNumber = page ?? 1;
                int pageSize = 10;

                return View(list.ToPagedList(pageNumber, pageSize));
            }


            // If no keyword are entered than send error message to user
            ViewBag.Message = true;
            return View();
        }



        [HttpPost]
        public JsonResult TopCategories(int categoryId)
        {
           var categoryIdList = _categoryRepository.GetAll()
                    .Where(c => c.RootCategoryId == categoryId)
                    .Select(c => c.Id)
                    .ToList();


            var topBestCompanies = _companyRepository.ListRatingLevelByCategoryId(categoryIdList, 10, true);

            var sbBest = new StringBuilder();
            int count = 1;
            foreach (var company in topBestCompanies)
            {
                sbBest.Append("<tr>");
                sbBest.AppendFormat("<td>{0}</td>", count);
                sbBest.AppendFormat("<td><a href='companies/{0}'>{1}</a></td>", company.Key.Slug, company.Key.Name);
                sbBest.AppendFormat("<td class='mark'>{0}</td>", String.Format("{0:0.0}", company.Value));
                sbBest.Append("</tr>");
                count++;
            }

            var topWorseCompanies = _companyRepository.ListRatingLevelByCategoryId(categoryIdList, 10, false);

            var sbWorse = new StringBuilder();
            count = 1;
            foreach (var company in topWorseCompanies)
            {
                sbWorse.Append("<tr>");
                sbWorse.AppendFormat("<td>{0}</td>", count);
                sbWorse.AppendFormat("<td><a href='companies/{0}'>{1}</a></td>", company.Key.Slug, company.Key.Name);
                sbWorse.AppendFormat("<td class='mark'>{0}</td>", String.Format("{0:0.0}", company.Value));
                sbWorse.Append("</tr>");
                count++;
            }
       

            return Json(new
            {
                best = sbBest.ToString(), 
                worse = sbWorse.ToString()
            });
        }



        public ActionResult SetCulture(string culture)
        {
            // Validate input
            culture = CultureHelper.GetImplementedCulture(culture);
            RouteData.Values["culture"] = (culture == "ru") ? null : culture;  // set culture


            return RedirectToAction("Index");
        }
    }
}