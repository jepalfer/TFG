using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// idleEnemyState es una clase que representa un estado del enemigo en el que no hace nada.
/// </summary>
public class idleEnemyState : enemyState
{
    /// <summary>
    /// Distancia máxima para escuchar el sfx de caminar.
    /// </summary>
    private float _maxHearingDistance = 20f;

    /// <summary>
    /// Distancia mínima para escuchar el sfx de caminar.
    /// </summary>
    private float _minHearingDistance = 5f;

    /// <summary>
    /// Tiempo que debe durar la animación idle.
    /// </summary>
    private float _idleTime;

    /// <summary>
    /// Tiempo que dura la animación idle cuando nos detecta.
    /// </summary>
    private float _idleTimeAttack;
    /// <summary>
    /// Método que implementa <see cref="enemyState.onEnter(enemyStateMachine)"/>.
    /// </summary>
    /// <param name="_stateMachine">La máquina de estados actual.</param>
    public override void onEnter(enemyStateMachine _stateMachine)
    {
        base.onEnter(_stateMachine);
        _idleTime = 1.5f;
        _idleTimeAttack = 1f;
        //Debug.Log("hola");
        //_stateMachine.getAnimator().SetTrigger("Idle");

    }
    /// <summary>
    /// Método que se ejecuta cada frame para actualizar la lógica del estado.
    /// </summary>
    public override void onUpdate()
    {
        Debug.Log("cucu");
        base.onUpdate();
        animatorEnum direction = _currentStateMachine.GetComponent<enemy>().getIsLookingRight() ? animatorEnum.back : animatorEnum.front;
        AnimatorClipInfo[] clipInfo = _currentStateMachine.GetComponent<Animator>().GetCurrentAnimatorClipInfo(0);
        AnimatorStateInfo stateInfo = _currentStateMachine.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0); 
        float distance = Vector3.Distance(GetComponent<enemy>().getHurtbox().transform.position, 
                                          config.getPlayer().GetComponent<combatController>().getHurtbox().transform.position);

        //Si los 2 puntos de la ruta existen
        //Debug.Log("clip => " + clipInfo[0].clip.name);
        Debug.Log("estoy dentro" + _currentStateMachine.GetComponent<enemy>().getEnemyID());

