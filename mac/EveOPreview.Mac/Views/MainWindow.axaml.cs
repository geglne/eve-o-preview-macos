using System;
using Avalonia.Controls;

namespace EveOPreview.Mac.Views;

public partial class MainWindow : Window
{
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

    private void UpdateResponsiveLayout()
    {
        if (this.FindControl<TextBlock>("SubtitleText") is { } subtitleText)
        {
            subtitleText.IsVisible = Bounds.Width >= 660;
        }
    }
}
