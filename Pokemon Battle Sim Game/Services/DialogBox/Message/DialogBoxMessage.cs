using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pokemon_Battle_Sim_Game.EventArg;
using Pokemon_Battle_Sim_Game.Inputs;
using Pokemon_Battle_Sim_Game.Services.Content;

namespace Pokemon_Battle_Sim_Game.Services.DialogBox.Message
{
    public class DialogBoxMessage : DialogBox
    {
        private const int MaxNumberOfRows = 2; 
        private readonly string text;
        private readonly Input input;
        private Vector2 margin;
        private List<DialogPage> pages;
        private int pageIndex;

        protected static Color FontColor { get; set; }
        public SpriteFont Font { get; set; }


        public DialogBoxMessage(Vector2 pos, int width, int height, string text, Input input) : base(pos, width, height)
        {
            this.text = text;
            this.input = input;
            this.input.NewInput += InputOnNewInput;
            pages = new List<DialogPage>();
            pageIndex = 0; 
            margin = new Vector2(10);
            FontColor = Color.Black;
            //var dialogBoxMessage = new DialogBoxMessage(new Vector2(10, 30), 400, 200, "", new InputKeyboard());
        }

        private void InputOnNewInput(object sender, NewInputEvent newInputEvent)
        {
            if (newInputEvent.Inputs != Enumerables.Inputs.DownArrow) return;
            if (pages[pageIndex].IsDone)
            {
                pageIndex++;
                if (pageIndex >= pages.Count)
                {
                    IsDone = true;
                    input.NewInput -= InputOnNewInput;
                }
            }
            else
            {
                pages[pageIndex].SpeedUpText();
            }
        }

        public override void LoadContent(IContentLoader contentLoader)
        {
            base.LoadContent(contentLoader);
            Font = contentLoader.LoadFont("PokemonFont");
            CreatePages(Font);
            
        }

        private void CreatePages(SpriteFont font)
        {
            var words = text.Split(' '); 
            var rowText = new StringBuilder();
            var index = 0;
            var rowIndex = 0;
            while (index < words.Length)
            {
                var word = words[index];
                var oldRowLength = rowText.Length;

                //Force a new page if we have a new line in the text
                if (word == Environment.NewLine)
                {
                    CreatePage(font, rowText);
                    rowIndex = 0;
                    index++;
                    continue; 
                }

                rowText.Append($"{word} ");
                if (font.MeasureString(rowText).X > Width - margin.X)
                {
                    rowText.Remove(oldRowLength, rowText.Length - oldRowLength);
                    if (rowIndex == MaxNumberOfRows - 1)
                    {
                        CreatePage(font, rowText);
                        rowIndex = 0;
                    }
                    else
                    {
                        rowText.Append($"{Environment.NewLine}{Environment.NewLine}");
                        rowIndex++; 
                    }
                }
                else
                {
                    index++;
                }
            }
            if (rowText.Length > 0)
            {
                pages.Add(new DialogPage(rowText.ToString(), Pos + margin, font, FontColor));
            }
        }

        private void CreatePage(SpriteFont font, StringBuilder rowText)
        {
            pages.Add(new DialogPage(rowText.ToString(), Pos + margin, font, FontColor));
            rowText.Clear();
        }

        public override void Update(double gameTime)
        {
            input.Update(gameTime);
            if (IsDone)
                return;
            pages[pageIndex].Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (!IsDone)
            {
                base.Draw(spriteBatch);
                pages[pageIndex].Draw(spriteBatch);
            }
        }

    }
}