        if (drawLeftRay() && GetComponent<enemy>().getIsLookingRight() && !GetComponent<enemy>().getIsFlipping())
        {
            _currentStateMachine.GetComponent<enemyAnimatorController>().playAnimation(animatorEnum.enemy_idle, _currentStateMachine.GetComponent<enemy>().getEnemyName(), direction);
            GetComponent<Animator>().SetFloat("idleSpeed", _currentStateMachine.GetComponent<enemy>().getIdleAnim().length / _idleTime);
            _currentStateMachine.GetComponent<enemy>().flipInTime(_idleTime);
        }
        else if (drawRightRay() && !GetComponent<enemy>().getIsLookingRight() && !GetComponent<enemy>().getIsFlipping())
        {
            _currentStateMachine.GetComponent<enemyAnimatorController>().playAnimation(animatorEnum.enemy_idle, _currentStateMachine.GetComponent<enemy>().getEnemyName(), direction);
            GetComponent<Animator>().SetFloat("idleSpeed", _currentStateMachine.GetComponent<enemy>().getIdleAnim().length / _idleTime);
            _currentStateMachine.GetComponent<enemy>().flipInTime(_idleTime);
        }
        else if (clipInfo[0].clip.name.Contains(animatorEnum.enemy_attack + "_" + _currentStateMachine.GetComponent<enemy>().getEnemyName()))
        {
            if (stateInfo.normalizedTime >= 1.0f)
            {
                _currentStateMachine.GetComponent<enemyAnimatorController>().playAnimation(animatorEnum.enemy_seath, _currentStateMachine.GetComponent<enemy>().getEnemyName(), 0, direction);
            }
        }
        else if (clipInfo[0].clip.name == _currentStateMachine.GetComponent<enemyAnimatorController>().getAnimationName(animatorEnum.enemy_unseath, _currentStateMachine.GetComponent<enemy>().getEnemyName(), direction))
        {
            if ((_currentStateMachine.GetComponent<enemy>().getHurtbox().transform.position.x > config.getPlayer().GetComponent<combatController>().getHurtbox().transform.position.x &&
                _currentStateMachine.GetComponent<enemy>().getIsLookingRight()) ||
                (_currentStateMachine.GetComponent<enemy>().getHurtbox().transform.position.x < config.getPlayer().GetComponent<combatController>().getHurtbox().transform.position.x &&
                !_currentStateMachine.GetComponent<enemy>().getIsLookingRight()))
            {
                if (stateInfo.normalizedTime >= 1.0f)
                {
                    _currentStateMachine.GetComponent<enemyAnimatorController>().playAnimation(animatorEnum.enemy_seath, _currentStateMachine.GetComponent<enemy>().getEnemyName(), 0, direction);
                }
                else
                {
                    _currentStateMachine.GetComponent<enemyAnimatorController>().playAnimation(animatorEnum.enemy_unseath, _currentStateMachine.GetComponent<enemy>().getEnemyName(), direction);
                }
            }
        }
        else if (clipInfo[0].clip.name == _currentStateMachine.GetComponent<enemyAnimatorController>().getAnimationName(animatorEnum.enemy_seath, _currentStateMachine.GetComponent<enemy>().getEnemyName(), 0, direction))
        {
            if (stateInfo.normalizedTime >= 1.0f)
            {
                _currentStateMachine.GetComponent<enemyAnimatorController>().playAnimation(animatorEnum.enemy_move, _currentStateMachine.GetComponent<enemy>().getEnemyName(), direction);
            }
        }
        else if (_currentStateMachine.GetComponent<enemy>().getPointA() != null && _currentStateMachine.GetComponent<enemy>().getPointB() != null &&
                 clipInfo[0].clip.name != _currentStateMachine.GetComponent<enemyAnimatorController>().getAnimationName(animatorEnum.enemy_unseath, _currentStateMachine.GetComponent<enemy>().getEnemyName(), direction))
        {
            //Obtenemos la posición del enemigo
            Vector3 position = _currentStateMachine.GetComponent<enemy>().transform.position;

            //Debug.Log("me estoy moviendo");

            //Si no se está girando movemos según la orientación del enemigo
            if (!_currentStateMachine.GetComponent<enemy>().getIsFlipping())
            {
                if (_currentStateMachine.GetComponent<enemy>().getIsLookingRight())
                {
                    position.x += _currentStateMachine.GetComponent<enemyController>().getSpeed() * Time.deltaTime;
                    _currentStateMachine.GetComponent<enemyAnimatorController>().playAnimation(animatorEnum.enemy_move, _currentStateMachine.GetComponent<enemy>().getEnemyName(), direction); 
                }
                else
                {
                    position.x -= _currentStateMachine.GetComponent<enemyController>().getSpeed() * Time.deltaTime; 
                    _currentStateMachine.GetComponent<enemyAnimatorController>().playAnimation(animatorEnum.enemy_move, _currentStateMachine.GetComponent<enemy>().getEnemyName(), direction);
                }
            }

            //Si llega al punto de la izquierda
            if (Mathf.Abs(_currentStateMachine.GetComponent<enemy>().transform.position.x -
                          _currentStateMachine.GetComponent<enemy>().getPointA().transform.position.x) <= 0.5f &&
                          !_currentStateMachine.GetComponent<enemy>().getIsLookingRight() && !_currentStateMachine.GetComponent<enemy>().getIsFlipping())
            {
                _currentStateMachine.GetComponent<AudioSource>().Stop();
                _currentStateMachine.GetComponent<enemyAnimatorController>().playAnimation(animatorEnum.enemy_idle, _currentStateMachine.GetComponent<enemy>().getEnemyName(), direction);
                GetComponent<Animator>().SetFloat("idleSpeed", _currentStateMachine.GetComponent<enemy>().getIdleAnim().length / _idleTime); 
                _currentStateMachine.GetComponent<enemy>().flipInTime(_idleTime);

            }

            //Si llega al punto de la derecha
            if (Mathf.Abs(_currentStateMachine.GetComponent<enemy>().transform.position.x - 
                          _currentStateMachine.GetComponent<enemy>().getPointB().transform.position.x) <= 0.5f &&
                          _currentStateMachine.GetComponent<enemy>().getIsLookingRight() && !_currentStateMachine.GetComponent<enemy>().getIsFlipping())
            {
                _currentStateMachine.GetComponent<AudioSource>().Stop();
                _currentStateMachine.GetComponent<enemyAnimatorController>().playAnimation(animatorEnum.enemy_idle, _currentStateMachine.GetComponent<enemy>().getEnemyName(), direction);
                GetComponent<Animator>().SetFloat("idleSpeed", _currentStateMachine.GetComponent<enemy>().getIdleAnim().length / _idleTime);
                _currentStateMachine.GetComponent<enemy>().flipInTime(_idleTime);
            }

            //Movemos al enemigo
            _currentStateMachine.GetComponent<enemy>().transform.position = position;
        }
        //Debug.Log("left ray => " + drawLeftRay() + " righ ray => " + drawRightRay());
        //Si estamos en rango de ataque
        if (drawLeftRay() || drawRightRay())
        {
            //if (GetComponent<enemy>().getHurtbox().transform.position.x < config.getPlayer().GetComponent<combatController>().getHurtbox().transform.position.x)
            if (drawRightRay())
            {
                if (clipInfo[0].clip.name.Contains(animatorEnum.enemy_seath.ToString()))
                {
                    if (stateInfo.normalizedTime >= 1.0f)
                    {
                        _currentStateMachine.GetComponent<enemyAnimatorController>().playAnimation(animatorEnum.enemy_idle, _currentStateMachine.GetComponent<enemy>().getEnemyName(), direction);
                        GetComponent<Animator>().SetFloat("idleSpeed", _currentStateMachine.GetComponent<enemy>().getIdleAnim().length / _idleTimeAttack);
                        _currentStateMachine.GetComponent<enemy>().flipInTime(_idleTimeAttack);
                    }
                }
                //Si está mirando en la otra dirección lo giramos
                if (!GetComponent<enemy>().getIsLookingRight() && !_currentStateMachine.GetComponent<enemy>().getIsFlipping())
                {
                    if (clipInfo[0].clip.name == _currentStateMachine.GetComponent<enemyAnimatorController>().getAnimationName(animatorEnum.enemy_move, _currentStateMachine.GetComponent<enemy>().getEnemyName(), direction))
                    {
                        _currentStateMachine.GetComponent<enemyAnimatorController>().playAnimation(animatorEnum.enemy_idle, _currentStateMachine.GetComponent<enemy>().getEnemyName(), direction); GetComponent<Animator>().SetFloat("idleSpeed", _currentStateMachine.GetComponent<enemy>().getIdleAnim().length / _idleTimeAttack);
                        GetComponent<Animator>().SetFloat("idleSpeed", _currentStateMachine.GetComponent<enemy>().getIdleAnim().length / _idleTimeAttack);
                        _currentStateMachine.GetComponent<enemy>().flipInTime(_idleTimeAttack);
                    }
                }
                else //Si no, hacemos que nos ataque
                {
                    //clipInfo = _currentStateMachine.GetComponent<Animator>().GetCurrentAnimatorClipInfo(0);
                    if (clipInfo[0].clip.name == _currentStateMachine.GetComponent<enemyAnimatorController>().getAnimationName(animatorEnum.enemy_idle, _currentStateMachine.GetComponent<enemy>().getEnemyName(), direction) ||
                        clipInfo[0].clip.name == _currentStateMachine.GetComponent<enemyAnimatorController>().getAnimationName(animatorEnum.enemy_move, _currentStateMachine.GetComponent<enemy>().getEnemyName(), direction))
                    {
                        //Debug.Log("DesenvainoI");
                        _currentStateMachine.GetComponent<enemyAnimatorController>().playAnimation(animatorEnum.enemy_unseath, _currentStateMachine.GetComponent<enemy>().getEnemyName(), direction);
                    }
                    else if (clipInfo[0].clip.name == _currentStateMachine.GetComponent<enemyAnimatorController>().getAnimationName(animatorEnum.enemy_unseath, _currentStateMachine.GetComponent<enemy>().getEnemyName(), direction))
                    {
                        if (stateInfo.normalizedTime >= 1.0f)
                        {
                            _currentStateMachine.GetComponent<enemyAnimatorController>().playAnimation(animatorEnum.enemy_attack, _currentStateMachine.GetComponent<enemy>().getEnemyName(), 0, direction);
                            _currentStateMachine.setNextState(new groundEnemyEntryState(GetComponent<enemy>().getTimes()[0], 0));
                            base.onExit();
                        }
                    }
                }

            }
            //else if (GetComponent<enemy>().getHurtbox().transform.position.x > config.getPlayer().GetComponent<combatController>().getHurtbox().transform.position.x)
            else if (drawLeftRay())
            {
                if (clipInfo[0].clip.name.Contains(animatorEnum.enemy_seath.ToString()))
                {
                    if (stateInfo.normalizedTime >= 1.0f)
                    {
                        _currentStateMachine.GetComponent<enemyAnimatorController>().playAnimation(animatorEnum.enemy_idle, _currentStateMachine.GetComponent<enemy>().getEnemyName(), direction);
                        GetComponent<Animator>().SetFloat("idleSpeed", _currentStateMachine.GetComponent<enemy>().getIdleAnim().length / _idleTimeAttack);
                        _currentStateMachine.GetComponent<enemy>().flipInTime(_idleTimeAttack);
                    }
                }
                //Si está mirando en la otra dirección lo giramos
                if (GetComponent<enemy>().getIsLookingRight() && !_currentStateMachine.GetComponent<enemy>().getIsFlipping())
                {
                    if (clipInfo[0].clip.name == _currentStateMachine.GetComponent<enemyAnimatorController>().getAnimationName(animatorEnum.enemy_move, _currentStateMachine.GetComponent<enemy>().getEnemyName(), direction))
                    {
                        _currentStateMachine.GetComponent<enemyAnimatorController>().playAnimation(animatorEnum.enemy_idle, _currentStateMachine.GetComponent<enemy>().getEnemyName(), direction); GetComponent<Animator>().SetFloat("idleSpeed", _currentStateMachine.GetComponent<enemy>().getIdleAnim().length / _idleTimeAttack);
                        GetComponent<Animator>().SetFloat("idleSpeed", _currentStateMachine.GetComponent<enemy>().getIdleAnim().length / _idleTimeAttack);
                        _currentStateMachine.GetComponent<enemy>().flipInTime(_idleTimeAttack);
                    }
                }
                else //Si no, hacemos que nos ataque
                {
                    clipInfo = _currentStateMachine.GetComponent<Animator>().GetCurrentAnimatorClipInfo(0);
                    //Debug.Log(clipInfo[0].clip.name);
                    if (clipInfo[0].clip.name == _currentStateMachine.GetComponent<enemyAnimatorController>().getAnimationName(animatorEnum.enemy_idle, _currentStateMachine.GetComponent<enemy>().getEnemyName(), direction) ||
                        clipInfo[0].clip.name == _currentStateMachine.GetComponent<enemyAnimatorController>().getAnimationName(animatorEnum.enemy_move, _currentStateMachine.GetComponent<enemy>().getEnemyName(), direction))
                    {
                        _currentStateMachine.GetComponent<enemyAnimatorController>().playAnimation(animatorEnum.enemy_unseath, _currentStateMachine.GetComponent<enemy>().getEnemyName(), direction);
                    }
                    else if (clipInfo[0].clip.name == _currentStateMachine.GetComponent<enemyAnimatorController>().getAnimationName(animatorEnum.enemy_unseath, _currentStateMachine.GetComponent<enemy>().getEnemyName(), direction))
                    {
                        if (stateInfo.normalizedTime >= 1.0f)
                        {
                            _currentStateMachine.GetComponent<enemyAnimatorController>().playAnimation(animatorEnum.enemy_attack, _currentStateMachine.GetComponent<enemy>().getEnemyName(), 0, direction);
                            _currentStateMachine.setNextState(new groundEnemyEntryState(GetComponent<enemy>().getTimes()[0], 0));
                            base.onExit();

                        }
                    }
                }
            }

        }
        else
        {
            if ((_currentStateMachine.GetComponent<Animator>().GetCurrentAnimatorClipInfo(0)[0].clip.name !=
                _currentStateMachine.GetComponent<enemyAnimatorController>().getAnimationName
                (animatorEnum.enemy_idle, _currentStateMachine.GetComponent<enemy>().getEnemyName(), direction)) &&
                (_currentStateMachine.GetComponent<Animator>().GetCurrentAnimatorClipInfo(0)[0].clip.name !=
                _currentStateMachine.GetComponent<enemyAnimatorController>().getAnimationName
                (animatorEnum.enemy_move, _currentStateMachine.GetComponent<enemy>().getEnemyName(), direction)) &&
                (_currentStateMachine.GetComponent<Animator>().GetCurrentAnimatorClipInfo(0)[0].clip.name !=
                _currentStateMachine.GetComponent<enemyAnimatorController>().getAnimationName
                (animatorEnum.enemy_death, _currentStateMachine.GetComponent<enemy>().getEnemyName(), direction)))
            {
                if (stateInfo.IsName(_currentStateMachine.GetComponent<enemyAnimatorController>().getAnimationName(animatorEnum.enemy_unseath, _currentStateMachine.GetComponent<enemy>().getEnemyName(), direction)))
                {
                    if (stateInfo.normalizedTime >= 1.0f)
                    {
                        _currentStateMachine.GetComponent<enemyAnimatorController>().playAnimation(animatorEnum.enemy_seath, _currentStateMachine.GetComponent<enemy>().getEnemyName(), 0, direction);
                    }
                }
                _currentStateMachine.GetComponent<Animator>().SetFloat("attackSpeed", 2 * (_currentStateMachine.GetComponent<Animator>().GetCurrentAnimatorClipInfo(0)[0].clip.length / _idleTimeAttack));
            }
        }

