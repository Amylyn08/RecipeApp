using RecipeApp.Context;
using Moq;
using RecipeApp.Searcher;

namespace RecipeAppTest.searcher;

[TestClass]

public class SearchByPriceRangeTests {

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void MinValueLessThan0(){
        //Arrange
        SplankContext context = new();
        double min = -0.1;
        double max = 5;

        //Act
        SearcherBase searcher = new SearchByPriceRange(context, min, max);
    }   
}


