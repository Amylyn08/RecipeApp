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
        listUser.Add(new User("Rida3", "I am rida 3", password3, new(), new(), salt3));
        
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
        PasswordEncrypter passwordEncrypter = new();
        
        var salt1 = passwordEncrypter.CreateSalt();
        var salt2 = passwordEncrypter.CreateSalt();
        var salt3 = passwordEncrypter.CreateSalt();

        var password1 = passwordEncrypter.CreateHash("Rida1Password", salt1);
        var password2 = passwordEncrypter.CreateHash("Rida1Password", salt2);
        var password3 = passwordEncrypter.CreateHash("Rida1Password", salt3);

        var listUser = new List<User>();
        listUser.Add(new User("Rida1", "I am rida 1", password1, new(), new(), salt1));
        listUser.Add(new User("Rida2", "I am rida 2", password2, new(), new(), salt2));
        listUser.Add(new User("Rida3", "I am rida 3", password3, new(), new(), salt3));
        
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

    [TestMethod]
    public void RegisterSucessfullyAddsUser() {
        PasswordEncrypter passwordEncrypter = new();
        
        var salt1 = passwordEncrypter.CreateSalt();
        var salt2 = passwordEncrypter.CreateSalt();
        var salt3 = passwordEncrypter.CreateSalt();

        var password1 = passwordEncrypter.CreateHash("Rida1Password", salt1);
        var password2 = passwordEncrypter.CreateHash("Rida1Password", salt2);
        var password3 = passwordEncrypter.CreateHash("Rida1Password", salt3);

        var listUser = new List<User>();
        listUser.Add(new User("Rida1", "I am rida 1", password1, new(), new(), salt1));
        listUser.Add(new User("Rida2", "I am rida 2", password2, new(), new(), salt2));
        listUser.Add(new User("Rida3", "I am rida 3", password3, new(), new(), salt3));
        
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

        userService.Register("Rida4", "Rida4Password", "I am Rida4");

        mockContext.Verify(m => m.Add(It.IsAny<User>()), Times.Once());
        mockContext.Verify(m => m.SaveChanges(), Times.Once());  
    }

    [TestMethod]
    [ExpectedException(typeof(UserAlreadyExistsException))]
    public void RegisterThrowsUserAlreadyExistsException() {
        PasswordEncrypter passwordEncrypter = new();
        
        var salt1 = passwordEncrypter.CreateSalt();
        var salt2 = passwordEncrypter.CreateSalt();
        var salt3 = passwordEncrypter.CreateSalt();

        var password1 = passwordEncrypter.CreateHash("Rida1Password", salt1);
        var password2 = passwordEncrypter.CreateHash("Rida1Password", salt2);
        var password3 = passwordEncrypter.CreateHash("Rida1Password", salt3);

        var listUser = new List<User>();
        listUser.Add(new User("Rida1", "I am rida 1", password1, new(), new(), salt1));
        listUser.Add(new User("Rida2", "I am rida 2", password2, new(), new(), salt2));
        listUser.Add(new User("Rida3", "I am rida 3", password3, new(), new(), salt3));
        
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

        userService.Register("Rida3", "Rida4Password", "I am Rida4");
    }

    [TestMethod]
    public void ChangePasswordSuccessfullyChangesPassword() {
        PasswordEncrypter passwordEncrypter = new();
        
        var salt1 = passwordEncrypter.CreateSalt();
        var salt2 = passwordEncrypter.CreateSalt();
        var salt3 = passwordEncrypter.CreateSalt();

        var password1 = passwordEncrypter.CreateHash("Rida1Password", salt1);
        var password2 = passwordEncrypter.CreateHash("Rida1Password", salt2);
        var password3 = passwordEncrypter.CreateHash("Rida1Password", salt3);

        var listUser = new List<User>();
        listUser.Add(new User("Rida1", "I am rida 1", password1, new(), new(), salt1));
        listUser.Add(new User("Rida2", "I am rida 2", password2, new(), new(), salt2));
        listUser.Add(new User("Rida3", "I am rida 3", password3, new(), new(), salt3));
        
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

        userService.ChangePassword(userToChangePassword, newPassword);

        mockContext.Verify(m => m.Update(It.IsAny<User>()), Times.Once);
        mockContext.Verify(m => m.SaveChanges(), Times.Once());  

        Assert.AreNotEqual(userToChangePassword.Password, oldPassword);
    }

    [TestMethod]
    public void DeleteAccountSuccessfullyDeletesAccount() {
        PasswordEncrypter passwordEncrypter = new();
        
        var salt1 = passwordEncrypter.CreateSalt();
        var salt2 = passwordEncrypter.CreateSalt();
        var salt3 = passwordEncrypter.CreateSalt();

        var password1 = passwordEncrypter.CreateHash("Rida1Password", salt1);
        var password2 = passwordEncrypter.CreateHash("Rida1Password", salt2);
        var password3 = passwordEncrypter.CreateHash("Rida1Password", salt3);

        var listUser = new List<User>();
        listUser.Add(new User("Rida1", "I am rida 1", password1, new(), new(), salt1));
        listUser.Add(new User("Rida2", "I am rida 2", password2, new(), new(), salt2));
        listUser.Add(new User("Rida3", "I am rida 3", password3, new(), new(), salt3));
        
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
        
        userService.DeleteAccount(userToDelete);

        mockContext.Verify(m => m.Remove(It.IsAny<User>()), Times.Once);
        mockContext.Verify(m => m.SaveChanges(), Times.Once());  
    }

    [TestMethod]
    public void AddToFavouritesSuccessfullyAddsToFavourites() {
        PasswordEncrypter passwordEncrypter = new();
        
        var salt1 = passwordEncrypter.CreateSalt();
        var salt2 = passwordEncrypter.CreateSalt();
        var salt3 = passwordEncrypter.CreateSalt();

        var password1 = passwordEncrypter.CreateHash("Rida1Password", salt1);
        var password2 = passwordEncrypter.CreateHash("Rida1Password", salt2);
        var password3 = passwordEncrypter.CreateHash("Rida1Password", salt3);

        var listUser = new List<User>();
        listUser.Add(new User("Rida1", "I am rida 1", password1, new(), new(), salt1));
        listUser.Add(new User("Rida2", "I am rida 2", password2, new(), new(), salt2));
        listUser.Add(new User("Rida3", "I am rida 3", password3, new(), new(), salt3));

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
        
        userService.AddToFavourites(favouritedRecipe, userWhoFavourited);

        mockContext.Verify(m => m.Add(It.IsAny<Favourite>()), Times.Once);
        mockContext.Verify(m => m.SaveChanges(), Times.Once());  
    }

    [TestMethod]
    [ExpectedException(typeof(AlreadyFavouritedException))]
    public void AddToFavouritesThrowsAlreadyFavouritedException() {
        PasswordEncrypter passwordEncrypter = new();
        
        var salt1 = passwordEncrypter.CreateSalt();
        var salt2 = passwordEncrypter.CreateSalt();
        var salt3 = passwordEncrypter.CreateSalt();

        var password1 = passwordEncrypter.CreateHash("Rida1Password", salt1);
        var password2 = passwordEncrypter.CreateHash("Rida1Password", salt2);
        var password3 = passwordEncrypter.CreateHash("Rida1Password", salt3);

        var listUser = new List<User>();
        listUser.Add(new User("Rida1", "I am rida 1", password1, new(), new(), salt1));
        listUser.Add(new User("Rida2", "I am rida 2", password2, new(), new(), salt2));
        listUser.Add(new User("Rida3", "I am rida 3", password3, new(), new(), salt3));

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
        
        userService.AddToFavourites(favouritedRecipe, userWhoFavourited);
    }

    [TestMethod]
    public void DeleteFromFavouritesSuccessfullyDeletesFavourite() {
        PasswordEncrypter passwordEncrypter = new();
        
        var salt1 = passwordEncrypter.CreateSalt();
        var salt2 = passwordEncrypter.CreateSalt();
        var salt3 = passwordEncrypter.CreateSalt();

        var password1 = passwordEncrypter.CreateHash("Rida1Password", salt1);
        var password2 = passwordEncrypter.CreateHash("Rida1Password", salt2);
        var password3 = passwordEncrypter.CreateHash("Rida1Password", salt3);

        var listUser = new List<User>();
        listUser.Add(new User("Rida1", "I am rida 1", password1, new(), new(), salt1));
        listUser.Add(new User("Rida2", "I am rida 2", password2, new(), new(), salt2));
        listUser.Add(new User("Rida3", "I am rida 3", password3, new(), new(), salt3));

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

        userService.DeleteFromFavourites(listFavourites[0]);

        mockContext.Verify(m => m.Remove(It.IsAny<Favourite>()), Times.Once);
        mockContext.Verify(m => m.SaveChanges(), Times.Once());  
    }
}