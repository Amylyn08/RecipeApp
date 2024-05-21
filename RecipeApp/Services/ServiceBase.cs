using RecipeApp.Context;

namespace RecipeApp.Services;

public abstract class ServiceBase {
    public SplankContext Context { get; private set; }

    public ServiceBase(SplankContext context) {
        Context = context;
    }
}