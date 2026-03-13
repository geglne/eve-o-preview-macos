# EVE-O Preview for macOS

`EVE-O Preview for macOS` is a native macOS companion app for running multiple EVE Online clients.

It scans running clients, shows live preview overlays, and lets you quickly switch focus to the selected window.

## Features

- detect running EVE Online clients
- live thumbnail overlays
- click-to-focus client switching
- saved local settings
- persistent thumbnail positions
- packaged macOS `.app` and `.dmg`

## Tech

- .NET 8
- Avalonia UI
- macOS window discovery via CoreGraphics and AppleScript/System Events

## Build

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

## Permissions

Depending on macOS settings, the app may request access to:

- Accessibility
- Automation
- Screen Recording

These are used for client discovery, activation, and preview capture.

## Status

This is an early public macOS release focused on the working core workflow:

- find running EVE clients
- preview them
- switch between them quickly

Some original desktop features are still planned and not yet implemented.
