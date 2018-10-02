using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppLinn3.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAppLinn3.Controllers
{

    public class CategoryController : Controller
    {
        CategoryDataAccessLayer objcategory = new CategoryDataAccessLayer();

        [HttpGet]
        [Route("api/Category/Index")]
        public IEnumerable<TblCategory> Index()
        {
            return objcategory.GetAllCategories();
        }

        [HttpPost]
        [Route("api/Category/Create")]
        public int Create([FromBody] TblCategory category)
        {
            return objcategory.AddCategory(category);
        }

        [HttpGet]
        [Route("api/Category/Details/{id}")]
        public TblCategory Details(int id)
        {
            return objcategory.GetCategoryData(id);
        }

        [HttpPut]
        [Route("api/Category/Edit")]
        public int Edit([FromBody]TblCategory category)
        {
            return objcategory.UpdateCategory(category);
        }

        [HttpDelete]
        [Route("api/Category/Delete/{id}")]
        public int Delete(int id)
        {
            return objcategory.DeleteCategory(id);
        }

        [HttpGet]
        [Route("api/Category/GetCityList")]
        public IEnumerable<TblCities> Details()
        {
            return objcategory.GetCities();
        }
    }

}
