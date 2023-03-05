using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SP2023_Assignment3_lrichardson.Models;

namespace SP2023_Assignment3_lrichardson.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<SP2023_Assignment3_lrichardson.Models.Actors> Actors { get; set; }
        public DbSet<SP2023_Assignment3_lrichardson.Models.Movies> Movies { get; set; }
        public DbSet<SP2023_Assignment3_lrichardson.Models.MoviesActors> MoviesActors { get; set; }
    }
}