﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Pokemon_Battle_Sim_Game.Battle.PlayerSprites;
using Pokemon_Battle_Sim_Game.Services.Content;
using Pokemon_Battle_Sim_Game.Services.DialogBox;

namespace Pokemon_Battle_Sim_Game.Battle.Phases.EnemyPhases
{
    public abstract class PlayerOutPhase<TTrainerSprite> : IPhase where TTrainerSprite : PokemonSprite
    {
        protected readonly List<PokemonSprite> TrainerSprites;
        public bool IsDone { get; protected set; }


        protected PlayerOutPhase(List<PokemonSprite> trainerSprites)
        {
            TrainerSprites = trainerSprites;
        }

        public virtual void LoadContent(IContentLoader contentLoader, IDialogBoxQueuer dialogBoxQueuer, BattleData battleData)
        {
            foreach (var trainerSprite in TrainerSprites.Where(t => t is TTrainerSprite))
            {
                trainerSprite.StartMovement();
            }
        }

        public virtual void Update(double gameTime)
        {
            TrainerSprites.ForEach(t => t.Update(gameTime));
            IsDone = TrainerSprites.TrueForAll(t => t.IsDone);
        }

        public abstract IPhase GetNextPhase();

        public virtual void Draw(SpriteBatch spriteBatch, IContentLoader contentLoader)
        {
            TrainerSprites.ForEach(t => t.Draw(spriteBatch));
        }
    }
}
