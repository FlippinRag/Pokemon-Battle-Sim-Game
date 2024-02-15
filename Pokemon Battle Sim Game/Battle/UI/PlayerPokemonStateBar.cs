using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pokemon_Battle_Sim_Game.Battle.Phases.PlayerPhases;
using Pokemon_Battle_Sim_Game.Pokemons;

namespace Pokemon_Battle_Sim_Game.Battle.UI
{
    public class PlayerPokemonStateBar : PokemonStateBar
    {
        private double directionCounter;
        private bool isGoingUp;
        private static int CurrentHp => PickAttackPhase.currentPlayerHp;
        public PlayerPokemonStateBar()
        {
            BasePosition = new Vector2(130, 70);
            HPbarPosition = new Vector2(146, 62);
        }

        public override void Update(double gameTime)
        {
            base.Update(gameTime);
            directionCounter += gameTime;
            if (!(directionCounter > 500)) return;
            BasePosition.Y += isGoingUp ? 1 : -1;
            HPbarPosition.Y += isGoingUp ? 1 : -1;
            directionCounter = 0;
            isGoingUp = !isGoingUp;
        }
        
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(playerBarTexture, new Rectangle((int)BasePosition.X, (int)BasePosition.Y, DefaultBarWidth, DefaultBarHeight), Color.White);
            spriteBatch.DrawString(font, GlobalPokemon.PlayerInstance.PlayerPokemonName, new Vector2(BasePosition.X + 18, BasePosition.Y + 4), Color.Black);
            spriteBatch.DrawString(font, $"Lv{GlobalPokemon.PlayerInstance.PlayerLevel}", new Vector2(BasePosition.X + 87, BasePosition.Y + 4), Color.Black);
            spriteBatch.DrawString(font,$"{CurrentHp} / {GlobalPokemon.PlayerInstance.PlayerHp}", new Vector2(BasePosition.X + 67, BasePosition.Y + 22), Color.Black);
            HpBar.Draw(spriteBatch, HPbarPosition);
        }
    }
}