using Microsoft.Xna.Framework;

namespace Pokemon_Battle_Sim_Game.Battle.PlayerSprites
{
    public class OpponentSprite : PokemonSprite
    {// opponent moving in and out of battle
        public OpponentSprite(string textureName) : base(textureName)
        {
            CurrentPosition = new Vector2(-64, 10);
            TargetPosition = new Vector2(165, 10);
        }

        protected override void Move(double gameTime)
        {
            CurrentPosition += new Vector2(3, 0);
        }

        public override void StartMovement()
        {
            TargetPosition = new Vector2(304, 10);
        }
    }
}
