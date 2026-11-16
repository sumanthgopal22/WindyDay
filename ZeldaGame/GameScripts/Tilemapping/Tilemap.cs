// File: Tilemapping/Tilemap.cs
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace ZeldaGame.GameScripts.Tilemapping
{
    public enum DoorDirection
    {
        Up,
        Down,
        Left,
        Right
    }

    public sealed record EnemySpawn(string Type, Vector2 Position);

    public sealed record ItemSpawn(string Type, Vector2 Position);

    public sealed record RoomDoor(
        DoorDirection Direction,
        string Destination,
        Rectangle TriggerBounds,
        Vector2 ArrivalPosition
    );

    public class Tilemap
    {
        private readonly Tileset tileset;
        private readonly int[] tiles;
        private readonly TileBlock[] blocks; // Parallel array for collision block objects

        // Expose blocks for collisions
        public TileBlock[] Blocks => blocks;
        public List<EnemySpawn> EnemySpawns { get; } = new();
        public List<ItemSpawn> ItemSpawns { get; } = new();
        public List<RoomDoor> Doors { get; } = new();
        public bool HasEnemyConfiguration { get; private set; }
        public bool HasItemConfiguration { get; private set; }

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
            if (tilesetID == 1216 || tilesetID == 1217 || tilesetID == 1248 || tilesetID == 1249 || tilesetID == 1226 || tilesetID == 1227 || tilesetID == 1258 || tilesetID == 1259)
            {
                return false; // nonCollidableObject (Passable floor)
            }

            if (tilesetID == 1224 || tilesetID == 1225 || tilesetID == 1256 || tilesetID == 1257)
            {
                return false; // Underground black corridor / pit floor (nonCollidableObject)
            }

            if (tilesetID == 1234 || tilesetID == 1235 || tilesetID == 1266 || tilesetID == 1267)
            {
                return false; // Underground ladder rungs & rails (nonCollidableObject)
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

                    XElement enemiesElement = root.Element("Enemies");
                    tilemap.HasEnemyConfiguration = enemiesElement != null;
                    if (enemiesElement != null)
                    {
                        foreach (XElement enemyElement in enemiesElement.Elements("Enemy"))
                        {
                            string type = (string)enemyElement.Attribute("type")
                                ?? throw new InvalidDataException("Enemy entries must have a type.");
                            float enemyX = ParseFloat(enemyElement, "x");
                            float enemyY = ParseFloat(enemyElement, "y");
                            tilemap.EnemySpawns.Add(new EnemySpawn(type, new Vector2(enemyX, enemyY)));
                        }
                    }

                    XElement itemsElement = root.Element("Items");
                    tilemap.HasItemConfiguration = itemsElement != null;
                    if (itemsElement != null)
                    {
                        foreach (XElement itemElement in itemsElement.Elements("Item"))
                        {
                            string type = (string)itemElement.Attribute("type")
                                ?? throw new InvalidDataException("Item entries must have a type.");
                            float itemX = ParseFloat(itemElement, "x");
                            float itemY = ParseFloat(itemElement, "y");
                            tilemap.ItemSpawns.Add(new ItemSpawn(type, new Vector2(itemX, itemY)));
                        }
                    }

                    XElement doorsElement = root.Element("Doors");
                    if (doorsElement != null)
                    {
                        foreach (XElement doorElement in doorsElement.Elements("Door"))
                        {
                            string directionText = (string)doorElement.Attribute("direction")
                                ?? throw new InvalidDataException("Door entries must have a direction.");
                            if (!Enum.TryParse(directionText, ignoreCase: true, out DoorDirection direction)
                                || !Enum.IsDefined(typeof(DoorDirection), direction))
                            {
                                throw new InvalidDataException($"Unknown door direction '{directionText}'.");
                            }

                            string destination = (string)doorElement.Attribute("destination")
                                ?? throw new InvalidDataException("Door entries must have a destination.");
                            int triggerX = ParseInt(doorElement, "triggerX");
                            int triggerY = ParseInt(doorElement, "triggerY");
                            int triggerWidth = ParseInt(doorElement, "triggerWidth");
                            int triggerHeight = ParseInt(doorElement, "triggerHeight");
                            if (triggerWidth <= 0 || triggerHeight <= 0)
                            {
                                throw new InvalidDataException("Door trigger dimensions must be positive.");
                            }

                            float arrivalX = ParseFloat(doorElement, "arrivalX");
                            float arrivalY = ParseFloat(doorElement, "arrivalY");
                            tilemap.Doors.Add(new RoomDoor(
                                direction,
                                destination,
                                new Rectangle(triggerX, triggerY, triggerWidth, triggerHeight),
                                new Vector2(arrivalX, arrivalY)
                            ));
                        }
                    }

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

        private static int ParseInt(XElement element, string attributeName)
        {
            string value = (string)element.Attribute(attributeName)
                ?? throw new InvalidDataException($"Missing '{attributeName}' attribute on {element.Name}.");
            return int.Parse(value, CultureInfo.InvariantCulture);
        }

        private static float ParseFloat(XElement element, string attributeName)
        {
            string value = (string)element.Attribute(attributeName)
                ?? throw new InvalidDataException($"Missing '{attributeName}' attribute on {element.Name}.");
            return float.Parse(value, CultureInfo.InvariantCulture);
        }
    }
}