using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pokemon_Battle_Sim_Game.Battle.Common.PokeBallEnterAnimations;
using Pokemon_Battle_Sim_Game.Pokemons.Battle.PokemonEnteranceAnimations;
using Pokemon_Battle_Sim_Game.Services.Content;

namespace Pokemon_Battle_Sim_Game.Battle.Common
{
    public class PokeBall
    {
        private const int EffectCount = 30;
        private const int TimeUntilOpen = 200;
        private const int TimeOpen = 1000;


        private readonly PokeBallData pokeBallData;
        private readonly IPokeBallEnterAnimation pokeBallEnterAnimation;
        private readonly IPokemonEntranceAnimation _pokemonEntranceAnimation;
        private readonly List<PokeBallOpenEffect> pokeballOpenEffects;
        private Texture2D pokeballTexture;
        private IContentLoader contentLoader;
        private Random rnd; 
        private double counter;
        private bool isOpen;

        public bool IsDone { get; set; }

        public PokeBall(PokeBallData pokeBallData, IPokeBallEnterAnimation pokeBallEnterAnimation, IPokemonEntranceAnimation pokemonEntranceAnimation)
        {
            this.pokeBallData = pokeBallData;
            this.pokeBallEnterAnimation = pokeBallEnterAnimation;
            _pokemonEntranceAnimation = pokemonEntranceAnimation;
            pokeballOpenEffects = new List<PokeBallOpenEffect>();
            rnd = new Random();
        }

        public void LoadContent(IContentLoader contentLoader)
        {
            pokeballTexture = contentLoader.LoadTexture(pokeBallData.TextureName);
            this.contentLoader = contentLoader;
        }

        public void Update(double gameTime)
        {
            if (!pokeBallEnterAnimation.IsDone)
            {
                pokeBallEnterAnimation.Update(gameTime, pokeBallData);
                return;
            }
            counter += gameTime;
            if (!isOpen && counter > TimeUntilOpen)
            {
                isOpen = true;
                counter = 0;
                CreatePokeballOpenEffects();
                _pokemonEntranceAnimation.StartEntranceAnimation();
            }
            if (isOpen)
            {
                UpdateOpenPokeball(gameTime);
            }
        }

        private void UpdateOpenPokeball(double gameTime)
        {
            if (counter < TimeOpen)
            {
                pokeballOpenEffects.ForEach(p => p.Update(gameTime));
            }
            if (!_pokemonEntranceAnimation.IsDone)
            {
                _pokemonEntranceAnimation.Update();
            }
            IsDone = counter > TimeOpen && _pokemonEntranceAnimation.IsDone;
        }

        private void CreatePokeballOpenEffects()
        {
            pokeballOpenEffects.Clear();
            for (int n = 0; n < EffectCount; n++)
            {
                Vector2 direction;
                do
                {
                    //Get a direction between -1 and 1.
                    direction = new Vector2((float) rnd.NextDouble()*2 - 1, (float) rnd.NextDouble()*2 - 1);
                } while (pokeballOpenEffects.Any(p => p.Direction == direction));
                var pokeballOpenEffect = new PokeBallOpenEffect(pokeBallData.Position, direction);
                pokeballOpenEffect.LoadContent(contentLoader);
                pokeballOpenEffects.Add(pokeballOpenEffect);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (IsDone) return; 
            spriteBatch.Draw(pokeballTexture, pokeBallData.Position, 
                new Rectangle(PokeBallData.PokeballWidth * (isOpen ? 1 : 0), 0, PokeBallData.PokeballWidth, PokeBallData.PokeballHeight), pokeBallData.Color,
                pokeBallData.Rotation, new Vector2(PokeBallData.PokeballWidth/2, PokeBallData.PokeballHeight/2), Vector2.One, SpriteEffects.None, 0);
            pokeballOpenEffects.ForEach(p => p.Draw(spriteBatch));
        }
    }
}
