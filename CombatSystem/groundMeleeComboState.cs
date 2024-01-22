using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class groundMeleeComboState : meleeBaseState
{
    public groundMeleeComboState(bool value) : base(value) { }
    public override void OnEnter(stateMachine _stateMachine)
    {
        base.OnEnter(_stateMachine);

        //Attack
        _timeNextAttack = 1.5f;
        _shouldCombo = true;
        //_animator.SetTrigger("Attack" + _attackIndex);
        string weapon = "Secundary ";
        if (_isPrimary)
        {
            weapon = "Primary ";
            GetComponent<combatController>().getHitbox().GetComponent<playerHitController>().setPrimary(true);
            GetComponent<combatController>().getPrimaryWeapon().GetComponent<weapon>().setCurrentAttack(meleeBaseState._attackIndex);
            GetComponent<combatController>().getPrimaryWeapon().GetComponent<weapon>().setIsAttacking(true);
        }
        else
        {
            GetComponent<combatController>().getHitbox().GetComponent<playerHitController>().setSecundary(true);
            GetComponent<combatController>().getSecundaryWeapon().GetComponent<weapon>().setCurrentAttack(meleeBaseState._attackIndex);
            GetComponent<combatController>().getSecundaryWeapon().GetComponent<weapon>().setIsAttacking(true);
        }
        GetComponent<combatController>().getHitbox().GetComponent<BoxCollider2D>().enabled = true;
        //Debug.Log(weapon + "fired attack n" + MeleeBaseState._attackIndex);
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        /*
        if (_time >= _timeNextAttack)
        {
            if (_shouldCombo)
            {
                if (MeleeBaseState._attackIndex < 3)
                {
                    _currentStateMachine.SetNextState(new groundComboState());
                }
                else
                {
                    _currentStateMachine.SetNextState(new groundFinisherState());
                }
            }
            else
            {
                _currentStateMachine.SetNextStateToMain();
            }
        }*/

        if (_time >= _timeNextAttack / 2)
        {
            if (GetComponent<combatController>().getPrimaryWeapon() != null)
            {
                if (inputManager.getKeyDown(inputEnum.PrimaryAttack.ToString()))
                {
                    if (meleeBaseState._attackIndex < (GetComponent<combatController>().getPrimaryWeapon().GetComponent<weapon>().getNumberOfAttacks() - 2))
                    {
                        meleeBaseState._attackIndex++;
                        GetComponent<combatController>().getHitbox().GetComponent<BoxCollider2D>().enabled = false;
                        _currentStateMachine.SetNextState(new groundMeleeComboState(true));
                    }
                    else if (meleeBaseState._attackIndex < (GetComponent<combatController>().getPrimaryWeapon().GetComponent<weapon>().getNumberOfAttacks() - 1))
                    {
                        meleeBaseState._attackIndex++;
                        GetComponent<combatController>().getHitbox().GetComponent<BoxCollider2D>().enabled = false;
                        _currentStateMachine.SetNextState(new groundMeleeFinisherState(true));
                    }

                }
            }
            if (GetComponent<combatController>().getSecundaryWeapon() != null)
            {
                if (inputManager.getKeyDown(inputEnum.SecundaryAttack.ToString()))
                {
                    if (meleeBaseState._attackIndex < (GetComponent<combatController>().getSecundaryWeapon().GetComponent<weapon>().getNumberOfAttacks() - 2))
                    {
                        meleeBaseState._attackIndex++;
                        GetComponent<combatController>().getHitbox().GetComponent<BoxCollider2D>().enabled = false;
                        _currentStateMachine.SetNextState(new groundMeleeComboState(false));
                    }
                    else if (meleeBaseState._attackIndex < (GetComponent<combatController>().getSecundaryWeapon().GetComponent<weapon>().getNumberOfAttacks() - 1))
                    {
                        meleeBaseState._attackIndex++;
                        GetComponent<combatController>().getHitbox().GetComponent<BoxCollider2D>().enabled = false;
                        _currentStateMachine.SetNextState(new groundMeleeFinisherState(false));
                    }
                }
            }
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
