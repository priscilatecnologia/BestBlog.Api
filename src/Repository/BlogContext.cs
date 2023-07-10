using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Model;

namespace Repository
{
    // this is used for our verification tests, don't rename or change the access modifier
    public class BlogContext : DbContext
    {

        protected readonly IConfiguration _configuration;

        public BlogContext(DbContextOptions<BlogContext> options, IConfiguration configuration) : base(options) 
        {
            _configuration = configuration;
        }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<Post> Posts { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("WebApiDatabase"));
        }
    }

}