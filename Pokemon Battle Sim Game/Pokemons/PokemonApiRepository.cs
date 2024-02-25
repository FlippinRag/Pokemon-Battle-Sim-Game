using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Pokemon_Battle_Sim_Game.Pokemons;

public class PokemonApiRepository : IPokemonRepository
{ // class is a repository for the Pokemon API and is responsible for getting the player and enemy Pokemon stats and updating the player Pokemon stats, storing it in global variables so it can be changed for later.
    private static readonly HttpClient _httpClient = new();
    private const string ApiBaseUrl = "http://localhost:8080";

    public async Task<PlayerPokemon> GetPlayerPokemonById(int playerPokemonId)
    {
        try
        {
            string endpoint = $"{ApiBaseUrl}/getPlayerPokemonStats?id={playerPokemonId}";
            HttpResponseMessage response = await _httpClient.GetAsync(endpoint);

            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                PlayerPokemon playerPokemon = JsonConvert.DeserializeObject<PlayerPokemon>(json);
                return playerPokemon;
            }

            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                Console.WriteLine("Pokémon not found.");
                return null;
            }

            Console.WriteLine($"Error: {response.StatusCode}");
            return null;
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"HTTP request failed: {ex.Message}");
            return null;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An unexpected error occurred: {ex.Message}");
            return null;
        }
    }
    public async Task<EnemyPokemon> GetEnemyPokemonById(int enemyPokemonId)
    {
        try
        {
            string endpoint = $"{ApiBaseUrl}/getEnemyPokemon?id={enemyPokemonId}";
            HttpResponseMessage response = await _httpClient.GetAsync(endpoint);

            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                EnemyPokemon enemyPokemon = JsonConvert.DeserializeObject<EnemyPokemon>(json);
                return enemyPokemon;
            }

            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                Console.WriteLine("Enemy Pokémon not found.");
                return null;
            }

            Console.WriteLine($"Error: {response.StatusCode}");
            return null;
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"HTTP request failed: {ex.Message}");
            return null;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An unexpected error occurred: {ex.Message}");
            return null;
        }
    }
    
    public static async Task<bool> UpdatePlayerPokemonStats(int playerPokemonId, int playerXP, int playerLevel, int playerHP, int evolution, string playerPokemonName)
    {
        try
        {
            var data = new Dictionary<string, string>
            {
                { "playerPokemonID", playerPokemonId.ToString() },
                { "playerXP", playerXP.ToString() },
                { "playerLevel", playerLevel.ToString() },
                { "playerHP", playerHP.ToString() },
                { "evolution", evolution.ToString() },
                { "playerPokemonName", playerPokemonName }
            };

            var response = await _httpClient.PostAsync($"{ApiBaseUrl}/updatePokemonStats", new FormUrlEncodedContent(data));
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Pokemon stats updated successfully!");
                return true;
            }

            Console.WriteLine($"Error: {response.StatusCode}");
            return false;
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"HTTP request failed: {ex.Message}");
            return false;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An unexpected error occurred: {ex.Message}");
            return false;
        }
    }
    
}