namespace RecipeAppUI.ViewModels;
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
using RecipeAppUI.ViewModels;

public class CreateRecipeViewModel : ViewModelBase
{

    private string _name;
    private string _description;
    private int _servings;
    private ObservableCollection<Ingredient> _ingredients;
    private ObservableCollection<Step> _steps;
    private ObservableCollection<Tag> _tags;
    private readonly RecipeService _recipeService;
    private MainWindowViewModel _mainWindowViewModel;
    private string _errorMessage;

    public string ErrorMessage { get => _errorMessage; set => this.RaiseAndSetIfChanged(ref _errorMessage, value); }
    // --------- Inredient stuff-------------
    private string _ingredientName;
    private UnitOfMeasurement _unitOfMeasurement;
    private double _ingredientPrice;
    private int _ingredientQuantity;
    private string _selectedIndex = "0";



    public UnitOfMeasurement UnitOfMeasurement{
        get => _unitOfMeasurement;
        set => this.RaiseAndSetIfChanged(ref _unitOfMeasurement, value);
    }

    public double IngredientPrice{
        get => _ingredientPrice;
        set => this.RaiseAndSetIfChanged(ref _ingredientPrice, value);
    }

    public int IngredientQuantity{
        get => _ingredientQuantity;
        set => this.RaiseAndSetIfChanged(ref _ingredientQuantity, value);
    }
    public string IngredientName{
        get => _ingredientName;
        set => this.RaiseAndSetIfChanged(ref _ingredientName, value);
    }

    public string SelectedIndex{
            get => _selectedIndex;
            set => this.RaiseAndSetIfChanged(ref _selectedIndex, value);
    }
// ----------------------------------------------------------------
// -------------------------- Steps -------------------------------

    private int _stepTime;
    private string _stepInstruction;

    public int StepTime{
        get => _stepTime;
        set => this.RaiseAndSetIfChanged(ref _stepTime, value);
    }
    public string StepInstruction{
        get => _stepInstruction;
        set => this.RaiseAndSetIfChanged(ref _stepInstruction, value);
    }

// ----------------------------------------------------------------
// -----------------------------Tags-------------------------------
    private string _tagName;
    public string TagName{
        get => _tagName;
        set => this.RaiseAndSetIfChanged(ref _tagName, value);
    }
// ---------------------------------------------
// ----------------------------------------------------------------
// --------------------------Recipe-----------------------------
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

    public ObservableCollection<Step> Steps {
        get => _steps;
        set => this.RaiseAndSetIfChanged(ref _steps, value);
    }

    public ObservableCollection<Tag> Tags {
        get => _tags;
        set => this.RaiseAndSetIfChanged(ref _tags, value);
    }
// ----------------------------------------------------------------------
    public ReactiveCommand<Unit,Unit> ChangeToAddIngredientViewCommand { get;}
    public ReactiveCommand<Unit,Unit> ChangeToDashboardViewCommand { get;}
    public MainWindowViewModel MainWindowViewModel { get => _mainWindowViewModel; private set => _mainWindowViewModel = value; }
    public ReactiveCommand<Unit, Unit> CreateIngredient { get; }
    public ReactiveCommand<Unit, Unit> CreateStep { get; }
    public ReactiveCommand<Unit, Unit> CreateTag { get; }
    public ReactiveCommand<Unit, Unit> CreateRecipe { get; }

    
    public CreateRecipeViewModel(SplankContext context, MainWindowViewModel mainWindowViewModel) 
    {
        Ingredients = new ObservableCollection<Ingredient>();
        Steps = new ObservableCollection<Step>();
        Tags = new ObservableCollection<Tag>();
        _recipeService = new RecipeService(context);
        CreateIngredient = ReactiveCommand.Create(CreateIngredientCommand);
        CreateStep = ReactiveCommand.Create(CreateStepCommand);
        CreateTag = ReactiveCommand.Create(CreateTagCommand);
        CreateRecipe= ReactiveCommand.Create(CreateRecipeCommand);
        MainWindowViewModel = mainWindowViewModel;
    }

    public void CreateRecipeCommand() {
        try {
            Recipe recipe = new Recipe(Name, UserSingleton.GetInstance(), Description, Servings, Ingredients, Steps, new() ,Tags);
            _recipeService.CreateRecipe(recipe);
            MainWindowViewModel.ChangeToDashboardView();
        }
        catch(ArgumentException e){
            ErrorMessage = e.Message;
        }
    }

    public void CreateIngredientCommand() {
        try {
            ChangeUnit();
            Ingredient ingredient = new Ingredient {
                Name = IngredientName,
                Quantity = IngredientQuantity,
                UnitOfMeasurement = UnitOfMeasurement,
                Price = IngredientPrice
            };
            Ingredients.Add(ingredient);
        }
        catch (ArgumentException e)
        {
            throw new ArgumentException(e.Message);

        }
    }

    public void ChangeUnit() {
        UnitOfMeasurement unit = UnitOfMeasurement.SPOONS;
        switch (_selectedIndex) {
            case "0":
                unit = UnitOfMeasurement.SPOONS;
                break;
            case "1":
                unit = UnitOfMeasurement.GRAMS;
                break;
            case "2":
                unit = UnitOfMeasurement.CUPS;
                break;
            case "3":
                unit = UnitOfMeasurement.TEASPOONS;
                break;
            case "4":
                unit = UnitOfMeasurement.AMOUNT;
                break;
        }
        _unitOfMeasurement = unit;   
    }

    public void CreateStepCommand() {
        try {
            Step step = new Step(StepTime, StepInstruction); 
            Steps.Add(step);
        } catch (ArgumentException e) {
            throw new ArgumentException(e.Message);
        }
    }

    public void CreateTagCommand() {
        try {
            Tag tag = new Tag(TagName);
            Tags.Add(tag);
        } catch (ArgumentException e) {
            throw new ArgumentException(e.Message);
        }
    }

}
