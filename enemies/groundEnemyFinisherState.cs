using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// groundEnemyFinisherState es una clase que representa el estado del último golpe de un combo de un enemigo.
/// </summary>
public class groundEnemyFinisherState : enemyBaseCombatState
{
    /// <summary>
    /// Constructor con parámetros de la clase que asigna un tiempo y contador.
    /// </summary>
    /// <param name="time">El tiempo asignado a <see cref="enemyBaseCombatState._attackTime"/>.</param>
    /// <param name="counter">El contador asignado a <see cref="enemyBaseCombatState._attackCounter"/></param>
    public groundEnemyFinisherState(float time, int counter) : base(time, counter) { }

    /// <summary>
    /// Método que termina de implementar <see cref="enemyBaseCombatState.onEnter(enemyStateMachine)"/>.
    /// </summary>
    /// <param name="_stateMachine">La máquina de estados actual.</param>
    public override void onEnter(enemyStateMachine _stateMachine)
    {
        base.onEnter(_stateMachine);
        Debug.Log("finisher" + getAttackCounter().ToString());
        GetComponent<enemy>().getHitbox().GetComponent<BoxCollider2D>().enabled = true;
        //_stateMachine.getAnimator().SetTrigger("Attack" + getAttackCounter().ToString());
    }

    /// <summary>
    /// Método que termina de implementar <see cref="enemyBaseCombatState.onUpdate()"/>.
    /// Si estamos fuera del rango de ataque cambia de estado, si no se queda quieto. Ver <see cref="enemyChaseState"/> y <see cref="idleEnemyState"/> para más información.
    /// </summary>
    public override void onUpdate()
    {
        base.onUpdate();
        if (_time >= _attackTime)
        {
            //No estamos en rango de ataque

            if (Vector3.Distance(_currentStateMachine.GetComponent<enemy>().transform.position, config.getPlayer().transform.position) > GetComponent<enemy>().getAttackRange())
            {
                GetComponent<enemy>().getHitbox().GetComponent<BoxCollider2D>().enabled = false;
                if (GetComponent<boss>() != null)
                {
                    _currentStateMachine.setNextState(new enemyChaseState());
                }
                else
                {
                    _currentStateMachine.setNextState(new idleEnemyState());
                }
            }
            else //Estamos en rango de ataque
            {
                GetComponent<enemy>().getHitbox().GetComponent<BoxCollider2D>().enabled = false;
                _currentStateMachine.setNextState(new idleEnemyState());
            }
        }
        
    }
    
}
