// File: Tilemapping/Tilemap.cs
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.IO;
using System.Reflection.Metadata.Ecma335;
using System.Xml;
using System.Xml.Linq;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace ZeldaGame.GameScripts.Tilemapping
{
    public class Tilemap
    {
        private readonly Tileset tileset;
        private readonly int[] tiles;
        private readonly TileBlock[] blocks; // Parallel array for collision block objects

        // Expose blocks for collisions
        public TileBlock[] Blocks => blocks;

        public int Rows { get; }
        public int Columns { get; }
        public int Count { get; }
        public Vector2 Scale { get; set; }
        public float TileWidth => tileset.TileWidth * Scale.X;
        public float TileHeight => tileset.TileHeight * Scale.Y;

        public Tilemap(Tileset tileset, int cols, int rows)
        {
            this.tileset = tileset;
            Rows = rows;
            Columns = cols;
            Count = Columns * Rows;
            Scale = Vector2.One;
            tiles = new int[Count];
            blocks = new TileBlock[Count];
        }

        public void SetTile(int index, int tilesetID)
        {
            tiles[index] = tilesetID;
        }

        public void SetTile(int col, int row, int tilesetID)
        {
            int index = row * Columns + col;
            SetTile(index, tilesetID);
        }

        public NonAnimatedSprite GetTile(int index)
        {
            return tileset.GetTile(tiles[index]);
        }

        public NonAnimatedSprite GetTile(int col, int row)
        {
            int index = row * Columns + col;
            return GetTile(index);
        }

        public TileBlock GetBlock(int index) => blocks[index];

        public TileBlock GetBlock(int col, int row)
        {
            int index = row * Columns + col;
            return GetBlock(index);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < Count; i++)
            {
                int tilesetIndex = tiles[i];
                NonAnimatedSprite tile = tileset.GetTile(tilesetIndex);

                int x = i % Columns;
                int y = i / Columns;

                Vector2 pos = new Vector2(x * TileWidth, y * TileHeight);
                tile.Draw(spriteBatch, pos, Color.White, Scale);
            }
        }

        // A tile is only passable (non-collidable) if it matches specific floor numbers
        private static bool IsTileCollidable(int tilesetID)
        {
            if (tilesetID == 1216 || tilesetID == 1217 || tilesetID == 1248 || tilesetID == 1249)
            {
                return false; // nonCollidableObject (Passable floor)
            }
            return true; // collidableObject (Walls, barriers, etc.)
        }

        public static Tilemap FromFile(ContentManager content, string filename, Vector2 scale)
        {
            string filePath = Path.Combine(content.RootDirectory, filename);

            using (Stream stream = TitleContainer.OpenStream(filePath))
            {
                using (XmlReader reader = XmlReader.Create(stream))
                {
                    XDocument doc = XDocument.Load(reader);
                    XElement root = doc.Root;

                    XElement tilesetElement = root.Element("Tileset");

                    string regionAttribute = tilesetElement.Attribute("region").Value;
                    string[] split = regionAttribute.Split(" ", StringSplitOptions.RemoveEmptyEntries);
                    int x = int.Parse(split[0]);
                    int y = int.Parse(split[1]);
                    int width = int.Parse(split[2]);
                    int height = int.Parse(split[3]);

                    int tileWidth = int.Parse(tilesetElement.Attribute("tileWidth").Value);
                    int tileHeight = int.Parse(tilesetElement.Attribute("tileHeight").Value);
                    string contentPath = tilesetElement.Value;

                    Texture2D texture = content.Load<Texture2D>(contentPath);

                    NonAnimatedSprite nonAnimatedSprite = new NonAnimatedSprite(texture, x, y, width, height);

                    Tileset tileset = new Tileset(nonAnimatedSprite, tileWidth, tileHeight);

                    XElement tilesElement = root.Element("Tiles");

                    string[] rows = tilesElement.Value.Trim().Split('\n', StringSplitOptions.RemoveEmptyEntries);

                    int columnCount = rows[0].Split(" ", StringSplitOptions.RemoveEmptyEntries).Length;

                    Tilemap tilemap = new Tilemap(tileset, columnCount, rows.Length);
                    tilemap.Scale = scale;

                    for (int row = 0; row < rows.Length; row++)
                    {
                        string[] columns = rows[row].Trim().Split(" ", StringSplitOptions.RemoveEmptyEntries);

                        for (int column = 0; column < columnCount; column++)
                        {
                            int tilesetIndex = int.Parse(columns[column]);

                            // Set rendering tile ID[cite: 1]
                            tilemap.SetTile(column, row, tilesetIndex);

                            // Create and map the corresponding collision TileBlock object once on load
                            Vector2 pos = new Vector2(column * tilemap.TileWidth, row * tilemap.TileHeight);
                            bool collidable = IsTileCollidable(tilesetIndex);

                            TileBlock block = new TileBlock(pos, (int)tilemap.TileWidth, (int)tilemap.TileHeight, tilesetIndex, collidable);

                            int index = row * columnCount + column;
                            tilemap.blocks[index] = block;
                        }
                    }

                    return tilemap;
                }
            }
        }
    }
}