using RecipeApp.Models;

namespace RecipeAppTest.Models;

[TestClass]
public class UserTest {


    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void NullName_Throws_ArugumentException() {
        //Act
        User Rida = new User(null!, "rida was here", "PrabBoss123", new List<Recipe>(), "Salt");
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void NameNotEnoughCharacters_Throws_ArugumentException() {
        //Act
        User Rida = new User("", "rida was here", "PrabBoss123", new List<Recipe>(), "Salt");
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void NameExceedsCharacters_Throws_ArugumentException() {
        //Act
        User Rida = new User("Riiiiiiiiiiiiida", "rida was here", "PrabBoss123", new List<Recipe>(), "Salt");
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void NullPassword_Throws_ArugumentException() {
        //Act
        User Rida = new User("Rida", "rida was here", null!, new List<Recipe>(), "Salt");
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void PasswordNotEnoughCharacters_Throws_ArugumentException() {
        //Act
        User Rida = new User("Rida", "rida was here", "Prab", new List<Recipe>(), "Salt");
    }

    [TestMethod]
    public void NullDescription_BecomesEmptyString() {
        //Act
        User Rida = new User("Rida", null!, "PrabBoss123", new List<Recipe>(), "Salt");
        //Assert
        Assert.AreEqual("", Rida.Description);
    }
    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void DescriptionExceedsCharacters_Throws_ArugumentException() {
        //Arrange
        string description = "The quick brown fox jumps over the lazy dog. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.a";
        //Act
        User Rida = new User("Rida", description, "PrabBoss123", new List<Recipe>(), "Salt");
    }

    [TestMethod]
    public void Constructor_Passes() {
        //Act
        User Rida = new User("Rida", "rida was here", "PrabBoss123", new List<Recipe>(), "Salt");
        //Assert
        Assert.AreEqual("Rida", Rida.Name);
        Assert.AreEqual("rida was here", Rida.Description);
        Assert.AreEqual("PrabBoss123", Rida.Password);
        Assert.AreEqual(0, Rida.MadeRecipes!.Count);
    }
}