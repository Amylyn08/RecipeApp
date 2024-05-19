using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ReactiveUI;
using RecipeApp.Context;
using RecipeApp.Models;
using RecipeApp.Services;
using RecipeAppUI.Session;

namespace RecipeAppUI.ViewModels;

public class SpecificRecipeViewModel : ViewModelBase{
    private RatingService __ratingService =  null!;
    private ObservableCollection<Rating> __ratings = null!;
    private ObservableCollection<Rating> __yourRatings = null!;
    private Recipe __recipe = null!;
    public SpecificRecipeViewModel(SplankContext context, Recipe recipe){
        __ratingService = new RatingService(context);
        Ratings = new ObservableCollection<Rating>(recipe.Ratings);
        YourRatings = new ObservableCollection<Rating>(context.Ratings.Where(r => r.User.UserId == UserSingleton.GetInstance().UserId));
    }

    public RatingService RatingService{
        get => __ratingService;
        private set => __ratingService = value;
    }
    public ObservableCollection<Rating> Ratings{
        get => __ratings;
        set => this.RaiseAndSetIfChanged(ref __ratings, value);
    }

    public ObservableCollection<Rating> YourRatings{
        get => __yourRatings;
        set => this.RaiseAndSetIfChanged(ref __yourRatings, value);
    }

    public Recipe recipe{
        get => __recipe;
        set => this.RaiseAndSetIfChanged(ref __recipe, value);
    }


    
}