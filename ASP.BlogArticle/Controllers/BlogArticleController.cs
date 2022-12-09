using ASP.BlogArticle.Core.Models;
using ASP.BlogArticle.DataAccess.ApplicationDbContext;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ASP.BlogArticle.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogArticleController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        public BlogArticleController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [HttpGet]
        public async Task<IEnumerable<Article>> GetArticlesAsync()
        {
            var articles = await _appDbContext.Articles.ToListAsync();
            return articles;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Article>> GetArticlesById(int id)
        {
            var article = await _appDbContext.Articles.FindAsync(id);
            if (article == null)
                return BadRequest("Article not found");

            return Ok(article);
        }

        [HttpPost]
        public async Task<IEnumerable<Article>> CreateArticle(Article article)
        {
            _appDbContext.Add(article);
            await _appDbContext.SaveChangesAsync();
            var articles = await _appDbContext.Articles.ToListAsync();
            return articles;
        }

        [HttpPut]
        public void Put(int id, [FromBody] Article article)
        {
            if (id != article.Id)
                throw new ArgumentException($"Article with {id} doesn't exist !");
            _appDbContext.Entry(article).State = EntityState.Modified;
            _appDbContext.SaveChanges();
        }

        [HttpDelete]
        public void Delete(int id)
        {
            var article = _appDbContext.Articles.SingleOrDefault(a=>a.Id==id);
            if (article == null)
                throw new ArgumentException($"Article with {id} doesn't exist !");
            _appDbContext.Remove(article);
            _appDbContext.SaveChanges();
        }
    }
}
