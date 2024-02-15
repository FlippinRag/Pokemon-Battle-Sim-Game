using Microsoft.Xna.Framework.Graphics;

namespace Pokemon_Battle_Sim_Game.Services.Content
{
    public interface IContentLoader
    {
        Texture2D LoadTexture(string textureName);
        SpriteFont LoadFont(string fontName);
    }
}
