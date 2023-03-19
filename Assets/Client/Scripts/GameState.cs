using UnityEngine;
using Client;
using System;

public class GameState
{
    private static GameState _gameState = null;

    #region Configs
    public GameSettings GameSettings { private set; get; }
    public ObstaclesConfig ObstaclesConfig { private set; get; }
    public AirBallonConfig AirBallonConfig { private set; get; }
    #endregion

    #region Pools
    public AllPools SharedPools;
    public AllPools ActivePools;
    #endregion

    #region LevelParts
    public LevelConfig LevelConfig { private set; get; }
    public LevelPart ActualLevelPart;
    public int ActualLevelPartIndex = 0;
    #endregion

    #region Saved Entities
    public const int NULL_ENTITY = -1;
    #endregion

    #region Events
    public static Action OnGameOvering;
    #endregion

    #region Systems
    public int FlyingHeight;
    public int SpawnedObstacles;
    public bool IsPlayingGame = true;
    #endregion

    private GameState(in EcsStartup ecsStartup)
    {
        SharedPools = ecsStartup.AllPools;

        GameSettings = ecsStartup.GameSettings;
        LevelConfig = ecsStartup.LevelConfig;
        ObstaclesConfig = ecsStartup.ObstaclesConfig;
        AirBallonConfig = ecsStartup.AirBallonConfig;

        ActualLevelPart = LevelConfig.LevelParts[ActualLevelPartIndex];
    }

    public static GameState InitializeNew(in EcsStartup ecsStartup)
    {
        
        _gameState = new GameState(in ecsStartup);

        return _gameState;
    }

    public static GameState Get()
    {
        return _gameState;
    }

    public void GameOver()
    {
        IsPlayingGame = false;
        Debug.Log("GameOver");
        OnGameOvering?.Invoke();
    }
}
