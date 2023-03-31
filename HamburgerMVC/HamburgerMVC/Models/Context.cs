using HamburgerMVC.Models.Configurations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace HamburgerMVC.Models
{
    public class Context : IdentityDbContext<AppUser>
    {
        public DbSet<ExtraIngredient> ExtraIngredients { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new ExtraIngredientConfig()).ApplyConfiguration(new MenuConfig());
            builder.Entity<Order>().Property(x => x.Size).HasConversion<string>();


        }

        public Context(DbContextOptions<Context> opt) : base(opt)
        {

        }
    }
}
