using Microsoft.Xna.Framework;

namespace Pokemon_Battle_Sim_Game.Battle.Common
{
    public class PokeBallData
    {
        public const int PokeballWidth = 12;
        public const int PokeballHeight = 12;

        public Vector2 Position { get; set; }
        public Color Color { get; }
        public float Rotation { get; set; }
        public string TextureName { get; }

        public PokeBallData(Vector2 position, string textureName)
        {
            Position = position;
            TextureName = textureName;
            Color = Color.White;
            Rotation = 0.0f;
        }
    }
}
