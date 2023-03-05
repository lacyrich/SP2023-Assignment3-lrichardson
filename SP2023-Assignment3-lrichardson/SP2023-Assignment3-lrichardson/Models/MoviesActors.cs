using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace SP2023_Assignment3_lrichardson.Models
{
    public class MoviesActors
    {
        public int Id { get; set; }

        [ForeignKey("Movies")]
        public int? MovieID { get; set; }
        public Movies? Movies { get; set; }

        [ForeignKey("Actors")]
        public int? ActorID { get; set; }
        public Actors? Actors { get; set; }
        
    }
}
