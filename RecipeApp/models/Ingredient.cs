namespace RecipeApp.Models;

/// <summary>
/// An ingredient inside a recipe
/// </summary>
public class Ingredient {
    public string Name { get; private set; }
    public int Quantity { get; private set; }
    public UnitOfMeasurement UnitOfMeasurement { get; private set; }
    public double Price { get; private set; }
    
    /// <summary>
    /// Constructor with name, quantity and unit of measurement
    /// </summary>
    /// <param name="name">Name like "tomato" or "potato"</param>
    /// <param name="quantity">Quantity like 0.5 or 1</param>
    /// <param name="unitOfMeasurement">Grams, teaspoons, etc</param>
    /// <exception cref="ArgumentException">If name is null, empty or if quantity is <= 0</exception>
    public Ingredient(string name, int quantity, UnitOfMeasurement unitOfMeasurement, double price) {
        if (name == null) throw new ArgumentException("Name cannot be null");
        if (name.Length == 0) throw new ArgumentException("Name cannot be empty");
        if (quantity <= 0) throw new ArgumentException("Quantity must be greater than 0");
        if (price < 0) throw new ArgumentException("Price cannot be negative");
        Name = name;
        Quantity = quantity;
        UnitOfMeasurement = unitOfMeasurement;
        Price = price;
    }

    public override string ToString() {
        return Name + ", Q: " + Quantity + " " + UnitOfMeasurement + ", Price: " + Price;  
    }
}