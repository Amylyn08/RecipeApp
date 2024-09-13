namespace RecipeAppTest.searcher;
using Microsoft.EntityFrameworkCore;
using Moq;
using RecipeApp.Context;
using RecipeApp.Models;
using RecipeApp.Searcher;

[TestClass]
public class SearchAllUsersTest{

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void UserNameIsNullException(){
        //Arrange
        SplankContext context = SplankContext.GetInstance();
        string username = null!;

        //Act
        SearchAllUsers searcher = new(context, username);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void UserNameIsLengthZero(){
        //Arrange
        SplankContext context = SplankContext.GetInstance();
        string username = "";

        //Act
        SearchAllUsers searcher = new(context, username);
    }

    [TestMethod]
    public void SearchAllUserByUsernameReturnsCorrectList(){
        //Arrange
        var listData = new List<User>{
            new("Amyly", "human", "12345Human", new(), "salty"),
            new("Amy123", "human", "12345Human", new(), "salty"),
            new("Amy0912", "human", "12345Human", new(), "salty"),
            new("Alexandre", "human", "12345Human", new(), "salty"),
            new("Alessandra", "human", "12345Human", new(), "salty"),
            new("Mark09", "human", "12345Human", new(), "salty"),
            new("Prabhjot_10", "human", "12345Human", new(), "salty"),
            new("Rida1", "human", "12345Human",  new(), "salty"),
            new("Rida2", "human", "12345Human",  new(), "salty"),
            new("Rida3", "human", "12345Human",  new(), "salty"),
        };

        var data = listData.AsQueryable();

        var mockSet = new Mock<DbSet<User>>();
        mockSet.As<IQueryable<User>>().Setup(m => m.Provider).Returns(data.Provider);
        mockSet.As<IQueryable<User>>().Setup(m => m.Expression).Returns(data.Expression);
        mockSet.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(data.ElementType);
        mockSet.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

        var mockContext = new Mock<SplankContext>();
        mockContext.Setup(m => m.Users).Returns(mockSet.Object);
        SearchAllUsers searcher = new(mockContext.Object, "Amy");
        //shoudl return ignoring case sensitivity

        //Act
        List<User> filteredUsers = searcher.GetUserByName();

        //Assert
        Assert.AreEqual(3, filteredUsers.Count);
        Assert.AreEqual("Amyly", filteredUsers[0].Name);
        Assert.AreEqual("Amy123", filteredUsers[1].Name);
        Assert.AreEqual("Amy0912", filteredUsers[2].Name);

    }
}