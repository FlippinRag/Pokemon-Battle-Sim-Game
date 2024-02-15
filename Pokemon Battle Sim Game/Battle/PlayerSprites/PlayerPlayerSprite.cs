using Microsoft.Xna.Framework;

namespace Pokemon_Battle_Sim_Game.Battle.PlayerSprites
{
    public class PlayerPlayerSprite : PlayerSprite
    {
        private const int AnimationFrameCount = 4;
        private const int AnimationFrameTime = 100;

        private double counter;
        private int animationFrameIndex;
        private int speed;
        private bool inMovement; 

        public PlayerPlayerSprite(string textureName) : base(textureName)
        {
            CurrentPosition = new Vector2(240, 48);
            TargetPosition = new Vector2(20, 48);
            speed = 3;
            inMovement = false;
            animationFrameIndex = 0; 
        }

        protected override void Move(double gameTime)
        {
            CurrentPosition -= new Vector2(speed, 0);
            if (inMovement && animationFrameIndex < AnimationFrameCount)
            {
                counter += gameTime;
                if (counter > AnimationFrameTime)
                {
                    counter = 0;
                    animationFrameIndex++; 
                    DrawRectangle = new Rectangle(PokemonTextureWidth*animationFrameIndex, 0, PokemonTextureWidth, PokemonTextureHeight);
                }
            }

        }

        public override void StartMovement()
        {
            TargetPosition = new Vector2(-64, 48);
            speed = 1;
            inMovement = true; 
        }
    }
}
