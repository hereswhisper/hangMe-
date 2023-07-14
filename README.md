# hangMe!
hangMe! is a full game written entirely in c# (Client, Gameservers, API, etc) .NET 4.8. This includes the entire source code for it.

# Classes Implemented
This includes Enums and Classes:<br>
**CLIENT:**
AGSConnecter - 0%<br>
APIConnecter - 0%<br>
AScreenPositioner - 100%<br>
AWidgetCreator - 60%<br>
EHangClientType - 100%<br>
EHangMatchState - 100%<br>
AMusicPlayer - 100%<br>
AReplicator - 2%<br>
AHangGameState - 100% (just for holding client version of the server variables)<br>
ASpectatorGameState - 100% (just for holding client version of the server variables)<br>
ADisclaimerScreen - 100%<br>
AStartMenuScreen - 1%<br>
ATemplateScreen<br>
ClientEntry<br><br>
**SERVER:**
ChallengeConfig - 2% (contains ACK sizes, etc)<br>
EHangClientType - 100%<br>
EHangMatchState - 100%<br>
AEngineBaseGameState - 100% (not supposed to have anything in it)<br>
AHangGameState - 10%<br>
ASpectatorGameState - 2%<br>
GUIDGenerator - 100%<br>
ServerEntry - 0%<br><br>
**COMMON:**
AConsoleUtilities - 5%<br>
AMusicSheets - 2% (Contains all music in the game, later I hope to implement playing of MP3 and OGG files)<br><br>


# Guide for Contributions
if you want to contribute to the hangMe project. Please follow these strict instructions that I have set out in place so it's clean for me.

## Widgets
When making new Widgets please follow this guide to keep everything in perfect order<br>
1. Please use the **ATemplateScreen** as the base EVERY time. This is in "HangMe\Engine\Client\Classes\Widgets\ATemplateScreen".<br>
2. If you made the widget for it. Please at the top of the file please add the following piece of code<br>
```
// THIS CODE IS MADE FOR hangMe! AND CANNOT BE USED BY ANYTHING ELSE.
// Contributor:
// Date of Code:
// Description:
```
<br>
3. In the showContents method. Make sure that at the beginning please add AConsoleUtilities smoothWriter = new AConsoleUtilities();<br>
4. In the same method at the end of it add return.
