using RecipeApp.Models;

namespace RecipeAppTest.Models;

[TestClass]
public class TagTest {
    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void NullName_Throws_ArgumentException() {
        Tag tag = new(null!);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void EmptyName_Throws_ArgumentException() {
        Tag tag = new("");
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void NameSetter_Null_Throws_ArgumentException() {
        Tag tag = new("Vegan");
        tag.TagName = null!;
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void NameSetter_Empty_Throws_ArgumentException() {
        Tag tag = new("Vegan");
        tag.TagName = "";
    }

    [TestMethod]
    public void Constructor_Init() {
        Tag tag = new("Vegan");
        Assert.AreEqual("Vegan", tag.TagName);
    }
}