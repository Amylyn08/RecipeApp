using RecipeApp.Models;

namespace RecipeAppTest.Models;

[TestClass]
public class UserTest {


    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void NullName_Throws_ArugumentException() {
        //Arrange
        List<Recipe> favorites = new List<Recipe>();
        //Act
        User Rida = new User(null, "rida was here", "PrabBoss123", favorites);
        //Assert
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void NameNotEnoughCharacters_Throws_ArugumentException() {
        //Arrange
        List<Recipe> favorites = new List<Recipe>();
        //Act
        User Rida = new User("", "rida was here", "PrabBoss123", favorites);
        //Assert
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void NameExceedsCharacters_Throws_ArugumentException() {
        //Arrange
        List<Recipe> favorites = new List<Recipe>();
        //Act
        User Rida = new User("Riiiiiiiiiiiiida", "rida was here", "PrabBoss123", favorites);
        //Assert
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void NullPassword_Throws_ArugumentException() {
        //Arrange
        List<Recipe> favorites = new List<Recipe>();
        //Act
        User Rida = new User("Rida", "rida was here", null, favorites);
        //Assert
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void PasswordNotEnoughCharacters_Throws_ArugumentException() {
        //Arrange
        List<Recipe> favorites = new List<Recipe>();
        //Act
        User Rida = new User("Rida", "rida was here", "Prab", favorites);
        //Assert
    }
}