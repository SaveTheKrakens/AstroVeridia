using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AstroVeridia
{
    internal class DisplayScreen
    {
        private int screenWidth = 100;  //100
        private int screenHeight = 30;  //30
        private string fileTitle = "../../../txt/title.txt";

        private char borderCorner = '+';
        private char borderHorizontal = '-';
        private char borderVertical = '|';

        public DisplayScreen(int screenWidth = 100, int screenHeight = 28)
        {
            this.screenWidth = screenWidth;
            this.screenHeight = screenHeight;

            CheckFiles();
        }

        public void DisplayGameScreen()
        {
            int consoleViewportWidth = this.screenWidth;
            int consoleViewportHeight = 1;
            int infoViewportWidth = 40;
            int mapViewportWidth = this.screenWidth - infoViewportWidth;
            int mapInfoViewportHeight = this.screenHeight - consoleViewportHeight;
            
            // Loop down the height starting at top 0
            for(int y = 0; y < screenHeight; y++)
            {   
                // Loop throught the width starting at left 0
                for(int x = 0; x < screenWidth; x++)
                {
                    // Hold a string for the line
                    string line = "";
                    line += GetNextChar(x, y, mapViewportWidth, mapInfoViewportHeight);
                    
                    Console.Write(line);
                }
                Console.WriteLine();
            }
        }

        public void DisplayTitleScreen()
        {
            // Grab the title
            List<string> titleLines = File.ReadLines(fileTitle).ToList();
            string[] titleColors =
                { "#7CB14D", "#B3A341", "#A7A741", "#D69164", "#C79390", "#AC9B9E" };

            // Find the longest line 
            int longestLineLength = 0;
            foreach(string line in titleLines)
            {
                if(line.Length > longestLineLength)
                {
                    longestLineLength = line.Length;
                }
            }

            // Set a padding based on longest line so we can center the title
            int padding = (int)((screenWidth - longestLineLength) / 2);

            // Make a quick line break border
            string lineBreak = "";
            for(int i = 0; i < screenWidth; i++)
            {
                lineBreak += "-";
            }

            // Print a line break
            Console.WriteLine(lineBreak);

            // Print the title
            for(int i = 0; i < titleLines.Count; i++)
            {
                if(i < titleColors.Length)
                {
                    for(int p = 0; p < padding; p++)
                    {
                        Console.Write(" ");
                    }
                    AnsiConsole.MarkupLine($"[{titleColors[i]}]{titleLines[i]}[/]");
                }
            }

            // Print a line break
            Console.WriteLine(lineBreak);
        }

        private void CheckFiles()
        {
            if (File.Exists(fileTitle))
            {
                Console.WriteLine("title.txt Exists");
            }
            else
            {
                Console.WriteLine("Could not find title.txt");
            }

            Thread.Sleep(500);
        }

        private char GetNextChar(int x, int y, int mapViewportWidth, int mapInfoViewportHeight)
        {
            // Check if 0, 0 and add a corner
            if (y == 0 && x == 0)
            {
                return borderCorner;
            }
            // Check if next corner
            else if (y == 0 && x == mapViewportWidth - 1)
            {
                return borderCorner;
            }
            // Check if next corner
            else if (y == 0 && x == mapViewportWidth)
            {
                return borderCorner;
            }
            // Check if last corner
            else if (y == 0 && x == screenWidth - 1)
            {
                return borderCorner;
            }
            else if (y == 0)
            {
                return borderHorizontal;
            }
            else if (x == 0 && y >= 1 && y < mapInfoViewportHeight - 1)
            {
                return borderVertical;
            }
            else if (x == mapViewportWidth - 1 && y >= 1 && y < mapInfoViewportHeight - 1)
            {
                return borderVertical;
            }
            else if (x == mapViewportWidth && y >= 1 && y < mapInfoViewportHeight - 1)
            {
                return borderVertical;
            }
            else if (x == screenWidth - 1 && y >= 1 && y < mapInfoViewportHeight - 1)
            {
                return borderVertical;
            }
            else if (x == 0 && y >= 1 && y < mapInfoViewportHeight)
            {
                return borderCorner;
            }
            else if (x == mapViewportWidth - 1 && y >= 1 && y < mapInfoViewportHeight)
            {
                return borderCorner;
            }
            else if (x == mapViewportWidth && y >= 1 && y < mapInfoViewportHeight)
            {
                return borderCorner;
            }
            else if (x == screenWidth - 1 && y >= 1 && y < mapInfoViewportHeight)
            {
                return borderCorner;
            }
            else if (y == mapInfoViewportHeight - 1)
            {
                return borderHorizontal;
            }
            else
            {
                return '?';
            }
        }
    }
}
