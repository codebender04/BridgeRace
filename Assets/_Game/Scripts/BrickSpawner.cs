using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class BrickSpawner : MonoBehaviour
{
    [SerializeField] private Stage stage;
    [SerializeField] private LevelBrick levelBrick;
    [SerializeField] private int numberOfRows;
    [SerializeField] private int numberOfColumns;
    [SerializeField] private float paddingLength = 1f;
    [SerializeField] private float paddingWidth = 1f;

    private void Start()
    {
        SpawnAll();
    }
    public void SpawnAll()
    {
        if (numberOfColumns <= 1 || numberOfRows <= 1) return;

        Vector3 planeScale = transform.localScale;
        float planeWidth = planeScale.x * 10; 
        float planeLength = planeScale.z * 10;

        float availablePlaneWidth = planeWidth - paddingWidth * 2;
        float availablePlaneLength = planeLength - paddingLength * 2;

        float xSpacing = availablePlaneWidth / (numberOfColumns - 1);
        float zSpacing = availablePlaneLength / (numberOfRows - 1);

        Vector3 startPosition = transform.position - new Vector3(availablePlaneWidth / 2, 0, availablePlaneLength / 2);

        for (int row = 0; row < numberOfRows; row++)
        {
            for (int col = 0; col < numberOfColumns; col++)
            {
                float xPos = startPosition.x + col * xSpacing;
                float zPos = startPosition.z + row * zSpacing;
                Vector3 spawnPosition = new Vector3(xPos, transform.position.y, zPos);

                Spawn(spawnPosition);
            }
        }
    }
    private void Spawn(Vector3 spawnPosition)
    {
        //LevelBrick brick = Instantiate(levelBrick, spawnPosition, Quaternion.identity);
        LevelBrick brick = SimplePool.Spawn<LevelBrick>(PoolType.LevelBrick, spawnPosition, Quaternion.identity);
        stage.AddBrick(brick);
    }
}
