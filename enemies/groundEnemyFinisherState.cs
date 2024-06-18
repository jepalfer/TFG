using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// groundEnemyFinisherState es una clase que representa el estado del último golpe de un combo de un enemigo.
/// </summary>
public class groundEnemyFinisherState : enemyBaseCombatState
{
    /// <summary>
    /// Constructor con parámetros de la clase que asigna un tiempo y contador.
    /// </summary>
    /// <param name="time">El tiempo asignado a <see cref="enemyBaseCombatState._attackTime"/>.</param>
    /// <param name="counter">El contador asignado a <see cref="enemyBaseCombatState._attackCounter"/></param>
    public groundEnemyFinisherState(float time, int counter) : base(time, counter) { }

    /// <summary>
    /// Método que termina de implementar <see cref="enemyBaseCombatState.onEnter(enemyStateMachine)"/>.
    /// </summary>
    /// <param name="_stateMachine">La máquina de estados actual.</param>
    public override void onEnter(enemyStateMachine _stateMachine)
    {
        base.onEnter(_stateMachine);
        //Debug.Log("entro finisher");
        AnimatorClipInfo[] clipInfo = _currentStateMachine.GetComponent<Animator>().GetCurrentAnimatorClipInfo(0);
        _currentStateMachine.GetComponent<Animator>().SetFloat("attackSpeed", clipInfo[0].clip.length / (_attackTime));
        _currentStateMachine.GetComponent<enemy>().getHitbox().GetComponent<enemyHitController>().setHasHitPlayer(false);
        _currentStateMachine.GetComponent<enemySFXController>().playAttackSFX();
    }

    /// <summary>
    /// Método que termina de implementar <see cref="enemyBaseCombatState.onUpdate()"/>.
    /// Si estamos fuera del rango de ataque cambia de estado, si no se queda quieto. Ver <see cref="enemyChaseState"/> y <see cref="idleEnemyState"/> para más información.
    /// </summary>
    public override void onUpdate()
    {
        base.onUpdate();
        if (_time >= _attackTime)
        {
            //No estamos en rango de ataque

            //float distance = Vector3.Distance(GetComponent<enemy>().getHurtbox().transform.position,
            //                                  config.getPlayer().GetComponent<combatController>().getHurtbox().transform.position);
            if (!drawLeftRay() && !drawRightRay())
            {
                //GetComponent<enemy>().getHitbox().GetComponent<BoxCollider2D>().enabled = false;
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
            else //Estamos en rango de ataque
            {
                _currentStateMachine.setNextState(new idleEnemyState());
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
