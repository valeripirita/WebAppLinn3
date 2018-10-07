using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppLinn3.Models
{
    public class CategoryDataAccessLayer
    {
        private static MyLinnWebApiContext api = new MyLinnWebApiContext();

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
        public ApiStatus AddCategory(Category category)
        {
            try
            {
                return api.AddCategory(category);
            }
            catch
            {
                throw;
            }
        }

        //To Update the records of a particluar category
        public ApiStatus UpdateCategory(Category category)
        {
            try
            {
                return api.UpdateCategory(category);
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
        public ApiStatus DeleteCategory(string id)
        {
            try
            {
                return api.DeleteCategory(id);
            }
            catch
            {
                throw;
            }
        }

        public ApiStatus GetApiStatus()
        {
            try
            {
                return api.apiStatus;
            }
            catch
            {
                throw;
            }
        }

        public ApiStatus SetApiToken(string token)
        {
            try
            {
                return api.SetApiToken(token);
            }
            catch
            {
                throw;
            }
        }
    }
}
