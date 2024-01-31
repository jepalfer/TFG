using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// groundEnemyEntryState es una clase que representa el estado en el que un enemigo entra a hacer un combo.
/// </summary>
public class groundEnemyEntryState : enemyBaseState
{
    /// <summary>
    /// Constructor con parámetros de la clase que asigna un tiempo y contador.
    /// </summary>
    /// <param name="time">El tiempo asignado a <see cref="enemyBaseState._attackTime"/>.</param>
    /// <param name="counter">El contador asignado a <see cref="enemyBaseState._attackCounter"/></param>
    public groundEnemyEntryState(float time, int counter) : base(time, counter) { }


    /// <summary>
    /// Método que termina de implementar <see cref="enemyBaseState.onEnter(enemyStateMachine)"/>-
    /// </summary>
    /// <param name="_stateMachine">Máquina de estados actual.</param>
    public override void onEnter(enemyStateMachine _stateMachine)
    {
        base.onEnter(_stateMachine);
        //Debug.Log("Entry" + getAttackCounter().ToString());

        //_stateMachine.getAnimator().SetTrigger("Attack" + getAttackCounter().ToString());
        GetComponent<enemy>().getHitbox().GetComponent<BoxCollider2D>().enabled = true;
    }

    /// <summary>
    /// Método que termina de implementar <see cref="enemyBaseState.onUpdate()"/>.
    /// Si estamos lejos del rango de ataque cambia de estado, si estamos en el rango de ataque comprueba para saber si el combo continúa o debe acabar, y si no estamos al a vista se queda quieto.
    /// Ver <see cref="enemyChaseState"/>, <see cref="idleEnemyState"/>, <see cref="groundEnemyComboState"/> y <see cref="groundEnemyFinisherState"/> para más información.
    /// </summary>
    public override void onUpdate()
    {
        base.onUpdate();
        //El tiempo de espera ha acabado
        if (_time >= _attackTime)
        {

            //Estamos a la vista pero fuera del rango de ataque
            if (Vector3.Distance(_currentStateMachine.GetComponent<enemy>().gameObject.transform.position, config.getPlayer().transform.position) >= _currentStateMachine.GetComponent<enemy>().getAttackRange())
            {
                GetComponent<enemy>().getHitbox().GetComponent<BoxCollider2D>().enabled = false;
                _currentStateMachine.setNextState(new enemyChaseState());
            } //No estamos a rango de ataque
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
            else //El siguiente ataque es combo
            {
                GetComponent<enemy>().getHitbox().GetComponent<BoxCollider2D>().enabled = false;
                _currentStateMachine.setNextState(new groundEnemyComboState(_currentStateMachine.GetComponent<enemy>().getTimes()[_attackCounter + 1], _attackCounter + 1));
            }
        }
    }

}
