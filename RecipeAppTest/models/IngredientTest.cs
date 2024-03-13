namespace RecipeAppTest.Models;

using RecipeApp.Models;

[TestClass]
public class IngredientTest {
    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void NullName_Throws_ArgumentException() {
        Ingredient ingredient = new Ingredient(null, 1, UnitOfMeasurement.AMOUNT, 100);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void EmptyName_Throws_ArgumentException() {
        Ingredient ingredient = new Ingredient("", 1, UnitOfMeasurement.AMOUNT, 100);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void ZeroQuantity_Throws_ArgumentException() {
        Ingredient ingredient = new Ingredient("Potato", 0, UnitOfMeasurement.AMOUNT, 100);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void NegativePrice_Throws_ArgumentException() {
        Ingredient ingredient = new Ingredient("Potato", 1, UnitOfMeasurement.AMOUNT, -1);
    }

    [TestMethod]
    public void Constructor_Init() {
        Ingredient ingredient = new Ingredient("Potato", 1, UnitOfMeasurement.AMOUNT, 100);
        Assert.AreEqual("Potato", ingredient.Name);
        Assert.AreEqual(1, ingredient.Quantity);
        Assert.AreEqual(UnitOfMeasurement.AMOUNT, ingredient.UnitOfMeasurement);
        Assert.AreEqual(100, ingredient.Price);
    }
}