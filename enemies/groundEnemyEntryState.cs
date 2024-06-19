using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// groundEnemyEntryState es una clase que representa el estado en el que un enemigo entra a hacer un combo.
/// </summary>
public class groundEnemyEntryState : enemyBaseCombatState
{
    /// <summary>
    /// Constructor con parámetros de la clase que asigna un tiempo y contador.
    /// </summary>
    /// <param name="time">El tiempo asignado a <see cref="enemyBaseCombatState._attackTime"/>.</param>
    /// <param name="counter">El contador asignado a <see cref="enemyBaseCombatState._attackCounter"/></param>
    public groundEnemyEntryState(float time, int counter) : base(time, counter) { }


    /// <summary>
    /// Método que termina de implementar <see cref="enemyBaseCombatState.onEnter(enemyStateMachine)"/>-
    /// </summary>
    /// <param name="_stateMachine">Máquina de estados actual.</param>
    public override void onEnter(enemyStateMachine _stateMachine)
    {
        //Debug.Log("entro entry");
        base.onEnter(_stateMachine);
        AnimatorClipInfo[] clipInfo = _currentStateMachine.GetComponent<Animator>().GetCurrentAnimatorClipInfo(0);
        _currentStateMachine.GetComponent<Animator>().SetFloat("attackSpeed", clipInfo[0].clip.length / (_attackTime));
        _currentStateMachine.GetComponent<enemy>().getHitbox().GetComponent<enemyHitController>().setHasHitPlayer(false);
        _currentStateMachine.GetComponent<enemySFXController>().playAttackSFX();
        //Debug.Log(clipInfo[0].clip.length / (_attackTime));
    }

