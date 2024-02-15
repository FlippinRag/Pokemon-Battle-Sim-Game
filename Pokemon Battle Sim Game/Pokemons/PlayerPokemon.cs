using Newtonsoft.Json;

namespace Pokemon_Battle_Sim_Game.Pokemons;

public class PlayerPokemon
{
    [JsonProperty("playerPokemonID")]
    public int PlayerPokemonId { get; set; }

    [JsonProperty("playerPokemonName")]
    public string PlayerPokemonName { get; set; }

    [JsonProperty("playerXP")]
    public int PlayerXp { get; set; }

    [JsonProperty("playerLevel")]
    public int PlayerLevel { get; set; }

    [JsonProperty("playerHP")]
    public int PlayerHp { get; set; }
}