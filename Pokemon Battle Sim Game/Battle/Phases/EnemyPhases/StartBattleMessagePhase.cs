using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Pokemon_Battle_Sim_Game.Battle.PlayerSprites;
using Pokemon_Battle_Sim_Game.Inputs;
using Pokemon_Battle_Sim_Game.Services.Content;
using Pokemon_Battle_Sim_Game.Services.DialogBox;
using Pokemon_Battle_Sim_Game.Services.DialogBox.Message;

namespace Pokemon_Battle_Sim_Game.Battle.Phases.EnemyPhases
{
    public class StartBattleMessagePhase : IPhase
    {
        private readonly List<PlayerSprite> trainerSprites;
        private IDialogBoxQueuer dialogBoxQueuer;
        public bool IsDone { get; private set; }


        public StartBattleMessagePhase(List<PlayerSprite> trainerSprites)
        {
            this.trainerSprites = trainerSprites;
        }

        public void LoadContent(IContentLoader contentLoader, IDialogBoxQueuer dialogBoxQueuer, BattleData battleData)
        {
            this.dialogBoxQueuer = dialogBoxQueuer;
            dialogBoxQueuer.AppendBoxIntoQueue(new DialogBoxBattleMessage($"{battleData.Opponent.Name} would like to battle! {Environment.NewLine} {battleData.Opponent.Name} sent out {GlobalBattleVariables.EnemyInstance.EnemyPokemonName}!", new InputKeyboard(), "MessageNavigation"));
            // dialogBoxQueuer.AppendBoxIntoQueue(new DialogBoxBattleMessage($"poopie would like to battle! {Environment.NewLine} poopie sent out poopie!", new InputKeyboard()));

        }

        public void Update(double gameTime)
        {
            IsDone = !dialogBoxQueuer.BoxActive;
        }

        public IPhase GetNextPhase()
        {
            if (IsDone)
            {
                return new OpponentPlayerOutPhase(trainerSprites);
            }

            return this;
        }

        public void Draw(SpriteBatch spriteBatch, IContentLoader contentLoader)
        {
            trainerSprites.ForEach(t => t.Draw(spriteBatch));
        }
    }
}
