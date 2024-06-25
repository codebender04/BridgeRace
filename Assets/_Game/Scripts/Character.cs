using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.Timeline;
using Random = UnityEngine.Random;

public class Character : MonoBehaviour
{
    [SerializeField] private CharacterBrick brickPrefab;
    [SerializeField] private Animator animator;
    [SerializeField] protected ColorSO colorSO;
    [SerializeField] protected Renderer meshRenderer;
    public int BrickCount => brickList.Count;
    public ColorType Color => color;
    public Transform brickHoldPoint;

    protected Stage currentStage;
    protected ColorType color;
    private List<CharacterBrick> brickList = new List<CharacterBrick>();
    private string currentAnimName = Constants.ANIM_IDLE;
    private LevelBrick levelBrick;
    private Step step;
    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constants.TAG_STAGE))
        {
            Stage stage = Cache.GetStageComponent(other);
            if (stage != null && stage != currentStage)
            {
                currentStage = stage;
            }
            if (this is Bot bot && GameManager.Instance.GameState == GameState.Playing)
            {
                bot.ChangeState(new PatrolState());
            }
        }
        if (other.CompareTag(Constants.TAG_LEVELBRICK))
        {
            levelBrick = Cache.GetLevelBrickComponent(other);
            if (levelBrick.Color == color || levelBrick.Color == ColorType.None)
            {
                AddBrick();
                if (currentStage != null)
                {
                    currentStage.StartCoroutine(currentStage.RemoveBrick(levelBrick));
                }
            }
        }
        if (other.CompareTag(Constants.TAG_STEP))
        {
            step = Cache.GetStepComponent(other);
            if (brickList.Count > 0 && step.Color != color)
            {
                RemoveBrick();
                step.Show(color);
            }
        }
    }
    protected void ChangeAnimation(string animName)
    {
        if (currentAnimName != animName)
        {
            animator.ResetTrigger(currentAnimName);
            currentAnimName = animName;
            animator.SetTrigger(currentAnimName);
        }
    }
    public void ChangeColor(ColorType color)
    {
        this.color = color;
        meshRenderer.material = colorSO.GetMaterial(this.color);
    }
    private void AddBrick()
    {
        CharacterBrick brick = SimplePool.Spawn<CharacterBrick>(brickPrefab.PoolType, brickHoldPoint.position, brickHoldPoint.rotation);
        brick.Initialize(this);
        brickList.Add(brick);
    }
    private void RemoveBrick()
    {
        brickList.Last().Despawn();
        brickList.Remove(brickList.Last());
    }
    protected void ClearBrick()
    {
        for (int i = brickList.Count - 1; i >= 0; i--)
        {
            brickList[i].Despawn(); 
        }
        brickList.Clear();
    }
    public void Hide()
    {
        meshRenderer.enabled = false;
        ClearBrick();
    }
    private void OnDestroy()
    {
        //for (int i = brickList.Count - 1; i >= 0; i--)
        //{
        //    brickList[i].transform.SetParent(FindObjectOfType<PoolControl>().transform);
        //    brickList[i].Despawn();
        //}
    }
    
}
