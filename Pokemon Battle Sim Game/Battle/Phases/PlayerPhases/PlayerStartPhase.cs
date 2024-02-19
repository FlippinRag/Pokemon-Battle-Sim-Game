using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Pokemon_Battle_Sim_Game.Battle.Phases.EnemyPhases;
using Pokemon_Battle_Sim_Game.Battle.PlayerSprites;
using Pokemon_Battle_Sim_Game.Services.Content;
using Pokemon_Battle_Sim_Game.Services.DialogBox;

namespace Pokemon_Battle_Sim_Game.Battle.Phases.PlayerPhases
{
    public class PlayerStartPhase : IPhase
    {
        private List<PlayerSprite> playerSprites;
        public bool IsDone { get; set; }

        public void LoadContent(IContentLoader contentLoader, IDialogBoxQueuer dialogBoxQueuer, BattleData battleData)
        {
            playerSprites = new List<PlayerSprite>
            {
                new PlayerOpponentSprite(battleData.Opponent.TextureName),
                new PlayerPlayerSprite("Trainers/trainer_back")
            };
            playerSprites.ForEach(t => t.LoadContent(contentLoader));
        }

        public void Update(double gameTime)
        {
            playerSprites.ForEach(t => t.Update(gameTime));
            IsDone = playerSprites.TrueForAll(t => t.IsDone);
        }

        public IPhase GetNextPhase()
        {
            return new StartBattleMessagePhase(playerSprites);
        }

        public void Draw(SpriteBatch spriteBatch, IContentLoader contentLoader)
        {
            playerSprites.ForEach(t => t.Draw(spriteBatch));
        }
    }
}
