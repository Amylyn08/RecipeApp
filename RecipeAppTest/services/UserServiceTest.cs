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

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void NullPassword_Login_ThrowsException() {
        UserService us = new UserService();
        string username = "Rida1";
        string password = null;

        us.Login(username,password);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void NullUsername_Register_ThrowsException() {
        UserService us = new UserService();
        string username = null;
        string password = "RidaNewPassword";
        string description = "Hey im rida";

        us.Register(username, password, description);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void NullPassword_Register_ThrowsException() {
        UserService us = new UserService();
        string username = "Rida4";
        string password = null;
        string description = "Hey im rida";

        us.Register(username, password, description);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void NullDescription_Register_ThrowsException() {
        UserService us = new UserService();
        string username = "Rida4";
        string password = "RidaNewPassword";
        string description = null;

        us.Register(username, password, description);
    }

}