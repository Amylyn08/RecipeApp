using System.Collections.ObjectModel;
using ReactiveUI;
using RecipeApp.Context;
using RecipeApp.Models;
using RecipeApp.Services;

namespace RecipeAppUI.ViewModels;

public class RatingsViewModel : ViewModelBase{
    private RatingService __ratingService =  null!;
    private ObservableCollection<Rating> __ratings = null!;
    private ObservableCollection<Rating> __yourRatings = null!;

    public RatingsViewModel(SplankContext context){
        __ratingService = new RatingService(context);
        Ratings = new ObservableCollection<Rating>(context.Ratings);
        YourRatings = new ObservableCollection<Rating>(context.Ratings);
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