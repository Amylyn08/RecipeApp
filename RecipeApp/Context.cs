using Microsoft.EntityFrameworkCore;
using RecipeApp.Models;

namespace RecipeApp.Context;

public class SplankContext : DbContext {
    public DbSet<User> Users { get; set; }
    //public DbSet<Rating> Ratings { get; set; }
    public DbSet<Recipe> Recipes { get; set; }
    public DbSet<Ingredient> Ingredients { get; set; }
    public DbSet<Step> Steps { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
        string? username = Environment.GetEnvironmentVariable("ORA_USER");
        string? password = Environment.GetEnvironmentVariable("ORA_PASSWORD");
        optionsBuilder.UseOracle($"User Id={username}; Password={password};Data Source=198.168.52.211:1521/pdbora19c.dawsoncollege.qc.ca;");
    }

}