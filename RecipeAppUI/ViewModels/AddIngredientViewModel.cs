namespace RecipeAppUI.ViewModels;
using ReactiveUI;
using RecipeApp.Context;
using RecipeApp.Exceptions;
using RecipeApp.Models;
using RecipeApp.Searcher;
using RecipeApp.Services;
using System;
using System.Collections.Generic;
using System.Reactive;
using Avalonia.Controls;
public class AddIngredientViewModel : ViewModelBase {

    private string _name;
    private double _price;
    private int _quantity;
    private UnitOfMeasurement _unitOfMeasurement;


    public string Name{
        get => _name;
        set => this.RaiseAndSetIfChanged(ref _name, value);
    }

    public double Price{
        get => _price;
        set => this.RaiseAndSetIfChanged(ref _price, value);
    }
    
    public int Quantity{
        get => _quantity;
        set => this.RaiseAndSetIfChanged(ref _quantity, value);
    }
    
    public UnitOfMeasurement UnitOfMeasurement{
        get => _unitOfMeasurement;
        set => this.RaiseAndSetIfChanged(ref _unitOfMeasurement, value);
    }

    private MainWindowViewModel _mainWindowViewModel;
    public CreateRecipeViewModel CreateRecipe {get;}
    private string _ingredientErrorMessage = "";
    public string IngredientErrorMessage { get => _ingredientErrorMessage; set => this.RaiseAndSetIfChanged(ref _ingredientErrorMessage, value); }

    private string _selectedIndex = "0";
    
    public string SelectedIndex{
        get => _selectedIndex;
        set => this.RaiseAndSetIfChanged(ref _selectedIndex, value);
    }


    public ReactiveCommand<Unit,Unit> AddIngredientCommand { get;}

    public AddIngredientViewModel(SplankContext context, MainWindowViewModel mainWindowViewModel){
        AddIngredientCommand = ReactiveCommand.Create(AddIngredient);
        _mainWindowViewModel = mainWindowViewModel;
    }

    public void AddIngredient(){
       try
       {
        UnitChange();
         Ingredient newIngredient = new Ingredient(Name, Quantity, UnitOfMeasurement, Price);
         CreateRecipe.Ingredients.Add(newIngredient);
         _mainWindowViewModel.ChangeToCreateRecipeView();
       }
       catch (ArgumentException e)
       {
        _ingredientErrorMessage = e.Message;
       }

    }

    private void UnitChange()
    {
        try
        {
            switch (_selectedIndex)
            {
                case "0":
                    UnitOfMeasurement = UnitOfMeasurement.SPOONS;
                    break;
                case "1":
                    UnitOfMeasurement = UnitOfMeasurement.GRAMS;
                    break;
                case "2":
                    UnitOfMeasurement = UnitOfMeasurement.CUPS;
                    break;
                case "3":
                    UnitOfMeasurement = UnitOfMeasurement.TEASPOONS;
                    break;
                case "4":
                    UnitOfMeasurement = UnitOfMeasurement.AMOUNT;
                    break;
            }
        }
        catch (ArgumentException e)
        {
            IngredientErrorMessage = e.Message;
        }
    }

}