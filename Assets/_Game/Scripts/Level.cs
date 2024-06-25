using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] private List<Bot> botList;
    public List<Bot> GetBotList()
    {
        return botList;
    }
}
