using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        List<Category> categories = new List<Category>();

        CategoryController()
        {
            categories.Add(new Category("To Do", "1"));
            categories.Add(new Category("Done", "2"));
            categories.Add(new Category("Doing", "3"));
        }

        /// <summary>
        /// get on category wuth the id given 
        /// </summary>
        /// <param name="id"> the id of the category</param>
        /// <returns>Category</returns>
        [HttpGet("id")]
        public IActionResult GetOne(string id)
        {
            foreach(Category category in categories)
                if(category.Id==id)
                    return Ok(category);
            return Ok(null);
        }
        /// <summary>
        /// get the list of categories
        /// </summary>
        /// <returns>a list of categories</returns>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(categories);
        }
        /// <summary>
        /// delete the category with the given id
        /// </summary>
        /// <param name="id">category id</param>
        /// <returns></returns>
        [HttpDelete("id")]

        public IActionResult Delete(string id)
        {
            foreach (Category category in categories)
                if (category.Id == id)
                    return Ok(category);
            return Ok(null);
        }
        /// <summary>
        /// adds a category to the list
        /// </summary>
        /// <returns></returns>
        [HttpPost("category")]
        public IActionResult Post(Category category)
        {
            categories.Add(category);
            return Ok(categories);
        }
    }
}
