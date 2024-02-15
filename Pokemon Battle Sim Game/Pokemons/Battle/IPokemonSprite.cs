using Microsoft.Xna.Framework.Graphics;
using Pokemon_Battle_Sim_Game.Services.Content;

namespace Pokemon_Battle_Sim_Game.Pokemons.Battle
{
    public interface IPokemonSprite
    {
        void LoadContent(IContentLoader contentLoader);
        void Draw(SpriteBatch spriteBatch);
    }
}
