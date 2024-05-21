using RecipeApp.Context;

namespace RecipeApp.Services;

/// <summary>
/// Gives each service a Context field
/// </summary>
public abstract class ServiceBase {
    public SplankContext Context { get; private set; }

    /// <summary>
    /// Construcs a new service base
    /// </summary>
    /// <param name="context">Db connection</param>
    public ServiceBase(SplankContext context) {
        Context = context;
    }
}