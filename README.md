# hangMe!
hangMe! is a full game written entirely in c# (Client, Gameservers, API, etc) .NET 4.8. This includes the entire source code for it.

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
