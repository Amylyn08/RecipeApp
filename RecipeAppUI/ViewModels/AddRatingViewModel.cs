using System;
using ReactiveUI;
using RecipeApp.Context;
using RecipeApp.Models;
using RecipeApp.Services;
using RecipeAppUI.Session;

namespace RecipeAppUI.ViewModels;

public class AddRatingViewModel : ViewModelBase{
    private RatingService __ratingService =  null!;
    private Recipe __recipe = null!;
    private int __stars;
    private string  __description;
    private string _errorMessage = null!;

    public AddRatingViewModel(SplankContext context, Recipe recipe){
        __ratingService = new RatingService(context);
        __recipe = recipe;
    }

    public RatingService RatingService{
        get => __ratingService;
        private set => __ratingService = value;
    }

    public int Stars{
        get => __stars;
        set => this.RaiseAndSetIfChanged(ref __stars, value);
    }

    public string Description{
        get => __description;
        set => this.RaiseAndSetIfChanged(ref __description, value);
    }
    public string ErrorMessage { get => _errorMessage; set => this.RaiseAndSetIfChanged(ref _errorMessage, value); }

    public void AddRating(){
        try{
            Rating rating = new(__stars, __description, UserSingleton.GetInstance());
            __ratingService.RatingRecipe(rating, __recipe);
        }
        catch(ArgumentException e){
            ErrorMessage = e.Message;
        }
    }
}