using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class LevelBrick : GameUnit
{
    [SerializeField] private Renderer meshRenderer;
    [SerializeField] private ColorSO colorSO;
    [SerializeField] private BoxCollider boxCollider;
    private ColorType color;
    public ColorType Color => color;
    private void OnEnable()
    {
        ColorType color = (ColorType)Random.Range(0, Enum.GetNames(typeof(ColorType)).Length);
        ChangeColor(color);
    }
    public void OnDespawn()
    {
        SimplePool.Despawn(this);    
        //gameObject.SetActive(false);
        Invoke(nameof(Respawn), 8f);
    }
    public void ChangeColor(ColorType color)
    {
        this.color = color;
        meshRenderer.material = colorSO.GetMaterial(this.color);
    }
    private void Respawn()
    {
        SimplePool.Spawn<LevelBrick>(PoolType.LevelBrick, transform.position, Quaternion.identity);
        //gameObject.SetActive(true);
    }
}
