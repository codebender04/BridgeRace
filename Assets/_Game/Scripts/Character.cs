using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private CharacterBrick brickPrefab;
    [SerializeField] private Animator animator;
    [SerializeField] protected ColorSO colorSO;
    [SerializeField] protected Renderer meshRenderer;
    protected Stage currentStage;
    public ColorType Color => color;
    public List<CharacterBrick> BrickList { get { return brickList; } }
    public Transform brickHoldPoint;

    protected ColorType color;
    private List<CharacterBrick> brickList = new List<CharacterBrick>();
    private string currentAnimName;
    private void OnEnable()
    {
        GameManager.OnGameStateChanged += GameManager_OnGameStateChanged;
    }

    protected virtual void GameManager_OnGameStateChanged(GameState newState)
    {
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
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
                currentStage?.RemoveBrick(levelBrick);
                levelBrick.OnDespawn();
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
    }
    protected void ChangeAnimation(string animName)
    {
        if (currentAnimName != animName)
        {
            animator.ResetTrigger(animName);
            currentAnimName = animName;
            animator.SetTrigger(animName);
        }
    }
    protected void ChangeColor(ColorType color)
    {
        this.color = color;
        meshRenderer.material = colorSO.GetMaterial(this.color);
    }
    private void AddBrick()
    {
        CharacterBrick brick = Instantiate(brickPrefab.gameObject, brickHoldPoint.position, Quaternion.identity).GetComponent<CharacterBrick>();
        brick.Initialize(this);
        brickList.Add(brick);
    }
    private void RemoveBrick()
    {
        brickList[^1].Despawn();
        brickList.Remove(brickList[^1]);
    }
}
