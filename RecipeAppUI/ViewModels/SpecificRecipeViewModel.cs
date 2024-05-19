using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using Microsoft.EntityFrameworkCore;
using ReactiveUI;
using RecipeApp.Context;
using RecipeApp.Models;
using RecipeApp.Services;
using RecipeAppUI.Session;

namespace RecipeAppUI.ViewModels;

public class SpecificRecipeViewModel : ViewModelBase{
    private ObservableCollection<Rating> __ratings = null!;
    private ObservableCollection<Rating> __yourRatings = null!;
    private Recipe __recipe = null!;
    private MainWindowViewModel _mainWindowViewModel;
    public MainWindowViewModel MainWindowViewModel { get => _mainWindowViewModel; private set => _mainWindowViewModel = value; }

    public ReactiveCommand<Unit, Unit> ChangeToAddRatingViewCommand {get;}
    public SpecificRecipeViewModel(SplankContext context, Recipe recipe, MainWindowViewModel mainWindowViewModel){
        MainWindowViewModel = mainWindowViewModel;
        Ratings = new ObservableCollection<Rating>(recipe.Ratings);
        Recipe = recipe;
        YourRatings = new ObservableCollection<Rating>(context.Ratings.Where(r => r.User.UserId == UserSingleton.GetInstance().UserId &&
                                                                                r.RecipeId == __recipe.RecipeId));
        ChangeToAddRatingViewCommand = ReactiveCommand.Create(ChangeToAddRatingView);
    }


    public ObservableCollection<Rating> Ratings{
        get => __ratings;
        set => this.RaiseAndSetIfChanged(ref __ratings, value);
    }

    public ObservableCollection<Rating> YourRatings{
        get => __yourRatings;
        set => this.RaiseAndSetIfChanged(ref __yourRatings, value);
    }

    public Recipe Recipe{
        get => __recipe;
        set => this.RaiseAndSetIfChanged(ref __recipe, value);
    }

    public void ChangeToAddRatingView(){
        MainWindowViewModel.ChangeToAddRatingView(__recipe);
    }
    
}