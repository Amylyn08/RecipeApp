using Microsoft.EntityFrameworkCore;
using RecipeApp.Context;

namespace RecipeApp.Services;

public abstract class ServiceBase {
    public DbContext Context { get; set; } = new SplankContext();
}