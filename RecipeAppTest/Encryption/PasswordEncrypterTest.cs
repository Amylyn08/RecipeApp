using RecipeApp.Security;

namespace RecipeAppTest.Encryption;

[TestClass]
public class PasswordEncrypterTest {
    [TestMethod]
    public void CreateSaltGeneratesSalt() {
        PasswordEncrypter passwordEncrypter = new PasswordEncrypter();
        Assert.IsNotNull(passwordEncrypter.CreateSalt());
    }

    [TestMethod]
    public void GenerateHashGeneratesHash() {
        PasswordEncrypter passwordEncrypter = new PasswordEncrypter();
        string salt = passwordEncrypter.CreateSalt();
        string hash = passwordEncrypter.CreateHash("Some password", salt);
        Assert.IsNotNull(hash);
        Assert.IsTrue(hash.Length > 0);
    }

    [TestMethod]
    public void GenerateHashActuallyChangesPassword() {
        PasswordEncrypter passwordEncrypter = new PasswordEncrypter();
        string password = "Hello, World";
        string salt = passwordEncrypter.CreateSalt();
        string hash = passwordEncrypter.CreateHash(password, salt);
        Assert.IsNotNull(hash);
        Assert.IsTrue(hash.Length > 0);
        Assert.AreNotEqual(password, hash);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void GenerateHashNullPasswordThrowsArgumentException() {
        PasswordEncrypter passwordEncrypter = new PasswordEncrypter();
        string salt = passwordEncrypter.CreateSalt();
        passwordEncrypter.CreateHash(null , salt);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void GenerateHashEmptyPasswordThrowsArgumentException() {
        PasswordEncrypter passwordEncrypter = new PasswordEncrypter();
        string salt = passwordEncrypter.CreateSalt();
        passwordEncrypter.CreateHash("" , salt);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void GenerateHashNullSaltThrowsArgumentException() {
        PasswordEncrypter passwordEncrypter = new PasswordEncrypter();
        passwordEncrypter.CreateHash("Some password", null);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void GenerateHashEmptySaltThrowsArgumentException() {
        PasswordEncrypter passwordEncrypter = new PasswordEncrypter();
        passwordEncrypter.CreateHash("Some password", "");
    }
}