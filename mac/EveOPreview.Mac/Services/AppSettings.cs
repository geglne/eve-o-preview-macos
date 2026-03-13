using System.Collections.Generic;

namespace EveOPreview.Mac.Services;

public sealed class AppSettings
{
    public bool TrackClientLocations { get; set; } = true;
    public bool HidePreviewOfActiveClient { get; set; }
    public bool MinimizeInactiveClients { get; set; }
    public bool PreviewsAlwaysOnTop { get; set; }
    public bool HidePreviewsWhenClientNotActive { get; set; }
    public bool UniqueLayoutForEachClient { get; set; }
    public int ThumbnailOpacity { get; set; } = 80;
    public int ThumbnailWidth { get; set; } = 320;
    public int ThumbnailHeight { get; set; } = 200;
    public bool ZoomOnHover { get; set; }
    public int ZoomFactor { get; set; } = 2;
    public bool ShowOverlay { get; set; } = true;
    public bool ShowFrames { get; set; } = true;
    public bool HighlightActiveClient { get; set; } = true;
    public Dictionary<string, OverlayPosition> OverlayPositions { get; set; } = new();
}

public sealed class OverlayPosition
{
    public int X { get; set; }

    public int Y { get; set; }
}
