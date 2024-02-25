using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pokemon_Battle_Sim_Game.Battle.Phases;
using Pokemon_Battle_Sim_Game.Battle.Phases.PlayerPhases;
using Pokemon_Battle_Sim_Game.Pokemons;

namespace Pokemon_Battle_Sim_Game.Battle.UI
{
    public class PlayerPokemonStateBar : PokemonStateBar
    { // health bar for the player
        private double directionCounter;
        private bool isGoingUp;

        public PlayerPokemonStateBar()
        {
            BasePosition = new Vector2(130, 70);
            HPbarPosition = new Vector2(146, 62);
        }

        public override void Update(double gameTime)
        {
            base.Update(gameTime); //shimmeys around up and down a little bit
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
            spriteBatch.DrawString(font, GlobalBattleVariables.PlayerInstance.PlayerPokemonName, new Vector2(BasePosition.X + 18, BasePosition.Y + 4), Color.Black);
            spriteBatch.DrawString(font, $"Lv{GlobalBattleVariables.PlayerInstance.PlayerLevel}", new Vector2(BasePosition.X + 87, BasePosition.Y + 4), Color.Black);
            if (GlobalBattleVariables.PlayerInstance.PlayerMaxHp >= 0 && !AttackPhase.IsAttackPhase)
            { //if the value is 0 because its drawn before currenthp is initialised, then make it max until it needs to be changed
                spriteBatch.DrawString(font,$"{GlobalBattleVariables.PlayerInstance.PlayerMaxHp} / {GlobalBattleVariables.PlayerInstance.PlayerMaxHp}", new Vector2(BasePosition.X + 67, BasePosition.Y + 22), Color.Black);
            }
            else
            {
                spriteBatch.DrawString(font,$"{GlobalBattleVariables.CurrentPlayerHp} / {GlobalBattleVariables.PlayerInstance.PlayerMaxHp}", new Vector2(BasePosition.X + 67, BasePosition.Y + 22), Color.Black);
            }
            HpBar.Draw(spriteBatch, HPbarPosition);
        }
    }
}