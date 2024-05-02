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

    public MainWindowViewModel() {
        ChangeToLoginViewCommand = ReactiveCommand.Create(ChangeToLoginView);
        ChangeToRegisterViewCommand = ReactiveCommand.Create(ChangeToRegisterView);
        ChangeToHomeViewCommand = ReactiveCommand.Create(ChangeToHomeView);
        ContentViewModel = new HomeViewModel();
    }

    public void ChangeToLoginView() {
        ContentViewModel = new LoginViewModel(SplankContext.GetInstance());
    }

    public void ChangeToRegisterView() {
        ContentViewModel = new RegisterViewModel(SplankContext.GetInstance());
    }

    public void ChangeToHomeView() {
        ContentViewModel = new HomeViewModel();
    }
}
