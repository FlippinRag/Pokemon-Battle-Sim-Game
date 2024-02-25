using Microsoft.Xna.Framework;

namespace Pokemon_Battle_Sim_Game.Battle.Common.PokeBallEnterAnimations
{
    public class PlayerCastingPokeBallEnterAnimation : IPokeBallEnterAnimation // casting pokeball to enter spinning
    {
        private const int MovementDelayTime = 100;
        private Vector2 speed = new(5f, -1.5f);
        private double counter = 0;


        public bool IsDone { get; set; }

        public void Update(double gameTime, PokeBallData pokeBallData)
        {
            pokeBallData.Rotation += MathHelper.WrapAngle(pokeBallData.Rotation + 0.1f);
            counter += gameTime;
            if (counter > MovementDelayTime)
            {
                pokeBallData.Position += speed;
                if (pokeBallData.Position.X > 55)
                {
                    speed.X = 0; 
                }
                speed.Y++;
                counter = 0; 
            }
            IsDone = pokeBallData.Position.Y > 100;
        }
    }
}
