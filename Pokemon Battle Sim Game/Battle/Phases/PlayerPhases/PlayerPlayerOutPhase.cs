using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pokemon_Battle_Sim_Game.Battle.Common;
using Pokemon_Battle_Sim_Game.Battle.Common.PokeBallEnterAnimations;
using Pokemon_Battle_Sim_Game.Battle.Phases.EnemyPhases;
using Pokemon_Battle_Sim_Game.Battle.PlayerSprites;
using Pokemon_Battle_Sim_Game.Inputs;
using Pokemon_Battle_Sim_Game.Pokemons;
using Pokemon_Battle_Sim_Game.Pokemons.Battle;
using Pokemon_Battle_Sim_Game.Pokemons.Battle.PokemonEnteranceAnimations;
using Pokemon_Battle_Sim_Game.Services.Content;
using Pokemon_Battle_Sim_Game.Services.DialogBox;
using Pokemon_Battle_Sim_Game.Services.DialogBox.Message;

namespace Pokemon_Battle_Sim_Game.Battle.Phases.PlayerPhases
{
    public class PlayerPlayerOutPhase : PlayerOutPhase<PlayerPlayerSprite>
    {
        private readonly IPokemonSprite opponentPokemonSprite;
        private readonly IPokemonSprite pokemonSpriteTest;
        private readonly PokeBall pokeBall;
        private IPokemonRepository _pokemonRepository;

        public PlayerPlayerOutPhase(List<PlayerSprite> trainerSprites, IPokemonSprite opponentPokemonSprite) : base(trainerSprites)
        {
            this.opponentPokemonSprite = opponentPokemonSprite;
            PokemonSprite pokemonSpriteTest = new PokemonSprite(new PokemonSpriteData(0, 0, new Vector2(50, 210), $"Pokemons/PlayerPokemons/{GlobalBattleVariables.PlayerInstance.PlayerPokemonName}"));
            pokeBall = new PokeBall(new PokeBallData(new Vector2(0, 70), "Battle/Pokeballs/pokeball_regular"), new PlayerCastingPokeBallEnterAnimation(), new GrowPokemonEntranceAnimation(pokemonSpriteTest.GetPokemonBattleSpriteData()));
            this.pokemonSpriteTest = pokemonSpriteTest;
        }

        public override void LoadContent(IContentLoader contentLoader, IDialogBoxQueuer dialogBoxQueuer, BattleData battleData)
        {
            base.LoadContent(contentLoader, dialogBoxQueuer, battleData);
            pokeBall.LoadContent(contentLoader);
            pokemonSpriteTest.LoadContent(contentLoader);
            dialogBoxQueuer.AppendBoxIntoQueue(new DialogBoxBattleMessage($"You sent out {GlobalBattleVariables.PlayerInstance.PlayerPokemonName}!", new InputKeyboard(), "MessageNavigation"));

        }

        public override void Update(double gameTime)
        {
            base.Update(gameTime);
            if (!ShowPokeBall()) return; 
            pokeBall.Update(gameTime);
            IsDone = pokeBall.IsDone;
        }

        private bool ShowPokeBall()
        {
            return TrainerSprites.All(t => t.CurrentPosition.X < 0);
        }

        public override IPhase GetNextPhase()
        {
            return new PickBattlePhase(pokemonSpriteTest, opponentPokemonSprite);
        }


        public override void Draw(SpriteBatch spriteBatch, IContentLoader contentLoader)
        {
            base.Draw(spriteBatch, null);
            if (ShowPokeBall())
            {
                pokeBall.Draw(spriteBatch);
            }
            pokemonSpriteTest.Draw(spriteBatch);
            opponentPokemonSprite.Draw(spriteBatch);
        }
    }
}
