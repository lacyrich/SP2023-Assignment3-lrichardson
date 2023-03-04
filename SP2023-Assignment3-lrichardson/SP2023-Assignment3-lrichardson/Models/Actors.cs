namespace SP2023_Assignment3_lrichardson.Models
{
    public class Actors
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
        public string IMDBHyperlink { get; set; }
        public byte[]? Photo { get; set; }
    }
}
