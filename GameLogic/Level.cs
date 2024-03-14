using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace ZeroFlip64Web.GameLogic
{
    class Level
    {
        private static Random _random = new Random();
        private static string _filePath = @"data\levels.json";

        // Temporary hard coded level data
        // TODO: Not this
        private static readonly List<Level> _levelList = new List<Level>
        {
            new Level
            {
                LevelNumber = 0,
                TileSets = new List<int[]>
                {
                    new int[] { 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 2, 2, 3 },
                    new int[] { 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 3, 3, 3 },
                    new int[] { 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2 },
                    new int[] { 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 2, 3, 3 },
                    new int[] { 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 3 }
                }
            }
        };

        public int LevelNumber { get; set; }
        public List<int[]> TileSets { get; set; }

        public int[] GetRandomTileSet(bool shuffled)
        {
            int[] tileSet = TileSets[_random.Next(0, TileSets.Count)];
            if (shuffled)
            {
                tileSet = tileSet.OrderBy(r => _random.Next()).ToArray();
            }
            return tileSet;
        }

        public static List<Level> LoadLevels()
        {
            // Cannot access levels.json on the file system in WebAssembly build
            // TODO: Properly load JSON file via HTTP request

            //string json = File.ReadAllText(_filePath);
            //List<Level> levels = JsonSerializer.Deserialize<List<Level>>(json);
            //return levels;
            return _levelList;
        }
    }
}