        if (distance <= _maxHearingDistance)
        {
            if (stateInfo.IsName(_currentStateMachine.GetComponent<enemyAnimatorController>().getAnimationName(animatorEnum.enemy_move, _currentStateMachine.GetComponent<enemy>().getEnemyName(), direction)))
            {
                _currentStateMachine.GetComponent<AudioSource>().volume = Mathf.Clamp01(1 - (distance - _minHearingDistance) / (_maxHearingDistance - _minHearingDistance));
                if (!_currentStateMachine.GetComponent<AudioSource>().isPlaying)
                {
                    _currentStateMachine.GetComponent<enemySFXController>().playWalkingSFX();
                }
            }
        }
        else
        {
            _currentStateMachine.GetComponent<AudioSource>().Stop();
        }

        if (UIController.getIsInPauseUI() || UIController.getIsInShopUI() || UIController.getIsInInventoryUI() || UIController.getIsInEquippingSkillUI() ||
            UIController.getIsInAdquireSkillUI() || UIController.getIsInShopUI() || UIController.getIsInLevelUpUI() || UIController.getIsInLevelUpWeaponUI() ||
            UIController.getIsInStateUI() || bonfireBehaviour.getIsInBonfireMenu())
        {
            _currentStateMachine.GetComponent<AudioSource>().Stop();
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