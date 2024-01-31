using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// enemyChaseState es una clase que representa el estado en el que un enemigo nos está persiguiendo.
/// </summary>
public class enemyChaseState : enemyState
{
    /// <summary>
    /// Método que termina de implementar <see cref="enemyState.onEnter(enemyStateMachine)"/>.
    /// </summary>
    /// <param name="_stateMachine">La máquina de estados actual.</param>
    public override void onEnter(enemyStateMachine _stateMachine)
    {
        base.onEnter(_stateMachine);
        //_stateMachine.getAnimator().SetTrigger("Chase");
    }

    /// <summary>
    /// Método que termina de implementar <see cref="enemyState.onUpdate()"/>
    /// </summary>
    public override void onUpdate()
    {
        base.onUpdate();
        //Si estamos fuera del rango de visión
        if (Vector3.Distance(_currentStateMachine.GetComponent<enemyController>().gameObject.transform.position, config.getPlayer().transform.position) >= _currentStateMachine.GetComponent<enemyController>().getDetectionRange())
        {
            _currentStateMachine.setNextStateToMain();
        }
        else    //Estamos dentro del rango de visión
        {
            //Movemos al enemigo hacia el jugador
            if (_currentStateMachine.GetComponent<enemyController>().gameObject.transform.position.x < config.getPlayer().transform.position.x)
            {
                if (!GetComponent<enemy>().getIsLookingRight())
                {
                    GetComponent<enemy>().flip();
                }
                _currentStateMachine.GetComponent<enemyController>().gameObject.transform.position = new Vector3(_currentStateMachine.GetComponent<enemyController>().gameObject.transform.position.x + (_currentStateMachine.GetComponent<enemyController>().getSpeed() * Time.deltaTime), _currentStateMachine.GetComponent<enemyController>().gameObject.transform.position.y, _currentStateMachine.GetComponent<enemyController>().gameObject.transform.position.z);
            }
            else
            {
                if (GetComponent<enemy>().getIsLookingRight())
                {
                    GetComponent<enemy>().flip();
                }
                _currentStateMachine.GetComponent<enemyController>().gameObject.transform.position = new Vector3(_currentStateMachine.GetComponent<enemyController>().gameObject.transform.position.x + (-_currentStateMachine.GetComponent<enemyController>().getSpeed() * Time.deltaTime), _currentStateMachine.GetComponent<enemyController>().gameObject.transform.position.y, _currentStateMachine.GetComponent<enemyController>().gameObject.transform.position.z);
            }
        }
    }
}
