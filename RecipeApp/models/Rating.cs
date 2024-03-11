namespace recipeapp.models;

public class Rating{

public Rating(){

}
private int __stars;
private string __comments;

public int Stars{
    get => __stars;
    set{
        //validator
    }
}

public string Comments{
    get => __comments;
    set{
        //max 512 letters
    }
}

public void AddRating(){


}


}