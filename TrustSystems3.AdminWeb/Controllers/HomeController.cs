using System.Linq;
using System.Web.Mvc;
using TrustSystems3.AdminWeb.Models;
using TrustSystems3.Repository;
using TrustSystems3.Repository.Infrastructure;
using TrustSystems3.Repository.Infrastructure.Contract;

namespace TrustSystems3.AdminWeb.Controllers
{
    public class HomeController : Controller
    {
        private IUnitOfWork _unitOfWork;
        private UserRepository _userRepository;
        private ReviewRepository _reviewRepository;
        private CompanyRepository _companyRepository;

        private LoggerRepository _loggerRepository;


        public HomeController()
        {
            _unitOfWork = new UnitOfWork();
            _userRepository = new UserRepository(_unitOfWork);
            _reviewRepository = new ReviewRepository(_unitOfWork);
            _companyRepository = new CompanyRepository(_unitOfWork);

            _loggerRepository = new LoggerRepository(_unitOfWork);
        }


        public ActionResult Index()
        {
            return View(new DashboardViewModel
            {
                ReviewsCount = _reviewRepository.GetAll().Count(),
                CompaniesCount = _companyRepository.GetAll().Count(),
                UsersCount = _userRepository.GetAll().Count(),
                Logs = _loggerRepository.GetAll().OrderByDescending(m => m.Date).Take(10)
                
            });
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}