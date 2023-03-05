namespace SP2023_Assignment3_lrichardson.Models
{
    public class ActorsMoviesVM
    {
        public Actors Actors { get; set; }   
        public Movies Movies { get; set; }
        public List<MoviesActors> MoviesActors { get; set; }
    }
}
