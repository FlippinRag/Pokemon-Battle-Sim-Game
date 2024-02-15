using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pokemon_Battle_Sim_Game.Battle;
using Pokemon_Battle_Sim_Game.Battle.Phases.TrainerPhases;
using Pokemon_Battle_Sim_Game.Display;
using Pokemon_Battle_Sim_Game.Display.DisplayTransition;
using Pokemon_Battle_Sim_Game.Pokemons;
using Pokemon_Battle_Sim_Game.Services.Content;
using Pokemon_Battle_Sim_Game.Services.DialogBox;

namespace Pokemon_Battle_Sim_Game
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class PokemonGame : Game
    {
        private RenderTarget2D backbuffer;
        private SpriteBatch spritebatch;
        private readonly DisplayLoader displayloader;
        private readonly DialogBoxHandler windowhandler;
        private double timer;
        private readonly IPokemonRepository _pokemonRepository;
        private readonly Random random = new();

        public PokemonGame()
        {
            var graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 720;
            Content.RootDirectory = "Content";
            IContentLoader contentLoader = new ContentLoader(Content);
            var dialogBoxManager = new DialogBoxManager(contentLoader);
            windowhandler = new DialogBoxHandler(dialogBoxManager);
            displayloader = new DisplayLoader(new DisplayTransitionFadeOut(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight, 5),
                new DisplayTransitionFadeIn(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight, 3), contentLoader);
            displayloader.LoadNextDisplay(new DisplayShowBattle(windowhandler, new PlayerStartPhase(), new BattleData(new PlayerTestLoader().LoadPlayer(1),new PlayerTestLoader().LoadPlayer(1))));
            _pokemonRepository = new PokemonApiRepository();
        }
        protected override async void LoadContent()
        {
            backbuffer = new RenderTarget2D(GraphicsDevice, 240, 160);
            spritebatch = new SpriteBatch(GraphicsDevice);
            displayloader.LoadContent();
            var rndPlayerId = random.Next(1, 5);
            var playerPokemon = await _pokemonRepository.GetPlayerPokemonById(1);
            // Display fetched player Pokemon data
            Console.WriteLine($"Fetched Player Pokemon Data: ID= {playerPokemon.PlayerPokemonId}, Player Name= {playerPokemon.PlayerPokemonName}, PlayerLevel= {playerPokemon.PlayerLevel}, PlayerHp= {playerPokemon.PlayerHp}");
            GlobalPokemon.PlayerInstance = playerPokemon;
            if (GlobalPokemon.PlayerInstance.PlayerLevel < 7)
            {
                var randomEnemyId = random.Next(1, 4);
                var enemyPokemon = await _pokemonRepository.GetEnemyPokemonById(1);
                Console.WriteLine($"Fetched Enemy Pokemon Data: ID= {enemyPokemon.EnemyPokemonID}, EnemyName= {enemyPokemon.EnemyPokemonName}, EnemyLevel= {enemyPokemon.EnemyLevel}, EnemyHp= {enemyPokemon.EnemyHp}, EnemySpecialMove= {enemyPokemon.EnemySpecialMove}");
                GlobalPokemon.EnemyInstance = enemyPokemon;
            }
            else
            {
                var enemyPokemon = await _pokemonRepository.GetEnemyPokemonById(1);
                Console.WriteLine($"Fetched Enemy Pokemon Data: ID= {enemyPokemon.EnemyPokemonID}, EnemyName= {enemyPokemon.EnemyPokemonName}, EnemyLevel= {enemyPokemon.EnemyLevel}, EnemyHp= {enemyPokemon.EnemyHp}, EnemySpecialMove= {enemyPokemon.EnemySpecialMove}");
                GlobalPokemon.EnemyInstance = enemyPokemon;
            }
            
        }
        
        protected override void Update(GameTime gameTime)
        {
            timer += gameTime.ElapsedGameTime.TotalSeconds;
            if (timer > 120)
            {
                displayloader.BeginClosingProcess();
                timer = 0;
            }

            displayloader.Update(gameTime.ElapsedGameTime.Milliseconds);
            if (windowhandler.BoxActive)
            {
                windowhandler.UpdateCurrentDialogBox(gameTime.ElapsedGameTime.Milliseconds);
            }            
            base.Update(gameTime);
        }

        protected override bool BeginDraw()
        {
            GraphicsDevice.SetRenderTarget(backbuffer);
            GraphicsDevice.Clear(Color.Green);

            spritebatch.Begin();
            displayloader.Draw(spritebatch);
            windowhandler.Draw(spritebatch);
            spritebatch.End();

            return base.BeginDraw();
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.SetRenderTarget(null);
            GraphicsDevice.Clear(Color.Green);
            spritebatch.Begin();
            spritebatch.Draw(backbuffer, new Rectangle(0, 0, GraphicsDevice.Viewport.Bounds.Width, GraphicsDevice.Viewport.Bounds.Height), Color.White);
            spritebatch.End();
            base.Draw(gameTime);
        }
    }
    
    public static class Program
    {
        public static void Main()
        {
            var game = new PokemonGame();
            game.Window.AllowUserResizing = true;
            game.Run();
        }
    }
}
