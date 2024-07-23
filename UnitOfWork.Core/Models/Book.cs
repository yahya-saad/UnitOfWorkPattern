using System.Text.Json.Serialization;

namespace UnitOfWork.Core.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int AuthorId { get; set; }
        [JsonIgnore]
        public Author? Author { get; set; }
    }
}
