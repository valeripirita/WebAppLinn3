using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppLinn3.Models
{
    public class CategoryDataAccessLayer
    {
        myTestDBContext db = new myTestDBContext();

        public IEnumerable<TblCategory> GetAllCategories()
        {
            try
            {
                return db.TblCategory.ToList();
            }
            catch
            {
                throw;
            }
        }

        //To Add new category record 
        public int AddCategory(TblCategory category)
        {
            try
            {
                db.TblCategory.Add(category);
                db.SaveChanges();
                return 1;
            }
            catch
            {
                throw;
            }
        }

        //To Update the records of a particluar category
        public int UpdateCategory(TblCategory category)
        {
            try
            {
                db.Entry(category).State = EntityState.Modified;
                db.SaveChanges();

                return 1;
            }
            catch
            {
                throw;
            }
        }

        //Get the details of a particular category
        public TblCategory GetCategoryData(int id)
        {
            try
            {
                TblCategory category = db.TblCategory.Find(id);
                return category;
            }
            catch
            {
                throw;
            }
        }

        //To Delete the record on a particular category
        public int DeleteCategory(int id)
        {
            try
            {
                TblCategory emp = db.TblCategory.Find(id);
                db.TblCategory.Remove(emp);
                db.SaveChanges();
                return 1;
            }
            catch
            {
                throw;
            }
        }

        //To Get the list of Cities
        public List<TblCities> GetCities()
        {
            List<TblCities> lstCity = new List<TblCities>();
            lstCity = (from CityList in db.TblCities select CityList).ToList();

            return lstCity;
        }
    }
}
