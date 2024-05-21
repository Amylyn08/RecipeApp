using System.Security.Cryptography;
namespace RecipeApp.Security;

public class PasswordEncrypter
{       
    /// <summary>
    ///Creates a salt using RNGCryptoServiceProvider for random crypto number generator,
    ///fills array of salt with random values.
    /// </summary>
    /// <returns>Returns the salt in base 64 string format.</returns>
    public string CreateSalt()
    {
#pragma warning disable SYSLIB0023 // Type or member is obsolete
        RNGCryptoServiceProvider rngCsp = new RNGCryptoServiceProvider();
#pragma warning restore SYSLIB0023 // Type or member is obsolete
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
        if (string.IsNullOrEmpty(input)) {
            throw new ArgumentException("Input cannot be null");
        }
        if (string.IsNullOrEmpty(salt)) {
            throw new ArgumentException("Salt cannot be null");
        }
        int numIterations = 1000;
        byte[] hash;
        using (Rfc2898DeriveBytes key = new Rfc2898DeriveBytes(input, Convert.FromBase64String(salt), numIterations))
        {
            hash = key.GetBytes(32);
        }
        return Convert.ToBase64String(hash);
    }
}