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
        
        //Obtenemos la posición del boss
        Vector3 position = _currentStateMachine.GetComponent<boss>().transform.position;
        
        //Si no se está girando
        if (!_currentStateMachine.GetComponent<enemy>().getIsFlipping())
        {
            //Lo movemos según si está mirando a la derecha o izquierda
            if (_currentStateMachine.GetComponent<enemy>().getIsLookingRight())
            {
                position.x += _currentStateMachine.GetComponent<enemyController>().getSpeed() * Time.deltaTime;
            }
            else
            {
                position.x -= _currentStateMachine.GetComponent<enemyController>().getSpeed() * Time.deltaTime;
            }
        }

        //Si nosotros estamos a la derecha y está mirando a la izquierda lo giramos
        if (GetComponent<enemy>().transform.position.x < config.getPlayer().GetComponent<playerMovement>().transform.position.x)
        {
            if (!GetComponent<enemy>().getIsLookingRight() && !_currentStateMachine.GetComponent<enemy>().getIsFlipping())
            {
                _currentStateMachine.GetComponent<enemy>().flipInTime(0.5f);
            }

        } //Si nosotros estamos a la izquierda y está mirando a la derecha lo giramos
        else if (GetComponent<enemy>().transform.position.x > config.getPlayer().GetComponent<playerMovement>().transform.position.x)
        {
            if (GetComponent<enemy>().getIsLookingRight() && !_currentStateMachine.GetComponent<enemy>().getIsFlipping())
            {
                _currentStateMachine.GetComponent<enemy>().flipInTime(0.5f);
            }
        }
        //Modificamos su posición
        _currentStateMachine.GetComponent<enemy>().transform.position = position;

        //Si estamos en rango de ataque
        if (Vector3.Distance(GetComponent<enemy>().transform.position, config.getPlayer().transform.position) <= GetComponent<enemy>().getAttackRange())
        {
            //y estamos a su derecha
            if (GetComponent<enemy>().transform.position.x < config.getPlayer().GetComponent<playerMovement>().transform.position.x)
            {
                //si está mirando al otro lado lo giramos
                if (!GetComponent<enemy>().getIsLookingRight() && !_currentStateMachine.GetComponent<enemy>().getIsFlipping())
                {
                    _currentStateMachine.GetComponent<enemy>().flipInTime(0.5f);
                }
                else //si no, nos ataca
                {
                    Debug.Log("hola");
                    _currentStateMachine.setNextState(new groundEnemyEntryState(GetComponent<enemy>().getTimes()[0], 0));
                }

            } //si estamos a su izquierda
            else if (GetComponent<enemy>().transform.position.x > config.getPlayer().GetComponent<playerMovement>().transform.position.x)
            {
                //si está mirando al otro lado lo giramos
                if (GetComponent<enemy>().getIsLookingRight() && !_currentStateMachine.GetComponent<enemy>().getIsFlipping())
                {
                    _currentStateMachine.GetComponent<enemy>().flipInTime(0.5f);
                }
                else //si no, nos ataca
                {
                    _currentStateMachine.setNextState(new groundEnemyEntryState(GetComponent<enemy>().getTimes()[0], 0));
                }
            }

        }
    }

    
}
