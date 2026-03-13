using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using EveOPreview.Mac.Models;

namespace EveOPreview.Mac.Services;

public interface IMacWindowService
{
    Task<IReadOnlyList<ClientWindowInfo>> GetWindowsAsync(CancellationToken cancellationToken = default);

    Task ActivateWindowAsync(ClientWindowInfo window, CancellationToken cancellationToken = default);

    Task<byte[]?> CaptureWindowPreviewPngAsync(int windowId, CancellationToken cancellationToken = default);
}
