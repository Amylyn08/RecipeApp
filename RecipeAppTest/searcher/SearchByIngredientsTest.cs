namespace RecipeAppTest.searcher;

using RecipeApp.Models;
using RecipeApp.Searcher;

[TestClass]

public class SearchByIngredientsTests{

    [TestMethod]
    public void SearcherReturnsRightSizeofIngredients(){
        //Arrange
        string ingredient = "Banana";
        ISearcher searcher = new SearchByIngredients(ingredient);

        //Act
        

    }

}


