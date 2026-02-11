namespace App.Api
{
    public class Article : ArticleCreateDto
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }
    }
    public class ArticleCreateDto
       
    {
        public string Title { get; set; }
        public string Content { get; set; }
    }
}
