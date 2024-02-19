using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pokemon_Battle_Sim_Game.Battle.Phases;
using Pokemon_Battle_Sim_Game.Battle.Phases.PlayerPhases;
using Pokemon_Battle_Sim_Game.Pokemons;

namespace Pokemon_Battle_Sim_Game.Battle.UI
{
    public class OpponentPokemonStateBar : PokemonStateBar
    {
        private double directionCounter;
        private bool isGoingUp;
        private static int CurrentMaxHp => GlobalBattleVariables.EnemyInstance.EnemyMaxHp;

        public OpponentPokemonStateBar()
        {
            BasePosition = new Vector2(0, 0); // changes place of the status bar
            directionCounter = 0;
            isGoingUp = true; 
        }

        public override void Update(double gameTime)
        {
            base.Update(gameTime);
            directionCounter += gameTime;
            if (!(directionCounter > 500)) return;
            BasePosition.Y += isGoingUp ? 1 : -1;
            directionCounter = 0;
            isGoingUp = !isGoingUp;
        }
        
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(enemyBarTexture, new Rectangle((int)BasePosition.X, (int)BasePosition.Y, DefaultBarWidth, DefaultBarHeight), Color.White);
            spriteBatch.DrawString(font, GlobalBattleVariables.EnemyInstance.EnemyPokemonName, new Vector2(BasePosition.X + 2, BasePosition.Y + 5), Color.Black);
            spriteBatch.DrawString(font, $"Lv{GlobalBattleVariables.EnemyInstance.EnemyLevel}", new Vector2(BasePosition.X + 72, BasePosition.Y + 5), Color.Black);
            if (GlobalBattleVariables.PlayerInstance.PlayerMaxHp >= 0 && !AttackPhase.IsAttackPhase)
            {
                spriteBatch.DrawString(font, $"{GlobalBattleVariables.EnemyInstance.EnemyMaxHp} / {GlobalBattleVariables.EnemyInstance.EnemyMaxHp}", new Vector2(BasePosition.X + 40, BasePosition.Y + 12), Color.Black);
            }
            else
            {
                spriteBatch.DrawString(font, $"{GlobalBattleVariables.CurrentOpponentHp} / {GlobalBattleVariables.EnemyInstance.EnemyMaxHp}", new Vector2(BasePosition.X + 40, BasePosition.Y + 12), Color.Black);

            }
            HpBar.Draw(spriteBatch, BasePosition);
        }
    }
}
