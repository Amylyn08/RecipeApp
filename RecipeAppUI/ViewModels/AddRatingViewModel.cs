using System;
using System.Reactive;
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
    private string  __description = null!;
    private string _errorMessage = null!;
    private bool _editAvailable;
    private MainWindowViewModel _mainWindowViewModel = null!;
    public MainWindowViewModel MainWindowViewModel { get => _mainWindowViewModel; private set => _mainWindowViewModel = value; }
    public ReactiveCommand<Unit, Unit> ChangeToSpecificViewCommand {get; }

    public bool EditAvailable {
        get => _editAvailable;
        set => this.RaiseAndSetIfChanged(ref _editAvailable, value);
    }
    public RatingService RatingService{
        get => __ratingService;
        private set => __ratingService = value;
    }

    public int Stars{
        get => __stars;
        set {
            try{
                this.RaiseAndSetIfChanged(ref __stars, Convert.ToInt32(value));
            }
            catch(Exception e){
                ErrorMessage = e.Message;
            }
        }
    }

    public string Description{
        get => __description;
        set {
            this.RaiseAndSetIfChanged(ref __description, value);
            EditAvailable = !string.IsNullOrEmpty(value);
        }
    }
    public string ErrorMessage { get => _errorMessage; set => this.RaiseAndSetIfChanged(ref _errorMessage, value); }

    /// <summary>
    /// Constructor of the AddRatingViewModel
    /// </summary>
    /// <param name="context">DB context</param>
    /// <param name="recipe">The recipe you are viewing</param>
    /// <param name="mainWindowViewModel">Instance of MainWindowViewModek</param>
    public AddRatingViewModel(SplankContext context, Recipe recipe, MainWindowViewModel mainWindowViewModel){
        __ratingService = new RatingService(context);
        __recipe = recipe;
        MainWindowViewModel = mainWindowViewModel;
        ChangeToSpecificViewCommand = ReactiveCommand.Create(ChangeToSpecificView);
    }

    /// <summary>
    /// Adds a rating to the recipe
    /// </summary>
    public void AddRating(){
        try{
            Rating rating = new(__stars, __description, UserSingleton.GetInstance());
            __ratingService.RatingRecipe(rating, __recipe);
            MainWindowViewModel.ChangeToSpecificView(__recipe);
        }
        catch(ArgumentException e){
            ErrorMessage = e.Message;
        }
    }

    /// <summary>
    /// Commands that changes the view to the specified recipe page
    /// </summary>
    public void ChangeToSpecificView(){
        MainWindowViewModel.ChangeToSpecificView(__recipe);
    }
}