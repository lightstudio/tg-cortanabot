# Experimental Cortana Bot for Telegram

## Status
- Visual Studio 2013: Build & test passed.
- Not many test cases yet. The coverage is extremely low.

## Plugins
- Chinese text chitchat
- Chinese text calculcator

## Dependencies
- ASP.NET 4.5
- HttpAgilityPack
- Fizzler

## TODO
- Image rendering
- Voice via Cortana's TTS(SSML)
- More plugins
= Bug fixes
- More scenario

## How to use
- Setup values in Web.Config(including the debug and release version for Visual Studio Web Publish)
- Deploy the package to IIS w/ ASP.NET 4.5 or higher
- Setup bot token and actions (e.g. /cortana)
- Enjoy

## How to dev
- Install Visual Studio 2013 w/ ASP.NET 4.5
- Open the solution file
- Restore NuGet packages

## Known Issues
- No response in group chat maybe
- Cannot handle metion and reply scenario

## Contact
- Ben.imbushuo Wang
- github#imbushuo.net
- imbushuo.net
