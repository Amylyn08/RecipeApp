namespace RecipeAppUI.Session;

using RecipeApp.Models;
using System;

public class UserSingleton {
    private static User instance = null!;

    private UserSingleton() {}

    public static User GetInstance() {
        // If we didn't instantiate the instance once before, then just throw an exception
        if (instance == null) {
            throw new InvalidOperationException("Please ensure InstantiateUserOnce has been called before getting an instance of this object");
        }
        return instance;
    }

    public static void InstantiateUserOnce(User user) {
        // don't instantiate user twice
        if (instance is not null) {
            throw new InvalidOperationException("InstantiateUserOnce cannot be called twice");
        }
        instance = user;
    }
}