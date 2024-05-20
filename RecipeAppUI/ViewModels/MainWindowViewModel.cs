using ReactiveUI;
using RecipeApp.Context;
using RecipeApp.Models;
using System.Reactive;

namespace RecipeAppUI.ViewModels;

public class MainWindowViewModel : ViewModelBase {
    private ViewModelBase _contentViewModel = null!;

    public ViewModelBase ContentViewModel {
        get => _contentViewModel;
        private set => this.RaiseAndSetIfChanged(ref _contentViewModel, value);
    }

    public ReactiveCommand<Unit, Unit>  ChangeToLoginViewCommand { get; }
    public ReactiveCommand<Unit, Unit> ChangeToRegisterViewCommand { get; }
    public ReactiveCommand<Unit, Unit> ChangeToHomeViewCommand { get; }
    public ReactiveCommand<Unit, Unit> ChangeToDashboardViewCommand { get; }
    public ReactiveCommand<Unit, Unit> ChangeToChangePasswordViewCommand { get; }
    public ReactiveCommand<Unit, Unit> ChangeToDeleteAccountViewCommand { get; }
    public ReactiveCommand<Unit, Unit> ChangeToFavouritesViewCommand { get; }
    public ReactiveCommand<Unit, Unit> ChangeToCreateRecipeViewCommand { get; }
    public ReactiveCommand<Unit, Unit> ChangeToRecipesViewCommand { get; }
    public ReactiveCommand<Unit, Unit> ChangeToProfileViewCommand { get; }
    public ReactiveCommand<Unit, Unit> ChangeToAddIngredientViewCommand { get; } = null!;
    public ReactiveCommand<Recipe, Unit> ChangeToNutritionFactsViewCommand { get; }
    public ReactiveCommand<Recipe, Unit> ChangeToUpdateRecipeCommand { get; }
    public ReactiveCommand<Recipe, Unit> ChangeToSpecificViewCommand {get; }
    public ReactiveCommand<Recipe, Unit> ChangeToAddRatingViewCommand {get; }


    public MainWindowViewModel() {
        ChangeToLoginViewCommand = ReactiveCommand.Create(ChangeToLoginView);
        ChangeToRegisterViewCommand = ReactiveCommand.Create(ChangeToRegisterView);
        ChangeToHomeViewCommand = ReactiveCommand.Create(ChangeToHomeView);
        ChangeToDashboardViewCommand = ReactiveCommand.Create(ChangeToDashboardView);
        ChangeToChangePasswordViewCommand = ReactiveCommand.Create(ChangeToChangePasswordView);
        ChangeToDeleteAccountViewCommand = ReactiveCommand.Create(ChangeToDeleteAccountView);
        ChangeToFavouritesViewCommand = ReactiveCommand.Create(ChangeToFavouritesView);
        ChangeToCreateRecipeViewCommand = ReactiveCommand.Create(ChangeToCreateRecipeView);
        ChangeToRecipesViewCommand = ReactiveCommand.Create(ChangeToRecipesView);
        ChangeToProfileViewCommand = ReactiveCommand.Create(ChangeToProfileView);
        ChangeToNutritionFactsViewCommand = ReactiveCommand.Create<Recipe>(ChangeToNutritionFactsView);
        ChangeToUpdateRecipeCommand = ReactiveCommand.Create<Recipe>(ChangeToUpdateRecipeView);
        ChangeToSpecificViewCommand = ReactiveCommand.Create<Recipe>(ChangeToSpecificView);
        ChangeToAddRatingViewCommand = ReactiveCommand.Create<Recipe>(ChangeToAddRatingView);
        ContentViewModel = new HomeViewModel();
    }

    public void ChangeToLoginView() {
        ContentViewModel = new LoginViewModel(SplankContext.GetInstance(), this);
    }

    public void ChangeToRegisterView() {
        ContentViewModel = new RegisterViewModel(SplankContext.GetInstance(), this);
    }

    public void ChangeToHomeView() {
        ContentViewModel = new HomeViewModel();
    }

    public void ChangeToDashboardView() {
        ContentViewModel = new DashboardViewModel(SplankContext.GetInstance(), this);
    }

    public void ChangeToChangePasswordView() {
        ContentViewModel = new ChangePasswordViewModel(SplankContext.GetInstance(), this);
    }

    public void ChangeToDeleteAccountView() {
        ContentViewModel = new DeleteAccountViewModel(SplankContext.GetInstance(), this);
    }

    public void ChangeToFavouritesView() {
        ContentViewModel = new FavouritesViewModel(SplankContext.GetInstance(), this);
    }
    
    public void ChangeToCreateRecipeView() {
        ContentViewModel = new CreateRecipeViewModel(SplankContext.GetInstance(), this);
    }
    
    public void ChangeToRecipesView() {
        ContentViewModel = new RecipesViewModel(SplankContext.GetInstance(), this);
    }
    public void ChangeToProfileView() {
        ContentViewModel = new ProfileViewModel(SplankContext.GetInstance(), this);
    }

    public void ChangeToNutritionFactsView(Recipe recipe) {
        ContentViewModel = new NutritionFactsViewModel(recipe, this);
    }

    public void ChangeToUpdateRecipeView(Recipe recipe) {
        ContentViewModel = new RecipeUpdateViewModel(SplankContext.GetInstance(),  this, recipe);
    }

    public void ChangeToSpecificView(Recipe recipe){
        ContentViewModel = new SpecificRecipeViewModel(SplankContext.GetInstance(), recipe, this);
    }
    public void ChangeToAddRatingView(Recipe recipe){
        ContentViewModel = new AddRatingViewModel(SplankContext.GetInstance(), recipe, this);
    }
}

