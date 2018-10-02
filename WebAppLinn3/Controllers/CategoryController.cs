using System;
using System.Collections.Generic;
using WebAppLinn3.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebAppLinn3.Controllers
{

    public class CategoryController : Controller
    {
        CategoryDataAccessLayer objcategory = new CategoryDataAccessLayer();

        [HttpGet]
        [Route("api/Category/Index")]
        public IEnumerable<Category> Index()
        {
            return objcategory.GetAllCategories();
        }

        [HttpPost]
        [Route("api/Category/Create")]
        public int Create([FromBody] Category category)
        {
            return objcategory.AddCategory(category);
        }

        [HttpGet]
        [Route("api/Category/Details/{id}")]
        public Category Details(string id)
        {
            return objcategory.GetCategoryData(id);
        }

        [HttpPut]
        [Route("api/Category/Edit")]
        public int Edit([FromBody]Category category)
        {
            return objcategory.UpdateCategory(category);
        }

        [HttpDelete]
        [Route("api/Category/Delete/{id}")]
        public int Delete(string id)
        {
            return objcategory.DeleteCategory(id);
        }
    }

}
