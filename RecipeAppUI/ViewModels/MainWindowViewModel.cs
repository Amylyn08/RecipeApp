using ReactiveUI;

namespace RecipeAppUI.ViewModels;

public class MainWindowViewModel : ViewModelBase {
    private ViewModelBase _contentViewModel;

    public ViewModelBase ContentViewModel {
        get => _contentViewModel;
        private set => this.RaiseAndSetIfChanged(ref _contentViewModel, value);
    }

    public MainWindowViewModel() {
        ContentViewModel = new HomeViewModel();
    }

    public void SwitchToLoginView() {
        ContentViewModel = new LoginViewModel();
    }
}
