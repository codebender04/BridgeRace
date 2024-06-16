using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.AI;

public class Bot : Character
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Stage[] stageArray;
    private int currentStageIndex = 0;
    private Vector3 destination;
    private List<LevelBrick> targetBricksList = new List<LevelBrick>();
    private IState currentState;
    private Stair currentStair;
    private void Start()
    {
        ChangeState(new IdleState());
        ChangeColor(ColorType.Red);
    }
    private void Update()
    {
        currentState?.Execute(this);
    }
    protected override void GameManager_OnGameStateChanged(GameState newState)
    {
        if (newState == GameState.Menu)
        {
            ChangeState(new IdleState());
        }
        else if (newState == GameState.Playing)
        {
            ChangeState(new PatrolState());
        }
    }
    public bool IsAtDestination()
    {
        return Vector3.Distance(transform.position, destination + (transform.position.y - destination.y) * Vector3.up) < 0.4f;
    }
    public void MoveToBrickPosition()
    {
        if (currentStage != null)
        {
            destination = GetRandomBrickPosition();
        }
        agent.SetDestination(destination);
        ChangeAnimation(Constants.ANIM_RUN);
    }
    public bool HasBricksToPickUp()
    {
        targetBricksList = currentStage.GetBricksWithSameColor(color);

        return targetBricksList.Count > 0;
    }
    private Vector3 GetRandomBrickPosition()
    {
        targetBricksList = currentStage.GetBricksWithSameColor(color);
        int randomIndex = Random.Range(0, targetBricksList.Count);

        if (targetBricksList.Count == 0) 
        return transform.position;
        else 
        return targetBricksList[randomIndex].transform.position;
    }
    public void GoToNextStage()
    {
        agent.SetDestination(stageArray[1].GetRandomPointInBounds());
    }
    public void StopMovement()
    {
        agent.SetDestination(transform.position);
        ChangeAnimation(Constants.ANIM_IDLE);
    }
    public void ChangeState(IState newState)
    {
        currentState?.Exit(this);
        currentState = newState;
        currentState?.Enter(this);
    }
}
