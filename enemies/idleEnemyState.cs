using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// idleEnemyState es una clase que representa un estado del enemigo en el que no hace nada.
/// </summary>
public class idleEnemyState : enemyState
{
    /// <summary>
    /// Método que implementa <see cref="enemyState.onEnter(enemyStateMachine)"/>.
    /// </summary>
    /// <param name="_stateMachine">La máquina de estados actual.</param>
    public override void onEnter(enemyStateMachine _stateMachine)
    {
        base.onEnter(_stateMachine);
        //_stateMachine.getAnimator().SetTrigger("Idle");

    }
    /// <summary>
    /// Método que se ejecuta cada frame para actualizar la lógica del estado.
    /// </summary>
    public override void onUpdate()
    {
        base.onUpdate();

        //Si los 2 puntos de la ruta existen
        if (_currentStateMachine.GetComponent<enemy>().getPointA() != null && _currentStateMachine.GetComponent<enemy>().getPointB() != null)
        {
            //Obtenemos la posición del enemigo
            Vector3 position = _currentStateMachine.GetComponent<enemy>().transform.position;

            //Si no se está girando movemos según la orientación del enemigo
            if (!_currentStateMachine.GetComponent<enemy>().getIsFlipping())
            {
                if (_currentStateMachine.GetComponent<enemy>().getIsLookingRight())
                {
                    position.x += _currentStateMachine.GetComponent<enemyController>().getSpeed() * Time.deltaTime;
                }
                else
                {
                    position.x -= _currentStateMachine.GetComponent<enemyController>().getSpeed() * Time.deltaTime;
                }
            }

            //Si llega al punto de la izquierda
            if (Vector3.Distance(_currentStateMachine.GetComponent<enemy>().transform.position, 
                                 _currentStateMachine.GetComponent<enemy>().getPointA().transform.position) <= 0.5f && 
                                 !_currentStateMachine.GetComponent<enemy>().getIsLookingRight() && !_currentStateMachine.GetComponent<enemy>().getIsFlipping())
            {
                _currentStateMachine.GetComponent<enemy>().flipInTime(1f);

            }

            //Si llega al punto de la derecha
            if (Vector3.Distance(_currentStateMachine.GetComponent<enemy>().transform.position, 
                                 _currentStateMachine.GetComponent<enemy>().getPointB().transform.position) <= 0.5f &&
                                 _currentStateMachine.GetComponent<enemy>().getIsLookingRight() && !_currentStateMachine.GetComponent<enemy>().getIsFlipping())
            {
                _currentStateMachine.GetComponent<enemy>().flipInTime(1f);
            }

            //Movemos al enemigo
            _currentStateMachine.GetComponent<enemy>().transform.position = position;
        }

        //Si estamos en rango de ataque
        if (Vector3.Distance(GetComponent<enemy>().transform.position, config.getPlayer().transform.position) <= GetComponent<enemy>().getAttackRange())
        {
            if (GetComponent<enemy>().transform.position.x < config.getPlayer().GetComponent<playerMovement>().transform.position.x)
            {
                //Si está mirando en la otra dirección lo giramos
                if (!GetComponent<enemy>().getIsLookingRight() && !_currentStateMachine.GetComponent<enemy>().getIsFlipping())
                {
                    _currentStateMachine.GetComponent<enemy>().flipInTime(0.2f);
                }
                else //Si no, hacemos que nos ataque
                {
                    _currentStateMachine.setNextState(new groundEnemyEntryState(GetComponent<enemy>().getTimes()[0], 0));
                }
                
            }
            else if (GetComponent<enemy>().transform.position.x > config.getPlayer().GetComponent<playerMovement>().transform.position.x)
            {
                //Si está mirando en la otra dirección lo giramos
                if (GetComponent<enemy>().getIsLookingRight() && !_currentStateMachine.GetComponent<enemy>().getIsFlipping())
                {
                    _currentStateMachine.GetComponent<enemy>().flipInTime(0.2f);
                }
                else //Si no, hacemos que nos ataque
                {
                    _currentStateMachine.setNextState(new groundEnemyEntryState(GetComponent<enemy>().getTimes()[0], 0));
                }
            }
            
        }
    }
}