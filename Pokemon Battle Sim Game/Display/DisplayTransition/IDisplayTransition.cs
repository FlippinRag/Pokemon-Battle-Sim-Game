using Microsoft.Xna.Framework.Graphics;
using Pokemon_Battle_Sim_Game.Services.Content;

namespace Pokemon_Battle_Sim_Game.Display.DisplayTransition
{
    public interface IDisplayTransition
    {
        bool IsDone { get; }
        void Start();
        void LoadContent(IContentLoader contentLoader);
        void Update();
        void Draw(SpriteBatch spriteBatch); 
    }
}
