using System;
using Microsoft.Xna.Framework.Graphics;
using Pokemon_Battle_Sim_Game.Display.DisplayTransition;
using Pokemon_Battle_Sim_Game.Services.Content;

namespace Pokemon_Battle_Sim_Game.Display
{
    public class DisplayLoader
    { //loads the display and transitions between them with a fade in and fade out
        private Display currentDisplay;
        private Display nextDisplay;
        private readonly IDisplayTransition fadeOutTransition;
        private readonly IDisplayTransition fadeInTransition;
        private readonly IContentLoader contentLoader;
        private enum DisplayLoaderPhases { ClosingPreviousDisplay, SettingUpNewDisplay, OnGoing}
        private DisplayLoaderPhases currentDisplayLoaderPhase; 

        public DisplayLoader(IDisplayTransition fadeOutTransition,
            IDisplayTransition fadeInTransition, IContentLoader contentLoader)
        {
            this.fadeOutTransition = fadeOutTransition;
            this.fadeInTransition = fadeInTransition;
            this.contentLoader = contentLoader;
            currentDisplayLoaderPhase = DisplayLoaderPhases.OnGoing;
        }

        private void SetupNextDisplay()
        {
            currentDisplay = nextDisplay;
            currentDisplay.LoadContent(contentLoader);
            fadeInTransition.Start();
            currentDisplayLoaderPhase = DisplayLoaderPhases.SettingUpNewDisplay;
        }

        public void LoadNextDisplay(Display display)
        {
            currentDisplayLoaderPhase = DisplayLoaderPhases.ClosingPreviousDisplay;
            fadeOutTransition.Start();
            nextDisplay = display;
        }

        public void LoadContent()
        {
            fadeOutTransition.LoadContent(contentLoader);
            fadeInTransition.LoadContent(contentLoader);
        }

        public void Update(double gameTime)
        {
            var exception = new ArgumentOutOfRangeException
            {
                HelpLink = null,
                HResult = 0,
                Source = null
            };
            switch (currentDisplayLoaderPhase)
            {
                case DisplayLoaderPhases.ClosingPreviousDisplay:
                    fadeOutTransition.Update();
                    if (fadeOutTransition.IsDone)
                    {
                        if (currentDisplay != null)
                        {
                            Environment.Exit(0);
                        }
                        else
                        {
                            SetupNextDisplay();
                        }
                    }
                    break;
                case DisplayLoaderPhases.SettingUpNewDisplay:
                    fadeInTransition.Update();
                    if (fadeInTransition.IsDone)
                    {
                        currentDisplayLoaderPhase = DisplayLoaderPhases.OnGoing;
                    }
                    break;
                case DisplayLoaderPhases.OnGoing:
                    currentDisplay?.Update(gameTime);
                    break;
                default:
                    throw exception;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            currentDisplay?.Draw(spriteBatch);
            fadeOutTransition.Draw(spriteBatch);
            fadeInTransition.Draw(spriteBatch);
        }

    }
}
