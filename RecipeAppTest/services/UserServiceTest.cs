using Microsoft.EntityFrameworkCore;
using Moq;
using RecipeApp.Context;
using RecipeApp.Exceptions;
using RecipeApp.Models;
using RecipeApp.Security;
using RecipeApp.Services;
using System;
using System.IO;

namespace RecipeAppTest.Services;

[TestClass]
public class UserServiceTest {
    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void NullEncrypterThrowsArgumentException() {
        // act
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
        UserService userService = new(SplankContext.GetInstance(), null);
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void LoginNullUsernameThrowsArgumentException() {
        // arrange
        UserService userService = new UserService(SplankContext.GetInstance(), new PasswordEncrypter());
        // act
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
        userService.Login(null, "Password");
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void LoginNullPasswordThrowsArgumentException() {
        // arrange
        UserService userService = new UserService(SplankContext.GetInstance(), new PasswordEncrypter());
        // act
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
        userService.Login("Username123", null);
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
    }

    [TestMethod]
    public void LoginSuccessfullReturnsUser() {
        // arrange
        PasswordEncrypter passwordEncrypter = new PasswordEncrypter();
        
        var salt1 = passwordEncrypter.CreateSalt();
        var salt2 = passwordEncrypter.CreateSalt();
        var salt3 = passwordEncrypter.CreateSalt();

        var password1 = passwordEncrypter.CreateHash("Rida1Password", salt1);
        var password2 = passwordEncrypter.CreateHash("Rida1Password", salt2);
        var password3 = passwordEncrypter.CreateHash("Rida1Password", salt3);

        var listUser = new List<User>();
        listUser.Add(new User("Rida1", "I am rida 1", password1, new(), salt1));
        listUser.Add(new User("Rida2", "I am rida 2", password2, new(), salt2));
        listUser.Add(new User("Rida2", "I am rida 3", password3, new(), salt3));
        
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

        // act
        var user = userService.Login("Rida1", "Rida1Password");

        // assert
        Assert.AreEqual(user.Name, "Rida1");
        Assert.AreEqual(user.Description, "I am rida 1");
    }

    [TestMethod]
    [ExpectedException(typeof(UserDoesNotExistException))]
    public void LoginNonExistentUserThrowsUserDoesNotException() {
        // arrange
        PasswordEncrypter passwordEncrypter = new PasswordEncrypter();
        
        var salt1 = passwordEncrypter.CreateSalt();
        var salt2 = passwordEncrypter.CreateSalt();
        var salt3 = passwordEncrypter.CreateSalt();

        var password1 = passwordEncrypter.CreateHash("Rida1Password", salt1);
        var password2 = passwordEncrypter.CreateHash("Rida1Password", salt2);
        var password3 = passwordEncrypter.CreateHash("Rida1Password", salt3);

        var listUser = new List<User>();
        listUser.Add(new User("Rida1", "I am rida 1", password1, new(), salt1));
        listUser.Add(new User("Rida2", "I am rida 2", password2, new(), salt2));
        listUser.Add(new User("Rida3", "I am rida 3", password3, new(), salt3));
        
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

        // act
        var user = userService.Login("Rida4", "Rida1Password");
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidCredentialsException))]
    public void LoginBadPasswordThrowsInvalidCredentialsException() {
        // arrange
        PasswordEncrypter passwordEncrypter = new();
        
        var salt1 = passwordEncrypter.CreateSalt();
        var salt2 = passwordEncrypter.CreateSalt();
        var salt3 = passwordEncrypter.CreateSalt();

        var password1 = passwordEncrypter.CreateHash("Rida1Password", salt1);
        var password2 = passwordEncrypter.CreateHash("Rida1Password", salt2);
        var password3 = passwordEncrypter.CreateHash("Rida1Password", salt3);

        var listUser = new List<User>();
        listUser.Add(new User("Rida1", "I am rida 1", password1, new(), salt1));
        listUser.Add(new User("Rida2", "I am rida 2", password2, new(), salt2));
        listUser.Add(new User("Rida3", "I am rida 3", password3, new(), salt3));
        
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

        // act
        var user = userService.Login("Rida1", "Rida4Password");
    }

