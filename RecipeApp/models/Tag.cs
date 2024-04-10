namespace RecipeApp.Models;

/// <summary>
/// Represents a tag to search for
/// </summary>
public class Tag {
    private string _tagName;
    public Tag() {}

    public int TagId { get; set; }
    public List<Recipe> Tagged { get; set; }

    public string TagName { 
        get => _tagName; 
        set {
            CheckTagName(value);
            _tagName = value;
        } 
    }

    /// <summary>
    /// Constructor with tag name
    /// </summary>
    /// <param name="tagName">Name of tag</param>
    /// <exception cref="ArgumentException">If tag name is empty or null</exception>
    public Tag(string tagName) {
        CheckTagName(tagName);
        _tagName = tagName;
    }

    /// <summary>
    /// Validate the tag name
    /// </summary>
    /// <param name="tagName">Tag name</param>
    /// <exception cref="ArgumentException">If null or empty</exception>
    private static void CheckTagName(string tagName) {
        if (tagName == null) 
            throw new ArgumentException("Tag cannot be null");
        if (tagName.Length == 0) 
            throw new ArgumentException("Tag cannot be empty");
    }

    public override string ToString()
    {
        return TagName;
    }
}