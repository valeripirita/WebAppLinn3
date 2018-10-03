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
        public ApiStatus Create([FromBody] Category category)
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
        public ApiStatus Edit([FromBody]Category category)
        {
            return objcategory.UpdateCategory(category);
        }

        [HttpDelete]
        [Route("api/Category/Delete/{id}")]
        public ApiStatus Delete(string id)
        {
            return objcategory.DeleteCategory(id);
        }

        [HttpGet]
        [Route("api/Category/ApiStatus")]
        public ApiStatus GetApiStatus()
        {
            return objcategory.GetApiStatus();
        }

        //public string GetApiToken() => objcategory.GetApiToken();

        [HttpGet]
        [Route("api/Category/SetApiToken/{token}")]
        public ApiStatus SetApiToken(string token)
        {
            return objcategory.SetApiToken(token);
        }
    }

}
