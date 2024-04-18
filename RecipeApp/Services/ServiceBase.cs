using RecipeApp.Context;

namespace RecipeApp.Services;

public abstract class ServiceBase {
    public SplankContext Context { get; set; } = new SplankContext();
}