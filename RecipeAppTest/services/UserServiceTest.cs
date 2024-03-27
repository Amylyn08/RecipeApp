using RecipeApp.Services;

namespace RecipeAppTest.Services;

[TestClass]
public class UserServiceTest {

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void NullUsername_Login_ThrowsException() {
        UserService us = new UserService();
        string username = null;
        string password = "RidaPassword";

        us.Login(username,password);


    }

}