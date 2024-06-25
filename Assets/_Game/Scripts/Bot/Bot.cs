using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.AI;

public class Bot : Character
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Stage[] stageArray;
    private Vector3 destination;
    private List<LevelBrick> targetBricksList = new List<LevelBrick>();
    private IState currentState;
    private void Start()
    {
        ChangeState(new IdleState());
        GameManager.OnGameStateChanged += GameManager_OnGameStateChanged;
    }

    private void Update()
    {
        currentState?.Execute(this);
    }
    private void GameManager_OnGameStateChanged(GameState newState)
    {
        if (newState == GameState.Pausing)
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
            SetDestination(GetRandomBrickPosition());
            ChangeAnimation(Constants.ANIM_RUN);
        }
    }
    public void SetDestination(Vector3 destination)
    {
        this.destination = destination;
        agent.SetDestination(destination);
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
        {
            return transform.position;
        }
        else
        {
            return targetBricksList[randomIndex].transform.position;
        }
    }
    public void GoToNextStage()
    {
        for (int i = 0; i < stageArray.Length - 1; i++)
        {
            if (currentStage == stageArray[i])
            {
                SetDestination(stageArray[i + 1].GetRandomPointInBounds());
                break;
            }
        }
    }
    public void StopMovement()
    {
        SetDestination(transform.position);
        ChangeAnimation(Constants.ANIM_IDLE);
    }
    public void ChangeState(IState newState)
    {
        currentState?.Exit(this);
        currentState = newState;
        currentState?.Enter(this);
    }
}
