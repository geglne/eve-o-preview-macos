using System;
using System.IO;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace EveOPreview.Mac.Services;

public sealed class JsonAppSettingsStore : IAppSettingsStore
{
    private static readonly JsonSerializerOptions SerializerOptions = new()
    {
        WriteIndented = true,
    };

    private readonly string _settingsPath;

    public JsonAppSettingsStore()
    {
        var baseDir = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "EveOPreview.Mac");
        _settingsPath = Path.Combine(baseDir, "settings.json");
    }

    public async Task<AppSettings> LoadAsync(CancellationToken cancellationToken = default)
    {
        if (!File.Exists(_settingsPath))
        {
            return new AppSettings();
        }

        await using var stream = File.OpenRead(_settingsPath);
        var loaded = await JsonSerializer.DeserializeAsync<AppSettings>(stream, SerializerOptions, cancellationToken);
        return loaded ?? new AppSettings();
    }

    public async Task SaveAsync(AppSettings settings, CancellationToken cancellationToken = default)
    {
        var directory = Path.GetDirectoryName(_settingsPath);
        if (!string.IsNullOrWhiteSpace(directory))
        {
            Directory.CreateDirectory(directory);
        }

        await using var stream = File.Create(_settingsPath);
        await JsonSerializer.SerializeAsync(stream, settings, SerializerOptions, cancellationToken);
    }
}
