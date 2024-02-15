using Microsoft.Xna.Framework;

namespace Pokemon_Battle_Sim_Game.Pokemons.Battle.PokemonEnteranceAnimations
{
    public class GrowPokemonEntranceAnimation : PokemonEntranceAnimation
    {
        public GrowPokemonEntranceAnimation(PokemonSpriteData pokemonSpriteData) : base(pokemonSpriteData)
        {
        }

        public override void Update()
        {
            base.Update();
            if (PokemonSpriteData.Position.Y > 145 - PokemonSpriteData.Height)
            {
                PokemonSpriteData.Position -= new Vector2(0, 2);
            }
        }
    }
}
