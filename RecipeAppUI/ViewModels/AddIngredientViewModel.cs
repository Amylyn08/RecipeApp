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

    public AddIngredientViewModel(SplankContext context){}

    // public 
}