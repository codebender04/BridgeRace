using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBrick : MonoBehaviour
{
    [SerializeField] private ColorSO colorSO;
    [SerializeField] private MeshRenderer meshRenderer;

    private const float BRICK_HEIGHT = 0.4f;
    private const float distanceBetweenBricks = 0.1f;
    private ColorType color; 
    public void Initialize(Character character)
    {
        Vector3 offset = new Vector3(0f, character.BrickList.Count * (BRICK_HEIGHT + distanceBetweenBricks), 0f);
        transform.SetPositionAndRotation(character.brickHoldPoint.position + offset, character.brickHoldPoint.rotation);
        transform.SetParent(character.transform);
        
        color = character.Color;
        meshRenderer.material = colorSO.GetMaterial(color);
    }
    public void Despawn()
    {
        Destroy(gameObject);
    }
}
