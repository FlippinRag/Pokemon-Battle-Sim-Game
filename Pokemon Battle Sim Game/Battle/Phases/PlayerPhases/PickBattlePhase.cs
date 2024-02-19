using System;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pokemon_Battle_Sim_Game.Battle.UI;
using Pokemon_Battle_Sim_Game.Inputs;
using Pokemon_Battle_Sim_Game.Pokemons;
using Pokemon_Battle_Sim_Game.Pokemons.Battle;
using Pokemon_Battle_Sim_Game.Services.Content;
using Pokemon_Battle_Sim_Game.Services.DialogBox;
using Pokemon_Battle_Sim_Game.Services.DialogBox.Message;

namespace Pokemon_Battle_Sim_Game.Battle.Phases.PlayerPhases
{
    public class PickBattlePhase : IPhase
    {
        private readonly IPokemonSprite playerPokemon;
        private readonly IPokemonSprite enemyPokemon;
        private PokemonStateBar opponentStatusBar;
        private PlayerPokemonStateBar playerStatusBar;
        public bool IsDone { get; private set; }
        private IDialogBoxQueuer dialogBoxQueuer;

        public PickBattlePhase(IPokemonSprite playerPokemon, IPokemonSprite enemyPokemon)
        {
            this.playerPokemon = playerPokemon;
            this.enemyPokemon = enemyPokemon;
        }


        public void LoadContent(IContentLoader contentLoader, IDialogBoxQueuer dialogBoxQueuer, BattleData battleData)
        {
            this.dialogBoxQueuer = dialogBoxQueuer;
            opponentStatusBar = new OpponentPokemonStateBar();
            opponentStatusBar.LoadContent(contentLoader);

            playerStatusBar = new PlayerPokemonStateBar();
            playerStatusBar.LoadContent(contentLoader);
            
            dialogBoxQueuer.AppendBoxIntoQueue(new DialogBoxBattleMessage($"What will you choose? {Environment.NewLine}{Environment.NewLine} 1. Fight           2. Run", new InputKeyboard(), "BattlePick"));
            
        }
        

        public void Update(double gameTime)
        {
            opponentStatusBar.Update(gameTime);
            playerStatusBar.Update(gameTime);
            
            IsDone = !dialogBoxQueuer.BoxActive;
        }

        public IPhase GetNextPhase()
        {
            return new AttackPhase(playerPokemon, enemyPokemon);
        }

        public void Draw(SpriteBatch spriteBatch, IContentLoader contentLoader)
        {
            playerPokemon.Draw(spriteBatch);
            enemyPokemon.Draw(spriteBatch);
            opponentStatusBar.Draw(spriteBatch);
            playerStatusBar.Draw(spriteBatch);

        }
        
    }
}
