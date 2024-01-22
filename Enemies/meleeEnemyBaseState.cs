using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class meleeEnemyBaseState : enemyState
{
    protected float _attackTime;
    protected int _attackCounter;

    public meleeEnemyBaseState(float time, int counter)
    {
        _attackTime = time;
        _attackCounter = counter;
    }

    protected float getAttackTime()
    {
        return _attackTime;
    }
    protected int getAttackCounter()
    {
        return _attackCounter;
    }

    public override void OnEnter(enemyStateMachine _stateMachine)
    {
        base.OnEnter(_stateMachine);
    }
    public override void OnExit()
    {
        base.OnExit();
    }
    public override void OnUpdate()
    {
        base.OnUpdate();
    }
    public override void OnFixedUpdate()
    {
        base.OnFixedUpdate();
    }
    public override void OnLateUpdate()
    {
        base.OnLateUpdate();
    }
}
