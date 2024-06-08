using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// enemyBaseState es una clase que representa el estado base de combate de un enemigo.
/// </summary>
public class enemyBaseCombatState : enemyState
{
    /// <summary>
    /// El tiempo de ataque.
    /// </summary>
    protected float _attackTime;

    /// <summary>
    /// El contador para saber qué ataque es.
    /// </summary>
    protected int _attackCounter;

    /// <summary>
    /// Constructor con parámetros de la clase. Asigna a <see cref="_attackTime"/> y <see cref="_attackCounter"/> los valores correspondientes.
    /// </summary>
    /// <param name="time">El tiempo asignado a <see cref="_attackTime"/>.</param>
    /// <param name="counter">El contador asignado a <see cref="_attackCounter"/>.</param>
    public enemyBaseCombatState(float time, int counter)
    {
        _attackTime = time;
        _attackCounter = counter;
    }

    /// <summary>
    /// Getter que devuelve <see cref="_attackTime"/>.
    /// </summary>
    /// <returns>Un float que representa el tiempo del ataque.</returns>
    protected float getAttackTime()
    {
        return _attackTime;
    }

    /// <summary>
    /// Getter que devuelve <see cref="_attackCounter"/>.
    /// </summary>
    /// <returns>Un int que representa el índice del ataque por el que va el enemigo.</returns>
    protected int getAttackCounter()
    {
        return _attackCounter;
    }

    /// <summary>
    /// Método que termina de implementar <see cref="enemyState.onEnter(enemyStateMachine)"/>.
    /// </summary>
    /// <param name="_stateMachine">La máquina de estados actual.</param>
    public override void onEnter(enemyStateMachine _stateMachine)
    {
        base.onEnter(_stateMachine);
    }
    /// <summary>
    /// Método que termina de implementar <see cref="enemyState.onExit()"/>
    /// </summary>
    public override void onExit()
    {
        base.onExit();
    }
    /// <summary>
    /// Método que termina de implementar <see cref="enemyState.onUpdate()"/>
    /// </summary>
    public override void onUpdate()
    {
        base.onUpdate();
    }
    /// <summary>
    /// Método que termina de implementar <see cref="enemyState.onFixedUpdate()"/>
    /// </summary>
    public override void onFixedUpdate()
    {
        base.onFixedUpdate();
    }
    /// <summary>
    /// Método que termina de implementar <see cref="enemyState.onLateUpdate()"/>
    /// </summary>
    public override void onLateUpdate()
    {
        base.onLateUpdate();
    }
}
