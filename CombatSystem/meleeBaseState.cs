using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// meleeBaseState es una clase de la que derivan otras clases que son cada uno de los estados de combate a melee
/// </summary>
public class meleeBaseState : state
{
    /// <summary>
    /// Es el tiempo hasta el pr�ximo ataque.
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
    /// Es el �ndice de combo por el que vamos.
    /// </summary>
    protected static int _attackIndex = 0;

    /// <summary>
    /// Constructor con par�metros de la clase.
    /// </summary>
    /// <param name="primary">Asigna a <see cref="state._isPrimary"/> si el ataque es con el arma primaria o con la secundaria.</param>
    public meleeBaseState(bool primary)
    {
        _isPrimary = primary;
    }

    /// <summary>
    /// M�todo que termina de implementar <see cref="state.OnEnter(stateMachine)"/>.
    /// </summary>
    /// <param name="_stateMachine">La m�quina de estados actual.</param>
    public override void OnEnter(stateMachine _stateMachine)
    {
        base.OnEnter(_stateMachine);
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
    /// M�todo que termina de implementar <see cref="state.OnExit()"/>.
    /// </summary>
    public override void OnExit()
    {
        base.OnExit();
    }
    /// <summary>
    /// M�todo que termina de implementar <see cref="state.OnUpdate()"/>.
    /// </summary>
    public override void OnUpdate()
    {
        base.OnUpdate();
    }
    /// <summary>
    /// M�todo que termina de implementar <see cref="state.OnFixedUpdate()"/>.
    /// </summary>
    public override void OnFixedUpdate()
    {
        base.OnFixedUpdate();
    }
    /// <summary>
    /// M�todo que termina de implementar <see cref="state.OnLateUpdate()"/>.
    /// </summary>
    public override void OnLateUpdate()
    {
        base.OnLateUpdate();
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
    /// M�todo que modifica <see cref="_attackIndex"/> aument�ndolo en 1.
    /// </summary>
    public static void incrementAttackIndex()
    {
        _attackIndex++;
    }
}