    [TestMethod]
    public void RegisterSucessfullyAddsUser() {
        // arrange
        PasswordEncrypter passwordEncrypter = new();
        
        var salt1 = passwordEncrypter.CreateSalt();
        var salt2 = passwordEncrypter.CreateSalt();
        var salt3 = passwordEncrypter.CreateSalt();

        var password1 = passwordEncrypter.CreateHash("Rida1Password", salt1);
        var password2 = passwordEncrypter.CreateHash("Rida1Password", salt2);
        var password3 = passwordEncrypter.CreateHash("Rida1Password", salt3);

        var listUser = new List<User>();
        listUser.Add(new User("Rida1", "I am rida 1", password1, new(), salt1));
        listUser.Add(new User("Rida2", "I am rida 2", password2, new(), salt2));
        listUser.Add(new User("Rida3", "I am rida 3", password3, new(), salt3));
        
        var data = listUser.AsQueryable();

        var mockSet = new Mock<DbSet<User>>();
        mockSet.As<IQueryable<User>>().Setup(m => m.Provider).Returns(data.Provider);
        mockSet.As<IQueryable<User>>().Setup(m => m.Expression).Returns(data.Expression);
        mockSet.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(data.ElementType);
        mockSet.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

        var mockContext = new Mock<SplankContext>();
        mockContext.Setup(m => m.Users).Returns(mockSet.Object);

        var encrypter = new PasswordEncrypter();
        UserService userService = new UserService(mockContext.Object, encrypter);

        // act
        userService.Register("Rida4", "Rida4Password", "I am Rida4");

        // assert
        mockContext.Verify(m => m.Add(It.IsAny<User>()), Times.Once());
        mockContext.Verify(m => m.SaveChanges(), Times.Once());  
    }

