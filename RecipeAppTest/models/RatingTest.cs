// namespace RecipeAppTest.Models;
// using RecipeApp.Models;

// [TestClass]
// public class RatingTest {
    
//     [TestMethod]
//     [ExpectedException(typeof(ArgumentException))]
//     public void IllegalStarsForRatingMoreThan5(){
//         //Arrange
//         int rating = 6;
//         //Act
//         //Rating r = new(rating,"mama", new User());
//     }

//     [TestMethod]
//     [ExpectedException(typeof(ArgumentException))]
//     public void IllegalStarsForRatingLessThan0(){
//         //Arrange
//         int rating = -1;
//         //Act
//        // Rating r = new(rating,"mama", new User());
//     }

//     [TestMethod]
//     public void StarsForRatingValid(){
//         //Arrange
//         int rating = 2;
//         //Act
//         //Rating r = new(rating, "mama", new User());
//         //Assert
//         Assert.AreEqual(2, r.__stars);
//     }
// }