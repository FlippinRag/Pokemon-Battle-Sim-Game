using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pokemon_Battle_Sim_Game.Pokemons;
using Pokemon_Battle_Sim_Game.Services.Content;

namespace Pokemon_Battle_Sim_Game.Battle.UI
{
    public abstract class PokemonStateBar
    {// this is a abstract class for the health bar of the player and the enemy
        protected const int DefaultBarWidth = 110;
        protected const int DefaultBarHeight = 40;
        protected Texture2D enemyBarTexture;
        protected Texture2D playerBarTexture;
        protected SpriteFont font;
        protected Vector2 BasePosition;
        protected Vector2 HPbarPosition;
        public HealthBar HpBar { get; } = new(GlobalBattleVariables.CurrentPlayerHp, GlobalBattleVariables.PlayerInstance.PlayerMaxHp);

        public void LoadContent(IContentLoader contentLoader)
        {
            playerBarTexture = contentLoader.LoadTexture("Battle/Gui/playerbox");
            enemyBarTexture = contentLoader.LoadTexture("Battle/Gui/enemybox");
            font = contentLoader.LoadFont("PokemonBattleFont");
            HpBar.LoadContent(contentLoader);
        }

        public virtual void Update(double gameTime)
        {
            HpBar.Update();
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
        }
    }
}
