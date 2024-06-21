using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;

public class Stage : MonoBehaviour
{
    [SerializeField] private Transform tf;
    private List<LevelBrick> levelBrickList = new List<LevelBrick>();
    private Bounds bounds;
    private float timer = 0;
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
    public IEnumerator RemoveBrick(LevelBrick brickToRemove)
    {
        levelBrickList.Remove(brickToRemove);
        brickToRemove.OnDespawn();
        yield return new WaitForSeconds(8f);
        AddBrick(brickToRemove);
    }
    public void AddBrick(LevelBrick brickToAdd)
    {
        levelBrickList.Add(brickToAdd);
        brickToAdd.transform.SetParent(tf);
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
