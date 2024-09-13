using System;
using Avalonia.Media.Imaging;
using System.IO;


namespace RecipeAppUI.Utils;

/// <summary>
/// Sets up bitmaps within our application to avoid
/// repeating the same logic in different view models
/// </summary>
public class BitMapper {
    /// <summary>
    /// Turns an array of byte data into an avalonia bitmap
    /// </summary>
    /// <param name="bytes">Data</param>
    /// <returns>Bitmap to use within views, or null</returns>
    public static Bitmap? DoBitmap(byte[] bytes) {
        if (bytes == null) {
            return null;
        }
        using MemoryStream stream = new(bytes);
        return new Bitmap(stream);
    }
}