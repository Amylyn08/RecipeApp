namespace RecipeAppTest.Models;
using RecipeApp.Models;

[TestClass]
public class RatingTest {
    
    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void IllegalStarsForRatingMoreThan5(){
        //Arrange
        int rating = 6;
        //Act
        Rating r = new(rating,"mama", new User("name", "mama","passsssss",new List<Recipe>()));
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void IllegalStarsForRatingLessThan0(){
        //Arrange
        int rating = -1;
        //Act
        Rating r = new(rating,"mama", new User("name", "mama","passssssss",new List<Recipe>()));
    }

    [TestMethod]
    public void StarsForRatingValid(){
        //Arrange
        int rating = 2;
        //Act
        Rating r = new(rating, "mama", new User("name", "mama","passsssssss",new List<Recipe>()));
        //Assert
        Assert.AreEqual(2, r.__stars);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void descriptionExceeds256Characters(){
        //Arrange
        string description="Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vivamus euismod velit ut lorem venenatis, eu tempus ante interdum. Nulla facilisi. Nunc sit amet ex id sapien fringilla tristique eu id sapien. Ut pretium lectus sed dui mattis, quis fermentum nunc dapibus. Donec euismod purus ut nisl fermentum aliquet. Integer et massa ac velit accumsan fermentum. Sed vitae orci nec massa rhoncus fermentum. Fusce ultricies, lorem sed feugiat commodo, turpis enim mattis risus, vel fringilla libero neque ac eros. Proin eu volutpat ligula. Curabitur ultricies sapien a sagittis fermentum. Phasellus aliquam vestibulum urna ac fermentum. Suspendisse potenti. Duis semper metus vel purus bibendum, sit amet commodo sem fermentum. Vivamus et ante in urna condimentum ultrices eget id lectus.";

        //Act
        Rating r = new(3, description, new User("name", "mama","passsssssss",new List<Recipe>()));
    }
    [TestMethod]
    public void DescriptionIsEmptyStringIfNull(){
        //Arrange
        string desc = null;
        //Act
        Rating r = new(5, desc, new User("name", "mama","passsssssss",new List<Recipe>()));
        //Assert
        StringAssert.Equals(r.__description, "");
    }

}   