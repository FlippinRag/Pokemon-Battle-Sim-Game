using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pokemon_Battle_Sim_Game.Services.Content;

namespace Pokemon_Battle_Sim_Game.Display.DisplayTransition
{
    public abstract class DisplayTransitionFade : IDisplayTransition
    {
        protected byte FadeStepCount;
        protected byte Alpha;
        private readonly Rectangle backgroundRectangle; 
        private Texture2D backgroundTexture; 
        public bool IsDone { get; protected set; }

        protected DisplayTransitionFade(int displayWidth, int displayHeight, byte fadeStepCount)
        {
            backgroundRectangle = new Rectangle(0, 0, displayWidth, displayHeight);
            FadeStepCount = fadeStepCount;
        }

        public void LoadContent(IContentLoader contentLoader)
        {
            backgroundTexture = contentLoader.LoadTexture("ScreenEffects/white_block");
        }

        public abstract void Start();

        public abstract void Update();

        public void Draw(SpriteBatch spriteBatch)
        {
            if (IsDone)
                return; 
            spriteBatch.Draw(backgroundTexture, backgroundRectangle, new Color((byte)0, (byte)0, (byte)0, Alpha));
        }
    }
}
