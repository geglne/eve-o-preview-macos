using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EveOPreview.Mac.Models;

namespace EveOPreview.Mac.Services;

public interface IOverlayWindowManager : IDisposable
{
    void SyncOverlays(
        IReadOnlyList<ClientWindowInfo> clients,
        AppSettings settings,
        Func<ClientWindowInfo, Task> activateClientAsync);
}
