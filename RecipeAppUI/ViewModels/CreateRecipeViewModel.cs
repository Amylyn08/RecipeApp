using ReactiveUI;
using RecipeApp.Services;
using RecipeApp.Context;
using RecipeApp.Models;
using System.Reactive;
using System;
using RecipeApp.Exceptions;
using RecipeAppUI.Session;
using System.Collections.Generic;
using System.Collections.ObjectModel;
namespace RecipeAppUI.ViewModels;

public class CreateRecipeViewModel : ViewModelBase
{

    private string _name;
    private string _description;
    private int _servings;
    private ObservableCollection<Ingredient> _ingredients;
    private List<Step> _steps;
    private List<Tag> _tags;
    private readonly RecipeService _recipeService;
    private ViewModelBase _contentViewModel;

    public ViewModelBase ContentViewModel{
        get => _contentViewModel;
        set => this.RaiseAndSetIfChanged(ref _contentViewModel, value);
    }
    public string Name {
        get => _name;
        set => this.RaiseAndSetIfChanged(ref _name, value);
    } 

    public string Description {
        get => _description;
        set => this.RaiseAndSetIfChanged(ref _description, value);
    }

    public int Servings {
        get => _servings;
        set => this.RaiseAndSetIfChanged(ref _servings, value);
    }

    public ObservableCollection<Ingredient> Ingredients {
        get => _ingredients;
        set => this.RaiseAndSetIfChanged(ref _ingredients, value);
    }

    public List<Step> Steps {
        get => _steps;
        set => this.RaiseAndSetIfChanged(ref _steps, value);
    }

    public List<Tag> Tags {
        get => _tags;
        set => this.RaiseAndSetIfChanged(ref _tags, value);
    }

    public ReactiveCommand<Unit,Unit> ChangeToAddIngredientViewCommand { get;}
    public ReactiveCommand<Unit,Unit> ChangeToDashboardViewCommand { get;}
    private MainWindowViewModel _mainWindowViewModel;
    
    public CreateRecipeViewModel(SplankContext context, MainWindowViewModel mainWindowViewModel) 
    {
        _mainWindowViewModel = mainWindowViewModel;
        ChangeToAddIngredientViewCommand = ReactiveCommand.Create(ChangeToIngredientView);
        ChangeToDashboardViewCommand = ReactiveCommand.Create(ChangeToDashboardView);
        _recipeService = new RecipeService(context);
    }

    public void CreateRecipe() {
        try {
            Recipe recipe = new Recipe {
                Name = Name,
                Description = Description,
                Servings = Servings,
                Ingredients = Ingredients,
                Steps = Steps,
                Ratings = new(),
                Tags = Tags
            };
            _recipeService.CreateRecipe(recipe);
        }
        catch(ArgumentException e){
            Console.WriteLine(e.Message);
        }
    }

    public void ChangeToIngredientView(){
        ContentViewModel = new AddIngredientViewModel(SplankContext.GetInstance(), _mainWindowViewModel);
    }

    public void ChangeToDashboardView(){
        ContentViewModel = new DashboardViewModel(SplankContext.GetInstance());
    }

}
