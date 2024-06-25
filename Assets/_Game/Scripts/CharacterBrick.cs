using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterBrick : GameUnit
{
    [SerializeField] private ColorSO colorSO;
    [SerializeField] private MeshRenderer meshRenderer;

    private const float BRICK_HEIGHT = 0.4f;
    private const float distanceBetweenBricks = 0.1f;
    public void Initialize(Character character)
    {
        Vector3 offset = new Vector3(0f, character.BrickCount * (BRICK_HEIGHT + distanceBetweenBricks), 0f);
        transform.position = new Vector3(transform.position.x, character.brickHoldPoint.transform.position.y, transform.position.z) + offset;    
        transform.SetParent(character.transform);

        ChangeColor(character.Color);
    }
    public void Despawn()
    {
        SimplePool.Despawn(this);
    }
    private void ChangeColor(ColorType color)
    {
        meshRenderer.material = colorSO.GetMaterial(color);
    }
}
