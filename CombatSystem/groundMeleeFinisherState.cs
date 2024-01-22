using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class groundMeleeFinisherState : meleeBaseState
{
    public groundMeleeFinisherState(bool value) : base(value) { }
    public override void OnEnter(stateMachine _stateMachine)
    {
        base.OnEnter(_stateMachine);

        //Attack
        _timeNextAttack = 1f;
        //animator.SetTrigger("Attack" + attackIndex);
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
        //Debug.Log(weapon + "finished with attack n" + MeleeBaseState._attackIndex);
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        if (_time >= _timeNextAttack)
        {
            GetComponent<combatController>().getHitbox().GetComponent<playerHitController>().setSecundary(false);
            GetComponent<combatController>().getHitbox().GetComponent<playerHitController>().setPrimary(false);
            GetComponent<combatController>().getHitbox().GetComponent<BoxCollider2D>().enabled = false;
            _currentStateMachine.SetNextStateToMain();
            meleeBaseState._attackIndex = 0;
        }
    }
}
