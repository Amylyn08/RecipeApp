namespace RecipeApp.Models;

/// <summary>
/// Represents a tag to search for
/// </summary>
public class Tag {
    private string? _tagName = null!;
   
    public int TagId { 
        get; 
        set; 
    }

    public List<Recipe>? Recipes {
        get; 
        set;
    }

    public string? TagName { 
        get => _tagName; 
        set {
            if (value == null) 
                throw new ArgumentException("Tag cannot be null");
            if (value.Length == 0) 
                throw new ArgumentException("Tag cannot be empty");
            _tagName = value;
        } 
    }

    /// <summary>
    /// Constructor with tag name
    /// </summary>
    /// <param name="tagName">Name of tag</param>
    /// <exception cref="ArgumentException">If tag name is empty or null</exception>
    public Tag(string tagName) {
        TagName = tagName;
    }

    /// <summary>
    /// Empty constructor for entity framework
    /// </summary>
    public Tag() {

    }

    /// <summary>
    /// Overriden ToString()
    /// </summary>
    /// <returns>String representation of a tag</returns>
    public override string? ToString() {
        return TagName;
    }
}