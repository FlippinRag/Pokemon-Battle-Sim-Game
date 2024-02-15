using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pokemon_Battle_Sim_Game.Services.Content;

namespace Pokemon_Battle_Sim_Game.Battle.PlayerSprites
{
    public abstract class PlayerSprite
    {
        protected const int PokemonTextureHeight = 64;
        protected const int PokemonTextureWidth = 64;

        protected Rectangle DrawRectangle;
        private Texture2D texture;

        protected Vector2 TargetPosition;
        private readonly string textureName;

        public Vector2 CurrentPosition { get; protected set; }
        public bool IsDone => Math.Abs(CurrentPosition.X - TargetPosition.X) < 5;

        protected PlayerSprite(string textureName)
        {
            this.textureName = textureName;
            DrawRectangle = new Rectangle(0, 0, PokemonTextureWidth, PokemonTextureHeight);
        }

        public void LoadContent(IContentLoader contentLoader)
        {
            texture = contentLoader.LoadTexture(textureName);
        }

        public void Update(double gameTime)
        {
            if (IsDone)
                return;
            Move(gameTime);
        }

        protected abstract void Move(double gameTime);

        public abstract void StartMovement();

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, CurrentPosition, DrawRectangle, Color.White);
        }
    }
}
