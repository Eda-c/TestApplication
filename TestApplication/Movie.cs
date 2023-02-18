using static System.Net.Mime.MediaTypeNames;

namespace TestApplication
{
    public class Movie
    {
        public int id { get; set; }
        public string title { get; set; }
        public string? description { get; set; }
        public float rating { get; set; }
        public string? image { get; set; }   
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
    }
}
