using Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect4
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Connect4 by Elgin Lam.");

            Console.WriteLine("Please press 'S' at any time to start a new game, or 'Q' to quit.");

            GameLoop();

            Console.WriteLine("Goodbye and thanks for playing!");

        }

        static void GameLoop()
        {
            while (true)
            {
                string strDimensions = null;
                bool validDimensions = false;
                int rows=0, columns=0;

                while (string.IsNullOrEmpty(strDimensions) || !validDimensions)
                {
                    Console.WriteLine("Please enter the board dimensions  (number of rows, number of columns).");

                    strDimensions = Console.ReadLine();

                    if (strDimensions != null && strDimensions.ToLower() == "s")
                        break;
                    else if (strDimensions != null && strDimensions.ToLower() == "q")
                        return;

                    // Split the input by a space, the first part gives us the row count and the second gives the column count
                    var inputArray = strDimensions.Split(' ');

                    if (inputArray.Length>=2)
                    {
                      
                        if (int.TryParse(inputArray[0],out rows) && int.TryParse(inputArray[1],out columns))
                        {
                            validDimensions = true;
                        }
                    }
                }

                var theGame = new Game(columns,rows);

                // Loop while the game is in progress
                while (theGame.CurrentState == Enums.GameStates.RedsTurn || theGame.CurrentState == Enums.GameStates.YellowsTurn)
                {
                    PrintGameState(theGame);
                    var input = Console.ReadLine();

                    if (input != null && input.ToLower() == "s")
                        break;
                    else if (input != null && input.ToLower() == "q")
                        return;

                    int rowIndex;

                    if (!int.TryParse(input, out rowIndex))
                        Console.WriteLine("Invalid input.");
                    else
                    {
                        try
                        {
                            Disc newDisc;
                            if (theGame.CurrentState == Enums.GameStates.RedsTurn)
                                newDisc = new Disc(Enums.Sides.Red);
                            else
                                newDisc = new Disc(Enums.Sides.Yellow);
                            // The reason we minus one is that the code is zero-indexed,
                            // but the users generally use 1 as the start
                            theGame.AddDisc(newDisc, rowIndex - 1);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }
                }

                // Final print to let the user see the end
                PrintGameState(theGame);
            }
        }

        static void PrintGameState(Game theGame)
        {
            Console.WriteLine(theGame.Board.ToString());
            switch (theGame.CurrentState)
            {
                case Enums.GameStates.Draw:
                    Console.WriteLine("It's a draw!");
                    break;
                case Enums.GameStates.RedsTurn:
                    Console.WriteLine("Red's turn:");
                    break;
                case Enums.GameStates.RedWins:
                    Console.WriteLine("Red wins!");
                    break;
                case Enums.GameStates.YellowsTurn:
                    Console.WriteLine("Yellow's turn");
                    break;
                case Enums.GameStates.YellowWins:
                    Console.WriteLine("Yellow wins!");
                    break;
            }
        }
    }
}
