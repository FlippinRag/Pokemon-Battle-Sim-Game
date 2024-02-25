namespace Pokemon_Battle_Sim_Game.Battle
{
    public class BattleData // get the data from opponent
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
