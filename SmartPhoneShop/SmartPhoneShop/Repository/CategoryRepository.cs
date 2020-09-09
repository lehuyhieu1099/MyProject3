using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartPhoneShop.Models
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbConText context;

        public CategoryRepository(AppDbConText context)
        {
            this.context = context;
        }
        public Category Create(Category cate)
        {
            context.Categories.Add(cate);
            context.SaveChanges();
            return cate;
        }

        public bool Delete(int id)
        {
            Category category = Get(id);
            if (category != null)
            {
                category.IsDelete = true;
                context.SaveChanges();
                return true;
            }
            return false;
        }

        public Category Edit(Category category)
        {
            var edit = context.Categories.Attach(category);
            edit.State = EntityState.Modified;
            context.SaveChanges();
            return category;
        }

        public Category Get(int id)
        {
            Category category = context.Categories.Find(id);
            return category;
        }

        public IEnumerable<Category> Gets()
        {
            return context.Categories.ToList();
        }
    }
}
