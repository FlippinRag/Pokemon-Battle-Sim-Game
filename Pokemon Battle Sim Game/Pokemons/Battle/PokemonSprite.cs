using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pokemon_Battle_Sim_Game.Services.Content;
using static Pokemon_Battle_Sim_Game.Pokemons.Battle.PokemonSpriteData;

namespace Pokemon_Battle_Sim_Game.Pokemons.Battle
{
    public class PokemonSprite : IPokemonSprite
    {
        private Texture2D texture;
        private readonly PokemonSpriteData pokemonSpriteData;

        public PokemonSprite(PokemonSpriteData pokemonSpriteData)
        {
            this.pokemonSpriteData = pokemonSpriteData;
        }

        public PokemonSpriteData GetPokemonBattleSpriteData()
        {
            return pokemonSpriteData;
        }

        public void LoadContent(IContentLoader contentLoader)
        {
            texture = contentLoader.LoadTexture(pokemonSpriteData.TextureName);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, pokemonSpriteData.Rectangle, DrawRectangle, 
                Color.White, 0f, new Vector2(40F, 32F), SpriteEffects.None, 0);
        }


    }
}
