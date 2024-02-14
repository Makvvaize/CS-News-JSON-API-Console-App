using System.Reflection.Metadata;
using Newtonsoft.Json;
using System.Net.Http;



namespace NewsJson
{
    public class Program
    {
        private static HttpClient newsapi = new HttpClient();

        public static async Task Main(string[] args)
        {
            await GetNews();
        }

        public static async Task GetNews()
        {
            string apiKey = "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx"; // Replace with your News API key
            string newsUrl = $"https://newsapi.org/v2/top-headlines?country=us&apiKey={apiKey}";

            try
            {
                HttpResponseMessage response = await newsapi.GetAsync(newsUrl);
                if (response.IsSuccessStatusCode)
                {
                    string newsResponse = await response.Content.ReadAsStringAsync();

                    // Deserialize the JSON response
                    News news = JsonConvert.DeserializeObject<News>(newsResponse);

                    // Access the articles
                    foreach (Article article in news.articles)
                    {
                        Console.WriteLine($"Title: {article.title}");
                        Console.WriteLine($"Author: {article.author}");
                        Console.WriteLine($"Description: {article.description}");
                        Console.WriteLine($"URL: {article.url}");
                        Console.WriteLine();
                    }
                }
                else
                {
                    Console.WriteLine($"Failed to fetch news. Status code: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }

    public class News
    {
        public string status { get; set; }
        public int totalResults { get; set; }
        public List<Article> articles { get; set; }
    }

    public class Article
    {
        public string id { get; set; }
        public string name { get; set; }
        public string author { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string url { get; set; }
        public string urlToImage { get; set; }
        public string publishedAt { get; set; }
        public string content { get; set; }
    }
}
