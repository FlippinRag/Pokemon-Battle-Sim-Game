using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Pokemon_Battle_Sim_Game.Battle.UI;
using Pokemon_Battle_Sim_Game.Inputs;
using Pokemon_Battle_Sim_Game.Pokemons;
using Pokemon_Battle_Sim_Game.Pokemons.Battle;
using Pokemon_Battle_Sim_Game.Services.Content;
using Pokemon_Battle_Sim_Game.Services.DialogBox;
using Pokemon_Battle_Sim_Game.Services.DialogBox.Message;

namespace Pokemon_Battle_Sim_Game.Battle.Phases;

public enum BattleState
{
    PlayerTurn,
    EnemyTurn,
    GameOver
}

public class AttackPhase : IPhase
{
    private readonly IPokemonSprite playerPokemon;
    private readonly IPokemonSprite enemyPokemon;
    private PokemonStateBar opponentStatusBar;
    private PlayerPokemonStateBar playerStatusBar;
    private IDialogBoxQueuer dialogBoxQueuer;
    public bool IsDone { get; private set; }
    public static bool IsAttackPhase { get; private set; }
    private readonly float randomiserDMG;
    Dictionary<string, string> pokemonMoves = new()
    {
        { "Charmander", "Ember" },
        { "Charmeleon", "Fire Fang" },
        { "Charizard", "Blast Burn" },
        { "Bulbasaur", "Vine Whip" },
        { "Ivysaur", "Razor Leaf" },
        { "Venusaur", "Solar Beam" },
        { "Squirtle", "Water Gun" },
        { "Wartortle", "Bubble Beam" },
        { "Blastoise", "Hydro Pump" },
        { "Riolu", "Force Palm" },
        { "Lucario", "Rock Smash" },
        { "Mega Lucario", "Close Combat" }
    };
    
    Dictionary<string, string> evolutionMap = new()
    {
        { "Charmander", "Charmeleon" },
        { "Charmeleon", "Charizard" },
        { "Bulbasaur", "Ivysaur" },
        { "Ivysaur", "Venusaur" },
        { "Squirtle", "Wartortle" },
        { "Wartortle", "Blastoise" },
        { "Riolu", "Lucario" },
        { "Lucario", "Mega Lucario" }
    };
    private int playerHealCount = 0;
    private int enemyHealCount = 0;
    private int enemyHealChance;
    private const int MaxHealCount = 3;
    private BattleState State;
    private bool isEnemyAttackApplied;
    private bool isPlayerAttackApplied;
    private bool gameOver;


    public AttackPhase(IPokemonSprite playerPokemon, IPokemonSprite enemyPokemon)
    {
        this.playerPokemon = playerPokemon;
        this.enemyPokemon = enemyPokemon;
        
        Random random = new Random();
        randomiserDMG = (float)(random.NextDouble() * (1.2 - 0.8) + 0.8);
        enemyHealChance = random.Next(0, 2); // Half of the time enemy is 15% HP or less the enemy will use a heal move and heal 25%
    }

    public void LoadContent(IContentLoader contentLoader, IDialogBoxQueuer dialogBoxQueuer, BattleData battleData)
    {
        this.dialogBoxQueuer = dialogBoxQueuer;
        opponentStatusBar = new OpponentPokemonStateBar();
        opponentStatusBar.LoadContent(contentLoader);
        playerStatusBar = new PlayerPokemonStateBar();
        playerStatusBar.LoadContent(contentLoader);
        GlobalBattleVariables.CurrentPlayerHp = GlobalBattleVariables.PlayerInstance.PlayerMaxHp;
        GlobalBattleVariables.CurrentOpponentHp = GlobalBattleVariables.EnemyInstance.EnemyMaxHp;
        State = BattleState.PlayerTurn;
    }

