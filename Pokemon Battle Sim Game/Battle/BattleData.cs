namespace Pokemon_Battle_Sim_Game.Battle
{
    public class BattleData
    {
        public Player Player { get; }
        public Player Opponent { get; }
        public BattleData(Player player, Player opponent)
        {
            Player = player;
            Opponent = opponent;
        }
    }
}
