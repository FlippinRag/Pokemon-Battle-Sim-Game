namespace Pokemon_Battle_Sim_Game.Battle.Common.PokeBallEnterAnimations
{
    public class NoPokeBallEnterAnimation : IPokeBallEnterAnimation
    {
        public bool IsDone { get; set; }

        public void Update(double gameTime, PokeBallData pokeBallData)
        {
            IsDone = true; 
        }
    }
}
