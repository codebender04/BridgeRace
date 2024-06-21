using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class Step : MonoBehaviour
{
    [SerializeField] private ColorSO colorSO;
    [SerializeField] private BoxCollider wall;
    [SerializeField] private BoxCollider box;
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private LayerMask playerLayer;
    private ColorType color;
    private Vector3 halfExtents;
    public ColorType Color { get { return color; } }
    private void Awake()
    {
        halfExtents = box.bounds.extents;
    }
    private void Update()
    {
        if (Physics.BoxCast(transform.position, halfExtents, -Vector3.forward, out RaycastHit hitInfo, Quaternion.identity, 1f, playerLayer))
        {
            Player player = hitInfo.collider.GetComponent<Player>();

            if (color != player.Color && player.BrickCount == 0)
            {
                ActivateWall();
            }
            else DeactivateWall();
        }
    }
    public void Show(ColorType color)
    {
        meshRenderer.enabled = true;
        ChangeColor(color);
    }
    private void ChangeColor(ColorType color)
    {
        this.color = color;
        meshRenderer.material = colorSO.GetMaterial(color);
    }
    private void ActivateWall()
    {
        wall.enabled = true;
    }
    private void DeactivateWall()
    {
        wall.enabled = false;
    }
}
