namespace RecipeApp.Security;

public interface IEncrypter {
    public string Encrypt(string plainText);
}