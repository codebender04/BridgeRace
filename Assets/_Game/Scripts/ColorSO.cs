using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ColorType
{
    None = 0,
    Red = 1,
    Blue = 2,
    Green = 3,
    Orange = 4,
}
[CreateAssetMenu(menuName = "ColorData")]
public class ColorSO : ScriptableObject
{
    [SerializeField] Material[] materialArray;
    public Material GetMaterial(ColorType color)
    {
        return materialArray[(int)color];
    }
}
