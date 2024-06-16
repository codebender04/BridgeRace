using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum GameState { Menu, Playing }

public class GameManager : Singleton<GameManager>
{
    public GameState GameState;
    public static event Action<GameState> OnGameStateChanged;
    private void Start()
    {
        ChangeGameState(GameState.Menu);
        UIManager.Instance.Open<CanvasMainMenu>();
    }
    public void ChangeGameState (GameState newState)
    {
        GameState = newState;
        OnGameStateChanged?.Invoke(newState);
    }
}
