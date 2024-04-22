namespace RecipeAppTest.Models;

using RecipeApp.Models;

[TestClass]
public class IngredientTest {
    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void NullName_Throws_ArgumentException() {
        Ingredient ingredient = new(null, 1, UnitOfMeasurement.AMOUNT, 100);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void EmptyName_Throws_ArgumentException() {
        Ingredient ingredient = new("", 1, UnitOfMeasurement.AMOUNT, 100);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void ZeroQuantity_Throws_ArgumentException() {
        Ingredient ingredient = new("Potato", 0, UnitOfMeasurement.AMOUNT, 100);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void NegativePrice_Throws_ArgumentException() {
        Ingredient ingredient = new("Potato", 1, UnitOfMeasurement.AMOUNT, -1);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void NameSetter_Null_Throws_ArgumentException() {
        Ingredient ingredient = new("Potato", 1, UnitOfMeasurement.AMOUNT, 100);
        ingredient.Name = null;
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void NameSetter_Empty_Throws_ArgumentException() {
        Ingredient ingredient = new("Potato", 1, UnitOfMeasurement.AMOUNT, 100);
        ingredient.Name = "";
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void QuantitySetter_Zero_Throws_ArgumentException() {
        Ingredient ingredient = new("Potato", 1, UnitOfMeasurement.AMOUNT, 100);
        ingredient.Quantity = 0;
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void PriceSetter_Negative_Throws_ArgumentException() {
        Ingredient ingredient = new Ingredient("Potato", 1, UnitOfMeasurement.AMOUNT, 100);
        ingredient.Price = -1;
    }

    [TestMethod]
    public void Constructor_Init() {
        Ingredient ingredient = new("potato", 1, UnitOfMeasurement.AMOUNT, 100);
        Assert.AreEqual("potato", ingredient.Name);
        Assert.AreEqual(1, ingredient.Quantity);
        Assert.AreEqual(UnitOfMeasurement.AMOUNT, ingredient.UnitOfMeasurement);
        Assert.AreEqual(100, ingredient.Price);
    }
}