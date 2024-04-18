using RecipeApp.Security;
using RecipeApp.Services;
using Moq;


namespace RecipeAppTest.Services;

[TestClass]
public class UserServiceTest {
    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void NullEncrypterThrowsArgumentException() {
        IEncrypter? encrypter = null;
        ServiceBase userService = new UserService(encrypter);
    }

    
}