using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public static class Utilities 
{
    //Generic
    //Extension
    public static T Last<T>(this List<T> list) where T : class
    {
        return list[^1];
    }
}
