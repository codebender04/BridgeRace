using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;
public enum GameState { Pausing, Playing }

public class GameManager : Singleton<GameManager>
{
    private Character[] characterArray = new Character[5];
    private HashSet<ColorType> characterColorHashSet = new HashSet<ColorType>();
    public GameState GameState;
    public static event Action<GameState> OnGameStateChanged;
    
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
    public void RandomizeCharacterColor()
    {
        //characterArray = FindObjectsOfType<Character>();
        //ColorType color;
        //if (characterArray.Length > Enum.GetNames(typeof(ColorType)).Length - 1) return;
        //foreach (Character character in characterArray)
        //{
        //    do
        //    {
        //        color = (ColorType)Random.Range(1, Enum.GetNames(typeof(ColorType)).Length);
        //    }
        //    while (characterColorHashSet.Contains(color));
        //    characterColorHashSet.Add(color);

        //    character.ChangeColor(color);
        //}
    }

}
