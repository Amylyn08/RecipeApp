using System.Security.Cryptography;
namespace RecipeApp.Security;

public class PasswordEncrypter : IEncrypter
{   
    /// <summary>
    /// Uses two methods below to return the hased version of the password.
    /// </summary>
    /// <param name="plainText">The plain text, can be referred to as the password input.</param>
    /// <returns>The hashed password version of plain text param.</returns>
    public string Encrypt(string plainText)
    {
        string salt = CreateSalt();
        string hashedPassword = CreateHash(plainText, salt);
        return hashedPassword;
    }

    /// <summary>
    ///Creates a salt using RNGCryptoServiceProvider for random crypto number generator,
    ///fills array of salt with random values.
    /// </summary>
    /// <returns>Returns the salt in base 64 string format.</returns>
    public string CreateSalt()
    {
        RNGCryptoServiceProvider rngCsp = new RNGCryptoServiceProvider();
        byte[] salt = new byte[8];
        rngCsp.GetBytes(salt);
        return Convert.ToBase64String(salt);
    }

    /// <summary>
    /// Creates hash for password, uses Rfc2898 to create cryptographic key from the 
    /// input and salt. Uses key to generate a hash of 32 bits.
    /// </summary>
    /// <param name="input">Password/input of the user</param>
    /// <param name="salt">the random salt that is being used</param>
    /// <returns>The hashsed possword in base 64 string format.</returns>
    public string CreateHash(string input, string salt)
    {
        int numIterations = 1000;
        byte[] hash;
        using (Rfc2898DeriveBytes key = new Rfc2898DeriveBytes(input, Convert.FromBase64String(salt), numIterations))
        {
            hash = key.GetBytes(32);
        }
        return Convert.ToBase64String(hash);
    }



}