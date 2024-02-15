namespace Pokemon_Battle_Sim_Game.Pokemons.Battle.PokemonEnteranceAnimations
{
    public class PokemonEntranceAnimation : IPokemonEntranceAnimation
    {
        private const int TargetWidthHeight = 64;
        protected readonly PokemonSpriteData PokemonSpriteData;
        public bool IsDone { get; private set; }

        public PokemonEntranceAnimation(PokemonSpriteData pokemonSpriteData)
        {
            PokemonSpriteData = pokemonSpriteData;
        }

        public void StartEntranceAnimation()
        {
            IsDone = false;
            PokemonSpriteData.Width = 0;
            PokemonSpriteData.Height = 0;

        } 
        public virtual void Update()
        {
            if (IsDone) return;
            if (PokemonSpriteData.Width != TargetWidthHeight)
            {
                PokemonSpriteData.Width ++;
                PokemonSpriteData.Height ++;
            }
            if (PokemonSpriteData.Width == TargetWidthHeight)
            {
                IsDone = true;
            }
        }
    }
}
