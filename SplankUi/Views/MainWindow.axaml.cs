using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace SplankUi.Views;

public partial class MainWindowView : Window
{
    public MainWindowView()
    {
        InitializeComponent();
    }

    private void InitializeComponent() {
        AvaloniaXamlLoader.Load(this);
    }
}