    [TestMethod]
    [ExpectedException(typeof(UserAlreadyExistsException))]
    public void RegisterThrowsUserAlreadyExistsException() {
        // arrange
        PasswordEncrypter passwordEncrypter = new();
        
        var salt1 = passwordEncrypter.CreateSalt();
        var salt2 = passwordEncrypter.CreateSalt();
        var salt3 = passwordEncrypter.CreateSalt();

        var password1 = passwordEncrypter.CreateHash("Rida1Password", salt1);
        var password2 = passwordEncrypter.CreateHash("Rida1Password", salt2);
        var password3 = passwordEncrypter.CreateHash("Rida1Password", salt3);

        var listUser = new List<User>();
        listUser.Add(new User("Rida1", "I am rida 1", password1, new(), salt1));
        listUser.Add(new User("Rida2", "I am rida 2", password2, new(), salt2));
        listUser.Add(new User("Rida3", "I am rida 3", password3, new(), salt3));
        
        var data = listUser.AsQueryable();

        var mockSet = new Mock<DbSet<User>>();
        mockSet.As<IQueryable<User>>().Setup(m => m.Provider).Returns(data.Provider);
        mockSet.As<IQueryable<User>>().Setup(m => m.Expression).Returns(data.Expression);
        mockSet.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(data.ElementType);
        mockSet.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

        var mockContext = new Mock<SplankContext>();
        mockContext.Setup(m => m.Users).Returns(mockSet.Object);

        var encrypter = new PasswordEncrypter();
        UserService userService = new UserService(mockContext.Object, encrypter);

        // act
        userService.Register("Rida3", "Rida4Password", "I am Rida4");
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void RegisterNullUsernameThrowsArgumentException() {
        // arrang/act
        UserService userService = new UserService(SplankContext.GetInstance(), new PasswordEncrypter());
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
        userService.Register(null, "ValidPassword", "Valid Description");
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void RegisterEmptyUsernameThrowsArgumentException() {
        // arrange/act
        UserService userService = new UserService(SplankContext.GetInstance(), new PasswordEncrypter());
        userService.Register("", "ValidPassword", "Valid Description");
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void RegisterNullPasswordThrowsArgumentException() {
        // arrange/act
        UserService userService = new UserService(SplankContext.GetInstance(), new PasswordEncrypter());
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
        userService.Register("ValidUsername", null, "Valid Description");
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void RegisterEmptyPasswordThrowsArgumentException() {
        // arrange/act
        UserService userService = new UserService(SplankContext.GetInstance(), new PasswordEncrypter());
        userService.Register("ValidUsername", "", "Valid Description");
    }

    [TestMethod]
    public void ChangePasswordSuccessfullyChangesPassword() {
        // arrange
        PasswordEncrypter passwordEncrypter = new();
        
        var salt1 = passwordEncrypter.CreateSalt();
        var salt2 = passwordEncrypter.CreateSalt();
        var salt3 = passwordEncrypter.CreateSalt();

        var password1 = passwordEncrypter.CreateHash("Rida1Password", salt1);
        var password2 = passwordEncrypter.CreateHash("Rida1Password", salt2);
        var password3 = passwordEncrypter.CreateHash("Rida1Password", salt3);

        var listUser = new List<User>();
        listUser.Add(new User("Rida1", "I am rida 1", password1, new(), salt1));
        listUser.Add(new User("Rida2", "I am rida 2", password2, new(), salt2));
        listUser.Add(new User("Rida3", "I am rida 3", password3, new(), salt3));
        
        var data = listUser.AsQueryable();

        var mockSet = new Mock<DbSet<User>>();
        mockSet.As<IQueryable<User>>().Setup(m => m.Provider).Returns(data.Provider);
        mockSet.As<IQueryable<User>>().Setup(m => m.Expression).Returns(data.Expression);
        mockSet.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(data.ElementType);
        mockSet.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

        var mockContext = new Mock<SplankContext>();
        mockContext.Setup(m => m.Users).Returns(mockSet.Object);

        var encrypter = new PasswordEncrypter();
        UserService userService = new UserService(mockContext.Object, encrypter);

        var userToChangePassword = listUser[0];
        var oldPassword = userToChangePassword.Password;
        var newPassword = "Rida1New";

        // act
        userService.ChangePassword(userToChangePassword, newPassword);

        // assert
        mockContext.Verify(m => m.Update(It.IsAny<User>()), Times.Once);
        mockContext.Verify(m => m.SaveChanges(), Times.Once()); 
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void ChangePasswordNullUserThrowsArgumentException() {
        // arrange/act
        UserService userService = new UserService(SplankContext.GetInstance(), new PasswordEncrypter());
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
        userService.ChangePassword(null, "Some random password");
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void ChangePasswordNullPasswordThrowsArgumentException() {
        // arrange/act
        UserService userService = new UserService(SplankContext.GetInstance(), new PasswordEncrypter());
        User user = new User("Rida", "Rida Description", "Some random password", new(), "This is salt");
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
        userService.ChangePassword(user, null);
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
    }

    public void ChangePasswordShortPasswordThrowsArgumentException() {
        // arrange/act
        UserService userService = new UserService(SplankContext.GetInstance(), new PasswordEncrypter());
        User user = new User("Rida", "Rida Description", "Some random password", new(), "This is salt");
        userService.ChangePassword(user, "short");
    }

    [TestMethod]
    public void DeleteAccountSuccessfullyDeletesAccount() {
        // arrange
        PasswordEncrypter passwordEncrypter = new();
        
        var salt1 = passwordEncrypter.CreateSalt();
        var salt2 = passwordEncrypter.CreateSalt();
        var salt3 = passwordEncrypter.CreateSalt();

        var password1 = passwordEncrypter.CreateHash("Rida1Password", salt1);
        var password2 = passwordEncrypter.CreateHash("Rida1Password", salt2);
        var password3 = passwordEncrypter.CreateHash("Rida1Password", salt3);

        var listUser = new List<User>();
        listUser.Add(new User("Rida1", "I am rida 1", password1, new(), salt1));
        listUser.Add(new User("Rida2", "I am rida 2", password2, new(), salt2));
        listUser.Add(new User("Rida3", "I am rida 3", password3, new(), salt3));
        
        var data = listUser.AsQueryable();

        var mockSet = new Mock<DbSet<User>>();
        mockSet.As<IQueryable<User>>().Setup(m => m.Provider).Returns(data.Provider);
        mockSet.As<IQueryable<User>>().Setup(m => m.Expression).Returns(data.Expression);
        mockSet.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(data.ElementType);
        mockSet.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

        var mockContext = new Mock<SplankContext>();
        mockContext.Setup(m => m.Users).Returns(mockSet.Object);

        var encrypter = new PasswordEncrypter();
        UserService userService = new UserService(mockContext.Object, encrypter);

        var userToDelete = listUser[0];
        
        // act
        userService.DeleteAccount(userToDelete);

        // assert
        mockContext.Verify(m => m.Remove(It.IsAny<User>()), Times.Once);
        mockContext.Verify(m => m.SaveChanges(), Times.Once());  
    }

    [TestMethod]
    public void AddToFavouritesSuccessfullyAddsToFavourites() {
        // arrange
        PasswordEncrypter passwordEncrypter = new();
        
        var salt1 = passwordEncrypter.CreateSalt();
        var salt2 = passwordEncrypter.CreateSalt();
        var salt3 = passwordEncrypter.CreateSalt();

        var password1 = passwordEncrypter.CreateHash("Rida1Password", salt1);
        var password2 = passwordEncrypter.CreateHash("Rida1Password", salt2);
        var password3 = passwordEncrypter.CreateHash("Rida1Password", salt3);

        var listUser = new List<User>();
        listUser.Add(new User("Rida1", "I am rida 1", password1, new(), salt1));
        listUser.Add(new User("Rida2", "I am rida 2", password2, new(), salt2));
        listUser.Add(new User("Rida3", "I am rida 3", password3, new(), salt3));

        var listIngredients = new List<Ingredient>() { new("Potato", 3, UnitOfMeasurement.AMOUNT, 30) };
        var listSteps = new List<Step>() { new(5, "Do potato") };

        var listRecipe = new List<Recipe>();
        listRecipe.Add(new Recipe("Potato recipe", listUser[0], "Some description", 5, listIngredients, listSteps, new(), new()));
        listRecipe.Add(new Recipe("Potato recipe", listUser[1], "Some description", 5, listIngredients, listSteps, new(), new()));

        var listFavourites = new List<Favourite>();
        listFavourites.Add(new Favourite() {
            User = listUser[0],
            Recipe = listRecipe[0]
        });

        var userData = listUser.AsQueryable();
        var recipeData = listRecipe.AsQueryable();
        var favouriteData = listFavourites.AsQueryable();

        var userMockSet = new Mock<DbSet<User>>();
        userMockSet.As<IQueryable<User>>().Setup(m => m.Provider).Returns(userData.Provider);
        userMockSet.As<IQueryable<User>>().Setup(m => m.Expression).Returns(userData.Expression);
        userMockSet.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(userData.ElementType);
        userMockSet.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(userData.GetEnumerator());

        var recipeMockSet = new Mock<DbSet<Recipe>>();
        recipeMockSet.As<IQueryable<Recipe>>().Setup(m => m.Provider).Returns(recipeData.Provider);
        recipeMockSet.As<IQueryable<Recipe>>().Setup(m => m.Expression).Returns(recipeData.Expression);
        recipeMockSet.As<IQueryable<Recipe>>().Setup(m => m.ElementType).Returns(recipeData.ElementType);
        recipeMockSet.As<IQueryable<Recipe>>().Setup(m => m.GetEnumerator()).Returns(recipeData.GetEnumerator());

        var favouriteMockSet = new Mock<DbSet<Favourite>>();
        favouriteMockSet.As<IQueryable<Favourite>>().Setup(m => m.Provider).Returns(favouriteData.Provider);
        favouriteMockSet.As<IQueryable<Favourite>>().Setup(m => m.Expression).Returns(favouriteData.Expression);
        favouriteMockSet.As<IQueryable<Favourite>>().Setup(m => m.ElementType).Returns(favouriteData.ElementType);
        favouriteMockSet.As<IQueryable<Favourite>>().Setup(m => m.GetEnumerator()).Returns(favouriteData.GetEnumerator());

        var mockContext = new Mock<SplankContext>();
        mockContext.Setup(m => m.Users).Returns(userMockSet.Object);
        mockContext.Setup(m => m.Recipes).Returns(recipeMockSet.Object);
        mockContext.Setup(m => m.Favourites).Returns(favouriteMockSet.Object);

        var encrypter = new PasswordEncrypter();
        UserService userService = new(mockContext.Object, encrypter);

        var userWhoFavourited = listUser[0];
        var favouritedRecipe = listRecipe[1];
        
        // act
        userService.AddToFavourites(favouritedRecipe, userWhoFavourited);

        // assert
        mockContext.Verify(m => m.Add(It.IsAny<Favourite>()), Times.Once);
        mockContext.Verify(m => m.SaveChanges(), Times.Once());  
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void AddToFavouritesNullFavouritedThrowsArgumentException() {
        // arrange/act
        User user = new User("Rida", "Rida Description", "Some random password", new(), "This is salt");
        UserService userService = new UserService(SplankContext.GetInstance(), new PasswordEncrypter());
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
        userService.AddToFavourites(null, user);
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void AddToFavouritesNullUserhrowsArgumentException() {
        // arrange
        UserService userService = new UserService(SplankContext.GetInstance(), new PasswordEncrypter());
        User user = new User("Rida", "Rida Description", "Some random password", new(),  "This is salt");
        var ingredients = new List<Ingredient>() {
            new Ingredient("Something", 1, UnitOfMeasurement.AMOUNT, 2.00)
        };
        var steps = new List<Step>() {
            new Step(5, "Something")
        };
        Recipe recipe = new Recipe("Potato", user, "Description", 2, ingredients, steps, new(), new());
        // act
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
        userService.AddToFavourites(recipe, null);
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
    }

    [TestMethod]
    [ExpectedException(typeof(AlreadyFavouritedException))]
    public void AddToFavouritesThrowsAlreadyFavouritedException() {
        // arrange
        PasswordEncrypter passwordEncrypter = new();
        
        var salt1 = passwordEncrypter.CreateSalt();
        var salt2 = passwordEncrypter.CreateSalt();
        var salt3 = passwordEncrypter.CreateSalt();

        var password1 = passwordEncrypter.CreateHash("Rida1Password", salt1);
        var password2 = passwordEncrypter.CreateHash("Rida1Password", salt2);
        var password3 = passwordEncrypter.CreateHash("Rida1Password", salt3);

        var listUser = new List<User>();
        listUser.Add(new User("Rida1", "I am rida 1", password1, new(), salt1));
        listUser.Add(new User("Rida2", "I am rida 2", password2, new(), salt2));
        listUser.Add(new User("Rida3", "I am rida 3", password3, new(), salt3));

        var listIngredients = new List<Ingredient>() { new("Potato", 3, UnitOfMeasurement.AMOUNT, 30) };
        var listSteps = new List<Step>() { new(5, "Do potato") };

        var listRecipe = new List<Recipe>();
        listRecipe.Add(new Recipe("Potato recipe", listUser[0], "Some description", 5, listIngredients, listSteps, new(), new()));
        listRecipe.Add(new Recipe("Potato recipe", listUser[1], "Some description", 5, listIngredients, listSteps, new(), new()));

        var listFavourites = new List<Favourite>();
        listFavourites.Add(new Favourite() {
            User = listUser[0],
            Recipe = listRecipe[0]
        });

        var userData = listUser.AsQueryable();
        var recipeData = listRecipe.AsQueryable();
        var favouriteData = listFavourites.AsQueryable();

        var userMockSet = new Mock<DbSet<User>>();
        userMockSet.As<IQueryable<User>>().Setup(m => m.Provider).Returns(userData.Provider);
        userMockSet.As<IQueryable<User>>().Setup(m => m.Expression).Returns(userData.Expression);
        userMockSet.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(userData.ElementType);
        userMockSet.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(userData.GetEnumerator());

        var recipeMockSet = new Mock<DbSet<Recipe>>();
        recipeMockSet.As<IQueryable<Recipe>>().Setup(m => m.Provider).Returns(recipeData.Provider);
        recipeMockSet.As<IQueryable<Recipe>>().Setup(m => m.Expression).Returns(recipeData.Expression);
        recipeMockSet.As<IQueryable<Recipe>>().Setup(m => m.ElementType).Returns(recipeData.ElementType);
        recipeMockSet.As<IQueryable<Recipe>>().Setup(m => m.GetEnumerator()).Returns(recipeData.GetEnumerator());

        var favouriteMockSet = new Mock<DbSet<Favourite>>();
        favouriteMockSet.As<IQueryable<Favourite>>().Setup(m => m.Provider).Returns(favouriteData.Provider);
        favouriteMockSet.As<IQueryable<Favourite>>().Setup(m => m.Expression).Returns(favouriteData.Expression);
        favouriteMockSet.As<IQueryable<Favourite>>().Setup(m => m.ElementType).Returns(favouriteData.ElementType);
        favouriteMockSet.As<IQueryable<Favourite>>().Setup(m => m.GetEnumerator()).Returns(favouriteData.GetEnumerator());

        var mockContext = new Mock<SplankContext>();
        mockContext.Setup(m => m.Users).Returns(userMockSet.Object);
        mockContext.Setup(m => m.Recipes).Returns(recipeMockSet.Object);
        mockContext.Setup(m => m.Favourites).Returns(favouriteMockSet.Object);

        var encrypter = new PasswordEncrypter();
        UserService userService = new(mockContext.Object, encrypter);

        var userWhoFavourited = listUser[0];
        var favouritedRecipe = listRecipe[0];
        
        // act
        userService.AddToFavourites(favouritedRecipe, userWhoFavourited);
    }

    [TestMethod]
    public void DeleteFromFavouritesSuccessfullyDeletesFavourite() {
        // arrange
        PasswordEncrypter passwordEncrypter = new();
        
        var salt1 = passwordEncrypter.CreateSalt();
        var salt2 = passwordEncrypter.CreateSalt();
        var salt3 = passwordEncrypter.CreateSalt();

        var password1 = passwordEncrypter.CreateHash("Rida1Password", salt1);
        var password2 = passwordEncrypter.CreateHash("Rida1Password", salt2);
        var password3 = passwordEncrypter.CreateHash("Rida1Password", salt3);

        var listUser = new List<User>();
        listUser.Add(new User("Rida1", "I am rida 1", password1, new(), salt1));
        listUser.Add(new User("Rida2", "I am rida 2", password2, new(), salt2));
        listUser.Add(new User("Rida3", "I am rida 3", password3, new(), salt3));

        var listIngredients = new List<Ingredient>() { new("Potato", 3, UnitOfMeasurement.AMOUNT, 30) };
        var listSteps = new List<Step>() { new(5, "Do potato") };

        var listRecipe = new List<Recipe>();
        listRecipe.Add(new Recipe("Potato recipe", listUser[0], "Some description", 5, listIngredients, listSteps, new(), new()));
        listRecipe.Add(new Recipe("Potato recipe", listUser[1], "Some description", 5, listIngredients, listSteps, new(), new()));

        var listFavourites = new List<Favourite>();
        listFavourites.Add(new Favourite() {
            User = listUser[0],
            Recipe = listRecipe[0]
        });

        var userData = listUser.AsQueryable();
        var recipeData = listRecipe.AsQueryable();
        var favouriteData = listFavourites.AsQueryable();

        var userMockSet = new Mock<DbSet<User>>();
        userMockSet.As<IQueryable<User>>().Setup(m => m.Provider).Returns(userData.Provider);
        userMockSet.As<IQueryable<User>>().Setup(m => m.Expression).Returns(userData.Expression);
        userMockSet.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(userData.ElementType);
        userMockSet.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(userData.GetEnumerator());

        var recipeMockSet = new Mock<DbSet<Recipe>>();
        recipeMockSet.As<IQueryable<Recipe>>().Setup(m => m.Provider).Returns(recipeData.Provider);
        recipeMockSet.As<IQueryable<Recipe>>().Setup(m => m.Expression).Returns(recipeData.Expression);
        recipeMockSet.As<IQueryable<Recipe>>().Setup(m => m.ElementType).Returns(recipeData.ElementType);
        recipeMockSet.As<IQueryable<Recipe>>().Setup(m => m.GetEnumerator()).Returns(recipeData.GetEnumerator());

        var favouriteMockSet = new Mock<DbSet<Favourite>>();
        favouriteMockSet.As<IQueryable<Favourite>>().Setup(m => m.Provider).Returns(favouriteData.Provider);
        favouriteMockSet.As<IQueryable<Favourite>>().Setup(m => m.Expression).Returns(favouriteData.Expression);
        favouriteMockSet.As<IQueryable<Favourite>>().Setup(m => m.ElementType).Returns(favouriteData.ElementType);
        favouriteMockSet.As<IQueryable<Favourite>>().Setup(m => m.GetEnumerator()).Returns(favouriteData.GetEnumerator());

        var mockContext = new Mock<SplankContext>();
        mockContext.Setup(m => m.Users).Returns(userMockSet.Object);
        mockContext.Setup(m => m.Recipes).Returns(recipeMockSet.Object);
        mockContext.Setup(m => m.Favourites).Returns(favouriteMockSet.Object);

        var encrypter = new PasswordEncrypter();
        UserService userService = new(mockContext.Object, encrypter);

        // act
        userService.DeleteFromFavourites(listRecipe[0], listUser[0]);

        // assert
        mockContext.Verify(m => m.Remove(It.IsAny<Favourite>()), Times.Once);
        mockContext.Verify(m => m.SaveChanges(), Times.Once());  
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void DeleteFromFavouritesNullArgumentsThrowsArgumentException() {
        // arrange/assert
        UserService userService = new UserService(SplankContext.GetInstance(), new PasswordEncrypter());
        userService.DeleteFromFavourites(null!, null!);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void SetProfilePictureNullArgumentThrowsArgumentException() {
        UserService userService = new(SplankContext.GetInstance(), new PasswordEncrypter());
        userService.SetProfilePicture(null!, null!);
    }

    [TestMethod]
    public void SetProfilePictureSucecssfullySetsProfilePicture() {
        // arrange
        PasswordEncrypter passwordEncrypter = new PasswordEncrypter();
        
        var salt1 = passwordEncrypter.CreateSalt();
        var salt2 = passwordEncrypter.CreateSalt();
        var salt3 = passwordEncrypter.CreateSalt();

        var password1 = passwordEncrypter.CreateHash("Rida1Password", salt1);
        var password2 = passwordEncrypter.CreateHash("Rida1Password", salt2);
        var password3 = passwordEncrypter.CreateHash("Rida1Password", salt3);

        var listUser = new List<User>();
        listUser.Add(new User("Rida1", "I am rida 1", password1, new(), salt1));
        listUser.Add(new User("Rida2", "I am rida 2", password2, new(), salt2));
        listUser.Add(new User("Rida2", "I am rida 3", password3, new(), salt3));

        listUser[0].ProfilePicture = null;
        listUser[1].ProfilePicture = null;
        listUser[2].ProfilePicture = null;

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

        try {
            byte[] bytes = File.ReadAllBytes("..\\..\\..\\..\\RecipeAppTest\\Services\\test_avatar.jpg");
            userService.SetProfilePicture(bytes, listUser[0]);
        } catch (IOException e) {
            Assert.Fail(e.Message);
        }

        // assert
        mockContext.Verify(m => m.SaveChanges(), Times.Once);
    }
}