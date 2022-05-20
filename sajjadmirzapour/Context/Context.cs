using Microsoft.EntityFrameworkCore;
using sajjadmirzapour.Models;

namespace sajjadmirzapour
{
    public class Context : DbContext
    {




        public Context(DbContextOptions<Context> dbContextOptions) : base(dbContextOptions)
        {

        }
        public DbSet<News> News { get; set; }
        public DbSet<ProNews> ProNews { get; set; }
        public DbSet<ShortNews> ShortNews { get; set; }
        public DbSet<ShortTextNews> ShortTextNews { get; set; }




        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            base.OnModelCreating(modelBuilder);
        }




        public DbSet<sajjadmirzapour.Models.Banner> Banner { get; set; }





    }
}
