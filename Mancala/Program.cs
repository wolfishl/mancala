using System;

namespace Mancala
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Mancalla, the board is being set up...");
            Board board = new Board();
            Console.WriteLine("Computer's pits are on top, yours are on the bottom, the order is counterclockwise. \nLet's begin");
            board.show();
            while (true)
            {
                Console.WriteLine("Computer's turn");
                int pitPicked = MiniMaxi.callingFunction(board, true, 4, new int[6]);
                Console.WriteLine($"Computer picks {++pitPicked}");
                board.turn(board.computerPits, board.computerMancala, board.playerPits, board.computerPits[--pitPicked], true);
                board.show();
                if (board.checkGameOver())
                {
                    Console.WriteLine("Game over!");
                    board.show();
                    break;
                }
                Console.WriteLine("Your turn");
                board.turn(board.playerPits, board.playerMancala, board.computerPits, getInput(board), false);
                board.show();
                if (board.checkGameOver())
                {
                    Console.WriteLine("Game over!");
                    board.show();
                    break;
                }
            }
            Console.WriteLine($"Computer: {board.computerMancala.howMany()} Player: {board.playerMancala.howMany()}");
            String winner = board.computerMancala.howMany() > board.playerMancala.howMany() ? "The computer won..." :
                board.computerMancala.howMany() == board.playerMancala.howMany() ? "It was a tie" : "You won!";
            Console.WriteLine(winner);
            Console.WriteLine("Game over!");

        }


        public static Pit getInput(Board board)
        {
            Pit pit = new Pit(0); 
            bool good = false;
            while (!good)
            {
                Console.WriteLine("Please enter the number of the pit you would like to choose (from 1 to 6)");
                String num = Console.ReadLine();
                int number;
                int.TryParse(num, out number);
                if (number > 0 && number < 7)
                {
                    number--;
                    if (!board.playerPits[number].isEmpty())
                    {
                        pit = board.playerPits[number];
                        good = true;
                    }
                    else
                    {
                        Console.WriteLine("That pit is empty, please choose another pit");
                    }
                }
                else
                {
                    Console.WriteLine("That was not a valid number");
                }
            }
            return pit;
        }



    }
}
