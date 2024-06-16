using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour
{
    [SerializeField] private List<LevelBrick> levelBrickList = new List<LevelBrick>();
    private Bounds bounds;
    private void Awake()
    {
        bounds = GetComponent<BoxCollider>().bounds;
    }
    public List<LevelBrick> GetBricksWithSameColor(ColorType color)
    {
        List<LevelBrick> result = new List<LevelBrick>();
        foreach (LevelBrick levelBrick in levelBrickList)
        {
            if (levelBrick.Color == color || levelBrick.Color == ColorType.None)
            {
                result.Add(levelBrick);
            }
        }
        return result;
    }
    public void RemoveBrick(LevelBrick brickToRemove)
    {
        levelBrickList.Remove(brickToRemove);
    }
    public Vector3 GetRandomPointInBounds()
    {
        return new Vector3(
            Random.Range(bounds.min.x, bounds.max.x),
            Random.Range(bounds.min.y, bounds.max.y),
            Random.Range(bounds.min.z, bounds.max.z)
        );
    }
}
