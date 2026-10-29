# EVE-O Preview for macOS v0.2.0 (fork pre-release)

Fixes for running with the native macOS EVE Online client, tested on macOS 27 (Apple Silicon) with two clients.

These changes have been offered upstream to `ciws/eve-o-preview-macos` and are not merged there yet.

What is fixed:

- **Native macOS EVE clients are detected.** System Events lists the native client with no windows, so v0.1.0 never found it. Windows are now also discovered through CoreGraphics.
- **Clicking a preview or "Activate selected client" switches clients.** Activation no longer depends on System Events, which reports the native client with the wrong process ID.
- **Thumbnail positions are saved.** Dragging a preview on macOS was never detected as a move, so positions weren't saved and every drag also switched clients.
- **"Save" no longer overwrites dragged positions,** and a corrupt `settings.json` is reported instead of silently reset. Preview refresh scales with client count and recovers from a stuck capture. (ciws/eve-o-preview-macos#1)
- **The app is no longer reported as "damaged".** The `.app` bundle is now properly signed (ad-hoc), so macOS shows the normal "could not verify" prompt with **Open Anyway**, and Screen Recording permission applies correctly.
- **The header is readable in Light Mode.** Dark Mode keeps the original look.
- **README:** install steps, macOS 27 permission names, Screen Recording setup, build prerequisites, and troubleshooting.

Important notes:

- still ad-hoc signed and not notarized: the first launch needs **Open Anyway**
- Screen Recording is never prompted for; add it manually in Privacy & Security
- each new build counts as a new app to macOS, so Screen Recording needs re-adding after updating
- Apple Silicon only
- settings from v0.1.0 are kept; the settings format is unchanged

Suggested short GitHub release description:

`Pre-release with fixes for the native macOS EVE client: client detection, switching, saved thumbnail positions, a properly signed app bundle, and a readable Light Mode header.`
