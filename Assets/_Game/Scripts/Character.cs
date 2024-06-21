using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
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


    private void OnEnable()
    {
        GameManager.OnGameStateChanged += GameManager_OnGameStateChanged;
        ChangeColor((ColorType)Random.Range(0, Enum.GetNames(typeof(ColorType)).Length));
    }

    protected virtual void GameManager_OnGameStateChanged(GameState newState)
    {
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        //TODO: Getcomponent + optimize cache
        if (other.TryGetComponent<Stage>(out Stage stage) && stage != currentStage)
        {
            currentStage = stage;
            if (this is Bot bot && GameManager.Instance.GameState == GameState.Playing)
            {
                bot.ChangeState(new PatrolState());
            }
        }
        if (other.TryGetComponent<LevelBrick>(out LevelBrick levelBrick))
        {
            if (levelBrick.Color == color || levelBrick.Color == ColorType.None)
            {
                AddBrick();
                if (currentStage != null)
                {
                    currentStage.StartCoroutine(currentStage.RemoveBrick(levelBrick));
                }
            }
        }
        if (other.TryGetComponent<Step>(out Step step))
        {
            if (brickList.Count > 0 && step.Color != color)
            {
                RemoveBrick();
                step.Show(color);
            }
        }
        if (other.TryGetComponent<WinStage>(out WinStage winStage))
        {
            ChangeAnimation(Constants.ANIM_DANCE);
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
        //TODO: cache getcomponent
        CharacterBrick brick = Instantiate(brickPrefab, brickHoldPoint.position, Quaternion.identity);
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
}
