using RecipeApp.Models;
using RecipeApp.Searcher;

namespace RecipeAppTest.searcher;

[TestClass]

public class SearchByServingsTests {

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void ServingsLessThanMin_ThrowsException() {
        ISearcher searcher = new SearchByServings(0);
    }


}