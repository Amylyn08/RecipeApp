using RecipeApp.Searcher;

namespace RecipeAppTest.searcher;

[TestClass]

public class SearchByTimeTests {

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void MinTimeNegative_ThrowsException() {
        ISearcher searcher = new SearchByTime(-1, 0);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void MaxTimeNegative_ThrowsException() {
        ISearcher searcher = new SearchByTime(1, -1);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void MinTimeBiggerThanMax_ThrowsException() {
        ISearcher searcher = new SearchByTime(1, 0);
    }


}