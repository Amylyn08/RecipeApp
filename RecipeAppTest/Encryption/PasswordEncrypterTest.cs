using RecipeApp.Security;

namespace RecipeAppTest.Encryption;

[TestClass]
public class PasswordEncrypterTest {
    [TestMethod]
    public void CreateSaltGeneratesSalt() {
        // Arrange
        PasswordEncrypter passwordEncrypter = new PasswordEncrypter();
        // Act       
        string salt = passwordEncrypter.CreateSalt();
        // Assert
        Assert.IsNotNull(salt);
    }

    [TestMethod]
    public void GenerateHashGeneratesHash() {
        // Arrange
        PasswordEncrypter passwordEncrypter = new PasswordEncrypter();
        string salt = passwordEncrypter.CreateSalt();
        // Act
        string hash = passwordEncrypter.CreateHash("Some password", salt);
        // Assert
        Assert.IsNotNull(hash);
        Assert.IsTrue(hash.Length > 0);
    }

    [TestMethod]
    public void GenerateHashActuallyChangesPassword() {
        // Arrange
        PasswordEncrypter passwordEncrypter = new PasswordEncrypter();
        string password = "Hello, World";
        string salt = passwordEncrypter.CreateSalt();
        // Act
        string hash = passwordEncrypter.CreateHash(password, salt);
        // Assert
        Assert.IsNotNull(hash);
        Assert.IsTrue(hash.Length > 0);
        Assert.AreNotEqual(password, hash);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void GenerateHashNullPasswordThrowsArgumentException() {
        // arrange
        PasswordEncrypter passwordEncrypter = new PasswordEncrypter();
        string salt = passwordEncrypter.CreateSalt();
        string input = null!;
        // act
        passwordEncrypter.CreateHash(input , salt);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void GenerateHashEmptyPasswordThrowsArgumentException() {
        // arrange
        PasswordEncrypter passwordEncrypter = new PasswordEncrypter();
        string salt = passwordEncrypter.CreateSalt();
        // act
        passwordEncrypter.CreateHash("" , salt);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void GenerateHashNullSaltThrowsArgumentException() {
        // arrange
        PasswordEncrypter passwordEncrypter = new PasswordEncrypter();
        string salt = null!;
        // act
        passwordEncrypter.CreateHash("Some password", salt);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void GenerateHashEmptySaltThrowsArgumentException() {
        // arrange
        PasswordEncrypter passwordEncrypter = new PasswordEncrypter();
        // act
        passwordEncrypter.CreateHash("Some password", "");
    }
}