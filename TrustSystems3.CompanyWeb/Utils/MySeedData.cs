using System;
using System.Data.Entity;
using System.Linq;
using Faker;
using TrustSystems3.BL.Utils;

namespace TrustSystems3.CompanyWeb.Utils
{
    public class MySeedData : DropCreateDatabaseIfModelChanges<TSModelContainer>
    {
        public MySeedData()
        {
            Seed(new TSModelContainer());
        }


        private void GenerateCategories(TSModelContainer context, RootCategory rootCategory)
        {
            Random rand = new Random();
            int companiesCount = context.Companies.Count();

            for (int i = 0; i < rand.Next(4, 14); i++)
            {
                var categoryName = Lorem.Sentence(1).Replace(".", "").Replace(",", "");

                rootCategory.Category.Add(new Category
                {
                    Name = categoryName,
                    Slug = categoryName.ToLower().Replace(" ", "_"),
                    Companies = context.Companies.OrderBy(x => x.Id).Skip(rand.Next(1, companiesCount)).Take(rand.Next(1, 20)).ToList()
                });

                context.RootCategories.Add(rootCategory);
            }
          
            context.SaveChanges();
        }


        private void Seed(TSModelContainer context)
        {
            for (int i = 0; i < 100; i++)
            {
                context.Companies.Add(new Companies
                {
                    Name = Faker.Lorem.Sentence(2),
                    Slug = Faker.Lorem.Sentence(1).ToLower().Replace(".", "").Replace(",", "").Replace(" ", "_"),
                    Description = Faker.Lorem.Sentence(18),
                    Website = string.Format("http://{0}", Faker.Internet.DomainName()),
                    PhoneNumber = Faker.Phone.Number(),
                    Email = Faker.Internet.Email(),
                    Country = Convert.ToInt32(CountryType.Russia),
                    Address = Faker.Address.StreetAddress(),
                    Zip = Faker.Address.ZipCode(),
                    IsOrderRequired = false
                });
            }
            context.SaveChanges();


            // Root categories
            var rc1 = new RootCategory {Name = "Games of Chance", Slug = "gambling", Icon = "cat-99"};
            GenerateCategories(context, rc1);

            var rc2 = new RootCategory { Name = "Money", Slug = "money", Icon = "cat-24" };
            GenerateCategories(context, rc2);

            var rc3 = new RootCategory { Name = "Kids", Slug = "kids", Icon = "cat-27" };
            GenerateCategories(context, rc3);

            var rc4 = new RootCategory { Name = "Home and Garden", Slug = "home_garden", Icon = "cat-41" };
            GenerateCategories(context, rc4);

            var rc5 = new RootCategory { Name = "Computer and Accessories", Slug = "computer", Icon = "cat-40" };
            GenerateCategories(context, rc5);

            var rc6 = new RootCategory { Name = "Health and Wellbeing", Slug = "health_wellbeing", Icon = "cat-47" };
            GenerateCategories(context, rc6);

            var rc7 = new RootCategory { Name = "Leisure", Slug = "leisure", Icon = "cat-46" };
            GenerateCategories(context, rc7);

            var rc8 = new RootCategory { Name = "Food and Beverage", Slug = "food_beverage", Icon = "cat-8" };
            GenerateCategories(context, rc8);

            var rc9 = new RootCategory { Name = "Art", Slug = "art", Icon = "cat-189" };
            GenerateCategories(context, rc9);


            Random rand = new Random();
            int usersCount = context.AspNetUsers.Count();

            foreach (var company in context.Companies)
            {
                for (int i = 0; i < rand.Next(6, 12); i++)
                {
                    int toSkipUsers = rand.Next(0, usersCount);
                    DateTime date = DateUtils.GetRandomDate(DateTime.Now.AddMonths(-2), DateTime.Now.AddYears(-2));

                    var review = new Review
                    {
                        UserId = context.AspNetUsers.OrderBy(x => x.Id).Skip(toSkipUsers).Take(1).First().Id,
                        Rating = rand.Next(1, 5),
                        ReviewShort = Lorem.Sentence(8),
                        ReviewFull = Lorem.Sentence(18),
                        OrderId = Guid.NewGuid().ToString(),
                        IsConfirmed = true,
                        DateCreated = date,
                        DatePublished = date,
                        DateUpdated = date
                    };

                    context.Companies.Find(company.Id).Reviews.Add(review);
                }
            }


            context.SaveChanges();
        }
    }
}