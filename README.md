# HelpopPlugin
HelpopPlugin is a [TShock] plugin that enables cross-server reports or calls for help.

![GitHub license](https://img.shields.io/github/license/Arthri/HelpopPlugin?style=flat-square) ![GitHub release (latest SemVer)](https://img.shields.io/github/v/release/Arthri/HelpopPlugin?sort=semver&style=flat-square) ![GitHub release (latest SemVer including pre-releases)](https://img.shields.io/github/v/release/Arthri/HelpopPlugin?include_prereleases&sort=semver&style=flat-square)

## Disclaimer
This is another one of my overengineered projects that I use for testing. I do not plan to officially provide support, but feel free to raise issues or use this plugin.

## Features
- Cross-server calls for help
- Highly configurable

## Getting Started

### Installation
1. Grab the [latest][Latest Release] release
2. Put the zip in `ServerPlugins` folder
3. Unzip the zip

### Setup
This project requires [Redis] for cross-server communication which natively only supports Linux and macOS/OS X. I will **NOT** be providing support on how to run Redis on Windows.

1. Run Redis
2. Run the server

### Usage
1. `/helpop <Message>`

The message will be broadcasted so all staff in the network can see it.

## Documentation
Further documentation is available [here][Documentation].

## Development

### Prequisites
- .NET 5 SDK or above
- .NET Framework 4.7.2 targetting pack

### Setup Dependencies
1. Restore dotnet tools(run `dotnet tool restore`)
2. Restore dependencies(run `dotnet paket restore`)

### Compile w/Visual Studio
1. Open `HelpopPlugin.sln`
2. Build solution

### Compile w/dotnet CLI
1. Navigate to project root directory
2. Run `dotnet build`

### Get Compiled Files
1. Navigate to `src/HelpopPlugin/bin/{BUILD_CONFIGURATION}/` where `{BUILD_CONFIGURATION}` is either Debug or Release
2. Do stuff with files



<!-- INDEX
    DO NOT REMOVE!!
-->
[Latest Release]: https://github.com/Arthri/HelpopPlugin/releases/latest
[Documentation]: https://arthri.github.io/HelpopPlugin
[TShock]: https://github.com/Pryaxis/TShock
[Redis]: https://redis.io/
