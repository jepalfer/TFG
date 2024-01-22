using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class idleEnemyState : enemyState
{

    public override void OnEnter(enemyStateMachine _stateMachine)
    {
        base.OnEnter(_stateMachine);
        //_stateMachine.getAnimator().SetTrigger("Idle");

    }
}