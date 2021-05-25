using System;
using System.Collections.Generic;
using System.Text;

namespace Mancala
{
    class Board
    {
        public Pit[] playerPits = new Pit[6];
        public Pit[] computerPits = new Pit[6];

        public Pit playerMancala;
        public Pit computerMancala;

        public Board()
        {
            playerMancala = new Pit(0);
            computerMancala = new Pit(0);

            setUp(playerPits);
            setUp(computerPits);
        }

        public Board(Board board)
        {
            playerMancala = new Pit(board.playerMancala.howMany());
            computerMancala = new Pit(board.computerMancala.howMany());

            for(int i = 0; i < computerPits.Length; i++)
            {
                computerPits[i] = new Pit(board.computerPits[i].howMany(), i);
                playerPits[i] = new Pit(board.playerPits[i].howMany(), i);
            }
        }


        public void setUp(Pit[] pits)
        {
            for (int i = 0; i < pits.Length; i++)
            {
                pits[i] = new Pit(4, i);
            }
        }

        public void turn(Pit[] turns, Pit mancala, Pit[] other, Pit starting, bool computer)
        {
                int stones = starting.emptyPit(); 
                bool first = true;
                while(stones > 0)
                {
                    stones = distributeStones(stones, turns, true, first, starting, computer);
                    first = false;
                    if (stones == 0)
                    {
                        break;
                    }
                    stones = dropStone(stones, mancala);
                    if (stones > 0)
                    {
                        stones = distributeStones(stones, other, false, first, starting, computer);
                    }
                }
           
        }


        public int dropStone(int stones, Pit well)
        {
            well.addStone();
            stones--;
            return stones;
        }


        public int distributeStones(int stones, Pit[] wells, bool playersWells, bool first, Pit starting, bool computer)
        {
            int? index = first ? starting.getIndex(): 0;
            if (first)
            {
                index++;
            }
            for (;  index < wells.Length; index++)
            {
                stones = dropStone(stones, wells[(int)index]);
                if (stones == 0)
                {
                    if (playersWells && wells[(int)index].howMany() == 1)
                    {
                        if (computer && !playerPits[playerPits.Length - (int)index - 1].isEmpty())
                        {
                            stones = wells[(int)index].emptyPit();
                            stones += playerPits[playerPits.Length - (int)index - 1].emptyPit();
                            computerMancala.addStones(stones);
                            stones = 0;
                        }
                        else if (!computer && !computerPits[playerPits.Length - (int)index - 1].isEmpty())
                        {
                            stones = wells[(int)index].emptyPit();
                            stones += computerPits[computerPits.Length - (int)index - 1].emptyPit();
                            playerMancala.addStones(stones);
                            stones = 0;
                        }
                    }
                    break;
                }
            }
            return stones;
        }


        public int hueristic()
        {
            return computerMancala.howMany() - playerMancala.howMany();
        }
        public bool isEmpty(Pit[] pits)
        {
            bool isEmpty = true;
            foreach(Pit pit in pits)
            {
                if (!pit.isEmpty())
                {
                    isEmpty = false;
                    break;
                }
            }
            return isEmpty;
        }

        public Board copy()
        {
            Board newBoard = new Board(this);

            return newBoard;
        }

        public void show()
        {
            Console.WriteLine(" _____________________");
            Console.WriteLine($"|{computerPits[5].howMany()} | {computerPits[4].howMany()} | {computerPits[3].howMany()} | " +
                $"{computerPits[2].howMany()} | {computerPits[1].howMany()} | {computerPits[0].howMany()}|");
            Console.WriteLine("|_____________________|");
            Console.WriteLine($"|{computerMancala.howMany()}|                 |{playerMancala.howMany()}|");
            Console.WriteLine("|_____________________|");
            Console.WriteLine($"|{playerPits[0].howMany()} | {playerPits[1].howMany()} | {playerPits[2].howMany()} | " +
                $"{playerPits[3].howMany()} | {playerPits[4].howMany()} | {playerPits[5].howMany()}|");
            Console.WriteLine("|_____________________|");
        }

        public void emptyAll(Pit[] pits, Pit mancala)
        {
            foreach(Pit pit in pits)
            {
                int stones = pit.emptyPit();
                mancala.addStones(stones);
            }
        }

        public bool checkGameOver()
        {
            bool gameOver = false;
            if (isEmpty(computerPits))
            {
                gameOver = true;
                if (!isEmpty(playerPits))
                {
                    emptyAll(playerPits, playerMancala);
                }
            }
            if (isEmpty(playerPits))
            {
                gameOver = true;
                if (!isEmpty(computerPits))
                {
                    emptyAll(computerPits, computerMancala);
                }
            }
            return gameOver;
        }
    }
}
