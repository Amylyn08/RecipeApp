using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace SplankUi.Views;

public partial class LoginViewAndRegisterView : UserControl
{
    public LoginViewAndRegisterView()
    {
        InitializeComponent();
    }

    
    private void InitializeComponent() {
        AvaloniaXamlLoader.Load(this);
    }
}