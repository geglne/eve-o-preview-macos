using System;
using System.IO;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Layout;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using EveOPreview.Mac.Models;

namespace EveOPreview.Mac.Views;

public sealed class OverlayThumbnailWindow : Window
{
    private readonly Border _root;
    private readonly Border _header;
    private readonly TextBlock _title;
    private readonly Image _previewImage;
    private Bitmap? _previewBitmap;
    private Func<ClientWindowInfo, Task>? _activateClientAsync;

    public event Action<ClientWindowInfo, PixelPoint>? PositionChangedByUser;

    public OverlayThumbnailWindow()
    {
        SystemDecorations = SystemDecorations.None;
        ShowInTaskbar = false;
        CanResize = false;
        Topmost = true;
        Background = Brushes.Transparent;

        _title = new TextBlock
        {
            Foreground = Brushes.White,
            FontSize = 14,
            Margin = new Thickness(8, 4),
            TextTrimming = TextTrimming.CharacterEllipsis,
        };

        _header = new Border
        {
            Background = new SolidColorBrush(Color.FromRgb(24, 24, 24)),
            BorderBrush = new SolidColorBrush(Color.FromRgb(50, 50, 50)),
            BorderThickness = new Thickness(0, 0, 0, 1),
            Child = _title,
            Cursor = new Cursor(StandardCursorType.SizeAll),
        };

        _previewImage = new Image
        {
            Stretch = Stretch.UniformToFill,
            HorizontalAlignment = HorizontalAlignment.Stretch,
            VerticalAlignment = VerticalAlignment.Stretch,
        };

        var previewContainer = new Border
        {
            Background = new SolidColorBrush(Color.FromRgb(10, 10, 10)),
            Child = _previewImage,
        };

        var layout = new Grid
        {
            RowDefinitions = new RowDefinitions("Auto,*"),
        };
        layout.Children.Add(_header);
        Grid.SetRow(previewContainer, 1);
        layout.Children.Add(previewContainer);

        _root = new Border
        {
            BorderBrush = new SolidColorBrush(Color.FromRgb(60, 60, 60)),
            BorderThickness = new Thickness(1),
            Background = new SolidColorBrush(Color.FromRgb(0, 0, 0)),
            Child = layout,
        };

        Content = _root;

        _root.PointerPressed += RootOnPointerPressed;
    }

    public ClientWindowInfo? Client { get; private set; }

    public void BindClient(ClientWindowInfo client, Func<ClientWindowInfo, Task> activateClientAsync)
    {
        Client = client;
        _activateClientAsync = activateClientAsync;
        _title.Text = client.WindowTitle;
    }

    public void SetOverlayHeaderVisible(bool isVisible)
    {
        _header.IsVisible = isVisible;
    }

    public void UpdateFrame(byte[]? pngBytes)
    {
        try
        {
            if (pngBytes is null || pngBytes.Length == 0)
            {
                return;
            }

            using var stream = new MemoryStream(pngBytes);
            var bitmap = new Bitmap(stream);
            _previewBitmap?.Dispose();
            _previewBitmap = bitmap;
            _previewImage.Source = _previewBitmap;
        }
        catch
        {
            // Ignore invalid frame payloads.
        }
    }

    protected override void OnClosed(EventArgs e)
    {
        _previewBitmap?.Dispose();
        _previewBitmap = null;
        base.OnClosed(e);
    }

    private async void RootOnPointerPressed(object? sender, PointerPressedEventArgs e)
    {
        var point = e.GetCurrentPoint(_root);
        if (!point.Properties.IsLeftButtonPressed)
        {
            return;
        }

        var startPosition = Position;
        try
        {
            BeginMoveDrag(e);
        }
        catch
        {
            // Ignore platform drag failures and fall back to click behavior below.
        }

        e.Handled = true;

        if (Position != startPosition && Client is not null)
        {
            PositionChangedByUser?.Invoke(Client, Position);
            return;
        }

        if (Client is not null && _activateClientAsync is not null)
        {
            await _activateClientAsync(Client);
        }
    }
}