    public void Update(double gameTime)
    {
        IsAttackPhase = true;
        opponentStatusBar.Update(gameTime);
        playerStatusBar.Update(gameTime);
        
        var playerDMG = GlobalBattleVariables.PlayerInstance.CalculateDamage(randomiserDMG);
        var playerHEAL = (int)(GlobalBattleVariables.PlayerInstance.PlayerMaxHp * 0.35);
        
        var enemyHEAL = (int)(GlobalBattleVariables.EnemyInstance.EnemyMaxHp * 0.25);
        var enemyDMG = GlobalBattleVariables.EnemyInstance.CalculateDamage(randomiserDMG);
        switch (State)
            {
                case BattleState.PlayerTurn:
                    isPlayerAttackApplied = false;
                    isEnemyAttackApplied = false;
                    if (!isPlayerAttackApplied)
                    {
                        var PokemonMove =
                            pokemonMoves.GetValueOrDefault(GlobalBattleVariables.PlayerInstance.PlayerPokemonName,
                                "Tackle");
                        GlobalBattleVariables.PlayerPokemonMove = PokemonMove;
                        dialogBoxQueuer.AppendBoxIntoQueue(new DialogBoxBattleMessage(
                            $"Which move would you like to choose?", new InputKeyboard(), "MessageNavigation"));
                        dialogBoxQueuer.AppendBoxIntoQueue(new DialogBoxBattleMessage(
                            $"1. {PokemonMove}{Environment.NewLine}{Environment.NewLine}2. Heal", new InputKeyboard(),
                            "MovePick"));
                        if (!DialogBoxMessage.IsPlayerChoiceMade)
                        {
                            return;
                        }
                        if (DialogBoxMessage.IsMove1)
                        {
                            GlobalBattleVariables.CurrentOpponentHp = (int)Math.Max(0, GlobalBattleVariables.CurrentOpponentHp - playerDMG);
                            opponentStatusBar.HpBar.UpdateHp(GlobalBattleVariables.CurrentOpponentHp, 
                                GlobalBattleVariables.EnemyInstance.EnemyMaxHp);
                        }
                        else if (DialogBoxMessage.IsHeal)
                        {
                            if (playerHealCount < MaxHealCount)
                            {
                                GlobalBattleVariables.CurrentPlayerHp = Math.Min(GlobalBattleVariables.PlayerInstance.PlayerMaxHp, GlobalBattleVariables.CurrentPlayerHp + playerHEAL);
                                playerStatusBar.HpBar.UpdateHp(GlobalBattleVariables.CurrentPlayerHp, 
                                    GlobalBattleVariables.PlayerInstance.PlayerMaxHp);
                                playerHealCount++;
                            }
                            else
                            {
                                GlobalBattleVariables.CurrentOpponentHp = (int)Math.Max(0, GlobalBattleVariables.CurrentOpponentHp - playerDMG);
                                opponentStatusBar.HpBar.UpdateHp(GlobalBattleVariables.CurrentOpponentHp, GlobalBattleVariables.EnemyInstance.EnemyMaxHp);
                                dialogBoxQueuer.AppendBoxIntoQueue(new DialogBoxBattleMessage($"{GlobalBattleVariables.PlayerPokemonMove} did {playerDMG} Damage!", new InputKeyboard(), "MessageNavigation"));
                            }
                        }
                        
                        isPlayerAttackApplied = true;
                        State = GlobalBattleVariables.CurrentOpponentHp == 0 ? BattleState.GameOver : BattleState.EnemyTurn;
                        DialogBoxMessage.IsPlayerChoiceMade = false;
                    }
                    break;

                case BattleState.EnemyTurn:
                    isPlayerAttackApplied = false;
                    isEnemyAttackApplied = false;
                    if (!isEnemyAttackApplied)
                    {
                        if (GlobalBattleVariables.CurrentOpponentHp <= GlobalBattleVariables.EnemyInstance.EnemyMaxHp * 0.15 && enemyHealChance == 0)
                        {
                            if (GlobalBattleVariables.CurrentOpponentHp <= GlobalBattleVariables.EnemyInstance.EnemyMaxHp * 0.15 && enemyHealCount < MaxHealCount && enemyHealChance == 0)
                            {
                                GlobalBattleVariables.CurrentOpponentHp = Math.Min(GlobalBattleVariables.EnemyInstance.EnemyMaxHp,
                                    GlobalBattleVariables.CurrentOpponentHp + enemyHEAL);
                                opponentStatusBar.HpBar.UpdateHp(GlobalBattleVariables.CurrentOpponentHp,
                                    GlobalBattleVariables.EnemyInstance.EnemyMaxHp);
                                enemyHealCount++;
                            }
                            else
                            {
                                GlobalBattleVariables.CurrentPlayerHp =
                                    (int)Math.Max(0, GlobalBattleVariables.CurrentPlayerHp - enemyDMG);
                                playerStatusBar.HpBar.UpdateHp(GlobalBattleVariables.CurrentPlayerHp,
                                    GlobalBattleVariables.PlayerInstance.PlayerMaxHp);
                            }
                        }
                        else
                        {
                            GlobalBattleVariables.CurrentPlayerHp =
                                (int)Math.Max(0, GlobalBattleVariables.CurrentPlayerHp - enemyDMG);
                            playerStatusBar.HpBar.UpdateHp(GlobalBattleVariables.CurrentPlayerHp,
                                GlobalBattleVariables.PlayerInstance.PlayerMaxHp);
                        }
                    }
                    isEnemyAttackApplied = true;
                    State = GlobalBattleVariables.CurrentPlayerHp == 0 ? BattleState.GameOver : BattleState.PlayerTurn; 
                    break;
                case BattleState.GameOver:
                    Console.WriteLine("Game Over");
                    gameOver = true;
                    isPlayerAttackApplied = true;
                    IsDone = true;
                    if (GlobalBattleVariables.CurrentPlayerHp == 0)
                    {
                        Console.WriteLine("Enemy wins!");
                    }
                    else
                    {
                        Console.WriteLine("Player wins!");
                        Random random = new Random();
                        double randomXPMultiplier = random.NextDouble() * 0.3 + 1.5;
                        
                        GlobalBattleVariables.PlayerInstance.PlayerXp += (int)GlobalBattleVariables.PlayerInstance.CalculateXpGain(GlobalBattleVariables.EnemyInstance.EnemyLevel, randomXPMultiplier);
                        if (GlobalBattleVariables.PlayerInstance.PlayerXp >= GlobalBattleVariables.PlayerInstance.CalculateRequiredXpToLevelUp())
                        {
                            GlobalBattleVariables.PlayerInstance.PlayerLevel++;
                            GlobalBattleVariables.PlayerInstance.PlayerXp = 0;
                            GlobalBattleVariables.PlayerInstance.PlayerMaxHp += 10;
                            GlobalBattleVariables.PlayerInstance.PlayerMaxHp = GlobalBattleVariables.PlayerInstance.PlayerMaxHp;
                        }

                        updatePlayerPokemonStats();

                        if (GlobalBattleVariables.PlayerInstance.PlayerLevel == 10 && GlobalBattleVariables.PlayerInstance.Evolution == 1)
                        {
                            GlobalBattleVariables.PlayerInstance.PlayerMaxHp += 20;
                            GlobalBattleVariables.PlayerInstance.PlayerXp = 0;
                            GlobalBattleVariables.PlayerInstance.Evolution++;
                            if (evolutionMap.ContainsKey(GlobalBattleVariables.PlayerInstance.PlayerPokemonName))
                            {
                                GlobalBattleVariables.PlayerInstance.PlayerPokemonName = evolutionMap[GlobalBattleVariables.PlayerInstance.PlayerPokemonName];
                            }
                            Console.WriteLine("Pokemon has evolved!");
                        }
                        if (GlobalBattleVariables.PlayerInstance.PlayerLevel == 20 && GlobalBattleVariables.PlayerInstance.Evolution == 2)
                        {
                            GlobalBattleVariables.PlayerInstance.PlayerMaxHp += 40;
                            GlobalBattleVariables.PlayerInstance.PlayerXp = 0;
                            GlobalBattleVariables.PlayerInstance.Evolution++;
                            if (evolutionMap.ContainsKey(GlobalBattleVariables.PlayerInstance.PlayerPokemonName))
                            {
                                GlobalBattleVariables.PlayerInstance.PlayerPokemonName = evolutionMap[GlobalBattleVariables.PlayerInstance.PlayerPokemonName];
                            }
                            Console.WriteLine("Pokemon has evolved!");
                        }
                        updatePlayerPokemonStats();

                    }

                    GlobalBattleVariables.PlayerAction = "Run";
                    return;
            }
    }

