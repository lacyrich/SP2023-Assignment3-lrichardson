using System.ComponentModel.DataAnnotations.Schema;

namespace SP2023_Assignment3_lrichardson.Models
{
    public class Movies
    {
        public int Id { get; set; }
        public string MovieTitle { get; set; }
        public string IMDBHyperlink { get; set; }
        public string Genre { get; set; }
        public int ReleaseYear { get; set; }
        public byte[]? Poster { get; set; }
    }
}
