using RecipeApp.Models;

namespace RecipeAppTest.Models;

[TestClass]
public class UserTest {


    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void NullName_Throws_ArugumentException() {
        //Arrange
        List<Recipe> favorites = new List<Recipe>();
        List<Recipe> recipes = new List<Recipe>();
        //Act
        User Rida = new User(null, "rida was here", "PrabBoss123", favorites, recipes);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void NameNotEnoughCharacters_Throws_ArugumentException() {
        //Arrange
        List<Recipe> favorites = new List<Recipe>();
        List<Recipe> recipes = new List<Recipe>();
        //Act
        User Rida = new User("", "rida was here", "PrabBoss123", favorites, recipes);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void NameExceedsCharacters_Throws_ArugumentException() {
        //Arrange
        List<Recipe> favorites = new List<Recipe>();
        List<Recipe> recipes = new List<Recipe>();
        //Act
        User Rida = new User("Riiiiiiiiiiiiida", "rida was here", "PrabBoss123", favorites, recipes);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void NullPassword_Throws_ArugumentException() {
        //Arrange
        List<Recipe> favorites = new List<Recipe>();
        List<Recipe> recipes = new List<Recipe>();
        //Act
        User Rida = new User("Rida", "rida was here", null, favorites, recipes);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void PasswordNotEnoughCharacters_Throws_ArugumentException() {
        //Arrange
        List<Recipe> favorites = new List<Recipe>();
        List<Recipe> recipes = new List<Recipe>();
        //Act
        User Rida = new User("Rida", "rida was here", "Prab", favorites, recipes);
    }

    [TestMethod]
    public void NullDescription_BecomesEmptyString() {
        //Arrange
        List<Recipe> favorites = new List<Recipe>();
        List<Recipe> recipes = new List<Recipe>();
        //Act
        User Rida = new User("Rida", null, "PrabBoss123", favorites, recipes);
        //Assert
        Assert.AreEqual("", Rida.Description);
    }
    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void DescriptionExceedsCharacters_Throws_ArugumentException() {
        //Arrange
        List<Recipe> favorites = new List<Recipe>();
        List<Recipe> recipes = new List<Recipe>();
        string description = "The quick brown fox jumps over the lazy dog. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.a";
        //Act
        User Rida = new User("Rida", description, "PrabBoss123", favorites, recipes);
    }

    [TestMethod]
    public void Constructor_Passes() {
        //Arrange
        List<Recipe> favorites = new List<Recipe>();
        List<Recipe> recipes = new List<Recipe>();
        //Act
        User Rida = new User("Rida", "rida was here", "PrabBoss123", favorites, recipes);
        //Assert
        Assert.AreEqual("Rida", Rida.Name);
        Assert.AreEqual("rida was here", Rida.Description);
        Assert.AreEqual("PrabBoss123", Rida.Password);
        Assert.AreEqual(0, Rida.Favorites.Count);
        Assert.AreEqual(0, Rida.MadeRecipes.Count);
    }
}