using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Pokemon_Battle_Sim_Game.Battle.Phases.EnemyPhases;
using Pokemon_Battle_Sim_Game.Battle.PlayerSprites;
using Pokemon_Battle_Sim_Game.Services.Content;
using Pokemon_Battle_Sim_Game.Services.DialogBox;

namespace Pokemon_Battle_Sim_Game.Battle.Phases.PlayerPhases
{
    public class PlayerStartPhase : IPhase // start of player sprites
    {
        private List<PokemonSprite> playerSprites;
        public bool IsDone { get; set; }

        public void LoadContent(IContentLoader contentLoader, IDialogBoxQueuer dialogBoxQueuer, BattleData battleData)
        {
            playerSprites = new List<PokemonSprite>
            {
                new OpponentSprite(battleData.Opponent.TextureName),
                new PlayerSprite("Trainers/trainer_back")
            };
            playerSprites.ForEach(t => t.LoadContent(contentLoader)); // load content for each sprite
        }

        public void Update(double gameTime)
        {
            playerSprites.ForEach(t => t.Update(gameTime)); // update each sprite
            IsDone = playerSprites.TrueForAll(t => t.IsDone);
        }

        public IPhase GetNextPhase()
        {
            return new StartBattleMessagePhase(playerSprites);
        }

        public void Draw(SpriteBatch spriteBatch, IContentLoader contentLoader)
        {
            playerSprites.ForEach(t => t.Draw(spriteBatch)); // draw each sprite
        }
    }
}
