using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pokemon_Battle_Sim_Game.Battle;
using Pokemon_Battle_Sim_Game.Battle.Phases;
using Pokemon_Battle_Sim_Game.Battle.Phases.EnemyPhases;
using Pokemon_Battle_Sim_Game.Services.Content;
using Pokemon_Battle_Sim_Game.Services.DialogBox;

namespace Pokemon_Battle_Sim_Game.Display
{
    public class DisplayShowBattle : Display
    { // This class is responsible for displaying the battle and being part of the queue of the dialog boxes
        private IContentLoader contentLoader;
        private readonly IDialogBoxQueuer dialogBoxQueuer;
        private IPhase currentPhase;
        private readonly BattleData battleData;
        private readonly DialogBoxBattle dialogBoxBattle;
        private Texture2D backgroundTexture;
        public DisplayShowBattle(IDialogBoxQueuer dialogBoxQueuer, IPhase startPhase, BattleData battleData)
        {
            this.dialogBoxQueuer = dialogBoxQueuer;
            this.battleData = battleData;
            currentPhase = startPhase;
            dialogBoxBattle = new DialogBoxBattle(new Vector2(-30, 98), 300, 80);
        }

        public override void LoadContent(IContentLoader contentLoader)
        {
            backgroundTexture = contentLoader.LoadTexture("Battle/Backgrounds/background");
            dialogBoxBattle.LoadContent(contentLoader);
            currentPhase.LoadContent(contentLoader, dialogBoxQueuer, battleData);
            this.contentLoader = contentLoader;
        }

        public override void Update(double gameTime)
        {
            currentPhase.Update(gameTime);
            if (!currentPhase.IsDone) return;
            currentPhase = currentPhase.GetNextPhase();
            currentPhase.LoadContent(contentLoader, dialogBoxQueuer, battleData);

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(backgroundTexture, new Rectangle(0, -20, 240, 160), Color.White);
            currentPhase.Draw(spriteBatch, null);
            dialogBoxBattle.Draw(spriteBatch);
        }
    }
}
