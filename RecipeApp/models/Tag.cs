namespace RecipeApp.Models;

/// <summary>
/// Represents a tag to search for
/// </summary>
public class Tag {
    public string TagName { get; private set; }

    /// <summary>
    /// Constructor with tag name
    /// </summary>
    /// <param name="tagName">Name of tag</param>
    /// <exception cref="ArgumentException">If tag name is empty or null</exception>
    public Tag(string tagName) {
        if (tagName == null) throw new ArgumentException("Tag cannot be null");
        if (tagName.Length == 0) throw new ArgumentException("Tag cannot be empty");
        this.TagName = tagName;
    }
}