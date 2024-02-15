using System;
using Microsoft.Xna.Framework.Graphics;
using Pokemon_Battle_Sim_Game.Battle.UI;
using Pokemon_Battle_Sim_Game.Pokemons;
using Pokemon_Battle_Sim_Game.Pokemons.Battle;
using Pokemon_Battle_Sim_Game.Services.Content;
using Pokemon_Battle_Sim_Game.Services.DialogBox;

namespace Pokemon_Battle_Sim_Game.Battle.Phases.PlayerPhases
{
    public class PickAttackPhase : IPhase
    {
        private readonly IPokemonSprite playerPokemon;
        private readonly IPokemonSprite enemyPokemon;
        private PokemonStateBar opponentStatusBar;
        private PlayerPokemonStateBar playerStatusBar;
        public bool IsDone { get; private set; }
        private double counter;
        public static int currentOpponentHp;
        public static int currentPlayerHp;
        private readonly int playerMaxHp = GlobalPokemon.PlayerInstance.PlayerHp;
        private readonly int opponentMaxHp = GlobalPokemon.EnemyInstance.EnemyHp;
        private readonly IDialogBoxQueuer dialogBoxQueuer;
        
        public PickAttackPhase(IPokemonSprite playerPokemon, IPokemonSprite enemyPokemon)
        {
            this.playerPokemon = playerPokemon;
            this.enemyPokemon = enemyPokemon;
        }


        public void LoadContent(IContentLoader contentLoader, IDialogBoxQueuer dialogBoxQueuer, BattleData battleData)
        {
            opponentStatusBar = new OpponentPokemonStateBar();
            opponentStatusBar.LoadContent(contentLoader);

            playerStatusBar = new PlayerPokemonStateBar();
            playerStatusBar.LoadContent(contentLoader);
            currentOpponentHp = opponentMaxHp;
            currentPlayerHp = playerMaxHp;
            
            //dialogBoxQueuer.AppendBoxIntoQueue(new DialogBoxBattleMessage("Attack Inventory Run", new InputKeyboard()));
            //
            // dialogBoxQueuer.AppendBoxIntoQueue(new DialogBoxMessage(new Vector2(10,113), 400, 200, "What will you choose?", new InputKeyboard()));
            // dialogBoxQueuer.AppendBoxIntoQueue(new DialogBoxMessage(new Vector2(10,113), 400, 200, "Attack", new InputKeyboard()));
            // dialogBoxQueuer.AppendBoxIntoQueue(new DialogBoxMessage(new Vector2(10,113), 400, 200, "Inventory", new InputKeyboard()));
            // dialogBoxQueuer.AppendBoxIntoQueue(new DialogBoxMessage(new Vector2(10,113), 400, 200, "Run", new InputKeyboard()));
        }
        

        public void Update(double gameTime)
        {
            opponentStatusBar.Update(gameTime);
            playerStatusBar.Update(gameTime);
        
            //HEALTH BAR TEST
            counter += gameTime;
            if (!(counter >= 1000)) return; // Check if a second has passed
            currentOpponentHp = Math.Max(0, currentOpponentHp -= 2);
            opponentStatusBar.HpBar.UpdateHp(currentOpponentHp, GlobalPokemon.EnemyInstance.EnemyHp);

            currentPlayerHp = Math.Max(0, currentPlayerHp -= 2);
            playerStatusBar.HpBar.UpdateHp(currentPlayerHp, GlobalPokemon.PlayerInstance.PlayerHp);

            counter = 0; // Reset the counter
        }

        public IPhase GetNextPhase()
        {
            return null;
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
