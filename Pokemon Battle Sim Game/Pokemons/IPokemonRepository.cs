using System.Threading.Tasks;

namespace Pokemon_Battle_Sim_Game.Pokemons;

public interface IPokemonRepository
{
    Task<PlayerPokemon> GetPlayerPokemonById(int playerPokemonId);
    Task<EnemyPokemon> GetEnemyPokemonById(int enemyPokemonId);
    // Define other methods as needed
}