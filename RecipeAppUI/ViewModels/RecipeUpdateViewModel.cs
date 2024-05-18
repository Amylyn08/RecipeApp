using ReactiveUI;
using RecipeApp.Context;
using RecipeApp.Exceptions;
using RecipeApp.Models;
using RecipeApp.Searcher;
using RecipeApp.Services;
using RecipeAppUI.Session;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reactive;
using Avalonia.Controls;
using System.Linq;
using System.Reactive.Linq;
using Avalonia.Input;
namespace RecipeAppUI.ViewModels;



public class RecipeUpdateViewModel : ViewModelBase {
    private MainWindowViewModel _mainWindowViewModel;
    private Recipe _recipe;
    public Recipe Recipe { get => _recipe; set => _recipe = this.RaiseAndSetIfChanged(ref _recipe, value); }
    public string Name 
    { 
        get => _recipe.Name; 
        set => _recipe.Name = value; 
    }

    public string Description 
    { 
        get => _recipe.Description; 
        set => _recipe.Description = value; 
    }

    public int Servings 
    { 
        get => _recipe.Servings; 
        set => _recipe.Servings = value; 
    }

// --------- Inredient stuff-------------
    private string _ingredientName = null!;
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
    private string _stepInstruction = null!;

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
    private string _tagName = null!;
    public string TagName{
        get => _tagName;
        set => this.RaiseAndSetIfChanged(ref _tagName, value);
    }
// ----------------------------------------------------------------

    public ObservableCollection<Ingredient> Ingredients { get; set; }
    public ObservableCollection<Step> Steps { get; set; }
    public ObservableCollection<Tag> Tags { get; set; }
    public ReactiveCommand<Ingredient, Unit> RemoveIngredientCommand { get; }
    public ReactiveCommand<Step, Unit> RemoveStepCommand { get; }
    public ReactiveCommand<Tag, Unit> RemoveTagCommand { get; }
    public ReactiveCommand<Unit, Unit> CreateIngredientCommand { get; } = null!;
    public ReactiveCommand<Unit, Unit> CreateStepCommand { get; } = null!;
    public ReactiveCommand<Unit, Unit> CreateTagCommand { get; } = null!;


    private readonly RecipeService _recipeService;
    public MainWindowViewModel MainWindowViewModel { get => _mainWindowViewModel; private set => _mainWindowViewModel = value; }
    public RecipeUpdateViewModel(SplankContext context, MainWindowViewModel mainWindowViewModel, Recipe recipe) {
        _recipeService = new RecipeService(context);
        MainWindowViewModel = mainWindowViewModel;
        Recipe = recipe;
        Ingredients = new ObservableCollection<Ingredient>(_recipe.Ingredients);
        Steps = new ObservableCollection<Step>(_recipe.Steps);
        Tags = new ObservableCollection<Tag>(_recipe.Tags);
        RemoveIngredientCommand = ReactiveCommand.Create<Ingredient>(RemoveIngredient);
        RemoveStepCommand = ReactiveCommand.Create<Step>(RemoveStep);
        RemoveTagCommand = ReactiveCommand.Create<Tag>(RemoveTag);
        CreateIngredientCommand = ReactiveCommand.Create(CreateIngredient);
        CreateStepCommand = ReactiveCommand.Create(CreateStep);
        CreateTagCommand = ReactiveCommand.Create(CreateTag);
    }

    private void RemoveIngredient(Ingredient ingredientToRemove)
    {
        Ingredients.Remove(ingredientToRemove);
    }

    private void RemoveStep(Step stepToRemove)
    {
        Steps.Remove(stepToRemove);
    }

    private void RemoveTag(Tag tagToRemove)
    {
        Tags.Remove(tagToRemove);
    }


    public void CreateIngredient() {
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

    public void CreateStep() {
        try {
            Step step = new Step(StepTime, StepInstruction); 
            Steps.Add(step);
        } catch (ArgumentException e) {
            throw new ArgumentException(e.Message);
        }
    }

    public void CreateTag() {
        try {
            Tag tag = new Tag(TagName);
            Tags.Add(tag);
        } catch (ArgumentException e) {
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

}