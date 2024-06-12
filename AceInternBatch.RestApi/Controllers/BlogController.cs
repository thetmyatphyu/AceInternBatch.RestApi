using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace AceInternBatch.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly SqlConnectionStringBuilder _connectionStringBuilder;
        public BlogController()
        {
            _connectionStringBuilder = new SqlConnectionStringBuilder()
            {
                DataSource = "DESKTOP-737UGPB",
                InitialCatalog = "AceInternBatch1DotNetCore",
                UserID = "sa",
                Password = "sasa@123"

            };
        }

        [HttpGet]
        public IActionResult GetBlogs()
        {
            using IDbConnection db = new SqlConnection(_connectionStringBuilder.ConnectionString);
            var lst = db.Query<TblBlog>(Querires.BlogList).ToList();
            return Ok(lst);
        }
        [HttpGet("{id}")]
        public IActionResult GetBlogs(int id)
        {
            using IDbConnection db = new SqlConnection(_connectionStringBuilder.ConnectionString);
            var item = db.Query<TblBlog>(Querires.BlogById, new {BlogId=id}).FirstOrDefault();
            if (item is null)
                return NotFound("No data found.");
            return Ok(item);
        }


        [HttpPost]
        public IActionResult CreateBlogs(TblBlog blog)
        {
            using IDbConnection db = new SqlConnection(_connectionStringBuilder.ConnectionString);
            int result=db.Execute(Querires.BlogCreate, blog);
            String message = result > 0 ? "Saving Successful." : "Saving Failed.";
            return Ok(message);
        }
        [HttpPut("{id}")]
        public IActionResult UpdateBlogs(int id, TblBlog blog)
        {
            blog.BlogId = id;
            using IDbConnection db = new SqlConnection(_connectionStringBuilder.ConnectionString);
            int result = db.Execute(Querires.BlogUpdate,blog);
            String message = result > 0 ? "Updating Successful." : "Updating Failed.";
            return Ok(message);
        }
        [HttpPatch]
        public IActionResult PatchBlogs()
        {
            return Ok("PatchBlog");
        }
        [HttpDelete("{id}")]
        
        public IActionResult DeleteBlogs(int id)
        {
            using IDbConnection db = new SqlConnection(_connectionStringBuilder.ConnectionString);
            int result = db.Execute(Querires.BlogDelete,new {BlogId=id});
            String message = result > 0 ? "Deleting Successful." : "Deleting Failed.";
            return Ok(message);
        }

    }

    public class TblBlog
    {
        public int BlogId { get; set; }
        public String BlogTitle { get; set; }
        public String BlogAuthor { get; set; }
        public String BlogContent { get; set; }

    }
    public static class Querires
    {
        public static string BlogList { get; } = @"SELECT [BlogId]
      ,[BlogTitle]
      ,[BlogAuthor]
      ,[BlogContent]
  FROM [dbo].[Tbl_Blog]";
        public static string BlogById { get; } = @"SELECT [BlogId]
      ,[BlogTitle]
      ,[BlogAuthor]
      ,[BlogContent]
  FROM [dbo].[Tbl_Blog] Where BlogId=@BlogId";

        public static string BlogCreate { get; } = @"INSERT INTO [dbo].[Tbl_Blog]
           ([BlogTitle]
           ,[BlogAuthor]
           ,[BlogContent])
     VALUES
           (@BlogTitle
           ,@BlogAuthor
           ,@BlogContent)";

        public static string BlogDelete { get; } = @"Delete from Tbl_Blog where BlogId = @BlogId";

        public static string BlogUpdate { get; } = @"UPDATE [dbo].[Tbl_Blog]
   SET [BlogTitle] = @BlogTitle
      ,[BlogAuthor] = @BlogAuthor
      ,[BlogContent] = @BlogContent
 WHERE BlogId = @BlogId";
    }
}
    