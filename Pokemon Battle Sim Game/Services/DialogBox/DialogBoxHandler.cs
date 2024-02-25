using Microsoft.Xna.Framework.Graphics;

namespace Pokemon_Battle_Sim_Game.Services.DialogBox
{
    public class DialogBoxHandler : IDialogBoxQueuer // a helper class for the dialog box manager
    {
        private readonly DialogBoxManager dialogBoxManager;

        public bool BoxActive => dialogBoxManager.BoxActive; // gets if the dialog box is active, never sets it, learnt that the hard way

        public DialogBoxHandler(DialogBoxManager dialogBoxManager)
        {
            this.dialogBoxManager = dialogBoxManager;
        }

        public void AppendBoxIntoQueue(DialogBox dialogBox)
        {
            dialogBoxManager.AppendBoxIntoQueue(dialogBox);
        }

        public void UpdateCurrentDialogBox(double gameTime)
        {
            dialogBoxManager.UpdateCurrentWindow(gameTime);
        }


        public void Draw(SpriteBatch spriteBatch)
        {
            dialogBoxManager.Draw(spriteBatch);
        }
    }
}
