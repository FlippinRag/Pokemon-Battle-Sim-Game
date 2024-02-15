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
    public int EnemyHp { get; set; }

    [JsonProperty("enemySpecialMove")]
    public string EnemySpecialMove { get; set; }
}