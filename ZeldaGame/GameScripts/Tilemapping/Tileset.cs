namespace ZeldaGame.GameScripts.Tilemapping
{
    public class Tileset
    {
        private readonly NonAnimatedSprite[] tiles;

        public int TileWidth { get; }
        public int TileHeight { get; }
        public int Columns { get; }
        public int Rows { get; }
        public int Count { get; }

        public Tileset(NonAnimatedSprite nonAnimatedSprite, int tileWidth, int tileHeight)
        {
            TileWidth = tileWidth;
            TileHeight = tileHeight;
            Columns = nonAnimatedSprite.Width / tileWidth;
            Rows = nonAnimatedSprite.Height / tileHeight;
            Count = Columns * Rows;

            tiles = new NonAnimatedSprite[Count];

            for (int i = 0; i < Count; i++)
            {
                int x = i % Columns * tileWidth;
                int y = i / Columns * tileHeight;
                tiles[i] = new NonAnimatedSprite(nonAnimatedSprite.SpriteTexture,
                    nonAnimatedSprite.SourceRectangle.X + x,
                    nonAnimatedSprite.SourceRectangle.Y + y,
                    tileWidth,
                    tileHeight);
            }
        }

        public NonAnimatedSprite GetTile(int index) => tiles[index];

        public NonAnimatedSprite GetTile(int col, int row)
        {
            int index = row * Columns + col;
            return GetTile(index);
        }
    }
}
