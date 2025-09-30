using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AstroVeridia
{
    internal class MapBuilder
    {

        /*--CLASS LEVEL VARIABLES-----------------------------------------------------VARIABLES--*/
        private int currentLevel;
        private int mapWidth;
        private int mapHeight;
        private GroundTile[,] currentMap;
        private static GroundTypesConfig config = LoadGroundTypesConfig("../../../ASCIISymbols.json");
        private List<GroundTile> currentGroundTiles = config.groundTypes;

        /*--CONSTRUCTOR-------------------------------------------------------------CONSTRUCTOR--*/
        public MapBuilder(int level)
        {
            this.currentLevel = level;
            mapWidth = level * 8;
            mapHeight = mapWidth * 2;
            currentMap = BuildNewMap(mapWidth, mapHeight, GetNoise(mapWidth, mapHeight, 8765309));
        }

        /*--METHODS---------------------------------------------------------------------METHODS--*/
        public void DrawMap()
        {
            if(currentMap != null)
            {
                for (int x = 0; x < this.GetMapSize().GetLength(0); x++)
                {
                    for (int y = 0; y < this.GetMapSize().GetLength(1); y++)
                    {
                        GroundTile tile = this.GetGroundTile(x, y) ?? new GroundTile { Symbol = "?", Color = "white" };
                        AnsiConsole.Markup($"[{tile.Color}]{tile.Symbol}[/]");
                    }
                    Console.WriteLine();
                }
            }
            else
            {
                Console.Write("Map has not been created or initialized");
            }
        }

        private GroundTile[,] BuildNewMap(int mapWidth, int mapHeight, double[,] noise)
        {
            GroundTile[,] map = new GroundTile[mapWidth, mapHeight];

            // Find min max noise
            double minNoise = Double.MaxValue;
            double maxNoise = Double.MinValue;
            for (int x = 0; x < mapWidth; x++)
            {
                for (int y = 0; y < mapHeight; y++)
                {
                    if (noise[x, y] < minNoise) minNoise = noise[x, y];
                    if (noise[x, y] > maxNoise) maxNoise = noise[x, y];
                }
            }

            //Normalize Noise 0.0 -> 1.0
            for (int x = 0; x < noise.GetLength(0); x++)
            {
                for (int y = 0; y < noise.GetLength(1); y++)
                {
                    double normalized = (noise[x, y] - minNoise) / (maxNoise - minNoise);
                    map[x, y] = GetGroundTile(normalized);
                }
            }

            return map;
        }

        private GroundTile GetGroundTile(double value)
        {
            if (value < 0.05) { return currentGroundTiles.FirstOrDefault(t => t.Name == "ice patch"); }
            if (value > 0.05 && value <= 0.35) { return currentGroundTiles.FirstOrDefault(t => t.Name == "bare rock"); }
            if (value > 0.35 && value <= 0.65) { return currentGroundTiles.FirstOrDefault(t => t.Name == "metallic ore"); }
            if (value > 0.65 && value <= 0.95) { return currentGroundTiles.FirstOrDefault(t => t.Name == "regolith"); }
            else { return currentGroundTiles.FirstOrDefault(t => t.Name == "crater"); }
        }

        private static GroundTypesConfig LoadGroundTypesConfig(string path)
        {
            try
            {
                string json = File.ReadAllText(path);
                return JsonSerializer.Deserialize<GroundTypesConfig>(json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading ground types: {ex.Message}");
                return new GroundTypesConfig { groundTypes = new List<GroundTile>() };
            }
        }

        private double[,] GetNoise(int mapWidth, int mapHeight, int seed, double scale = 0.1)
        {
            Random rnd = new Random(seed);
            double[,] noise = new double[mapWidth, mapHeight];

            for (int x = 0; x < mapWidth; x++)
            {
                for (int y = 0; y < mapHeight; y++)
                {
                    double value = Math.Sin(x * scale + seed) + Math.Sin(y * scale + seed * 2);
                    value += rnd.NextDouble() * 0.3; // Add some randomness
                    noise[x, y] = value;
                }
            }

            return noise;
        }

        /*--GETTERS SETTERS-----------------------------------------------------GETTERS SETTERS--*/
        public int[,] GetMapSize()
        {
            return new int[mapWidth,mapHeight];
        }

        public GroundTile GetGroundTile(int x, int y)
        {
            return currentMap[x, y];
        }

        public List<GroundTile> GetGroundTiles()
        {
            return this.currentGroundTiles;
        }

    }
}
