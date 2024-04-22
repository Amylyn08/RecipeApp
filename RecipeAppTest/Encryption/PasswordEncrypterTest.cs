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
    public void GenerateHashGeneratesHash() {
        PasswordEncrypter passwordEncrypter = new PasswordEncrypter();
        string salt = passwordEncrypter.CreateSalt();
        string hash = passwordEncrypter.CreateHash("Some password", salt);
        Assert.IsNotNull(hash);
        Assert.IsTrue(hash.Length > 0);
    }
}