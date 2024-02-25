using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pokemon_Battle_Sim_Game.Battle;
using Pokemon_Battle_Sim_Game.EventArg;
using Pokemon_Battle_Sim_Game.Inputs;
using Pokemon_Battle_Sim_Game.Pokemons;
using Pokemon_Battle_Sim_Game.Services.Content;

namespace Pokemon_Battle_Sim_Game.Services.DialogBox.Message
{
    public class DialogBoxMessage : DialogBox
    { // This class is responsible for the message box that appears in the game and how its displayed letter by letter, aswell as skipping the text with down key
        private const int MaxNumberOfRows = 2; 
        private readonly string text;
        private readonly Input input;
        private readonly string CurrentStatus;
        private Vector2 margin;
        private List<DialogPage> pages;
        private int pageIndex;

        protected static Color FontColor { get; set; }
        private SpriteFont Font { get; set; }
        public static bool IsMove1 { get; set; }
        public static bool IsHeal { get; set; }
        public static bool IsPlayerChoiceMade { get; set; }



        protected DialogBoxMessage(Vector2 pos, int width, int height, string text, Input input, string CurrentStatus) : base(pos, width, height)
        {
            this.text = text;
            this.input = input;
            this.CurrentStatus = CurrentStatus;
            this.input.NewInput += InputOnNewInput;
            pages = new List<DialogPage>();
            pageIndex = 0; 
            margin = new Vector2(10);
            FontColor = Color.Black;
        }

        private void InputOnNewInput(object sender, NewInputEvent newInputEvent)
        {
            switch (CurrentStatus)
            {
                case "MessageNavigation":
                    if (newInputEvent.Inputs == Enumerables.Inputs.DownArrow)
                    {
                        HandlePageNavigation();
                    }
                    break;
                case "BattlePick":
                    switch (newInputEvent.Inputs)
                    {
                        case Enumerables.Inputs.Choice1:
                            GlobalBattleVariables.PlayerAction = "Fight";
                            Console.WriteLine(GlobalBattleVariables.PlayerAction);
                            HandlePageNavigation();
                            break;
                        case Enumerables.Inputs.Choice2:
                            GlobalBattleVariables.PlayerAction = "Run";
                            Console.WriteLine(GlobalBattleVariables.PlayerAction);
                            HandlePageNavigation();
                            break;
                        case Enumerables.Inputs.DownArrow:
                            pages[pageIndex].SpeedUpText();
                            break;
                        case Enumerables.Inputs.None:
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                    break;
                case "MovePick":
                    switch (newInputEvent.Inputs)
                    {
                        case Enumerables.Inputs.Choice1:
                            IsMove1 = true;
                            IsPlayerChoiceMade = true;
                            Console.WriteLine(GlobalBattleVariables.PlayerPokemonMove);
                            HandlePageNavigation();
                            break;
                        case Enumerables.Inputs.Choice2:
                            IsHeal = true;
                            IsPlayerChoiceMade = true;
                            Console.WriteLine("Heal");
                            HandlePageNavigation();
                            break;
                        case Enumerables.Inputs.DownArrow:
                            pages[pageIndex].SpeedUpText();
                            break;
                        case Enumerables.Inputs.None:
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                    
                    break;
                
            }
        }


        private void HandlePageNavigation()
        {
            if (pages[pageIndex].IsDone)
            {
                pageIndex++;
                if (pageIndex < pages.Count) return;
                IsDone = true;
                input.NewInput -= InputOnNewInput;
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
            if (IsDone) return;
            base.Draw(spriteBatch);
            pages[pageIndex].Draw(spriteBatch);
        }

    }
}
