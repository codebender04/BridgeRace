using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Cache
{
    private static Dictionary<Collider, object> objectDict = new Dictionary<Collider, object>();
    public static Stage GetStageComponent(Collider collider)
    {
        if (!objectDict.ContainsKey(collider))
        {
            Stage stage = collider.GetComponent<Stage>();
            objectDict.Add(collider, stage);
        }
        return objectDict[collider] as Stage;
    }
    public static Step GetStepComponent(Collider collider)
    {
        if (!objectDict.ContainsKey(collider))
        {
            Step step = collider.GetComponent<Step>();
            objectDict.Add(collider, step);
        }
        return objectDict[collider] as Step;
    }
    public static LevelBrick GetLevelBrickComponent(Collider collider)
    {
        if (!objectDict.ContainsKey(collider))
        {
            LevelBrick levelBrick = collider.GetComponent<LevelBrick>();
            objectDict.Add(collider, levelBrick);
        }
        return objectDict[collider] as LevelBrick;
    }
}
