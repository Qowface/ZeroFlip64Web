using System;
using System.Collections.Generic;
using System.Linq;

namespace ZeroFlip64Web.GameLogic
{
    class Board
    {
        private static Random _random = new Random();

        public int Rows { get; private set; }
        public int Cols { get; private set; }

        public int[] Counts { get; private set; }
        public bool Won { get; private set; }
        public bool Lost { get; private set; }

        public Tile[,] Tiles { get; private set; }
        public Counter[] RowCounters { get; private set; }
        public Counter[] ColCounters { get; private set; }

        public List<Level> Levels { get; private set; }
        public int CurrentLevel { get; private set; }

        public Board(int rows, int cols)
        {
            Rows = rows;
            Cols = cols;

            Counts = new int[] { 0, 0, 0, 0 };
            Won = false;
            Lost = false;

            Tiles = new Tile[rows, cols];
            RowCounters = new Counter[rows];
            ColCounters = new Counter[cols];

            Levels = Level.LoadLevels();
            CurrentLevel = 0;

            Create();
        }

        public void Create()
        {
            int currentTile = 0;
            int[] tileSet = Levels[CurrentLevel].GetRandomTileSet(true);
            
            for (int row = 0; row < Rows; row++)
            {
                for (int col = 0; col < Cols; col++)
                {
                    Tiles[row, col] = new Tile(tileSet[currentTile]);
                    Counts[Tiles[row, col].Value]++;
                    currentTile++;
                }
            }

            for (int i = 0; i < Rows; i++)
            {
                RowCounters[i] = new Counter(GetRow(i));
            }

            for (int i = 0; i < Cols; i++)
            {
                ColCounters[i] = new Counter(GetCol(i));
            }
        }

        public Tile[] GetRow(int row)
        {
            return Enumerable.Range(0, Cols).Select(x => Tiles[row, x]).ToArray();
        }

        public Tile[] GetCol(int col)
        {
            return Enumerable.Range(0, Rows).Select(x => Tiles[x, col]).ToArray();
        }

        public bool FlipTile(int row, int col)
        {
            Tile tile = Tiles[row, col];

            // Can't flip a tile that's already flipped
            if (tile.Flipped)
            {
                return false;
            }

            tile.Flipped = true;
            Counts[tile.Value]--;

            // If we flipped over a 0, we lose
            if (tile.Value == 0)
            {
                Lost = true;
            }
            // If we flipped over all 2 and 3 tiles, we win
            else if (Counts[2] < 1 && Counts[3] < 1)
            {
                Won = true;
            }

            return true;
        }

        public void FlipAllTiles()
        {
            for (int row = 0; row < Rows; row++)
            {
                for (int col = 0; col < Cols; col++)
                {
                    Tiles[row, col].Flipped = true;
                }
            }
        }
    }
}
