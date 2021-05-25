using System;
using System.Collections.Generic;
using System.Text;

namespace Mancala
{
    class MiniMaxi
    {

        public static int callingFunction(Board board, bool computer, int depth, int[] currentLevel)
        {
            Board tempBoard;
            int maxValue = -48;
            int retIndex = 0;
            for (int index = 0; index < board.computerPits.Length; index++)
            {
                if (computer && board.computerPits[index].isEmpty())
                {
                    continue;
                }
                else if (!computer && board.playerPits[index].isEmpty())
                {
                    continue;
                }
                tempBoard = board.copy();
                if (depth > 0)
                {
                    int startingAmount = doingTurn(tempBoard, computer, index);
                    if (tempBoard.checkGameOver())
                    {
                        currentLevel[index] = tempBoard.hueristic();
                        continue;
                    }
                    else
                    {
                        currentLevel[index] = miniMaxi(tempBoard, !computer, --depth, new int[6]);
                    }
                }
                else
                {
                    if (computer)
                    {
                        tempBoard.turn(tempBoard.computerPits, tempBoard.computerMancala, tempBoard.playerPits, tempBoard.computerPits[index], computer);
                    }
                    else
                    {
                        tempBoard.turn(tempBoard.playerPits, tempBoard.playerMancala, tempBoard.computerPits, tempBoard.playerPits[index], computer);
                    }

                    currentLevel[index] = tempBoard.hueristic();
                }
            }
            for(int i = 0; i < currentLevel.Length; i++)
            {
                if( currentLevel[i] > maxValue)
                {
                    maxValue = currentLevel[i];
                    retIndex = i;
                }
            }
            return retIndex;
        }
        public static int miniMaxi(Board board, bool computer, int depth, int[] currentLevel)
        {
            Board tempBoard;
            int maxValue = -48;
            int minValue = 48;
            for (int index = 0; index < board.computerPits.Length; index++)
            {
                if (computer && board.computerPits[index].isEmpty())
                {
                    continue;
                }
                else if (!computer && board.playerPits[index].isEmpty())
                {
                    continue;
                }
                tempBoard = board.copy();
                if (depth > 0)
                {
                    if (tempBoard.checkGameOver())
                    {
                        currentLevel[index] = tempBoard.hueristic();
                        continue;
                    }
                    else
                    {
                        currentLevel[index] = miniMaxi(tempBoard, !computer, --depth, new int[6]);
                    }
                }
                else
                {
                    if (computer)
                    {
                        tempBoard.turn(tempBoard.computerPits, tempBoard.computerMancala, tempBoard.playerPits, tempBoard.computerPits[index], computer);
                    }
                    else
                    {
                        tempBoard.turn(tempBoard.playerPits, tempBoard.playerMancala, tempBoard.computerPits, tempBoard.playerPits[index], computer);
                    }
                    bool gameOver = tempBoard.checkGameOver();
                    currentLevel[index] = tempBoard.hueristic();
                }
            }
            foreach (int value in currentLevel)
            {
                if (computer && value > maxValue)
                {
                    maxValue = value;
                }
                else if (!computer && value < minValue)
                {
                    minValue = value;
                }
            }
            int retValue = computer ? maxValue : minValue;
            return retValue;
        }


        public static int doingTurn(Board board, bool computer, int index)
        {
            int startingAmount;
            if (computer)
            {
                startingAmount = board.computerMancala.howMany();
                board.turn(board.computerPits, board.computerMancala, board.playerPits, board.computerPits[index], computer);
            }
            else
            {
                startingAmount = board.playerMancala.howMany();
                board.turn(board.playerPits, board.playerMancala, board.computerPits, board.playerPits[index], computer);
            }
            return startingAmount;
        }

       
    }

}
