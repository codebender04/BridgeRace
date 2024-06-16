using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class BuildState : IState
{
    public void Enter(Bot bot)
    {
        bot.GoToNextStage();
    }

    public void Execute(Bot bot)
    {
        if (bot.BrickList.Count == 0)
        {
            bot.ChangeState(new PatrolState());
        }
    }

    public void Exit(Bot bot)
    {
        bot.StopMovement();
    }
}
