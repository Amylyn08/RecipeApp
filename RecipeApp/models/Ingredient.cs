namespace RecipeApp.Models;

/// <summary>
/// An ingredient inside a recipe
/// </summary>
public class Ingredient {
    private string _name;
    private double _price;
    private int _quantity;
    private UnitOfMeasurement _unitOfMeasurement;

    public string Name { 
        get => _name; 
        set {
            CheckName(value);
            _name = value;
        } 
    }
    
    public int Quantity { 
        get => _quantity; 
        set {
            CheckQuantity(value);
            _quantity = value;
        } 
    }
    
    public UnitOfMeasurement UnitOfMeasurement { 
        get => _unitOfMeasurement; 
        set => _unitOfMeasurement = value;
    }
    
    public double Price { 
        get => _price; 
        set {
            CheckPrice(value);
            _price = value;
        } 
    }
    
    /// <summary>
    /// Constructor with name, quantity and unit of measurement
    /// </summary>
    /// <param name="name">Name like "tomato" or "potato"</param>
    /// <param name="quantity">Quantity like 0.5 or 1</param>
    /// <param name="unitOfMeasurement">Grams, teaspoons, etc</param>
    /// <exception cref="ArgumentException">If name is null, empty or if quantity is <= 0</exception>
    public Ingredient(string name, int quantity, UnitOfMeasurement unitOfMeasurement, double price) {
        CheckName(name);
        CheckQuantity(quantity);
        CheckPrice(price);
        _name = name;
        _quantity = quantity;
        _unitOfMeasurement = unitOfMeasurement;
        _price = price;
    }

    /// <summary>
    /// Validates the name
    /// </summary>
    /// <param name="name">Name of ingredient</param>
    /// <exception cref="ArgumentException">If empty or null</exception>
    private static void CheckName(string name) {
        if (name == null) 
            throw new ArgumentException("Name cannot be null");
        if (name.Length == 0)
            throw new ArgumentException("Name cannot be empty");
    }

    /// <summary>
    /// Validate the quantity
    /// </summary>
    /// <param name="quantity">If 0 or null</param>
    /// <exception cref="ArgumentException"></exception>
    private static void CheckQuantity(int quantity) {
        if (quantity <= 0) 
            throw new ArgumentException("Quantity must be greater than 0");
    }

    /// <summary>
    /// Validate the price
    /// </summary>
    /// <param name="price">Price of ingredient</param>
    /// <exception cref="ArgumentException">If price is negative</exception>
    private static void CheckPrice(double price) {
        if (price < 0)
            throw new ArgumentException("Price cannot be negative");
    }

    public override string ToString() {
        return Name + ", Q: " + Quantity + " " + UnitOfMeasurement + ", Price: " + Price;  
    }
}