using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField] private GameObject[] levelArray;
    [SerializeField] private Player player;
    private GameObject currentLevel;
    private int levelIndex = 0;
    private Transform startingPosition;
    public void LoadLevel()
    {
        DestroyLevel();
        currentLevel = Instantiate(levelArray[levelIndex]);
        player.Initialize();
        GameManager.Instance.RandomizeCharacterColor();
    }
    public void LoadNextLevel()
    {
        levelIndex++;
        if (levelIndex >= levelArray.Length)
        {
            levelIndex = 0;
        }
        LoadLevel();
    }
    private void DestroyLevel()
    {
        if (currentLevel != null)
        {
            Destroy(currentLevel);
        }
    }
}

