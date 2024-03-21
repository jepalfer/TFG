using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// groundEnemyComboState es una clase que representa el estado en medio de un combo de un enemigo.
/// </summary>
public class groundEnemyComboState : enemyBaseState
{
    /// <summary>
    /// Constructor con parámetros de la clase que asigna un tiempo y contador.
    /// </summary>
    /// <param name="time">El tiempo asignado a <see cref="enemyBaseState._attackTime"/>.</param>
    /// <param name="counter">El contador asignado a <see cref="enemyBaseState._attackCounter"/></param>
    public groundEnemyComboState(float time, int counter) : base(time, counter) { }

    /// <summary>
    /// Método que termina de implementar <see cref="enemyBaseState.onEnter(enemyStateMachine)"/>.
    /// </summary>
    /// <param name="_stateMachine">La máquina de estados actual.</param>
    public override void onEnter(enemyStateMachine _stateMachine)
    {
        base.onEnter(_stateMachine);
        Debug.Log("combo" + getAttackCounter().ToString());
        GetComponent<enemy>().getHitbox().GetComponent<BoxCollider2D>().enabled = true;
        //_stateMachine.getAnimator().SetTrigger("Attack" + getAttackCounter().ToString());

    }

    
    /// <summary>
    /// Método que termina de implementar <see cref="enemyBaseState.onUpdate()"/>.
    /// Si estamos lejos del rango de ataque cambia de estado, si estamos en el rango de ataque comprueba para saber si el combo continúa o debe acabar, y si no estamos al a vista se queda quieto.
    /// Ver <see cref="enemyChaseState"/>, <see cref="idleEnemyState"/>, <see cref="groundEnemyComboState"/> y <see cref="groundEnemyFinisherState"/> para más información.
    /// </summary>
    public override void onUpdate()
    {
        base.onUpdate();
        if (_time >= _attackTime)
        {


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
            else if (_attackCounter == (_currentStateMachine.GetComponent<enemy>().getTimes().Count - 1))    //Ya ha acabado
            {
                GetComponent<enemy>().getHitbox().GetComponent<BoxCollider2D>().enabled = false;
                _currentStateMachine.setNextState(new idleEnemyState());
            } //El siguiente ataque es el último
            else if (_attackCounter == (_currentStateMachine.GetComponent<enemy>().getTimes().Count - 2))   //Es finisher
            {
                GetComponent<enemy>().getHitbox().GetComponent<BoxCollider2D>().enabled = false;
                _currentStateMachine.setNextState(new groundEnemyFinisherState(_currentStateMachine.GetComponent<enemy>().getTimes()[_attackCounter + 1], _attackCounter + 1));
            }
            else //Seguimos en combo
            {
                GetComponent<enemy>().getHitbox().GetComponent<BoxCollider2D>().enabled = false;
                _currentStateMachine.setNextState(new groundEnemyComboState(_currentStateMachine.GetComponent<enemy>().getTimes()[_attackCounter + 1], _attackCounter + 1));
            }
        }
    }
}
