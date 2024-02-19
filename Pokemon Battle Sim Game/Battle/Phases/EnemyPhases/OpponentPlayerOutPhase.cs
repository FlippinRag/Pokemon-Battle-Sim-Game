using System.Collections.Generic;
using System.Linq;
using Pokemon_Battle_Sim_Game.Battle.PlayerSprites;

namespace Pokemon_Battle_Sim_Game.Battle.Phases.EnemyPhases
{
    public class OpponentPlayerOutPhase : PlayerOutPhase<PlayerOpponentSprite>
    {
        public OpponentPlayerOutPhase(List<PlayerSprite> trainerSprites) : base(trainerSprites)
        {
        }

        public override IPhase GetNextPhase()
        {
            var opponentTrainerSprites = TrainerSprites.Where(t => t is PlayerOpponentSprite).ToList();
            foreach (var opponentTrainerSprite in opponentTrainerSprites)
            {
                TrainerSprites.Remove(opponentTrainerSprite);
            }
            return new EnemyPlayerFirstPokemonPhase(TrainerSprites);
        }
    }
}
