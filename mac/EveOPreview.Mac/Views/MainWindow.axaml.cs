using System;
using Avalonia.Controls;

namespace EveOPreview.Mac.Views;

public partial class MainWindow : Window
{
    private static readonly Uri HelpUri = new("https://github.com/ciws/eve-o-preview-macos");

    public MainWindow()
    {
        InitializeComponent();
        Opened += OnOpened;
        SizeChanged += OnSizeChanged;
    }

    private void OnOpened(object? sender, EventArgs e)
    {
        UpdateResponsiveLayout();
    }

    private void OnSizeChanged(object? sender, SizeChangedEventArgs e)
    {
        UpdateResponsiveLayout();
    }

    private async void OnHelpClicked(object? sender, EventArgs e)
    {
        try
        {
            await Launcher.LaunchUriAsync(HelpUri);
        }
        catch
        {
            // Ignore failures to open the browser.
        }
    }

    private void UpdateResponsiveLayout()
    {
        if (this.FindControl<TextBlock>("SubtitleText") is { } subtitleText)
        {
            subtitleText.IsVisible = Bounds.Width >= 660;
        }
    }
}
