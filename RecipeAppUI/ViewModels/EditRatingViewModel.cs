using System;
using System.Linq;
using System.Reactive;
using ReactiveUI;
using RecipeApp.Context;
using RecipeApp.Models;
using RecipeApp.Services;

namespace RecipeAppUI.ViewModels;

public class EditRatingViewModel : ViewModelBase
{
    private string _errorMessage = null!;
    private RatingService _ratingService = null!;
    private Rating _rating;
    private int _stars;
    private string _description;
    private MainWindowViewModel _mainWindowViewModel = null!;
    private SplankContext _context;

    public ReactiveCommand<Unit, Unit> ChangeToSpecificViewCommand {get; }

    public EditRatingViewModel(SplankContext context, Rating rating, MainWindowViewModel mainViewModel)
    {
        _ratingService = new RatingService(context);
        _rating = rating;
        MainWindowViewModel = mainViewModel;
        _context = context;
        ChangeToSpecificViewCommand = ReactiveCommand.Create(ChangeToSpecificView);

    }
    public string ErrorMessage { get => _errorMessage; set => this.RaiseAndSetIfChanged(ref _errorMessage, value); }

    public MainWindowViewModel MainWindowViewModel
    {
        get => _mainWindowViewModel;
        private set => _mainWindowViewModel = value;
    }

    public int Stars
    {
        get => _stars;
        set => this.RaiseAndSetIfChanged(ref _stars, value);
    }

    public string Description
    {
        get => _description;
        set => this.RaiseAndSetIfChanged(ref _description, value);
    }

    public Rating Rating
    {
        get => _rating;
        set => this.RaiseAndSetIfChanged(ref _rating, value);
    }
    public void EditRating()
    {
        try
        {
            _ratingService.UpdateRecipeRating(_rating, _description, _stars);
            Recipe? recipeOfRating = _context.Recipes.FirstOrDefault(r => r.RecipeId == _rating.RecipeId);

            if (recipeOfRating == null)
            {
                ErrorMessage = "Recipe not found.";
                return;
            }
            MainWindowViewModel.ChangeToSpecificView(recipeOfRating);
        }
        catch (ArgumentException e)
        {
            ErrorMessage = $"Error updating rating: {e.Message}";
        }
        catch (Exception e)
        {
            ErrorMessage = $"An unexpected error occurred: {e.Message}";
        }
    }

    public void ChangeToSpecificView()
    {
        try{
            Recipe? recipeOfSpecific = _context.Recipes.FirstOrDefault(r => r.RecipeId == _rating.RecipeId);
            if (recipeOfSpecific == null)
            {
                ErrorMessage = "Unable to go back.";
                return;
            }
            MainWindowViewModel.ChangeToSpecificView(recipeOfSpecific);
        }
        catch(Exception){
            ErrorMessage = "Internal Issue, unable to go back at the moment. Please reboot application.";
        }
    }
}

