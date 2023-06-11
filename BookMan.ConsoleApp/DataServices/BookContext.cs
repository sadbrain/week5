using Microsoft.EntityFrameworkCore;


namespace BookMan.ConsoleApp.DataServices
{
    using Models;
    public class SchoolContext : DbContext
    {
        public DbSet<Book> Books { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS;Database=SchoolDB;Trusted_Connection=True;");
        }
    }

}
