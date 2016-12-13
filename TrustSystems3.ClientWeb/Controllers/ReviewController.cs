using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.AspNet.Identity;
using PagedList;
using TrustSystems3.ClientWeb.Models;
using TrustSystems3.ClientWeb.Models.Base;
using TrustSystems3.Repository;
using TrustSystems3.Repository.Infrastructure;
using TrustSystems3.Repository.Infrastructure.Contract;

namespace TrustSystems3.ClientWeb.Controllers
{
    public class ReviewController : Controller
    {
        private IUnitOfWork _uow = null;
        private CategoryRepository _categoryRepository = null;
        private CompanyRepository _companyRepository = null;
        private CompanyBoxRepository _companyBoxRepository = null;
        private ReviewRepository _reviewRepository = null;
        private UserRepository _userRepository = null;
        private CompanyUserRepository _companyUserRepository = null;
        private InvitationRepository _invitationRepository = null;

        private ReviewLikeRepository _reviewLikeRepository = null;
        private ReviewWarningRepository _reviewWarningRepository = null;


        public ReviewController()
        {
            _uow = new UnitOfWork();
            _categoryRepository = new CategoryRepository(_uow);
            _companyRepository = new CompanyRepository(_uow);
            _companyBoxRepository = new CompanyBoxRepository(_uow);
            _reviewRepository = new ReviewRepository(_uow);
            _userRepository = new UserRepository(_uow);
            _companyUserRepository = new CompanyUserRepository(_uow);
            _invitationRepository = new InvitationRepository(_uow);

            _reviewLikeRepository = new ReviewLikeRepository(_uow);
            _reviewWarningRepository = new ReviewWarningRepository(_uow);
        }


        protected int GetCurrentCompanyId
        {
            get {
                if (Session["CompanyId"] == null) {
                    // save companyId to session
                    var companyUser = _companyUserRepository.GetAll().FirstOrDefault(c => c.UserId == User.Identity.GetUserId());
                    if (companyUser != null) {
                        Session["CompanyId"] = companyUser.CompanyId;
                        Session["CompanyId"] = companyUser.UserState;

                        return companyUser.CompanyId;
                    }
                } else {
                    return Convert.ToInt32(Session["CompanyId"]);
                }
                return 0;
            }
        }


        [HttpPost]
        public JsonResult Like(int reviewId)
        {
            string userId = User.Identity.GetUserId();

            if (!_reviewLikeRepository.HasUserLikedReview(userId, reviewId))
            {
                // Like review, send +1 like 
                _reviewLikeRepository.Like(userId, reviewId);
                return Json(new {
                    count = _reviewRepository.SingleOrDefault(reviewId).ReviewLike.Count,
                    isactive = true
                });
            }

            // Dislike, send -1 like
            _reviewLikeRepository.Dislike(userId, reviewId);
            return Json(new {
                count = _reviewRepository.SingleOrDefault(reviewId).ReviewLike.Count,
                isactive = false
            });
        }

        [HttpPost]
        public JsonResult Complaint(int reviewId, string text)
        {
            string userId = User.Identity.GetUserId();
            bool isActive = false;
            int counter = 0;

            try
            {
                isActive = _reviewWarningRepository.AddWarning(userId, reviewId, text);
            }
            catch (Exception ex)
            {
            }
            finally
            {
                counter = _reviewRepository.SingleOrDefault(reviewId).ReviewWarning.Count;
            }

            return Json(new
            {
                count = counter,
                isactive = isActive
            });
        }


