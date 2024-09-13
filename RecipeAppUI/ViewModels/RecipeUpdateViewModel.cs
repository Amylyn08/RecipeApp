using ReactiveUI;
using RecipeApp.Context;
using RecipeApp.Models;
using RecipeApp.Services;
using RecipeAppUI.Session;
using System;
using System.Collections.ObjectModel;
using System.Reactive;
using System.Linq;
using System.Reactive.Linq;
namespace RecipeAppUI.ViewModels;



public class RecipeUpdateViewModel : ViewModelBase {
    private MainWindowViewModel _mainWindowViewModel = null!;
    private Recipe _recipe;
    public Recipe Recipe { get => _recipe; set => _recipe = this.RaiseAndSetIfChanged(ref _recipe, value); }
    public Recipe NewRecipe { get; set; } = null!;
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

    private string _errorMessage = null!;

    public string ErrorMessage { get => _errorMessage; set => this.RaiseAndSetIfChanged(ref _errorMessage, value); }
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
    public ReactiveCommand<Unit, Unit> UpdateRecipeCommand { get; } = null!;

    private readonly RecipeService _recipeService;
    public MainWindowViewModel MainWindowViewModel { get => _mainWindowViewModel; private set => _mainWindowViewModel = value; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="context">DB context</param>
    /// <param name="mainWindowViewModel">Instance of the MainWindowViewModel</param>
    /// <param name="recipe">Recipe you are updating</param>
    public RecipeUpdateViewModel(SplankContext context, MainWindowViewModel mainWindowViewModel, Recipe recipe) {
        _recipeService = new RecipeService(context);
        MainWindowViewModel = mainWindowViewModel;
        Recipe = recipe;
        Ingredients = new ObservableCollection<Ingredient>(_recipe!.Ingredients);
        Steps = new ObservableCollection<Step>(_recipe.Steps);
        Tags = new ObservableCollection<Tag>(_recipe.Tags);
        RemoveIngredientCommand = ReactiveCommand.Create<Ingredient>(RemoveIngredient);
        RemoveStepCommand = ReactiveCommand.Create<Step>(RemoveStep);
        RemoveTagCommand = ReactiveCommand.Create<Tag>(RemoveTag);
        CreateIngredientCommand = ReactiveCommand.Create(CreateIngredient);
        CreateStepCommand = ReactiveCommand.Create(CreateStep);
        CreateTagCommand = ReactiveCommand.Create(CreateTag);
        UpdateRecipeCommand= ReactiveCommand.Create(UpdateRecipe);
    }

    /// <summary>
    /// Removes the ingredient from the list of ingredients
    /// </summary>
    /// <param name="ingredientToRemove">Ingredient to remove</param>
    private void RemoveIngredient(Ingredient ingredientToRemove)
    {
        Ingredients.Remove(ingredientToRemove);
    }

    /// <summary>
    /// Removes the step from the list of steps
    /// </summary>
    /// <param name="stepToRemove">Step to remove</param>
    private void RemoveStep(Step stepToRemove)
    {
        Steps.Remove(stepToRemove);
    }

    /// <summary>
    /// Removes the tag from the list of tags
    /// </summary>
    /// <param name="tagToRemove">Tag to remove</param>
    private void RemoveTag(Tag tagToRemove)
    {
        Tags.Remove(tagToRemove);
    }


    /// <summary>
    /// Creates an ingredient and adds it to the list
    /// </summary>
    /// <exception cref="ArgumentException"></exception>
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

    /// <summary>
    /// Creates a step and adds it to the list of steps
    /// </summary>
    /// <exception cref="ArgumentException"></exception>
    public void CreateStep() {
        try {
            Step step = new Step(StepTime, StepInstruction); 
            Steps.Add(step);
        } catch (ArgumentException e) {
            throw new ArgumentException(e.Message);
        }
    }

    /// <summary>
    /// Creates a tag and adds it to the list of tags
    /// </summary>
    /// <exception cref="ArgumentException"></exception>
    public void CreateTag() {
        try {
            Tag tag = new Tag(TagName);
            Tags.Add(tag);
        } catch (ArgumentException e) {
            throw new ArgumentException(e.Message);
        }
    }

    /// <summary>
    /// Changes the unit of ingredient depending of the combo box index
    /// </summary>
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

    /// <summary>
    /// Command that updates the recipe
    /// </summary>
    public void UpdateRecipe() {
        try {

            Recipe recipe = new Recipe(Name, UserSingleton.GetInstance(), Description, Servings, Ingredients.ToList(), Steps.ToList(), new() ,Tags.ToList());
            _recipeService.UpdateRecipe(Recipe, recipe);
            MainWindowViewModel.ChangeToRecipesView();
        }
        catch(ArgumentException e){
            ErrorMessage = e.Message;
        }
    }

}