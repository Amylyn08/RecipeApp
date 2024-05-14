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
    public virtual DbSet<Tag> Tags { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
        string? username = Environment.GetEnvironmentVariable("ORA_USER");
        string? password = Environment.GetEnvironmentVariable("ORA_PASSWORD");
        optionsBuilder.UseOracle($"User Id={username}; Password={password};Data Source=198.168.52.211:1521/pdbora19c.dawsoncollege.qc.ca;");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        modelBuilder.Entity<User>().Property(prop => prop.ProfilePicture).HasColumnType("BLOB");
    }

    public SplankContext() {} // keep it public for the tests

    public static SplankContext instance = null!;

    public static SplankContext GetInstance() {
        if (instance == null) {
            instance = new SplankContext();
        }
        return instance;
    }
}