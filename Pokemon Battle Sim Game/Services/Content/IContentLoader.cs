using Microsoft.Xna.Framework.Graphics;

namespace Pokemon_Battle_Sim_Game.Services.Content
{
    public interface IContentLoader
    {// This interface is responsible for loading the content of the game
        Texture2D LoadTexture(string textureName);
        SpriteFont LoadFont(string fontName);
    }
}
