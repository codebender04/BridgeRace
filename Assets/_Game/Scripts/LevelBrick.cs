using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class LevelBrick : MonoBehaviour
{
    [SerializeField] private Renderer meshRenderer;
    [SerializeField] private ColorSO colorSO;
    [SerializeField] private BoxCollider boxCollider;
    private ColorType color;
    public ColorType Color => color;
    private void Awake()
    {
        ColorType color = (ColorType)Random.Range(0, Enum.GetNames(typeof(ColorType)).Length);
        ChangeColor(color);
    }
    public void OnDespawn()
    {
        gameObject.SetActive(false);
        Invoke(nameof(ActivateSelf), 8f);
    }
    public void ChangeColor(ColorType color)
    {
        this.color = color;
        meshRenderer.material = colorSO.GetMaterial(this.color);
    }
    private void ActivateSelf()
    {
        gameObject.SetActive(true);
    }
}
