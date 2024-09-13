using System;
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

public class SpecificRecipeViewModel : ViewModelBase
{
    private RatingService __ratingService = null!;
    private ObservableCollection<Rating> __ratings = null!;
    private ObservableCollection<Rating> __yourRatings = null!;
    private Recipe __recipe = null!;
    private MainWindowViewModel _mainWindowViewModel = null!;
    public MainWindowViewModel MainWindowViewModel { get => _mainWindowViewModel; private set => _mainWindowViewModel = value; }
    private string _errorMessage = null!;

    public ReactiveCommand<Unit, Unit> ChangeToAddRatingViewCommand { get; }
    public ReactiveCommand<int, Unit> DeleteRatingCommand { get; } 
    public ReactiveCommand<int, Unit> EditRatingViewCommand { get; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="context">DB context</param>
    /// <param name="recipe">Recipe you are viewing</param>
    /// <param name="mainWindowViewModel">Instance of the MainWindowViewModel</param>
    public SpecificRecipeViewModel(SplankContext context, Recipe recipe, MainWindowViewModel mainWindowViewModel)
    {
        __ratingService = new RatingService(context);
        MainWindowViewModel = mainWindowViewModel;
        Ratings = new ObservableCollection<Rating>(recipe.Ratings);
        Recipe = recipe;
        YourRatings = new ObservableCollection<Rating>(context.Ratings.Where(r => r.User.UserId == UserSingleton.GetInstance().UserId &&
                                                                                r.RecipeId == __recipe.RecipeId));
        ChangeToAddRatingViewCommand = ReactiveCommand.Create(ChangeToAddRatingView);
        DeleteRatingCommand = ReactiveCommand.Create<int>(DeleteRating);
        EditRatingViewCommand = ReactiveCommand.Create<int>(EditRatingView);
    }

    public RatingService RatingService
    {
        get => __ratingService;
        private set => __ratingService = value;
    }
    public string ErrorMessage { get => _errorMessage; set => this.RaiseAndSetIfChanged(ref _errorMessage, value); }

    public ObservableCollection<Rating> Ratings
    {
        get => __ratings;
        set => this.RaiseAndSetIfChanged(ref __ratings, value);
    }

    public ObservableCollection<Rating> YourRatings
    {
        get => __yourRatings;
        set => this.RaiseAndSetIfChanged(ref __yourRatings, value);
    }

    public Recipe Recipe
    {
        get => __recipe;
        set => this.RaiseAndSetIfChanged(ref __recipe, value);
    }

    /// <summary>
    /// Changes to the AddRatingView
    /// </summary>
    public void ChangeToAddRatingView()
    {
        MainWindowViewModel.ChangeToAddRatingView(__recipe);
    }

    /// <summary>
    /// Deletes the rating
    /// </summary>
    /// <param name="ratingId">Rating you are deleting</param>
    public void DeleteRating(int ratingId)
    {
        Rating? ratingToDelete = __yourRatings.FirstOrDefault(r => r.RatingId == ratingId);

        if (ratingToDelete != null)
        {
            try
            {
                __ratingService.DeleteRecipeRating(ratingToDelete);
                __yourRatings.Remove(ratingToDelete);
                __ratings.Remove(ratingToDelete);
            }
            catch (Exception e)
            {
                ErrorMessage = $"Error deleting rating: {e.Message}";
            }
        }
        else
        {
            ErrorMessage = "Rating not found.";
        }
    }

    /// <summary>
    /// Gets the rating and switches to the EditRatingView
    /// </summary>
    /// <param name="ratingId">Rating you are editing</param>
    public void EditRatingView(int ratingId){
        try{
            Rating? rating = __yourRatings.FirstOrDefault(r => r.RatingId == ratingId);   
            if(rating == null){
                ErrorMessage = "Unable to retrieve rating";
                return;
            }
            MainWindowViewModel.ChangeToEditRatingView(rating);
        }
        catch(Exception){
            ErrorMessage = "Unable to go into edit for this rating";
        }
    }

}