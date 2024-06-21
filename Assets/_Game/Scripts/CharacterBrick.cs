using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBrick : MonoBehaviour
{
    [SerializeField] private Transform tf;
    [SerializeField] private ColorSO colorSO;
    [SerializeField] private MeshRenderer meshRenderer;

    private const float BRICK_HEIGHT = 0.4f;
    private const float distanceBetweenBricks = 0.1f;
    public void Initialize(Character character)
    {
        Vector3 offset = new Vector3(0f, character.BrickCount * (BRICK_HEIGHT + distanceBetweenBricks), 0f);
        tf.SetPositionAndRotation(character.brickHoldPoint.position + offset, character.brickHoldPoint.rotation);
        tf.SetParent(character.transform);

        ChangeColor(character.Color);
    }
    public void Despawn()
    {
        Destroy(gameObject);
    }
    private void ChangeColor(ColorType color)
    {
        meshRenderer.material = colorSO.GetMaterial(color);
    }
}
