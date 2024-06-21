using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : IState
{
    private int targetBrickAmount;
    private int pickedUpBricks;
    
    public void Enter(Bot bot)
    {
        pickedUpBricks = 0;
        targetBrickAmount = Random.Range(2, 4);
        bot.MoveToBrickPosition();
    }

    public void Execute(Bot bot)
    {
        if (bot.HasBricksToPickUp() && bot.IsAtDestination())
        {
            pickedUpBricks++;
            bot.MoveToBrickPosition();
        }
        if (pickedUpBricks == targetBrickAmount || !bot.HasBricksToPickUp())
        {
            bot.ChangeState(new BuildState());
        }
    }

    public void Exit(Bot bot)
    {
    }
}
