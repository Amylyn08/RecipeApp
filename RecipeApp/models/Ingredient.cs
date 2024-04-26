namespace RecipeApp.Models;

/// <summary>
/// An ingredient inside a recipe
/// </summary>
/// 
public class Ingredient {
    private string _name = null!;
    private double _price;
    private int _quantity;
    private UnitOfMeasurement _unitOfMeasurement;
    private Recipe _recipe = null!;

    public Recipe Recipe {
        get => _recipe; 
        set {
            if (value is null) {
                throw new ArgumentException("Recipe cannot be null");
            }
            _recipe = value;
        }
    }

    public int RecipeId { get; set; }

    public int IngredientId { 
        get; 
        set; 
    }

    public string Name { 
        get => _name; 
        set {
            if (value == null) 
                throw new ArgumentException("Name cannot be null");
            if (value.Length == 0)
                throw new ArgumentException("Name cannot be empty");
            _name = value;
        } 
    }
    
    public int Quantity { 
        get => _quantity; 
        set {
            if (value <= 0) 
                throw new ArgumentException("Quantity must be greater than 0");
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
            if (value < 0)
                throw new ArgumentException("Price cannot be negative");
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
        Name = name;
        Quantity = quantity;
        UnitOfMeasurement = unitOfMeasurement;
        Price = price;
    }

    /// <summary>
    /// Empty constructor for Entity framework
    /// </summary>
    public Ingredient() {

    }

    /// <summary>
    /// String representation of ingredient
    /// </summary>
    /// <returns>String representation of ingredient</returns>
    public override string ToString() {
        return Name + ", Quantity: " + Quantity + " Unit of measurement: " + UnitOfMeasurement + ", Price: " + Price;  
    }
}