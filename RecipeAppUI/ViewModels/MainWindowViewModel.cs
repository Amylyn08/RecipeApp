using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using ReactiveUI;
using RecipeApp.Context;
using System.Reactive;

namespace RecipeAppUI.ViewModels;

public class MainWindowViewModel : ViewModelBase {
    private ViewModelBase _contentViewModel;

    public ViewModelBase ContentViewModel {
        get => _contentViewModel;
        private set => this.RaiseAndSetIfChanged(ref _contentViewModel, value);
    }

    public ReactiveCommand<Unit, Unit>  ChangeToLoginViewCommand { get; }
    public ReactiveCommand<Unit, Unit> ChangeToRegisterViewCommand { get; }
    public ReactiveCommand<Unit, Unit> ChangeToHomeViewCommand { get; }
    public ReactiveCommand<Unit, Unit> ChangeToDashboardViewCommand { get; }


    public MainWindowViewModel() {
        ChangeToLoginViewCommand = ReactiveCommand.Create(ChangeToLoginView);
        ChangeToRegisterViewCommand = ReactiveCommand.Create(ChangeToRegisterView);
        ChangeToHomeViewCommand = ReactiveCommand.Create(ChangeToHomeView);
        ChangeToDashboardViewCommand = ReactiveCommand.Create(ChangeToDashboardView);
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
        ContentViewModel = new DashboardViewModel(SplankContext.GetInstance());
    }
}

//peackaboo prabhjot
