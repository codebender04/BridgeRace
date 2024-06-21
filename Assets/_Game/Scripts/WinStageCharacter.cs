using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;

public class WinStageCharacter : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private SkinnedMeshRenderer meshRenderer;
    [SerializeField] private ColorSO colorSO;
    private void Awake()
    {
        Hide();
    }
    public void Show(ColorType color)
    {
        meshRenderer.enabled = true;    
        meshRenderer.material = colorSO.GetMaterial(color);
        animator.SetTrigger(Constants.ANIM_DANCE);
    }
    private void Hide()
    {
        meshRenderer.enabled = false;
    }
}
