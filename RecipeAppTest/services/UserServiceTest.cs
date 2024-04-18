using Microsoft.EntityFrameworkCore;
using Moq;
using RecipeApp.Context;
using RecipeApp.Models;
using RecipeApp.Security;
using RecipeApp.Services;


namespace RecipeAppTest.Services;

[TestClass]
public class UserServiceTest {
    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void NullEncrypterThrowsArgumentException() {
        IEncrypter? encrypter = null;
        ServiceBase userService = new UserService(encrypter);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void LoginNullUsernameThrowsArgumentException() {
        IEncrypter encrypter = new PasswordEncrypter();
        ServiceBase userService = new UserService(encrypter);
        ((UserService) userService).Login(null, "Password");
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void LoginNullPasswordThrowsArgumentException() {
        IEncrypter encrypter = new PasswordEncrypter();
        ServiceBase userService = new UserService(encrypter);
        ((UserService) userService).Login("Username123", null);
    }

    [TestMethod]
    public void LoginSuccessfullReturnsUser() {
        var listUser = new List<User>();
        listUser.Add(new User("Rida1", "I am rida 1", "Rida1Password", new(), new()));
        listUser.Add(new User("Rida2", "I am rida 2", "Rida2Password", new(), new()));
        listUser.Add(new User("Rida2", "I am rida 3", "Rida3Password", new(), new()));
        
        var data = listUser.AsQueryable();

        var mockSet = new Mock<DbSet<User>>();
        mockSet.As<IQueryable<User>>().Setup(m => m.Provider).Returns(data.Provider);
        mockSet.As<IQueryable<User>>().Setup(m => m.Expression).Returns(data.Expression);
        mockSet.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(data.ElementType);
        mockSet.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

        var mockContext = new Mock<SplankContext>();
        mockContext.Setup(m => m.Users).Returns(mockSet.Object);

        var encrypter = new PasswordEncrypter();
        var userService = new UserService(encrypter);

        var user = userService.Login("Rida1", "Rida1Password");

        Assert.AreEqual(user.Name, "Rida1");
        Assert.AreEqual(user.Description, "I am rida 1");
    }
}