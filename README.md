# EVE-O Preview for macOS

This repository contains a macOS port-in-progress of the original Windows-only `eve-o-preview` project.

The goal of the app is simple:

- detect running EVE Online clients
- show live preview thumbnails for them
- let you quickly switch focus to the selected client

## Repository layout

- `mac/EveOPreview.Mac`: the macOS app built with .NET 8 and Avalonia
- `mac/scripts/build-dmg.sh`: build and package script for the macOS `.app` and `.dmg`
- `windows/eve-o-preview`: upstream Windows project kept for reference

## Current macOS status

The macOS app currently supports:

- scanning running windows and matching EVE clients
- live thumbnail overlays
- click-to-activate client switching
- persistent thumbnail positions
- saved local settings
- packaged `.app` and `.dmg` output

The macOS version is still an independent port, not a direct conversion of the Windows codebase.

## Build

From the repository root:

```bash
./mac/scripts/build-dmg.sh
```

Output:

- app bundle: `mac/dist/EVE-O Preview.app`
- dmg: `mac/dist/eve-o-preview-mac-osx-arm64.dmg`

Optional Intel build:

```bash
RUNTIME=osx-x64 ./mac/scripts/build-dmg.sh
```

## macOS permissions

Depending on your system settings, macOS may ask for:

- Accessibility
- Automation
- Screen Recording

These permissions are needed for window discovery, activation, and preview capture.

## Notes

- This repo currently focuses on the macOS port.
- The upstream project in `windows/eve-o-preview` remains Windows-specific because it depends on WinForms and Windows APIs.
