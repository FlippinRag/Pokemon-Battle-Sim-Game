using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pokemon_Battle_Sim_Game.Services.DialogBox.Message
{
    public class DialogPage
    {// this class makes the pages for the message to be split into and read letter by letter
        private const int CharacterDelay = 75;
        private readonly SpriteFont font;
        private readonly Color fontColor;
        private readonly string fullText;
        private readonly Vector2 position;
        private int index;
        private double counter;


        public bool IsDone { get; private set; }

        public DialogPage(string text, Vector2 position, SpriteFont font, Color fontColor)
        {
            fullText = text;
            this.position = position;
            this.font = font;
            this.fontColor = fontColor;
            index = 0;
            counter = 0;
        }

        public void Update(double gameTime)
        {
            if (index >= fullText.Length)
                return;
            counter += gameTime;
            if (counter > CharacterDelay)
            {
                counter = 0;
                index++;
                if (index == fullText.Length - 1)
                    IsDone = true;
            }
        }

        public void SpeedUpText()  //goes to the end of the text, when you press down a key
        {
            index = fullText.Length;
            IsDone = true;
        }


        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(font, fullText[..index], position, fontColor);
        }
    }
}
