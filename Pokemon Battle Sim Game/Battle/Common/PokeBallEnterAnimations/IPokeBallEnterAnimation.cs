namespace Pokemon_Battle_Sim_Game.Battle.Common.PokeBallEnterAnimations
{
    public interface IPokeBallEnterAnimation
    {
        bool IsDone { get; }
        void Update(double gameTime, PokeBallData pokeBallData);
    }
}
