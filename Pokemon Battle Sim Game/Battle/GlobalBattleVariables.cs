using Pokemon_Battle_Sim_Game.Pokemons;

namespace Pokemon_Battle_Sim_Game.Battle;

public static class GlobalBattleVariables
{
    public static string PlayerAction { get; set; }
    public static string PlayerPokemonMove { get; set; }
    
    public static int CurrentPlayerHp;
    
    public static int CurrentOpponentHp;
    public static PlayerPokemon PlayerInstance { get; set; }
    public static EnemyPokemon EnemyInstance { get; set; }
    
}