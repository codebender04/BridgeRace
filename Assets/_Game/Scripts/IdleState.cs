using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState
{
    public void Enter(Bot bot)
    {
    }

    public void Execute(Bot bot)
    {
        bot.StopMovement();
    }

    public void Exit(Bot bot)
    {
    }
}
