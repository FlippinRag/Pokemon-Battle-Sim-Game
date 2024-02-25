using Microsoft.Xna.Framework;
using Pokemon_Battle_Sim_Game.Services.Content;

namespace Pokemon_Battle_Sim_Game.Services.DialogBox
{
    public class DialogBoxBattle : DialogBox // intermediate class for the passing through the positions and sizes
    {
        public DialogBoxBattle(Vector2 pos, int width, int height) : base(pos, width, height)
        {
        }

        public override void LoadContent(IContentLoader contentLoader)
        {
            Texture = contentLoader.LoadTexture("Windows/DialogBox");
        }
    }
}
