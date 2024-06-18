using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// comboState es una clase que representa el estado en el que estamos en medio de un combo.
/// </summary>
public class comboState : baseState
{
    /// <summary>
    /// Constructor con parámetros de la clase.
    /// </summary>
    /// <param name="value">Asigna a <see cref="state._isPrimary"/> si el ataque es con el arma primaria o con la secundaria.</param>
    public comboState(bool value) : base(value) { }

    /// <summary>
    /// Método que termina de implementar <see cref="baseState.onEnter(stateMachine)"/>.
    /// </summary>
    /// <param name="_stateMachine">La máquina de estados actual</param>
    public override void onEnter(stateMachine _stateMachine)
    {
        base.onEnter(_stateMachine);

        //Attack
        _timeNextAttack = 0.75f; 
        
        AnimatorClipInfo[] clipInfo = config.getPlayer().GetComponent<Animator>().GetCurrentAnimatorClipInfo(0);
        //Debug.Log(stateInfo[0].clip.IsName(config.getPlayer().GetComponent<playerAnimatorController>().getAnimationName(animatorEnum.player_attack, 0, 1, animatorEnum.front)));
        GetComponent<Animator>().SetFloat("attackSpeed", clipInfo[0].clip.length / (_timeNextAttack - _timeNextAttack / 6));

        _shouldCombo = true;
        //_animator.SetTrigger("Attack" + _attackIndex);
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

        //Esperamos un poco de tiempo
        if (_time >= _timeNextAttack / 2)
        {
            if (!inputManager.GetKey(inputEnum.down) && !inputManager.GetKey(inputEnum.up))
            {
                int primaryAttack = 0, secundaryAttack = 0;
                animatorEnum attackDirection = config.getPlayer().GetComponent<playerMovement>().getIsFacingLeft() ? animatorEnum.front : animatorEnum.back;

                config.getPlayer().GetComponent<combatController>().calculateExtraComboHits(ref primaryAttack, ref secundaryAttack);
                if (GetComponent<combatController>().getPrimaryWeapon() != null)
                {
                    if (inputManager.GetKeyDown(inputEnum.primaryAttack))
                    {
                        if (baseState.getAttackIndex() < ((GetComponent<combatController>().getPrimaryWeapon().GetComponent<weapon>().getNumberOfAttacks() + primaryAttack) - 2))
                        {
                            baseState.incrementAttackIndex();
                            //GetComponent<combatController>().getHitbox().GetComponent<BoxCollider2D>().enabled = false;
                            GetComponent<playerAnimatorController>().playAnimation(animatorEnum.player_attack, weaponConfig.getPrimaryWeapon().GetComponent<weapon>().getID(), baseState.getAttackIndex(), attackDirection);
                            _currentStateMachine.setNextState(new comboState(true));
                        }
                        else if (baseState.getAttackIndex() < ((GetComponent<combatController>().getPrimaryWeapon().GetComponent<weapon>().getNumberOfAttacks() + primaryAttack) - 1))
                        {
                            baseState.incrementAttackIndex();
                            //GetComponent<combatController>().getHitbox().GetComponent<BoxCollider2D>().enabled = false;
                            GetComponent<playerAnimatorController>().playAnimation(animatorEnum.player_attack, weaponConfig.getPrimaryWeapon().GetComponent<weapon>().getID(), baseState.getAttackIndex(), attackDirection);
                            _currentStateMachine.setNextState(new finisherState(true));
                        }

                    }
                }
                if (GetComponent<combatController>().getSecundaryWeapon() != null)
                {
                    if (inputManager.GetKeyDown(inputEnum.secundaryAttack))
                    {
                        if (baseState.getAttackIndex() < ((GetComponent<combatController>().getSecundaryWeapon().GetComponent<weapon>().getNumberOfAttacks() + secundaryAttack) - 2))
                        {
                            baseState.incrementAttackIndex();
                            //GetComponent<combatController>().getHitbox().GetComponent<BoxCollider2D>().enabled = false;
                            GetComponent<playerAnimatorController>().playAnimation(animatorEnum.player_attack, weaponConfig.getPrimaryWeapon().GetComponent<weapon>().getID(), baseState.getAttackIndex(), attackDirection);
                            _currentStateMachine.setNextState(new comboState(false));
                        }
                        else if (baseState.getAttackIndex() < ((GetComponent<combatController>().getSecundaryWeapon().GetComponent<weapon>().getNumberOfAttacks() + secundaryAttack) - 1))
                        {
                            baseState.incrementAttackIndex();
                            //GetComponent<combatController>().getHitbox().GetComponent<BoxCollider2D>().enabled = false;
                            GetComponent<playerAnimatorController>().playAnimation(animatorEnum.player_attack, weaponConfig.getPrimaryWeapon().GetComponent<weapon>().getID(), baseState.getAttackIndex(), attackDirection);
                            _currentStateMachine.setNextState(new finisherState(false));
                        }
                    }
                }
            }
                
            //Si supera el tiempo máximo
            if (_time >= _timeNextAttack)
            {
                GetComponent<combatController>().getHitbox().GetComponent<BoxCollider2D>().enabled = false;
                GetComponent<combatController>().getHitbox().GetComponent<playerHitController>().setSecundary(false);
                GetComponent<combatController>().getHitbox().GetComponent<playerHitController>().setPrimary(false);
                baseState._attackIndex = 0;
                _currentStateMachine.setNextStateToMain();
            }
        }
    }
}
