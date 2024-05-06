using RecipeApp.Services;
using RecipeApp.Context;
using RecipeApp.Security;
using RecipeApp.Exceptions;
using ReactiveUI;
using System;
using System.Reactive;


namespace RecipeAppUI.ViewModels {
    public class DeleteAccountViewModel : ViewModelBase {
        private string _accountDeletionErrorMessage;
        private string _username;
        private string _password;
        private UserService _userService;
        private MainWindowViewModel _mainWindowViewModel;

        public string AccountDeletionErrorMessage {
            get => _accountDeletionErrorMessage;
            set => this.RaiseAndSetIfChanged(ref _accountDeletionErrorMessage, value);
        }

        public string Username {
            get => _username;
            set => this.RaiseAndSetIfChanged(ref _username, value);
        }

        public string Password {
            get => _password;
            set => this.RaiseAndSetIfChanged(ref _password, value);
        }

        public UserService UserService {
            get => _userService;
            private set => _userService = value;
        }

        public MainWindowViewModel MainWindowViewModel { 
            get => _mainWindowViewModel; 
            private set => _mainWindowViewModel = value; 
        }

        public ReactiveCommand<Unit, Unit> DeleteAccountCommand { get; }

        public DeleteAccountViewModel(SplankContext context, MainWindowViewModel mainWindowViewModel){
            UserService = new UserService(context, new PasswordEncrypter());
            MainWindowViewModel = mainWindowViewModel;
        }

        public void DeleteAccount() {
            try {
                var user = UserService.Login(Username, Password);
                UserService.DeleteAccount(user);
                MainWindowViewModel.ChangeToHomeView();
            } catch (Exception e) {
                if (e is ArgumentException || e is UserDoesNotExistException || e is InvalidCredentialsException) {
                    AccountDeletionErrorMessage = e.Message;
                } else {
                    AccountDeletionErrorMessage = "Cannot delete account at this time, please try again later.";
                }
            }
        }
    }
}