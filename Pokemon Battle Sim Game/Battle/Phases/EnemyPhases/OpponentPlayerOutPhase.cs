using System.Collections.Generic;
using System.Linq;
using Pokemon_Battle_Sim_Game.Battle.PlayerSprites;

namespace Pokemon_Battle_Sim_Game.Battle.Phases.EnemyPhases
{
    public class OpponentPlayerOutPhase : PlayerOutPhase<OpponentSprite>
    {
        public OpponentPlayerOutPhase(List<PokemonSprite> trainerSprites) : base(trainerSprites)
        {
        }

        public override IPhase GetNextPhase()
        {
            var opponentTrainerSprites = TrainerSprites.Where(t => t is OpponentSprite).ToList();
            foreach (var opponentTrainerSprite in opponentTrainerSprites)
            {
                TrainerSprites.Remove(opponentTrainerSprite);
            }
            return new EnemyPokemonPhase(TrainerSprites);
        }
    }
}
