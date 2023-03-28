using Entitys.Abstract;
using Microsoft.EntityFrameworkCore;


namespace EntityFramework.Context
{
    public class ZDbContext : DbContext
    {
        public ZDbContext(DbContextOptions<ZDbContext> options) : base(options) { }

        public DbSet<Basket> Baskets { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Campaign> Campaigns { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<Favority> Favorities { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Model> Models { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Shoe> Shoes { get; set; }
        public DbSet<User> Users { get; set; }

    }
}
