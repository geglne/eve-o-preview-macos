# macOS build notes

This folder contains the macOS app (`EveOPreview.Mac`) and packaging scripts.

## Build DMG

From repository root:

```bash
./mac/scripts/build-dmg.sh
```

Output:

- App bundle: `mac/dist/EVE-O Preview.app`
- Installer image: `mac/dist/eve-o-preview-mac-osx-arm64.dmg`

## Optional Intel build

```bash
RUNTIME=osx-x64 ./mac/scripts/build-dmg.sh
```

## Current status

- Phase 1 implemented:
  - scans running windows and only accepts `exefile` with title format `EVE - <Character>`
  - lists matching clients in the UI
  - activates selected client/window process
- opens floating overlay windows that:
  - stream window frames from the attached character client
  - can be dragged around
  - activate/focus the corresponding client on click
- includes tabs for General/Thumbnail/Zoom/Overlay settings
- settings are saved to `~/Library/Application Support/EveOPreview.Mac/settings.json`
- auto-refresh runs every 2 seconds, and manual refresh is available

## macOS permissions

To allow window detection/switching, macOS may prompt for automation/accessibility permissions.

If needed, enable permissions in:

- System Settings -> Privacy & Security -> Accessibility
- System Settings -> Privacy & Security -> Automation
- System Settings -> Privacy & Security -> Screen Recording
