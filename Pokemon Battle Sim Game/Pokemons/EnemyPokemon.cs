using System;
using Newtonsoft.Json;

namespace Pokemon_Battle_Sim_Game.Pokemons;

public class EnemyPokemon
{
    [JsonProperty("enemyPokemonID")]
    public int EnemyPokemonID { get; set; }

    [JsonProperty("enemyPokemonName")]
    public string EnemyPokemonName { get; set; }

    [JsonProperty("enemyLevel")]
    public int EnemyLevel { get; set; }

    [JsonProperty("enemyHp")]
    public int EnemyMaxHp { get; set; }
    
    public int EnemyCurrentHp { get; set; }
    
    public double CalculateDamage(double randomiser)
    {
        return Math.Floor(((EnemyMaxHp / 3.0) * 0.6) * randomiser);
    }

    // public bool ShouldUseSpecialMove(double healthPercentage)
    // {
    //     return healthPercentage <= 0.15;
    // }
}