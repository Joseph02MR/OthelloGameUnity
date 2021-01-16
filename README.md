# OthelloGameUnity
This repo contains the Othello game  I designed with Iván León for the Data Structures assignature in the ITC


The main menu has 3 buttons:

Jugar: Opens configuration for a game versus player or AI and the color for the player one
Dificultad: Sets the funcionallity of the AI. "Fácil" (easy) configures the AI to select random movements. "Normal" AI selects movements using Minimax algorithm
Salir : Closes the game.


In the game screen the player has 3 buttons.

Pasar: The player that has no movements available has to pass to keep the game running
	If both players in a PVP game pass, the game is over.

Reiniciar: (Reset) Starts a new game with the pre established configuration.

Salir: (Quit) Exits to the main menu.


The game ends when both players have no available movements or the board has all the 64 tiles ocupied by a token. The winner will be shown as "Jugador 1", "Jugador 2" or "Empate".
