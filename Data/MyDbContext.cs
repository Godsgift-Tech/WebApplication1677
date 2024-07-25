using Microsoft.EntityFrameworkCore;
using WebApplication1677.Entities;

namespace WebApplication1677.Data
{
    public class MyDbContext:DbContext
    {

        public MyDbContext(DbContextOptions<MyDbContext>options): base(options)
        {
            
        }


        public DbSet<Product>Products { get; set; }
        public DbSet<CategoryEntity> CategoryEntities { get; set; }



    }

}
