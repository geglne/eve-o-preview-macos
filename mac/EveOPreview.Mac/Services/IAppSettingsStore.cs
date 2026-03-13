using System.Threading;
using System.Threading.Tasks;

namespace EveOPreview.Mac.Services;

public interface IAppSettingsStore
{
    Task<AppSettings> LoadAsync(CancellationToken cancellationToken = default);

    Task SaveAsync(AppSettings settings, CancellationToken cancellationToken = default);
}
