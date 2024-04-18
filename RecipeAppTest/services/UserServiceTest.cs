using Microsoft.EntityFrameworkCore;
using Moq;
using RecipeApp.Context;
using RecipeApp.Exceptions;
using RecipeApp.Models;
using RecipeApp.Security;
using RecipeApp.Services;


namespace RecipeAppTest.Services;

[TestClass]
public class UserServiceTest {
    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void NullEncrypterThrowsArgumentException() {
        UserService userService = new(new(), null);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void LoginNullUsernameThrowsArgumentException() {
        UserService userService = new UserService(new(), new PasswordEncrypter());
        userService.Login(null, "Password");
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void LoginNullPasswordThrowsArgumentException() {
        UserService userService = new UserService(new(), new PasswordEncrypter());
        userService.Login("Username123", null);
    }

    [TestMethod]
    public void LoginSuccessfullReturnsUser() {
        PasswordEncrypter passwordEncrypter = new PasswordEncrypter();
        
        var salt1 = passwordEncrypter.CreateSalt();
        var salt2 = passwordEncrypter.CreateSalt();
        var salt3 = passwordEncrypter.CreateSalt();

        var password1 = passwordEncrypter.CreateHash("Rida1Password", salt1);
        var password2 = passwordEncrypter.CreateHash("Rida1Password", salt2);
        var password3 = passwordEncrypter.CreateHash("Rida1Password", salt3);

        var listUser = new List<User>();
        listUser.Add(new User("Rida1", "I am rida 1", password1, new(), new(), salt1));
        listUser.Add(new User("Rida2", "I am rida 2", password2, new(), new(), salt2));
        listUser.Add(new User("Rida2", "I am rida 3", password3, new(), new(), salt3));
        
        var data = listUser.AsQueryable();

        var mockSet = new Mock<DbSet<User>>();
        mockSet.As<IQueryable<User>>().Setup(m => m.Provider).Returns(data.Provider);
        mockSet.As<IQueryable<User>>().Setup(m => m.Expression).Returns(data.Expression);
        mockSet.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(data.ElementType);
        mockSet.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

        var mockContext = new Mock<SplankContext>();
        mockContext.Setup(m => m.Users).Returns(mockSet.Object);

        var encrypter = new PasswordEncrypter();
        UserService userService = new(mockContext.Object, encrypter);

        var user = userService.Login("Rida1", "Rida1Password");

        Assert.AreEqual(user.Name, "Rida1");
        Assert.AreEqual(user.Description, "I am rida 1");
    }

    [TestMethod]
    [ExpectedException(typeof(UserDoesNotExistException))]
    public void LoginNonExistentUserThrowsUserDoesNotException() {
        PasswordEncrypter passwordEncrypter = new PasswordEncrypter();
        
        var salt1 = passwordEncrypter.CreateSalt();
        var salt2 = passwordEncrypter.CreateSalt();
        var salt3 = passwordEncrypter.CreateSalt();

        var password1 = passwordEncrypter.CreateHash("Rida1Password", salt1);
        var password2 = passwordEncrypter.CreateHash("Rida1Password", salt2);
        var password3 = passwordEncrypter.CreateHash("Rida1Password", salt3);

        var listUser = new List<User>();
        listUser.Add(new User("Rida1", "I am rida 1", password1, new(), new(), salt1));
        listUser.Add(new User("Rida2", "I am rida 2", password2, new(), new(), salt2));
        listUser.Add(new User("Rida2", "I am rida 3", password3, new(), new(), salt3));
        
        var data = listUser.AsQueryable();

        var mockSet = new Mock<DbSet<User>>();
        mockSet.As<IQueryable<User>>().Setup(m => m.Provider).Returns(data.Provider);
        mockSet.As<IQueryable<User>>().Setup(m => m.Expression).Returns(data.Expression);
        mockSet.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(data.ElementType);
        mockSet.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

        var mockContext = new Mock<SplankContext>();
        mockContext.Setup(m => m.Users).Returns(mockSet.Object);

        var encrypter = new PasswordEncrypter();
        UserService userService = new(mockContext.Object, encrypter);

        var user = userService.Login("Rida4", "Rida1Password");
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidCredentialsException))]
    public void LoginBadPasswordThrowsInvalidCredentialsException() {
        PasswordEncrypter passwordEncrypter = new PasswordEncrypter();
        
        var salt1 = passwordEncrypter.CreateSalt();
        var salt2 = passwordEncrypter.CreateSalt();
        var salt3 = passwordEncrypter.CreateSalt();

        var password1 = passwordEncrypter.CreateHash("Rida1Password", salt1);
        var password2 = passwordEncrypter.CreateHash("Rida1Password", salt2);
        var password3 = passwordEncrypter.CreateHash("Rida1Password", salt3);

        var listUser = new List<User>();
        listUser.Add(new User("Rida1", "I am rida 1", password1, new(), new(), salt1));
        listUser.Add(new User("Rida2", "I am rida 2", password2, new(), new(), salt2));
        listUser.Add(new User("Rida2", "I am rida 3", password3, new(), new(), salt3));
        
        var data = listUser.AsQueryable();

        var mockSet = new Mock<DbSet<User>>();
        mockSet.As<IQueryable<User>>().Setup(m => m.Provider).Returns(data.Provider);
        mockSet.As<IQueryable<User>>().Setup(m => m.Expression).Returns(data.Expression);
        mockSet.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(data.ElementType);
        mockSet.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

        var mockContext = new Mock<SplankContext>();
        mockContext.Setup(m => m.Users).Returns(mockSet.Object);

        var encrypter = new PasswordEncrypter();
        UserService userService = new(mockContext.Object, encrypter);

        var user = userService.Login("Rida1", "Rida4Password");
    }
}