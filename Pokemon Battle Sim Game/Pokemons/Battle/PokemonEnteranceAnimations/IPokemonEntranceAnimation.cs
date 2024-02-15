namespace Pokemon_Battle_Sim_Game.Pokemons.Battle.PokemonEnteranceAnimations
{
    public interface IPokemonEntranceAnimation
    {
        bool IsDone { get; }
        void StartEntranceAnimation();
        void Update();
    }
}
