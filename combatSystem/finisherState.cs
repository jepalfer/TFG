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
        AnimatorClipInfo[] clipInfo = config.getPlayer().GetComponent<Animator>().GetCurrentAnimatorClipInfo(0);
        //Debug.Log(stateInfo[0].clip.IsName(config.getPlayer().GetComponent<playerAnimatorController>().getAnimationName(animatorEnum.player_attack, 0, 1, animatorEnum.front)));
        GetComponent<Animator>().SetFloat("attackSpeed", clipInfo[0].clip.length / (_timeNextAttack - _timeNextAttack / 6));

        //animator.SetTrigger("Attack" + attackIndex);
        if (_isPrimary)
        {
            GetComponent<combatController>().getHitbox().GetComponent<playerHitController>().setPrimary(true);
            GetComponent<combatController>().getPrimaryWeapon().GetComponent<weapon>().setCurrentAttack(baseState.getAttackIndex());
            GetComponent<combatController>().getPrimaryWeapon().GetComponent<weapon>().setIsAttacking(true);
            //GetComponent<combatController>().getHitbox().GetComponent<BoxCollider2D>().enabled = true;
            weaponConfig.getPrimaryWeapon().GetComponent<weaponSFXController>().playAttackSFX();
        }
        else
        {
            GetComponent<combatController>().getHitbox().GetComponent<playerHitController>().setSecundary(true);
            GetComponent<combatController>().getSecundaryWeapon().GetComponent<weapon>().setCurrentAttack(baseState.getAttackIndex());
            GetComponent<combatController>().getSecundaryWeapon().GetComponent<weapon>().setIsAttacking(true);
            weaponConfig.getSecundaryWeapon().GetComponent<weaponSFXController>().playAttackSFX();

            if (GetComponent<combatController>().getSecundaryWeapon().GetComponent<weapon>().getRange() == rangeEnum.melee)
            {
                //GetComponent<combatController>().getHitbox().GetComponent<BoxCollider2D>().enabled = true;
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
            //GetComponent<combatController>().getHitbox().GetComponent<BoxCollider2D>().enabled = false;
            _currentStateMachine.setNextStateToMain();
            baseState._attackIndex = 0;
        }
    }
}
