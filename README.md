# ACE.Mods.TreeStats

A TreeStats based data exporter for ACEmulator servers

[![Discord](https://img.shields.io/discord/261242462972936192.svg?label=play+now!&style=for-the-badge&logo=discord)](https://discord.gg/C2WzhP9)

[![License](https://img.shields.io/github/license/acemulator/ace.mods.treestats)](https://github.com/ACEmulator/ACE.Mods.TreeStats/blob/main/LICENSE)[![GitHub last commit (main)](https://img.shields.io/github/last-commit/acemulator/ace.mods.treestats/main)](https://github.com/ACEmulator/ACE.Mods.TreeStats/commits/main)[![Windows CI](https://ci.appveyor.com/api/projects/status/4stu731gorpmcd7b/branch/main?svg=true)](https://ci.appveyor.com/project/LtRipley36706/ace-mods-treestats/branch/main)

[![Download Latest Release](https://img.shields.io/github/v/release/ACEmulator/ACE.Mods.TreeStats?label=latest%20release)![GitHub Release Date](https://img.shields.io/github/release-date/acemulator/ace.mods.treestats)](https://github.com/ACEmulator/ACE.Mods.Metrics/releases/latest)

[![GitHub All Releases](https://img.shields.io/github/downloads/acemulator/ace.mods.treestats/total?label=mod%20downloads)](https://github.com/ACEmulator/ACE.Mods.TreeStats/releases)

## Features

- **Real-time on-demand data output**: Allows your ACEmulator server to be polled for data collection by TreeStats
https://github.com/amoeba/treestats.net/blob/main/docs/asherons-call-server.md#quick-start-for-server-operators

## Installation

1. **Prerequisites**
   - .NET 10.0 runtime
   - [ACEmulator](https://github.com/ACEmulator/ACE) server installation, version 1.75.4736 or higher required
 
2. **Installation Steps**
   - Download the latest release zip file from the releases page
   - Extract the zip file to your ACEmulator server's `Mods` directory
   - Ensure the extracted folder is named `ACE.Mods.TreeStats`
   - Ensure the plugin is enabled in your ACEmulator server configuration
   - Restart your ACEmulator server or reload mods using `/mod f` command from console or in-game.

## Configuration

Edit `Settings.json` to customize the plugin behavior:

```json
{
  "EnableLogging": false,
  "APIBasePath": "/",
  "OutputToConsole": false,
  "EnableEmulatorInfoOutput": true
}
```

### Configuration Options

You can change these options below, but we do recommend the defaults be left as-is.

- **EnableLogging**: Enables output of more verbose logging (default: false)
- **APIBasePath**: Base path that server is published to (default: "/")
- **OutputToConsole**: Enables TreeStats Server raw output to console (default: false)
- **EnableEmulatorInfoOutput**: Enables output of Emulator host information including type, server and database version, and DAT iterations (default: true)

## Troubleshooting

**Plugin not loading:**
- Check that the plugin is in the correct Mods directory
- Verify .NET 10.0 is installed
- Check server logs for error messages

**Settings not loading:**
- Ensure Settings.json is properly formatted
- Check for JSON syntax errors
- Verify file permissions

## Dependencies

- .NET 10.0 Runtime
- [ACEmulator](https://github.com/ACEmulator/ACE) Server (with mod support)
- Lib.Harmony bundled with ACEmulator (for patching)
- ACE.Shared library bundled with this plugin
- GenHTTP libraries bundled with this plugin
