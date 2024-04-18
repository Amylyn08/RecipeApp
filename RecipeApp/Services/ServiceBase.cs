using RecipeApp.Context;

namespace RecipeApp.Services;

public abstract class ServiceBase {
    public static SplankContext Context { get; set; } = new SplankContext();
}