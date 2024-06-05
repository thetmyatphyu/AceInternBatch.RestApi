using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AceInternBatch.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetBlogs()
        {
            return Ok("GetBlog");
        }

        [HttpPost]
        public IActionResult CreateBlogs()
        {
            return Ok("CreateBlog");
        }
        [HttpPut]
        public IActionResult UpdateBlogs()
        {
            return Ok("UpdateBlog");
        }
        [HttpPatch]
        public IActionResult PatchBlogs()
        {
            return Ok("PatchBlog");
        }
        [HttpDelete]
        public IActionResult DeleteBlogs()
        {
            return Ok("DeleteBlogs");
        }

    }
}

    