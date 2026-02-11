using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace App.Api.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class BlogController : ControllerBase
    {

        private static List<Article> articles = new();

        [HttpGet]
        [ProducesResponseType(typeof(List<Article>), StatusCodes.Status200OK)]       

        public IActionResult GetArticles()
        {
            return Ok(articles);
        }


        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Article), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public IActionResult getArticleById(int id)
        {
            var article = articles.Find(a => a.Id == id);
            if (article == null)
            {
                return NotFound();
            }
            return Ok(article);

        }

        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(Article), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult CreateArticle([FromBody] ArticleCreateDto dtoArticle)
        {

            var newArticle = new Article
            { 
            Title = dtoArticle.Title,
            Content = dtoArticle.Content,
            Id = articles.Count + 1 
            };

            if (string.IsNullOrWhiteSpace(newArticle.Title))
            {
                return BadRequest("Makale başlığı boş olamaz.");
            }

            articles.Add(newArticle);

            return CreatedAtAction(nameof(getArticleById), new { id = newArticle.Id }, newArticle);

        }


        [HttpPut("{id}")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(Article), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public IActionResult UpdateArticle(int id, [FromBody] ArticleCreateDto updatedArticle)
        {
            if (string.IsNullOrWhiteSpace(updatedArticle.Title))
            {
                return BadRequest("Makale başlığı boş olamaz..");
            }

            var article = articles.Find(a => a.Id == id);

            if (article == null)
            {
                return BadRequest("Makale bulunamadı.");
            }
            article.Title = updatedArticle.Title;
            article.Content = updatedArticle.Content;
            return Ok(article);

        }



        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(Article), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult DeleteArticle(int id)
        {
            var article = articles.Find(a => a.Id == id);

            if (article == null)
            {
                return NoContent();
            }
            articles.Remove(article);
            return Ok(article);
        }

    }
}
