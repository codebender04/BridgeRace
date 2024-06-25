using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField] private GameObject[] levelArray;
    [SerializeField] private Player player;
    private GameObject currentLevelPrefab;
    private Level currentLevel;
    private int levelIndex = 0;
    public void LoadLevel()
    {
        DestroyLevel();
        currentLevelPrefab = Instantiate(levelArray[levelIndex]);
        currentLevel = currentLevelPrefab.GetComponent<Level>();
        player.Initialize();
        GameManager.Instance.RandomizeBotColor();
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
    public List<Bot> GetBotListInCurrentLevel()
    {
        return currentLevel.GetBotList();
    }
    private void DestroyLevel()
    {
        SimplePool.CollectAll();
        if (currentLevelPrefab != null)
        {
            DestroyImmediate(currentLevelPrefab);
        }
    }
}

