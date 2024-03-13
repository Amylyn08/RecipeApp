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
    public void NameLo_Throws_ArugumentException() {
        //Arrange
        List<Recipe> favorites = new List<Recipe>();
        //Act
        User Rida = new User(null, "rida was here", "PrabBoss123", favorites);
        //Assert
    }
}