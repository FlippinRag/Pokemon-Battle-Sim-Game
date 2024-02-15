using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pokemon_Battle_Sim_Game.Battle.Common;
using Pokemon_Battle_Sim_Game.Battle.Common.PokeBallEnterAnimations;
using Pokemon_Battle_Sim_Game.Battle.Phases.PlayerPhases;
using Pokemon_Battle_Sim_Game.Battle.PlayerSprites;
using Pokemon_Battle_Sim_Game.Pokemons;
using Pokemon_Battle_Sim_Game.Pokemons.Battle;
using Pokemon_Battle_Sim_Game.Pokemons.Battle.PokemonEnteranceAnimations;
using Pokemon_Battle_Sim_Game.Services.Content;
using Pokemon_Battle_Sim_Game.Services.DialogBox;

namespace Pokemon_Battle_Sim_Game.Battle.Phases.TrainerPhases
{
    public class EnemyPlayerFirstPokemonPhase : IPhase
    {
        private readonly List<PlayerSprite> trainerSprites;
        private readonly PokeBall pokeBall;
        private readonly IPokemonSprite pokemonSpriteTest;
        public bool IsDone { get; private set; }

        public EnemyPlayerFirstPokemonPhase(List<PlayerSprite> trainerSprites)
        {
            this.trainerSprites = trainerSprites;
            var pokemonSpriteTest = new PokemonSprite(new PokemonSpriteData(0, 0, new Vector2(165, 40), $"Pokemons/EnemyPokemons/{GlobalPokemon.EnemyInstance.EnemyPokemonName}"));
            pokeBall = new PokeBall(new PokeBallData(new Vector2(165, 55), "Battle/Pokeballs/pokeball_regular"), new NoPokeBallEnterAnimation(), new PokemonEntranceAnimation(pokemonSpriteTest.GetPokemonBattleSpriteData()));
            this.pokemonSpriteTest = pokemonSpriteTest;
        }

        public void LoadContent(IContentLoader contentLoader, IDialogBoxQueuer dialogBoxQueuer, BattleData battleData)
        {
            pokeBall.LoadContent(contentLoader);
            pokemonSpriteTest.LoadContent(contentLoader);
        }

        public void Update(double gameTime)
        {
            pokeBall.Update(gameTime);
            IsDone = pokeBall.IsDone;
        }

        public IPhase GetNextPhase()
        {
            return new PlayerPlayerOutPhase(trainerSprites, pokemonSpriteTest);
        }

        public void Draw(SpriteBatch spriteBatch, IContentLoader contentLoader)
        {
            pokeBall.Draw(spriteBatch);
            trainerSprites.ForEach(t => t.Draw(spriteBatch));
            pokemonSpriteTest.Draw(spriteBatch);
        }


    }
}
