using AstroVeridia;
using Spectre.Console;
using System;
using System.Runtime.CompilerServices;

class Program
{

    /*--CLASS LEVEL VARIABLES-------------------------------------------------------------------------------------------CLASS VARIABLES--*/
    private static GameState gameState;

    /*--MAIN METHOD---------------------------------------------------------------------------------------------------------MAIN METHOD--*/
    public static void Main(string[] args)
    {
        gameState = new GameState();

        if (!gameState.GetIsGameOver())
        {
            
        }
        else
        {
            Console.Write("Game Over!");
        }
    }

    /*--NOTES--*/

    // POSSIBLE PLAYER COLORS
    // FF00FF C800C8 CC33CC B400B4 D100A3
}