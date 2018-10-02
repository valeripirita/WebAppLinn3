using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppLinn3.Models
{
    public class CategoryDataAccessLayer
    {
        myLinnWebApiContext api = new myLinnWebApiContext();

        public IEnumerable<Category> GetAllCategories()
        {
            try
            {
                return api.GetAllCategories();
            }
            catch
            {
                throw;
            }
        }

        //To Add new category record 
        public int AddCategory(Category category)
        {
            try
            {
                api.AddCategory(category);
                return 1;
            }
            catch
            {
                throw;
            }
        }

        //To Update the records of a particluar category
        public int UpdateCategory(Category category)
        {
            try
            {
                api.UpdateCategory(category);
                return 1;
            }
            catch
            {
                throw;
            }
        }

        //Get the details of a particular category
        public Category GetCategoryData(string id)
        {
            try
            {
                return api.GetCategoryData(id);
            }
            catch
            {
                throw;
            }
        }

        //To Delete the record on a particular category
        public int DeleteCategory(string id)
        {
            try
            {
                api.DeleteCategory(id);
                return 1;
            }
            catch
            {
                throw;
            }
        }
    }
}
