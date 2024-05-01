using ReactiveUI;
using RecipeApp.Context;

namespace RecipeAppUI.ViewModels;

public class MainWindowViewModel : ViewModelBase {
    private ViewModelBase _contentViewModel;
    private SplankContext _context;

    public SplankContext Context { get; private set; }

    public ViewModelBase ContentViewModel {
        get => _contentViewModel;
        private set => this.RaiseAndSetIfChanged(ref _contentViewModel, value);
    }

    public MainWindowViewModel() {
        Context = new SplankContext();
        ContentViewModel = new HomeViewModel();
    }

    public void ChangeToLoginView() {
        ContentViewModel = new LoginViewModel(Context);
    }

    public void ChangeToRegisterView() {
        ContentViewModel = new RegisterViewModel(Context);
    }

    public void ChangeToHomeView() {
        ContentViewModel = new HomeViewModel();
    }
}
