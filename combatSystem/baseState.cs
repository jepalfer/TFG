using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// baseState es una clase de la que derivan otras clases que son cada uno de los estados de combate.
/// </summary>
public class baseState : state
{
    /// <summary>
    /// Es el tiempo hasta el próximo ataque.
    /// </summary>
    [SerializeField] protected float _timeNextAttack;

    /// <summary>
    /// Es el animator.
    /// </summary>
    protected Animator _animator;

    /// <summary>
    /// Establece si el golpe debe poderse encadenar con otros para hacer combo o no.
    /// </summary>
    protected bool _shouldCombo;

    /// <summary>
    /// Es el índice de combo por el que vamos.
    /// </summary>
    protected static int _attackIndex = 0;

    /// <summary>
    /// Constructor con parámetros de la clase.
    /// </summary>
    /// <param name="primary">Asigna a <see cref="state._isPrimary"/> si el ataque es con el arma primaria o con la secundaria.</param>
    public baseState(bool primary)
    {
        _isPrimary = primary;
    }

    /// <summary>
    /// Método que termina de implementar <see cref="state.onEnter(stateMachine)"/>.
    /// </summary>
    /// <param name="_stateMachine">La máquina de estados actual.</param>
    public override void onEnter(stateMachine _stateMachine)
    {
        base.onEnter(_stateMachine);
        _animator = GetComponent<Animator>();
        GetComponent<playerMovement>().setCanMove(false);
        GetComponent<playerMovement>().setCouldRoll(GetComponent<playerMovement>().getCanRoll());
        GetComponent<playerMovement>().setCanRoll(false);
        GetComponent<playerMovement>().setCanClimb(false);
        GetComponent<combatController>().setIsAttacking(true);
        GetComponent<playerMovement>().getRigidBody().gravityScale = 0f;
        GetComponent<playerMovement>().getRigidBody().velocity = Vector2.zero;

    }

    /// <summary>
    /// Método que termina de implementar <see cref="state.onExit()"/>.
    /// </summary>
    public override void onExit()
    {
        base.onExit();
    }
    /// <summary>
    /// Método que termina de implementar <see cref="state.onUpdate()"/>.
    /// </summary>
    public override void onUpdate()
    {
        base.onUpdate();
    }
    /// <summary>
    /// Método que termina de implementar <see cref="state.onFixedUpdate()"/>.
    /// </summary>
    public override void onFixedUpdate()
    {
        base.onFixedUpdate();
    }
    /// <summary>
    /// Método que termina de implementar <see cref="state.onLateUpdate()"/>.
    /// </summary>
    public override void onLateUpdate()
    {
        base.onLateUpdate();
    }

    /// <summary>
    /// Getter que devuelve <see cref="_attackIndex"/>.
    /// </summary>
    /// <returns>Un int que representa el golpe del combo por el que vamos.</returns>
    public static int getAttackIndex()
    {
        return _attackIndex;
    }

    /// <summary>
    /// Método que modifica <see cref="_attackIndex"/> aumentándolo en 1.
    /// </summary>
    public static void incrementAttackIndex()
    {
        _attackIndex++;
    }
}