        // GET: Company reviews
        public ActionResult Index(string slug, int? page)
        {
            var company = _companyRepository.GetAll().Single(c => c.Slug == slug);

            if (company != null)
            {
                //                bool canUserEvaluateReview = !(User.Identity.IsAuthenticated &&
                //                                _repository.CompanyReviewRepository.HasUserReviewedCompany(WebSecurity.CurrentUserId,
                //                                                                                            model.CompanyID));

                //Last review pass hour
                int? lastReviewPassHour = null;
                if (company.Reviews.Any() && company.Reviews.Last().DatePublished.HasValue)
                {
                    //TODO: unkoment when publish date will be ready
                    TimeSpan lastReviewPassTime = DateTime.Now - company.Reviews.Last().DatePublished.Value;
                    lastReviewPassHour = Convert.ToInt32(lastReviewPassTime.TotalHours);
                }

                //Category position
                List<CategoryPosition> positions = new List<CategoryPosition>();
                var companyCategories = _categoryRepository.ListCategoriesByCompanyId(company.Id);
                foreach (var category in companyCategories)
                {
                    int total;
                    int pos = _companyRepository.GetPositionInCategory(category.Id, company.Id, out total);

                    positions.Add(new CategoryPosition
                    {
                        Category = category, 
                        Position = pos, 
                        TotalCompaniesCount = total
                    });
                }

                //Reviews
                var reviewList = new List<ReviewModel>();
                if (company.Reviews.Any())
                {
                    reviewList.AddRange(company.Reviews.OrderByDescending(x => x.DatePublished).Select(item => new ReviewModel
                    {
                        Review = item,
                        User = (item.UserId != null) ? _userRepository.SingleOrDefault(item.UserId) : null, 
                        UserAvatar = "/images/team/our-team04.jpg", 
                        UserReviewsCount = (item.UserId != null) ? _reviewRepository.GetAll().Count(x => x.UserId == item.UserId) : 0
                    }));
                }

                int pageNumber = page ?? 1;
                int pageSize = 6;

                
                //todo: rates range
                Dictionary<int, int> ratesRangeDictionary = new Dictionary<int, int>();
                ratesRangeDictionary.Add(5, 0);
                ratesRangeDictionary.Add(4, 0);
                ratesRangeDictionary.Add(3, 0);
                ratesRangeDictionary.Add(2, 0);
                ratesRangeDictionary.Add(1, 0);

                List<Review> lrReviews = _reviewRepository.GetReviewsByCompanyId(company.Id).ToList();
                StringBuilder rateRangesStr = new StringBuilder();
                foreach (Review review in lrReviews) {
                    switch (review.Rating)  {
                        case 5:
                            ratesRangeDictionary[5] += 1;
                            break;
                        case 4:
                            ratesRangeDictionary[4] += 1;
                            break;
                        case 3:
                            ratesRangeDictionary[3] += 1;
                            break;
                        case 2:
                            ratesRangeDictionary[2] += 1;
                            break;
                        case 1:
                            ratesRangeDictionary[1] += 1;
                            break;
                        default:
                            break;
                    }
                }


                ViewBag.RateRanges = renderRateRanges(ratesRangeDictionary);
                //end rates range

                float rating = _reviewRepository.GetRatingByCompanyId(company.Id);
                String ratingLabel = "Не определено";
                switch ((int)rating)
                {
                    case 1:
                        ratingLabel = "Очень плохо";
                        break;
                    case 2:
                        ratingLabel = "Плохо";
                        break;
                    case 3:
                        ratingLabel = "Средне";
                        break;
                    case 4:
                        ratingLabel = "Хорошо";
                        break;
                    case 5:
                        ratingLabel = "Отлично";
                        break;
                    default:
                        break;
                }

                

                return View(new CompanyModel {
                    Company = company,
                    PromotionBox = _companyBoxRepository.GetCompanyBoxByCompanyIdAndType(company.Id, CompanyBoxType.Promotion),
                    GuaranteeBox = _companyBoxRepository.GetCompanyBoxByCompanyIdAndType(company.Id, CompanyBoxType.Guarantee),
                    FacebookBox =  _companyBoxRepository.GetCompanyBoxByCompanyIdAndType(company.Id, CompanyBoxType.Facebook),
                    Reviews = reviewList.ToPagedList(pageNumber, pageSize),
                    CompanyRating = _reviewRepository.GetRatingByCompanyId(company.Id),
                    CompanyRatingLabel = ratingLabel,
                    LastReviewPassTime = lastReviewPassHour,
                    CanUserEvaluateReview = !CanUserEvaluateReview(company.Id),
                    CategoryPositions = positions
                });
            }


            return RedirectToAction("Index", "Home");
        }





        private string renderRateRanges(Dictionary<int, int> rates)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<table class=\"rate_ranges\" width=\"100%\">");
            foreach (var item in rates) {
                sb.Append("<tr>");
                sb.Append("<td>");
                sb.Append("<ul class=\"list-group\">");
                sb.Append("<li class=\"list-group-item\">");
                sb.AppendFormat("<span class=\"badge\">{0} reviews</span>", item.Value);
                sb.Append("<ul class=\"rates\">");
                for (int i = 0; i < item.Key; i++) {
                    sb.Append("<li></li>");
                }
                sb.Append("</ul>");
                sb.Append("</li>");
                sb.Append("</ul>");
                sb.Append("</td>");
                sb.Append("</tr>");
            }
            sb.Append("</table>");

