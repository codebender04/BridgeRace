using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;
public enum GameState { Pausing, Playing }

public class GameManager : Singleton<GameManager>
{
    public GameState GameState;
    public static event Action<GameState> OnGameStateChanged;
    private List<Bot> botList = new List<Bot>();
    private List<ColorType> availableColors;

    private void Awake()
    {
        ResetColor();
    }
    private void Start()
    {
        ChangeGameState(GameState.Pausing);
        UIManager.Instance.Open<CanvasMainMenu>();
    }
    public void ChangeGameState (GameState newState)
    {
        GameState = newState;
        OnGameStateChanged?.Invoke(newState);
    }
    public void RandomizeBotColor()
    {
        botList = LevelManager.Instance.GetBotListInCurrentLevel();
        foreach (Bot bot in botList) 
        {
            int randomIndex = Random.Range(0, availableColors.Count);
            bot.ChangeColor(availableColors[randomIndex]);
            availableColors.RemoveAt(randomIndex);
        }
        ResetColor();
    }
    private void ResetColor()
    {
        availableColors = new List<ColorType> { ColorType.Red, ColorType.Green, ColorType.Orange, ColorType.Yellow };
    }

}
