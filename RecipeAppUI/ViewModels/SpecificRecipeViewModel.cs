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

    public SpecificRecipeViewModel(SplankContext context){
        __ratingService = new RatingService(context);
        Ratings = new ObservableCollection<Rating>(context.Ratings);
        YourRatings = new ObservableCollection<Rating>(context.Ratings
                                            .Include(r => r.Stars)
                                            .Include(r => r.Description)
                                            .Include(r => r.User)
                                            .Where(r => r.User.Equals(UserSingleton.GetInstance()))
                                            .ToList());
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



    
}