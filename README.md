# EVE-O Preview for macOS

![EVE-O Preview icon](mac/EveOPreview.Mac/icon.png)

`EVE-O Preview for macOS` is a lightweight companion app for EVE Online multiboxing on macOS.

It helps you keep track of multiple running clients with live preview overlays and quick client switching, while staying focused on a simple, practical workflow.

> [!NOTE]
> **This is a fork with a v0.2.0 pre-release build.** It contains fixes that have been offered upstream to [ciws/eve-o-preview-macos](https://github.com/ciws/eve-o-preview-macos) but are not merged there yet. The rest of this README describes the app in general; see [Install this pre-release build](#install-this-pre-release-build) for this fork.

## Install this pre-release build

What v0.2.0 fixes compared with v0.1.0 is listed in [RELEASE_NOTES_v0.2.0.md](RELEASE_NOTES_v0.2.0.md).

1. Optional: back up your settings so you can roll back:
   ```bash
   cp ~/Library/Application\ Support/EveOPreview.Mac/settings.json ~/Desktop/eveo-settings-backup.json
   ```
2. Quit any running EVE-O Preview.
3. Download `eve-o-preview-mac-osx-arm64.dmg` from the [v0.2.0 release](https://github.com/geglne/eve-o-preview-macos/releases/tag/v0.2.0), open it, and drag **EVE-O Preview** to Applications, replacing the old copy.
4. Open it. When macOS says it *"could not verify"* the app, click **Done**, then **System Settings → Privacy & Security → Open Anyway**.
5. Grant permissions as described in [macOS permissions](#macos-permissions). If you used v0.1.0 before, remove its **EVE-O Preview** entry under **Screen & System Audio Recording** and add the new app, then quit and reopen.

To roll back, delete the app, reinstall [v0.1.0](https://github.com/ciws/eve-o-preview-macos/releases/tag/v0.1.0), and copy your settings backup back into `~/Library/Application Support/EveOPreview.Mac/`.

Found a problem with this build? Please open an issue on [this fork](https://github.com/geglne/eve-o-preview-macos/issues).

## Screenshots

Main app window:

![Main app window](app-main-window.png)

Overlay previews in action:

![Overlay previews in action](overlay-in-action.png)

## What it does

- detects running EVE Online clients
- shows live thumbnail previews
- lets you click a preview to focus the matching client
- remembers thumbnail positions
- saves local app settings
- ships as a ready-to-test macOS `.app` and `.dmg`

## Built for macOS

This project is a dedicated macOS implementation built with:

- .NET 8
- Avalonia UI
- CoreGraphics
- AppleScript / System Events

## Install

Requirements: macOS 12 or later on Apple Silicon (M1 or newer). The prebuilt release is `arm64` only; Intel Macs need to [build from source](#build) with `RUNTIME=osx-x64`.

1. Download `EVE-O.Preview.zip` from the [latest release](https://github.com/ciws/eve-o-preview-macos/releases/latest).
2. Unzip it and move `EVE-O Preview.app` to `/Applications`.
3. The app is ad-hoc signed and not notarized, so Gatekeeper blocks the first launch with *"Apple could not verify…"*. Click **Done**, then go to **System Settings → Privacy & Security** and click **Open Anyway**.
   - If macOS instead says the app *"is damaged and can't be opened"* (v0.1.0), click **Cancel** and remove the quarantine flag from Terminal:

     ```bash
     xattr -dr com.apple.quarantine "/Applications/EVE-O Preview.app"
     ```

4. Launch the app and grant the permissions listed under [macOS permissions](#macos-permissions). Screen Recording is not prompted for; add it manually, then quit and reopen the app.

## How to use

1. Start one or more EVE Online clients and log in a character on each. Clients are detected by their `EVE - <Character>` window title, so clients at the launcher or character selection screen are not listed.
2. Launch `EVE-O Preview for macOS`.
3. Allow macOS permissions if prompted.
4. The client list refreshes automatically every 2 seconds. Use the main window to manage settings.
5. Click a floating preview to bring the matching client to the front. Drag a preview to move it; its position is remembered per character.

## Build

Prerequisites:

- Xcode Command Line Tools (`xcode-select --install`) for `sips`, `iconutil`, and `hdiutil`
- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)

With Homebrew, `dotnet@8` is keg-only, so it isn't on your `PATH` after installing. Add it:

```bash
brew install dotnet@8
echo 'export PATH="/opt/homebrew/opt/dotnet@8/bin:$PATH"' >> ~/.zshrc
echo 'export DOTNET_ROOT="/opt/homebrew/opt/dotnet@8/libexec"' >> ~/.zshrc
source ~/.zshrc
dotnet --version   # 8.0.x
```

From the repository root:

```bash
./mac/scripts/build-dmg.sh
```

Build output:

- `mac/dist/EVE-O Preview.app`
- `mac/dist/eve-o-preview-mac-osx-arm64.dmg`

Optional Intel build:

```bash
RUNTIME=osx-x64 ./mac/scripts/build-dmg.sh
```

`open` on its own relaunches whatever is already in `mac/dist`, so re-run the build script after pulling changes.

## macOS permissions

All three are in **System Settings → Privacy & Security**:

| Permission | Used for | How it's granted |
|---|---|---|
| Automation → System Events | finding and raising client windows | prompt on first launch → **Allow** |
| Accessibility (**Device Control and Data Access** on macOS 27) | controlling other apps' windows | prompt → **Open System Settings** → turn on EVE-O Preview |
| Screen Recording (**Screen & System Audio Recording**) | reading client window titles and capturing previews | **no prompt** → click **+**, add EVE-O Preview, turn it on, then quit and reopen the app |

Without Screen Recording, macOS hides other apps' window titles, so no clients are detected.

Each ad-hoc signed build counts as a new app to macOS. After installing a new version or rebuilding, remove the old **EVE-O Preview** entry under Screen Recording with **−** and add the new one, or the permission won't apply.

## Troubleshooting

- **"No EVE windows found" while clients are running:** make sure a character is logged in, then check that Screen Recording is granted to *this* copy of the app (see above) and relaunch.
- **"is damaged and can't be opened":** see step 3 of [Install](#install).

## Project status

This is an early public macOS release focused on the core experience:

- find running EVE clients
- preview them live
- switch between them quickly

## Known limitations

- this is still an early macOS release
- some settings are visible but not implemented yet
- feature parity with the Windows ecosystem is not complete
- Apple Silicon build is the default packaged output

## Credits

- macOS port and current project work by `SihKic`
- inspired by the original `EVE-O Preview` idea and workflow created by the wider EVE multiboxing community

## Support

If this project is useful to you and you would like to support it, ISK donations are welcome in EVE Online.

- in-game character: `SihKic`

## Release

Prebuilt downloads are on the [Releases page](https://github.com/ciws/eve-o-preview-macos/releases). The current release ships `EVE-O.Preview.zip` (Apple Silicon `.app`).

Running `./mac/scripts/build-dmg.sh` locally produces:

- `mac/dist/eve-o-preview-mac-osx-arm64.dmg`