    /// <summary>
    /// Método que termina de implementar <see cref="enemyBaseCombatState.onUpdate()"/>.
    /// Si estamos lejos del rango de ataque cambia de estado, si estamos en el rango de ataque comprueba para saber si el combo continúa o debe acabar, y si no estamos al a vista se queda quieto.
    /// Ver <see cref="enemyChaseState"/>, <see cref="idleEnemyState"/>, <see cref="groundEnemyComboState"/> y <see cref="groundEnemyFinisherState"/> para más información.
    /// </summary>
    public override void onUpdate()
    {
        //Debug.Log("cucu");
        base.onUpdate();
        animatorEnum direction = _currentStateMachine.GetComponent<enemy>().getIsLookingRight() ? animatorEnum.back : animatorEnum.front;
        //El tiempo de espera ha acabado
        //float distance = Vector3.Distance(GetComponent<enemy>().getHurtbox().transform.position,
        //                                  config.getPlayer().GetComponent<combatController>().getHurtbox().transform.position);

        bool rightHit = drawRightRay();
        bool leftHit = drawLeftRay();
        //Debug.Log("Rayo dcha => " + drawRightRay());
        //Debug.Log("Rayo izq => " + drawLeftRay());
        if (_time >= _attackTime)
        {
            //Si estamos fuera del rango de ataque
            if (!rightHit && !leftHit)
            {
                //GetComponent<enemy>().getHitbox().GetComponent<BoxCollider2D>().enabled = false;
                if (GetComponent<boss>() != null)
                {
                    _currentStateMachine.setNextState(new enemyChaseState());
                    base.onExit();
                }
                else
                {
                    Debug.Log("ha entrado");
                    _currentStateMachine.setNextState(new idleEnemyState());
                    base.onExit();
                }
            }
            else if (_attackCounter == (_currentStateMachine.GetComponent<enemy>().getTimes().Count - 1))    //Ya ha acabado
            {
                //GetComponent<enemy>().getHitbox().GetComponent<BoxCollider2D>().enabled = false;
                _currentStateMachine.GetComponent<enemyAnimatorController>().playAnimation(animatorEnum.enemy_attack, _currentStateMachine.GetComponent<enemy>().getEnemyName(), 0, direction);
                _currentStateMachine.setNextState(new groundEnemyEntryState(GetComponent<enemy>().getTimes()[0], 0));
                base.onExit();
            } //El siguiente ataque es el último
            else if (_attackCounter == (_currentStateMachine.GetComponent<enemy>().getTimes().Count - 2))   //El siguiente es el último golpe
            {
                //GetComponent<enemy>().getHitbox().GetComponent<BoxCollider2D>().enabled = false;
                _currentStateMachine.GetComponent<enemyAnimatorController>().playAnimation(animatorEnum.enemy_attack, _currentStateMachine.GetComponent<enemy>().getEnemyName(), _attackCounter + 1, direction);
                _currentStateMachine.setNextState(new groundEnemyFinisherState(_currentStateMachine.GetComponent<enemy>().getTimes()[_attackCounter + 1], _attackCounter + 1));
                base.onExit();
            }
            else //El siguiente ataque es combo
            {
                //GetComponent<enemy>().getHitbox().GetComponent<BoxCollider2D>().enabled = false;
                _currentStateMachine.GetComponent<enemyAnimatorController>().playAnimation(animatorEnum.enemy_attack, _currentStateMachine.GetComponent<enemy>().getEnemyName(), _attackCounter + 1, direction);
                _currentStateMachine.setNextState(new groundEnemyComboState(_currentStateMachine.GetComponent<enemy>().getTimes()[_attackCounter + 1], _attackCounter + 1));
                base.onExit();
            }
        }
        //if (_currentStateMachine.GetComponent<enemy>().getHurtbox().transform.position.x < config.getPlayer().GetComponent<combatController>().getHurtbox().transform.position.x)
        if (drawRightRay())
        {
            if (!_currentStateMachine.GetComponent<enemy>().getIsLookingRight())
            {
                if (GetComponent<boss>() != null)
                {
                    _currentStateMachine.setNextState(new enemyChaseState());
                    base.onExit();
                }
                else
                {
                    _currentStateMachine.setNextState(new idleEnemyState());
                    base.onExit();
                }
            }
        }
        //if (_currentStateMachine.GetComponent<enemy>().getHurtbox().transform.position.x > config.getPlayer().GetComponent<combatController>().getHurtbox().transform.position.x)
        if (drawLeftRay())
        {
            if (_currentStateMachine.GetComponent<enemy>().getIsLookingRight())
            {
                if (GetComponent<boss>() != null)
                {
                    _currentStateMachine.setNextState(new enemyChaseState());
                    base.onExit();
                }
                else
                {
                    _currentStateMachine.setNextState(new idleEnemyState());
                    base.onExit();
                }
            }
        }


    }
    private bool drawRay(Vector2 direction)
    {
        int step = 5;
        float distanceBetweenRays = (_currentStateMachine.GetComponent<enemy>().transform.position.x +
                                    (_currentStateMachine.GetComponent<enemy>().getHurtbox().GetComponent<BoxCollider2D>().size.x / 2)) -
                                    (_currentStateMachine.GetComponent<enemy>().transform.position.x -
                                    (_currentStateMachine.GetComponent<enemy>().getHurtbox().GetComponent<BoxCollider2D>().size.x / 2));
        distanceBetweenRays = _currentStateMachine.GetComponent<enemy>().getHurtbox().GetComponent<BoxCollider2D>().size.y / step;

        float rayDistance = _currentStateMachine.GetComponent<enemy>().getAttackRange();
        bool rayHit = false;
        //Trazamos todos los rayos
        for (int i = 0; i <= step; i++)
        {
            //Calculamos la posición inicial de la que parte el rayo
            Vector3 initialPos = new Vector3(0, 0, 0);

            if (direction == Vector2.left)
            {
                initialPos = new Vector3(_currentStateMachine.GetComponent<enemy>().transform.position.x - (_currentStateMachine.GetComponent<enemy>().getHurtbox().GetComponent<BoxCollider2D>().size.x / 2) -
                                        (Mathf.Abs(_currentStateMachine.GetComponent<enemy>().getHurtbox().GetComponent<BoxCollider2D>().offset.x / 2)),
                                         _currentStateMachine.GetComponent<enemy>().transform.position.y + (_currentStateMachine.GetComponent<enemy>().getHurtbox().GetComponent<BoxCollider2D>().size.y / 2) -
                                        (Mathf.Abs(_currentStateMachine.GetComponent<enemy>().getHurtbox().GetComponent<BoxCollider2D>().offset.y / 2)),
                                         1.0f);
            }
            else if (direction == Vector2.right)
            {
                initialPos = new Vector3(_currentStateMachine.GetComponent<enemy>().transform.position.x + (_currentStateMachine.GetComponent<enemy>().getHurtbox().GetComponent<BoxCollider2D>().size.x / 2) +
                                       (Mathf.Abs(_currentStateMachine.GetComponent<enemy>().getHurtbox().GetComponent<BoxCollider2D>().offset.x / 2)),
                                        _currentStateMachine.GetComponent<enemy>().transform.position.y + (_currentStateMachine.GetComponent<enemy>().getHurtbox().GetComponent<BoxCollider2D>().size.y / 2) -
                                        (Mathf.Abs(_currentStateMachine.GetComponent<enemy>().getHurtbox().GetComponent<BoxCollider2D>().offset.y / 2)),
                                        1.0f);
            }
            Vector3 initialRayPosition = new Vector3(initialPos.x, initialPos.y - (distanceBetweenRays * i));

            Debug.DrawRay(initialRayPosition, direction, Color.red);

            //Trazamos cada rayo
            RaycastHit2D hit = Physics2D.Raycast(initialRayPosition, direction, rayDistance, _currentStateMachine.GetComponent<enemy>().getHurtboxLayer());
            if (hit)
            {
                rayHit = true;
            }
        }
        return rayHit;
    }

    private bool drawLeftRay()
    {
        return drawRay(Vector2.left);
    }
    private bool drawRightRay()
    {
        return drawRay(Vector2.right);
    }
}
