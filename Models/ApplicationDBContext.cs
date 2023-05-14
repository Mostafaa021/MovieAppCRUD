using Microsoft.EntityFrameworkCore;


namespace MovieAppCRUD.Models
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext() : base()
        {
            
        }
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
            
        } 

        public DbSet<Genre> Genres { get; set; }
        public DbSet<Movie> Movies { get; set; }

       
    }
}
