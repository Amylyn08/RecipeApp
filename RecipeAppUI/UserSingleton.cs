namespace RecipeAppUI.UserSingleton;
using RecipeAppUI.Models;


public class UserSingleton {

    public static User instance;

    private UserSingleton() {}

    public static User GetInstance() {
        if (instance == null) {
            instance = new User();
        }
        return instance;
    }




}