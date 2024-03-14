namespace ZeroFlip64Web.GameLogic
{
    class Counter
    {
        private Tile[] _tiles;

        public int Total { get; private set; }
        public int ZeroCount { get; private set; }

        public Counter(Tile[] tiles)
        {
            _tiles = tiles;

            Count();
        }

        public void Count()
        {
            Total = 0;
            ZeroCount = 0;

            foreach (Tile tile in _tiles)
            {
                Total += tile.Value;
                if (tile.Value == 0) ZeroCount++;
            }
        }
    }
}
