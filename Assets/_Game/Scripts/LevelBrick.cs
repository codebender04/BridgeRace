using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBrick : MonoBehaviour
{
    [SerializeField] private Renderer meshRenderer;
    [SerializeField] private ColorSO colorSO;
    private ColorType color;
    public ColorType Color => color;
    private void Awake()
    {
        float chance = Random.value;
        if (chance < 0.2f)
        {
            color = ColorType.None;
        } 
        else if (chance < 0.4f)
        {
            color = ColorType.Red;
        }
        else if (chance < 0.6f)
        {
            color = ColorType.Blue;
        }
        else if (chance < 0.8f)
        {
            color = ColorType.Green;
        }
        else if (chance < 1f)
        {
            color = ColorType.Orange;
        }
        ChangeColor(color);
    }
    public void OnDespawn()
    {
        gameObject.SetActive(false);
    }
    public void ChangeColor(ColorType color)
    {
        this.color = color;
        meshRenderer.material = colorSO.GetMaterial(this.color);
    }
}
