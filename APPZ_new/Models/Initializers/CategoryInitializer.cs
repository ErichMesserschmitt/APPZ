using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APPZ_new.Data;
using Microsoft.Extensions.DependencyInjection;


namespace APPZ_new.Models.Initializers
{
    public class CategoryInitializer
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            var mainContext = serviceProvider.GetRequiredService<AppDBContext>();
            var categoriesToAdd = GetDefaultCategories();

            var isExist = mainContext.Categorys.Any(x => categoriesToAdd.Select(c => c.Title).Contains(x.Title));
            if (!isExist)
            {
                mainContext.Categorys.RemoveRange(mainContext.Categorys);
                mainContext.Categorys.AddRange(categoriesToAdd);
                mainContext.SaveChanges();
            }
        }

        private static IEnumerable<Category> GetDefaultCategories()
        {
            var categories = new List<Category>() { 
                new Category
                {
                    Title = ".Net"
                },
                new Category
                {
                    Title = "C++"
                },
                new Category
                {
                    Title = "Java"
                }
            };
            return categories;
        }
    }
}
