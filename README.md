# Connect4
A simple Connect4 game written in C#

# Rules
Connect Four (also known as Captain's Mistress, Four Up, Plot Four, Find Four, Fourplay[citation needed], Four in a Row, Four in a Line and Gravitrips (in Soviet Union)) is a two-player connection game in which the players first choose a color and then take turns dropping colored discs from the top into a seven-column, six-row vertically suspended grid. The pieces fall straight down, occupying the next available space within the column. The objective of the game is to be the first to form a horizontal, vertical, or diagonal line of four of one's own discs. Connect Four is a solved game. The first player can always win by playing the right moves.

# User Interface
The game can either be played in the console or in a simple Windows forms interface.

# Prerequisites:
- Microsoft .NET framework 4.5
- Visual Studio 2012, 2013 or 2015 to open the solution

# Projects in the solution:
- Connect4.Console: This is the console version of the application
- Connect4.UI: This is the Windows forms version of the application
- Logic: This contains the code for game logic
- UnitTests: This contains all the unit tests that were run

# Instructions:
- To run the console app, simply run Connect4.exe from the Connect4 application directory and follow the onscreen instructions.
- To run the Windows forms app, run Connect4.UI.exe and then start a new game using the File -> New Game option.

# Limitations and future development
The game currently only supports 2 player mode, with win detection. No AI is built for single player action.
The user interface is also very simplistic and lacking; future enhancements can possibly include drawing lines through the winning discs so that players know which discs caused the game to win.
