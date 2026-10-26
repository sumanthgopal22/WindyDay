using Microsoft.Xna.Framework;

namespace ZeldaGame.GameScripts.Tilemapping
{
    public class TileBlock : ICollidable
    {
        public Vector2 Position { get; set; }
        public Rectangle BoundingBox { get; set; }
        public int TileID { get; set; }
        public bool IsCollidable { get; set; }

        public TileBlock(Vector2 position, int width, int height, int tileID, bool isCollidable)
        {
            Position = position;
            TileID = tileID;
            IsCollidable = isCollidable;
            BoundingBox = new Rectangle((int)position.X, (int)position.Y, width, height);
        }
    }
}