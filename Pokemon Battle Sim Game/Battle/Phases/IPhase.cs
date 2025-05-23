﻿using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Pokemon_Battle_Sim_Game.Battle.Phases.EnemyPhases;
using Pokemon_Battle_Sim_Game.Battle.Phases.PlayerPhases;
using Pokemon_Battle_Sim_Game.Services.Content;
using Pokemon_Battle_Sim_Game.Services.DialogBox;

namespace Pokemon_Battle_Sim_Game.Battle.Phases
{
    public interface IPhase //phases of the battle in sequenceable order
    {
        bool IsDone { get; }
        void LoadContent(IContentLoader contentLoader, IDialogBoxQueuer dialogBoxQueuer, BattleData battleData);
        void Update(double gameTime);
        IPhase GetNextPhase();
        void Draw(SpriteBatch spriteBatch, IContentLoader contentLoader);
    }
}
