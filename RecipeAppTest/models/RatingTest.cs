namespace RecipeAppTest.Models;
using RecipeApp.Models;

[TestClass]
public class RatingTest {
    
    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void IllegalStarsForRatingMoreThan5(){
        //Arrange
        int rating = 6;
        User user = new("name", "mama","passsssss",new List<Recipe>(), "salt");
        //Act
        Rating r = new(rating,"mama", user);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void IllegalStarsForRatingLessThan0(){
        //Arrange
        int rating = -1;
        
        //Act
        Rating r = new(rating,"mama", new User("name", "mama","passssssss", new List<Recipe>(), "salt"));
    }

    [TestMethod]
    public void StarsForRatingValid(){
        //Arrange
        int rating = 2;
        //Act
        Rating r = new(rating, "mama", new User("name", "mama","passsssssss", new List<Recipe>(), "salt"));
        //Assert
        Assert.AreEqual(2, r.Stars);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void descriptionExceeds256Characters(){
        //Arrange
        string description="Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vivamus euismod velit ut lorem venenatis, eu tempus ante interdum. Nulla facilisi. Nunc sit amet ex id sapien fringilla tristique eu id sapien. Ut pretium lectus sed dui mattis, quis fermentum nunc dapibus. Donec euismod purus ut nisl fermentum aliquet. Integer et massa ac velit accumsan fermentum. Sed vitae orci nec massa rhoncus fermentum. Fusce ultricies, lorem sed feugiat commodo, turpis enim mattis risus, vel fringilla libero neque ac eros. Proin eu volutpat ligula. Curabitur ultricies sapien a sagittis fermentum. Phasellus aliquam vestibulum urna ac fermentum. Suspendisse potenti. Duis semper metus vel purus bibendum, sit amet commodo sem fermentum. Vivamus et ante in urna condimentum ultrices eget id lectus.";

        //Act
        Rating r = new(3, description, new User("name", "mama","passsssssss",new List<Recipe>(), "salt"));
    }
    // [TestMethod]
    // public void DescriptionIsEmptyStringIfNull(){
    //     //Arrange
    //     string desc = null;
    //     //Act
    //     Rating r = new(5, desc, new User("name", "mama","passsssssss",new List<Recipe>(), new List<Recipe>(), "salt"));
    //     //Assert
    //     Assert.AreEqual(r.Description, "");
    // }

    [TestMethod]
    public void DescriptionIsValid(){
        //Arrange
        string desc = "This is a description";
        //Act
        Rating r = new(5, desc, new User("name", "mama","passsssssss",new List<Recipe>(), "salt"));
        //Assert 
        Assert.AreEqual(r.Description, "This is a description");
    }

}   