using RecipeApp.Models;

namespace RecipeAppTest.Models;

[TestClass]
public class TagTest {
    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void NullName_Throws_ArgumentException() {
        Tag tag = new Tag(null);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void EmptyName_Throws_ArgumentException() {
        Tag tag = new Tag("");
    }

    [TestMethod]
    public void Constructor_Init() {
        Tag tag = new Tag("Vegan");
        Assert.AreEqual("Vegan", tag.TagName);
    }
}