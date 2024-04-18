using RecipeApp.Searcher;

namespace RecipeAppTest.searcher;

[TestClass]

public class SearchByTagsTest {

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void TagNameNull_ThrowsException() {
        ISearcher searcher = new SearchByTags(null);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void TagNameEmpty_ThrowsException() {
        ISearcher searcher = new SearchByTags("");
    }


}