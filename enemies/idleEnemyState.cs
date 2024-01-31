using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// idleEnemyState es una clase que representa un estado del enemigo en el que no hace nada.
/// </summary>
public class idleEnemyState : enemyState
{

    /// <summary>
    /// M�todo que implementa <see cref="enemyState.onEnter(enemyStateMachine)"/>.
    /// </summary>
    /// <param name="_stateMachine">La m�quina de estados actual.</param>
    public override void onEnter(enemyStateMachine _stateMachine)
    {
        base.onEnter(_stateMachine);
        //_stateMachine.getAnimator().SetTrigger("Idle");

    }
}