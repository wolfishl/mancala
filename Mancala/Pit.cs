using System;
using System.Collections.Generic;
using System.Text;

namespace Mancala
{
    class Pit
    {
		private int stones;
		private int? index;

		public Pit(int starting)
		{
			stones = starting;
			index = null;
		}

		public Pit(int starting, int number)
		{
			stones = starting;
			index = number;
		}

		public int? getIndex()
        {
			return index;
        }

		public int emptyPit()
		{
			int tempStones = stones;
			stones = 0;
			return tempStones;
		}

		public void addStone()
		{
			stones++;
		}

		public int howMany()
		{
			return stones;
		}

		public bool isEmpty()
        {
			return stones == 0;
        }

		public void addStones(int howMany)
        {
			stones += howMany;
        }
	}
}
