using Microsoft.Xna.Framework;
using Pokemon_Battle_Sim_Game.Inputs;

namespace Pokemon_Battle_Sim_Game.Services.DialogBox.Message
{
    public class DialogBoxBattleMessage : DialogBoxMessage
    {
        public DialogBoxBattleMessage(string text, Input input) : base(new Vector2(10, 113), 280, 45, text, input)
        {
            Color = Color.Transparent;
            FontColor = Color.Black;
        }
    }
}
