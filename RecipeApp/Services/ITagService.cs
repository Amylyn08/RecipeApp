using RecipeApp.Models;

namespace RecipeApp.Services;

public interface ITagService {
    public List<Tag> GetAvailableTags();
}