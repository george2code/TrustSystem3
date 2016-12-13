using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using TrustSystems3.ClientWeb.Models;
using TrustSystems3.Repository;
using TrustSystems3.Repository.Infrastructure;
using TrustSystems3.Repository.Infrastructure.Contract;

namespace TrustSystems3.ClientWeb.Controllers
{
    public class CategoryController : BaseController
    {
        private IUnitOfWork _uow = null;
        private RootCategoryRepository _rootCategoryRepository = null;
        private CategoryRepository _categoryRepository = null;
        private CompanyRepository _companyRepository = null;

        public CategoryController()
        {
            _uow = new UnitOfWork();

            _rootCategoryRepository = new RootCategoryRepository(_uow);
            _categoryRepository = new CategoryRepository(_uow);
            _companyRepository = new CompanyRepository(_uow);
        }


        // GET: Categories
        public ActionResult Index()
        {
            IDictionary<int, IDictionary<Companies, float>> rootCompanies = new Dictionary<int, IDictionary<Companies, float>>();
            IDictionary<RootCategory, IEnumerable<Category>> catDictionary = new Dictionary<RootCategory, IEnumerable<Category>>();
            var rootCategoriesList = _rootCategoryRepository.GetAll().OrderBy(c => c.Name).ToList();

            foreach (var rootCategory in rootCategoriesList)
            {
                // get subcategories list
                var subCategoriesList = _categoryRepository.GetAll()
                    .Where(c => c.RootCategoryId == rootCategory.Id)
                    .ToList();

                catDictionary.Add(rootCategory, subCategoriesList);

                rootCompanies.Add(rootCategory.Id,
                    _companyRepository.ListRatingLevelByCategoryId(subCategoriesList.Select(c => c.Id), 3, true));
            }

            return View(new CategoriesViewModel {
                RootCategories = rootCategoriesList,
                RootCompanies = rootCompanies,
                CategoryDictionary = catDictionary
            });
        }


        //GET: Categories/Slug
        public ActionResult CategoryCompanies(string slug)
        {
            var rootCategoriesList = _rootCategoryRepository.GetAll().ToList();

            var currentRootCategory = _rootCategoryRepository.GetAll().SingleOrDefault(r => r.Slug == slug);

            if (currentRootCategory != null)
            {
                //Current category is ROOT
                // get subcategories list
                var subCategoriesList = _categoryRepository.GetAll()
                    .Where(c => c.RootCategoryId == currentRootCategory.Id)
                    .ToList();

                var categoryCompanies =
                    _companyRepository.ListRatingLevelByCategoryId(subCategoriesList.Select(c => c.Id), int.MaxValue,
                        true);

                return View(new CategoryCompaniesViewModel
                {
                    CategoryName = currentRootCategory.Name,
                    CurrentRootCategoryId = currentRootCategory.Id,
                    RootCategories = rootCategoriesList,
                    SubCategories = subCategoriesList,
                    CategoryCompanies = categoryCompanies
                });
            }
            else
            {
                var currentCategory = _categoryRepository.GetAll().SingleOrDefault(c => c.Slug == slug);

                if (currentCategory != null)
                {
                    var subCategoriesList = _categoryRepository.GetAll()
                    .Where(c => c.RootCategoryId == currentCategory.RootCategoryId)
                    .ToList();

                    var categoryCompanies =
                        _companyRepository.ListRatingLevelByCategoryId(new List<int> { currentCategory.Id }, 
                        int.MaxValue, true);

                    return View(new CategoryCompaniesViewModel
                    {
                        CategoryName = currentCategory.Name,
                        CurrentRootCategoryId = currentCategory.RootCategoryId,
                        CurrentCategoryId = currentCategory.Id,
                        RootCategories = rootCategoriesList,
                        SubCategories = subCategoriesList,
                        CategoryCompanies = categoryCompanies
                    });
                }
            }

            return RedirectToAction("Index");
        }
    }
}