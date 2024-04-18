using Microsoft.EntityFrameworkCore;
using RecipeApp.Models;

namespace RecipeApp.Context;

public class SplankContext : DbContext {
    public virtual DbSet<User> Users { get; set; }
    public virtual DbSet<Rating> Ratings { get; set; }
    public virtual DbSet<Recipe> Recipes { get; set; }
    public virtual DbSet<Ingredient> Ingredients { get; set; }
    public virtual DbSet<Step> Steps { get; set; }
    public virtual DbSet<Favourite> Favourites { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
        string? username = Environment.GetEnvironmentVariable("ORA_USER");
        string? password = Environment.GetEnvironmentVariable("ORA_PASSWORD");
        optionsBuilder.UseOracle($"User Id={username}; Password={password};Data Source=198.168.52.211:1521/pdbora19c.dawsoncollege.qc.ca;");
    }
}