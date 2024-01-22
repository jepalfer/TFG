using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// groundMeleeEntryState es una clase que representa el estado en el que entramos a un combo (primer golpe).
/// </summary>
public class groundMeleeEntryState : meleeBaseState
{
    /// <summary>
    /// Constructor con parámetros de la clase.
    /// </summary>
    /// <param name="value">Asigna a <see cref="state._isPrimary"/> si el ataque es con el arma primaria o con la secundaria.</param>
    public groundMeleeEntryState(bool value) : base(value) { }

    /// <summary>
    /// Método que termina de implementar <see cref="meleeBaseState.OnEnter(stateMachine)"/>.
    /// </summary>
    /// <param name="_stateMachine">La máquina de estados actual</param>
    public override void OnEnter(stateMachine _stateMachine)
    {
        base.OnEnter(_stateMachine);

        //Attack
        _timeNextAttack = 1.5f;
        //_animator.SetTrigger("Attack" + _attackIndex);

        if (_isPrimary)
        {
            GetComponent<combatController>().getHitbox().GetComponent<playerHitController>().setPrimary(true);
            GetComponent<combatController>().getPrimaryWeapon().GetComponent<weapon>().setCurrentAttack(meleeBaseState.getAttackIndex());
            GetComponent<combatController>().getPrimaryWeapon().GetComponent<weapon>().setIsAttacking(true);
        }
        else
        {
            GetComponent<combatController>().getHitbox().GetComponent<playerHitController>().setSecundary(true);
            GetComponent<combatController>().getSecundaryWeapon().GetComponent<weapon>().setCurrentAttack(meleeBaseState.getAttackIndex());
            GetComponent<combatController>().getSecundaryWeapon().GetComponent<weapon>().setIsAttacking(true);
        }
        GetComponent<combatController>().getHitbox().GetComponent<BoxCollider2D>().enabled = true;
        //Debug.Log(weapon + "fired attack n" + MeleeBaseState._attackIndex);
    }
    /// <summary>
    /// Método que termina de implementar <see cref="meleeBaseState.OnUpdate()"/>.
    /// </summary>
    public override void OnUpdate()
    {
        base.OnUpdate();
        /*
        if (_time >= _duration)
        {
            if (_shouldCombo)
            {
                _currentStateMachine.SetNextState(new groundComboState());
            }
            else
            {
                _currentStateMachine.SetNextStateToMain();
            }
        }*/

        //Esperamos un poco de tiempo
        if (_time >= _timeNextAttack / 2)
        {
            if (GetComponent<combatController>().getPrimaryWeapon() != null)
            {
                if (inputManager.getKeyDown(inputEnum.PrimaryAttack.ToString()))
                {
                    if (meleeBaseState.getAttackIndex() < GetComponent<combatController>().getPrimaryWeapon().GetComponent<weapon>().getNumberOfAttacks())
                    {
                        meleeBaseState.incrementAttackIndex();
                        GetComponent<combatController>().getHitbox().GetComponent<BoxCollider2D>().enabled = false;
                        _currentStateMachine.SetNextState(new groundMeleeComboState(true));
                    }
                }
            }
            if (GetComponent<combatController>().getSecundaryWeapon() != null)
            {
                if (inputManager.getKeyDown(inputEnum.SecundaryAttack.ToString()))
                {
                    if (meleeBaseState.getAttackIndex() < GetComponent<combatController>().getSecundaryWeapon().GetComponent<weapon>().getNumberOfAttacks())
                    {
                        meleeBaseState.incrementAttackIndex();
                        GetComponent<combatController>().getHitbox().GetComponent<BoxCollider2D>().enabled = false;
                        _currentStateMachine.SetNextState(new groundMeleeComboState(false));
                    }
                }
            }

            //Si supera el tiempo máximo
            if (_time >= _timeNextAttack)
            {
                GetComponent<combatController>().getHitbox().GetComponent<BoxCollider2D>().enabled = false;
                GetComponent<combatController>().getHitbox().GetComponent<playerHitController>().setSecundary(false);
                GetComponent<combatController>().getHitbox().GetComponent<playerHitController>().setPrimary(false);
                meleeBaseState._attackIndex = 0;
                _currentStateMachine.SetNextStateToMain();
            }
        }
    }
}
