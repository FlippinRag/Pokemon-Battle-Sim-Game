using System;
using Newtonsoft.Json;

namespace Pokemon_Battle_Sim_Game.Pokemons;

public class PlayerPokemon // contains the player's pokemon's stats and methods for calculating the xp gain, required xp to level up and damage
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
    public int PlayerMaxHp { get; set; }

    [JsonProperty("evolution")]
    public int Evolution { get; set; }

    public double CalculateXpGain(int enemyLevel, double randomiserXP)
    {
        return Math.Ceiling(((enemyLevel * PlayerLevel) / 7.0) + randomiserXP);
    }

    public double CalculateRequiredXpToLevelUp()
    {
        const int multiplier = 4;
        return Math.Ceiling((double)(multiplier * PlayerLevel));
    }

    public double CalculateDamage(double randomiserDMG)
    {
        return Math.Ceiling(((PlayerMaxHp / 3.0) * Evolution) * randomiserDMG);
    }
}