using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pokemon_Battle_Sim_Game.Services.Content;

namespace Pokemon_Battle_Sim_Game.Services.DialogBox
{
    public class DialogBox 
    {
        protected Texture2D Texture;
        protected Vector2 Pos;
        protected Color Color { get; set; }
        protected readonly int Width;
        private readonly int height;
        public bool IsDone { get; protected set; }
        

        protected DialogBox(Vector2 pos, int width, int height)
        {
            Pos = pos;
            Width = width;
            this.height = height;
        }

        public virtual void LoadContent(IContentLoader contentLoader)
        {
        }

        public virtual void Update(double gameTime)
        {
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (Texture == null) return;
            spriteBatch.Draw(Texture, new Rectangle((int)Pos.X, (int)Pos.Y, Width, height), Color.White);
        }
    }

}
