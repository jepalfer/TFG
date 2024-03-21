using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// enemyController es una clase que maneja la lógica de los enemigos.
/// </summary>
public class enemyController : MonoBehaviour
{
    /// <summary>
    /// Es una referencia al jugador.
    /// </summary>
    private GameObject _player;

    /// <summary>
    /// Es una referencia a la máquina de estados propia.
    /// </summary>
    private enemyStateMachine _stateMachine;

    /// <summary>
    /// Es la velocidad de movimiento del enemigo.
    /// </summary>
    [SerializeField] private float _movementSpeed;

    /// <summary>
    /// Es la distancia de visionado del enemigo.
    /// </summary>
    [SerializeField] private float _sightDistance;

    /// <summary>
    /// Método que se ejecuta al inicio del script, asigna las referencias y variables correspondientes y hace que se ignore las colisiones entre el jugador y el enemigo.
    /// </summary>
    private void Start()
    {
        //Asignamos variables
        _player = config.getPlayer();
        _stateMachine = GetComponent<enemyStateMachine>();
        _stateMachine.setNextStateToMain();
        _movementSpeed = GetComponent<enemy>().getSpeed();
        _sightDistance = GetComponent<enemy>().getDetectionRange();

        //Ignoramos colisiones
        Physics2D.IgnoreCollision(config.getPlayer().GetComponent<BoxCollider2D>(), GetComponent<BoxCollider2D>());
    }

    /// <summary>
    /// Método que se ejecuta cada frame para actualizar la lógica. Cambia de estado inicial de la máquina de estados según la situación.
    /// </summary>
    private void Update()
    {
        
        //Si estamos en rango de visionado y fuera del rango de ataque
        if (Vector3.Distance(transform.position, config.getPlayer().transform.position) <= _sightDistance && Vector3.Distance(transform.position, config.getPlayer().transform.position) >= GetComponent<enemy>().getAttackRange())
        {
            /*
            //Perseguimos al jugador
            if (_stateMachine.getCurrentState().GetType() == typeof(enemyChaseState) || _stateMachine.getCurrentState().GetType() == typeof(idleEnemyState))
            {
                _stateMachine.setNextState(new enemyChaseState());
            }*/
        }
        //Si estamos en rango de ataque
        else if (Vector3.Distance(transform.position, config.getPlayer().transform.position) <= GetComponent<enemy>().getAttackRange())
        {
            //Atacamos
            if (_stateMachine.getCurrentState().GetType() == typeof(enemyChaseState) || _stateMachine.getCurrentState().GetType() == typeof(idleEnemyState))
            {
                _stateMachine.setNextState(new groundEnemyEntryState(GetComponent<enemy>().getTimes()[0], 0));
            }
        }
        
    }

    /// <summary>
    /// Getter que devuelve <see cref="_movementSpeed"/>.
    /// </summary>
    /// <returns>Un float que representa la velocidad de movimiento del enemigo.</returns>
    public float getSpeed()
    {
        return _movementSpeed;
    }

    /// <summary>
    /// Getter que devuelve <see cref="_sightDistance"/>.
    /// </summary>
    /// <returns>Un float que representa la distania de visionado del enemigo.</returns>
    public float getDetectionRange()
    {
        return _sightDistance;
    }

}
