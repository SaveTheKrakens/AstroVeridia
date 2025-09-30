using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AstroVeridia
{
    internal class GameState
    {
        /*--CLASS LEVEL VARIABLES-----------------------------------------------------VARIABLES--*/
        private bool isGameOver = false;
        private enum GameStates
        {
            TITLESTATE,
            NEWGAMESTATE,
            GAMESTATE
        };
        private static GameStates gameStates;
        private DisplayScreen displayScreen;

        private int level = 1;

        /*--CONSTRUCTOR-------------------------------------------------------------CONSTRUCTOR--*/
        public GameState()
        {
            gameStates = GameStates.TITLESTATE;
            displayScreen = new DisplayScreen();
            displayScreen.DisplayTitleScreen();
            EnterTitleScreenState();
        }

        private void EnterGameState()
        {
            while(gameStates == GameStates.GAMESTATE)
            {
                // Clear and refresh the title
                Console.Clear();
                AnsiConsole.Clear();
                Console.SetCursorPosition(0, 0);
                displayScreen.DisplayGameScreen();

                // For testing
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey(true);
            }
        }

        private void EnterNewGameState()
        {
            while (gameStates == GameStates.NEWGAMESTATE)
            {
                // Clear and refresh the title
                Console.Clear();
                displayScreen.DisplayTitleScreen();

                // Read off the tutorial for new players
                Console.WriteLine("This is the placeholder tutorial");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey(true);

                // Change to the game state
                gameStates = GameStates.GAMESTATE;
                EnterGameState();
            }
        }

        private void EnterTitleScreenState()
        {
            while (gameStates == GameStates.TITLESTATE)
            {
                // Clear and refresh the title screen
                Console.Clear();
                displayScreen.DisplayTitleScreen();

                // Prompt the user
                string menuChoice = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("Please select an option with ^/v and press enter:")
                        .AddChoices(new[] {
                            "New Game", "Load Game", "Exit"
                        })
                    );

                // Check user choices
                // New Game Chosen
                if (menuChoice == "New Game")
                {
                    Console.WriteLine("New Game!");
                    Thread.Sleep(750);
                    gameStates = GameStates.NEWGAMESTATE;
                    EnterNewGameState();
                }
                else if (menuChoice == "Load Game")
                {
                    Console.WriteLine("Sorry, not implemented yet!");
                    Thread.Sleep(2000);
                }
                else if (menuChoice == "Exit")
                {
                    Console.WriteLine("Goodbye!");
                    Thread.Sleep(2000);
                    Environment.Exit(0);
                }
                else
                {
                    Console.WriteLine("Sorry, not an option!");
                    Thread.Sleep(1000);
                }
            }
        }

        /*--GETTERS SETTERS-----------------------------------------------------GETTERS SETTERS--*/
        public bool GetIsGameOver()
        {
            return this.isGameOver;
        }

        public void SetIsGameOver(bool isGameOver)
        {
            this.isGameOver = isGameOver;
        }
    }
}
