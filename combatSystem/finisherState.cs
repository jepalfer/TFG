using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// finisherState es una clase que representa el estado en el que finalizamos un combo.
/// </summary>
public class finisherState : baseState
{
    /// <summary>
    /// Constructor con parámetros de la clase.
    /// </summary>
    /// <param name="value">Asigna a <see cref="state._isPrimary"/> si el ataque es con el arma primaria o con la secundaria.</param>
    public finisherState(bool value) : base(value) { }

    /// <summary>
    /// Método que termina de implementar <see cref="baseState.onEnter(stateMachine)"/>.
    /// </summary>
    /// <param name="_stateMachine">La máquina de estados actual</param>
    public override void onEnter(stateMachine _stateMachine)
    {
        base.onEnter(_stateMachine);

        //Attack
        _timeNextAttack = 1f;
        //animator.SetTrigger("Attack" + attackIndex);
        if (_isPrimary)
        {
            GetComponent<combatController>().getHitbox().GetComponent<playerHitController>().setPrimary(true);
            GetComponent<combatController>().getPrimaryWeapon().GetComponent<weapon>().setCurrentAttack(baseState.getAttackIndex());
            GetComponent<combatController>().getPrimaryWeapon().GetComponent<weapon>().setIsAttacking(true);
            GetComponent<combatController>().getHitbox().GetComponent<BoxCollider2D>().enabled = true;
        }
        else
        {
            GetComponent<combatController>().getHitbox().GetComponent<playerHitController>().setSecundary(true);
            GetComponent<combatController>().getSecundaryWeapon().GetComponent<weapon>().setCurrentAttack(baseState.getAttackIndex());
            GetComponent<combatController>().getSecundaryWeapon().GetComponent<weapon>().setIsAttacking(true);

            if (GetComponent<combatController>().getSecundaryWeapon().GetComponent<weapon>().getRange() == rangeEnum.melee)
            {
                GetComponent<combatController>().getHitbox().GetComponent<BoxCollider2D>().enabled = true;
            }
            else
            {
                GetComponent<combatController>().createBullet();
            }
        }
    }

    /// <summary>
    /// Método que termina de implementar <see cref="baseState.onUpdate()"/>.
    /// </summary>
    public override void onUpdate()
    {
        base.onUpdate();

        if (_time >= _timeNextAttack)
        {
            GetComponent<combatController>().getHitbox().GetComponent<playerHitController>().setSecundary(false);
            GetComponent<combatController>().getHitbox().GetComponent<playerHitController>().setPrimary(false);
            GetComponent<combatController>().getHitbox().GetComponent<BoxCollider2D>().enabled = false;
            _currentStateMachine.setNextStateToMain();
            baseState._attackIndex = 0;
        }
    }
}
