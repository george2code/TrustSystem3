using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using TrustSystems3.ClientWeb.Models.Base;
using TrustSystems3.Repository;
using TrustSystems3.Repository.Infrastructure;
using TrustSystems3.Repository.Infrastructure.Contract;

namespace TrustSystems3.ClientWeb.Controllers
{
    public class BadgeController : BaseController
    {
        private IUnitOfWork _uow = null;
        private CompanyRepository _companyRepository = null;
        private ReviewRepository _reviewRepository = null;
        private UserRepository _userRepository = null;

        public BadgeController() 
        {
            _uow = new UnitOfWork();
            _companyRepository = new CompanyRepository(_uow);
            _reviewRepository = new ReviewRepository(_uow);
            _userRepository = new UserRepository(_uow);
        }



        public ActionResult Reviews(int companyId)
        {
            var latestReviews = new List<ReviewModel>();
            
            var companyReviews = _reviewRepository.GetReviewsByCompanyId(companyId);
            int reviewCount = (companyReviews != null) ?  companyReviews.Count() : 0;
            var list = companyReviews.OrderBy(rew => Guid.NewGuid()).Take(3);
            int rating = Convert.ToInt32(_reviewRepository.GetRatingByCompanyId(companyId));

            ViewBag.ReviewCount = reviewCount;
            ViewBag.Rating = rating;
            ViewBag.RateStr = "Нет рейтинга";

            switch (rating) {
                case 1:
                    ViewBag.RateStr = "Очень плохо";
                    break;
                case 2:
                    ViewBag.RateStr = "Плохо";
                    break;
                case 3:
                    ViewBag.RateStr = "Средне";
                    break;
                case 4:
                    ViewBag.RateStr = "Хорошо";
                    break;
                case 5:
                    ViewBag.RateStr = "Отлично";
                    break;
                default:
                    break;
            }

            foreach (var item in list) {
                latestReviews.Add(new ReviewModel {
                    Review = item,
                    User = _userRepository.SingleOrDefault(item.UserId),
                    UserAvatar = "/images/team/our-team01.jpg",
                    UserReviewsCount = _reviewRepository.GetAll().Count(x => x.UserId == item.UserId)
                });
            }

            return View(latestReviews);
        }





        //
        // GET: /Badge/
        public String Index(int companyId)
        {
            var company = _companyRepository.GetByCompanyId(companyId);
            int rating = Convert.ToInt32(_reviewRepository.GetRatingByCompanyId(companyId));

            String badgeUrl = Url.Content("~/images/review/badge1.png");
            switch (rating)
            {
                case 2:
                    badgeUrl = Url.Content("~/images/review/badge2.png");
                    break;
                case 3:
                    badgeUrl = Url.Content("~/images/review/badge3.png");
                    break;
                case 4:
                    badgeUrl = Url.Content("~/images/review/badge4.png");
                    break;
                case 5:
                    badgeUrl = Url.Content("~/images/review/badge5.png");
                    break;
            }

           StringBuilder sb = new StringBuilder();
            sb.Append("<div class=\"panel-badge\">");
            sb.AppendFormat("<a href=\"http://{0}/review/{1}\" title=\"{2}\" target=\"_blank\"><img src=\"http://{0}{3}\" width=\"100%\" alt=\"{2}\" /></a>",
                "trustsystem.ru", //Request.Url.Host,
                company.Slug,
                company.Name,
                badgeUrl);
            sb.Append("</div>");

            return sb.ToString();
        }
	}
}