    private async Task updatePlayerPokemonStats()
    {
        int playerPokemonID = GlobalBattleVariables.PlayerInstance.PlayerPokemonId;
        int playerXP = GlobalBattleVariables.PlayerInstance.PlayerXp;
        int playerLevel = GlobalBattleVariables.PlayerInstance.PlayerLevel;
        int playerHP = GlobalBattleVariables.PlayerInstance.PlayerMaxHp;
        int evolution = GlobalBattleVariables.PlayerInstance.Evolution;
        string playerPokemonName = GlobalBattleVariables.PlayerInstance.PlayerPokemonName;

        bool updateSuccess = await PokemonApiRepository.UpdatePlayerPokemonStats(playerPokemonID, playerXP, playerLevel, playerHP, evolution, playerPokemonName);
        if (updateSuccess)
        {
            Console.WriteLine("Player Pokemon stats updated successfully!");
        }
        else
        {
            Console.WriteLine("Failed to update player Pokemon stats.");
        }
        
    }

    public IPhase GetNextPhase()
    {

        return null;
    }

    public void Draw(SpriteBatch spriteBatch, IContentLoader contentLoader)
    {
        playerPokemon.Draw(spriteBatch);
        enemyPokemon.Draw(spriteBatch);
        opponentStatusBar.Draw(spriteBatch);
        playerStatusBar.Draw(spriteBatch);
    }
}