            return sb.ToString();
        }





        // Get: Show review by id
        public ActionResult Show(int id)
        {
            var review = _reviewRepository.SingleOrDefault(id);
            if (review != null)
            {
                return View(new ReviewModel
                {
                    Review = review,
                    User = _userRepository.SingleOrDefault(review.UserId),
                    UserAvatar = "/images/team/our-team06.jpg",
                    UserReviewsCount = _reviewRepository.GetUserReviewsCount(review.UserId)
                });
            }

            return RedirectToAction("Index", "Home");
        }

        public ActionResult UserReviews(string id, int? page)
        {
            var user = _userRepository.SingleOrDefault(id);
            if (user != null)
            {
                int pageNumber = page ?? 1;
                int pageSize = 5;

                var model = new UserReviewsViewModel
                {
                    User = user,
                    Reviews = _reviewRepository.GetUserReviews(user.Id).ToList().ToPagedList(pageNumber, pageSize)
                };

                return View(model);
            }

            return Redirect(Request.UrlReferrer.ToString());
        }

        #region Evaluate

        public ActionResult Evaluate(string slug)
        {
            ViewBag.ReturnUrl = Url.Action("evaluate", "review");
            var model = new EvaluateReviewModel { Rating = 0 };
            
            if (!string.IsNullOrEmpty(slug))
            {
                var company = _companyRepository.GetAll().SingleOrDefault(c => c.Slug == slug);
                if (company != null)
                {
                    model.Company = company;
                    return View(model);
                }
            }

            return RedirectToAction("Index", "Review");
        }

        [HttpPost]
        public ActionResult Evaluate(string slug, EvaluateReviewModel model)
        {
            try
            {
                var company = _companyRepository.GetByCompanySlug(slug);
                model.Company = company;

                if (company.IsOrderRequired && string.IsNullOrEmpty(model.OrderId))
                {
                    ModelState.AddModelError("OrderIdError", "OrderId отзыва данной компании обязателен для заполнения");
                }

                if (company.IsOrderRequired)
                {
                    if (!_invitationRepository.GetAll()
                        .Any(i => i.ReferenceId == model.OrderId && i.Status == InivationStatusType.Sent))
                    {
                        ModelState.AddModelError("OrderIdError",
                            "Такого OrderId в заявках на обзор не найдо! Проверьте еще раз код в письме.");
                    }
                }

                if (ModelState.IsValid)
                {
                    // if user logged in and no order required, - getUserid


                    // if user was invited to review with code

                    var review = new Review
                    {
                        UserId = (!company.IsOrderRequired) ? User.Identity.GetUserId() : null,
                        CompanyId = company.Id,
                        OrderId = (company.IsOrderRequired) ? model.OrderId : null,
                        Rating = model.Rating,
                        ReviewShort = model.ReviewTitle,
                        ReviewFull = model.ReviewDescription,
                        DateCreated = DateTime.Now,
                        DateUpdated = DateTime.Now,
                        DatePublished = DateTime.Now
                    };

                    if (!company.IsOrderRequired)
                    {
                        review.IsConfirmed = true;
                    }
                    else
                    {
                        review.IsConfirmed = false;

                        //update invite, - set status as Reviewed
                        var invitation = _invitationRepository.GetAll()
                            .FirstOrDefault(i => i.ReferenceId == model.OrderId && i.Status == InivationStatusType.Sent);
                        if (invitation != null)
                        {
                            invitation.Status = InivationStatusType.Reviewed;
                            _invitationRepository.Update(invitation);
                        }
                    }

                    //TODO: check for save
                    _reviewRepository.Insert(review);


                    // back to company review
                    return RedirectToAction("Index",
                        new RouteValueDictionary(new {controller = "Review", action = "Index", slug = company.Slug}));
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }


            return View(model);
        }

        public bool CanUserEvaluateReview(int companyId)
        {
            return (Request.IsAuthenticated && !_companyUserRepository.IsUserOwnThisCompany(
                Request.LogonUserIdentity.GetUserId(), companyId));
        }

        #endregion
    }
}