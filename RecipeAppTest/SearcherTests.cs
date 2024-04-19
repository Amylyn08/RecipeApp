using System.Reflection;
using RecipeApp.Searcher;

namespace RecipeAppTest;

[TestClass]
public class SearcherTests{

[TestMethod]
[ExpectedException(typeof(ArgumentException))]
public void SearchByIngredientsNULLname(){
    //Arrange
    string name = null;
    //Act
    SearchByIngredients searcher = new(name);
}

[TestMethod]
[ExpectedException(typeof(ArgumentException))]
public void SearchByIngredientsNameLength0(){
    //Arrange
    string name = "";
    //Act
    SearchByIngredients searcher = new(name);
}

[TestMethod]
[ExpectedException(typeof(ArgumentException))]
public void SearchByKeywordNull(){
    //Arrange
    string keyword = null;
    //Act
    SearchKeyWord searcher = new(keyword);
}

[TestMethod]
[ExpectedException(typeof(ArgumentException))]
public void SearchByKeywordLength0(){
    //Arrange
    string keyword = "";
    //Act
    SearchKeyWord searcher = new(keyword);
}

[TestMethod]
[ExpectedException(typeof(ArgumentException))]
public void SearchByPriceRangeTooLow(){
    //Arrange
    double minPrice = -1;
    double maxPrice = 5;
    //Act
    SearchByPriceRange searcher = new(minPrice, maxPrice);
}

[TestMethod]
[ExpectedException(typeof(ArgumentException))]
public void SearchByPriceRangeMaxTooLow(){
    //Arrange
    double minPrice = 1;
    double maxPrice = -1;
    //Act
    SearchByPriceRange searcher = new(minPrice, maxPrice);
}

[TestMethod]
[ExpectedException(typeof(ArgumentException))]
public void SearchByPriceRangeMaxLowerThanMin(){
    //Arrange
    double minPrice =101;
    double maxPrice = 2;
    //Act
    SearchByPriceRange searcher = new(minPrice, maxPrice);
}




}