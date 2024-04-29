using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace SplankUi.Views;

public partial class LoginViewAndRegister : UserControl
{
    public LoginViewAndRegister()
    {
        InitializeComponent();
    }

    
    private void InitializeComponent() {
        AvaloniaXamlLoader.Load(this);
    }
}