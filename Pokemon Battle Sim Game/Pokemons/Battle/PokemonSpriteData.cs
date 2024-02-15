using Microsoft.Xna.Framework;

namespace Pokemon_Battle_Sim_Game.Pokemons.Battle
{
    public class PokemonSpriteData
    {
        private const int TextureWidth = 80;
        private const int TextureHeight = 64;

        public string TextureName { get; }
        public int Width { get; set; }
        public int Height { get; set; }
        public Vector2 Position { get; set; }
        public Rectangle Rectangle => new((int) Position.X, (int) Position.Y, Width, Height);
        public static Rectangle DrawRectangle => new(0, 0, TextureWidth, TextureHeight);
        
        public PokemonSpriteData(int width, int height, Vector2 position, string textureName)
        {
            TextureName = textureName;
            Width = width;
            Height = height;
            Position = position;
        }
    }
}
