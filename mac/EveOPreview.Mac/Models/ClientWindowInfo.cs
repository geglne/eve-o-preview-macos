namespace EveOPreview.Mac.Models;

public sealed class ClientWindowInfo
{
    public int WindowId { get; init; }

    public int ProcessId { get; init; }

    public required string ProcessName { get; init; }

    public required string WindowTitle { get; init; }

    public string DisplayName =>
        string.IsNullOrWhiteSpace(WindowTitle)
            ? ProcessName
            : $"{ProcessName} - {WindowTitle}";
}
