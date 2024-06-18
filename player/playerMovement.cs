using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

/// <summary>
/// playerMovement es una clase que se encarga de manejar la lógica del movimiento del jugador.
/// </summary>
public class playerMovement : MonoBehaviour
{
    /// <summary>
    /// Referencia a la cámara asociada al jugador.
    /// </summary>
    [SerializeField] private GameObject _camera;
    //------------------------------------------- PLAYER COMPONENTS --------------------------------------------------------
    
    /// <summary>
    /// Referencia al RigidBody del jugador.
    /// </summary>
    [SerializeField] private Rigidbody2D _rb;

    /// <summary>
    /// Referencia a la BoxCollider del jugador.
    /// </summary>
    [SerializeField] private BoxCollider2D _bc;
    //----------------------------------------------------------------------------------------------------------------------
    
    /// <summary>
    /// Flag booleano para controlar la cámara cuando nos agachamos.
    /// </summary>
    [Header("Crouching")]
    [SerializeField] private bool _isLookingDown = false;

    /// <summary>
    /// Variable para guardar la gravedad inicial para poder hacer cambios sin afectar.
    /// </summary>
    [SerializeField] private float _g0;

    /// <summary>
    /// Distancia de salto máximo.
    /// </summary>
    [Header("Jump")]
    [SerializeField] private const float _JUMP_HEIGHT = 4f;

    /// <summary>
    /// Altura que saltamos en cada bucle del update.
    /// </summary>
    [SerializeField] private float _jump = 5f;

    /// <summary>
    /// Contador para ir sumando la distancia saltada y que no nos pasemos.
    /// </summary>
    [SerializeField] private float _distanceJumped = 0f;

    /// <summary>
    /// Flag booleano para saber cuándo podemos saltar.
    /// </summary>
    [SerializeField] private bool _canJump = true;

    /// <summary>
    /// Flag booleano para indicar si estamos saltando.
    /// </summary>
    [SerializeField] private bool _isJumping = false;

    /// <summary>
    /// Flag booleano para indicar si estábamos saltando.
    /// </summary>
    [SerializeField] private bool _wasJumping = false;

    /// <summary>
    /// Flag booleano que activa el doble salto.
    /// </summary>
    [SerializeField] private bool _canDoubleJump = false;

    /// <summary>
    /// Variable que almacena los saltos dados para saber si podemos o no hacer un doble salto.
    /// </summary>
    [SerializeField] private int _jumpsDone = 0;

    /// <summary>
    /// Variable que almacena el tiempo máximo que dura una esquiva.
    /// </summary>
    [Header("Dodge")]
    [SerializeField] private float _rollTime;

    /// <summary>
    /// Distancia que se recorre en una esquiva.
    /// </summary>
    private float _rollDistance;

    /// <summary>
    /// Flag booleana que indica si después de realizar una acción podíamos esquivar antes.
    /// </summary>
    [SerializeField] private bool _couldRoll;

    /// <summary>
    /// Flag booleano que indica si estamos esquivando.
    /// </summary>
    [SerializeField] private bool _isDodging = false;

    /// <summary>
    /// Tiempo límite que dura la esquiva..
    /// </summary>
    [SerializeField] private float _timeLimit;

    /// <summary>
    /// Distancia que se recorre por segundo en la esquiva.
    /// </summary>
    [SerializeField] private float _roll;

    /// <summary>
    /// Flag booleano para indicar si podemos esquivar.
    /// </summary>
    [SerializeField] private bool _canRoll = true;

    /// <summary>
    /// Flag booleano para saber si podemos movernos.
    /// </summary>
    [SerializeField] private bool _canMove = true;

    /// <summary>
    /// Flag booleano para saber si hemos hecho una esquiva.
    /// </summary>
    [SerializeField] private bool _hasRolled = false;

    /// <summary>
    /// Valor que almacena la velocidad horizontal del personaje.
    /// </summary>
    [Header("Movement")]
    [SerializeField] private float _HSpeed;

    /// <summary>
    /// Valor que almacena la velocidad vertical del personaje.
    /// </summary>
    [SerializeField] private float _VSpeed;

    /// <summary>
    /// Valor que almacena la velocidad de movimiento del personaje.
    /// </summary>
    [SerializeField] private float _movementSpeed = 6f;

    /// <summary>
    /// Valor que almacena la velocidad de escalada del personaje.
    /// </summary>
    [SerializeField] private float _climbingSpeed = 4.5f;

    /// <summary>
    /// Valor que almacena la velocidad de caída del personaje.
    /// </summary>
    [SerializeField] private float _fallSpeed = 1.5f;

    /// <summary>
    /// Flag booleano para saber a qué lado está mirando el personaje.
    /// </summary>
    [SerializeField] private bool _facingLeft = true;

    /// <summary>
    /// Flag booleano para saber si estamos sin hacer nada.
    /// </summary>
    [SerializeField] private bool _idle = true;

    /// <summary>
    /// Variable que almacena el tiempo límite de pulsar hacia abajo o arriba para que la cámara se mueva en esa dirección.
    /// </summary>
    [SerializeField] private const float _TIMELIMIT = 0.75f;

    /// <summary>
    /// Valor que almacena la dirección en la que nos movemos.
    /// </summary>
    private int _direction = 0;

    /// <summary>
    /// Timer para controlar que la cámara se mueva hacia abajo o arriba.
    /// </summary>
    [SerializeField] private float _cameraTimer = 0f;

    /// <summary>
    /// Flag booleano para saber si podemos escalar.
    /// </summary>
    [SerializeField] private bool _canClimb = false;

    /// <summary>
    /// Flag booleano que indica si podíamos escalar.
    /// </summary>
    [SerializeField] private bool _couldClimb = false;

    /// <summary>
    /// Flag booleano que indica si estábamos escalando
    /// </summary>
    [SerializeField] private bool _wasClimbing = false;

    /// <summary>
    /// Valor que almacena el ratio en el que aumenta la velocidad de esquiva al subir 10 niveles.
    /// </summary>
    [SerializeField] private float _dashTimeMultiplier;

    /// <summary>
    /// Clip para calcular la velocidad de la animación de rodar.
    /// </summary>
    [SerializeField] private AnimationClip _rollAnim;

    /// <summary>
    /// Clip para calcular la velocidad de la animación de rodar en idle.
    /// </summary>
    [SerializeField] private AnimationClip _backDashAnim;

    lastBonfireData _data;

    /// <summary>
    /// Primer método que se ejecuta al iniciar el script.
    /// </summary>
    void Awake()
    {
        //Obtenemos la referencia al RigidBody y el Animator
        _rb = GetComponent<Rigidbody2D>();

        //Obtenemos la referencia al collider que no es trigger
        _bc =  GetComponent<BoxCollider2D>();

        //Configuramos algunas variables
        _g0 = _rb.gravityScale;
        _dashTimeMultiplier = 5f;
        config.setPlayer(gameObject);
    }

    /// <summary>
    /// Método que se ejecuta al inicio tras <see cref="Awake"/>.
    /// </summary>
    private void Start()
    {
        //if (!_facingLeft)
        //{
        //    GetComponent<SpriteRenderer>().flipX = true;
        //}

        if (_facingLeft)
        {
            GetComponent<playerAnimatorController>().playAnimation(animatorEnum.player_idle, 0, animatorEnum.front);
        }
        else
        {
            GetComponent<playerAnimatorController>().playAnimation(animatorEnum.player_idle, 0, animatorEnum.back);
        }

        _data = saveSystem.loadLastBonfireData();
        if (_data != null)
        {
            if (_data.getHasRested())
            {
                animatorEnum direction = config.getPlayer().GetComponent<playerMovement>().getIsFacingLeft() ? animatorEnum.front : animatorEnum.back;
                //Debug.Log(direction);
                GetComponent<playerAnimatorController>().playAnimation(animatorEnum.player_get_up, direction);
                saveSystem.saveLastBonfireData(false);
            }
        }

    }

    /// <summary>
    /// Método que se ejecuta cada frame para actualizar la lógica.
    /// </summary>
    void Update()
    {
        //Forzamos a que el jugador caiga si pausamos
        if (UIController.getIsInPauseUI())
        {
            _distanceJumped = _JUMP_HEIGHT;
        }


        AnimatorStateInfo stateInfo = GetComponent<Animator>().GetCurrentAnimatorStateInfo(0);
        animatorEnum direction = _facingLeft ? animatorEnum.front : animatorEnum.back;
        //Mientras no estemos en ningún menú del juego
        if (!UIController.getIsInPauseUI() && !UIController.getIsInEquippingSkillUI() && !UIController.getIsInLevelUpUI() && !UIController.getIsInAdquireSkillUI() && 
            !UIController.getIsInLevelUpWeaponUI() && !UIController.getIsInInventoryUI() && !UIController.getIsInShopUI() && !bonfireBehaviour.getIsInBonfireMenu() &&
            !GetComponent<downWardBlowController>().getIsInDownWardBlow() && !GetComponent<combatController>().getIsAttacking())
        { 
       
            //No permitimos movimiento con las teclas izquierda y derecha del d-pad y del teclado
            if ((!inputManager.GetKey(inputEnum.right) && !inputManager.GetKey(inputEnum.left)))
            {
                //Miramos la dirección horizontal
                if (Input.GetAxisRaw("Horizontal") < 0)
                {
                    _direction = -1;
                }
                else if (Input.GetAxisRaw("Horizontal") > 0)
                {
                    _direction = 1;
                }
                else
                {
                    _direction = 0;
                }

                //Obtenemos la velocidad horizontal
                _HSpeed = _direction * _movementSpeed;
                _idle = !_isDodging && _HSpeed == 0;


                //Damos la vuelta si estábamos mirando hacia derecha y vamos a izquierda o viceversa
                if (((_HSpeed < 0 && !_facingLeft) || (_HSpeed > 0 && _facingLeft)) && _canMove)
                {
                    flip();
                }

                if (GetComponent<Animator>().GetCurrentAnimatorClipInfo(0)[0].clip.name ==
                    GetComponent<playerAnimatorController>().getAnimationName(animatorEnum.player_get_up, direction))
                {
                    if (stateInfo.normalizedTime >= 1.0f)
                    {
                        GetComponent<playerAnimatorController>().playAnimation(animatorEnum.player_idle, 0, direction);
                    }
                    else
                    {
                        GetComponent<playerAnimatorController>().playAnimation(animatorEnum.player_get_up, direction);
                    }
                }
                else if (_idle && GetComponent<collisionController>().getIsOnPlatform() && !bonfireBehaviour.getIsInBonfireMenu() && !_isJumping)
                {
                    if (stateInfo.IsName(GetComponent<playerAnimatorController>().getAnimationName(animatorEnum.player_idle, 0, direction)))
                    {
                        if (stateInfo.normalizedTime >= 1.0f)
                        {
                            GetComponent<playerAnimatorController>().playAnimation(animatorEnum.player_idle, 1, direction);
                        }
                        else
                        {
                            GetComponent<playerAnimatorController>().playAnimation(animatorEnum.player_idle, 0, direction);
                        }
                    }
                    else if (stateInfo.IsName(GetComponent<playerAnimatorController>().getAnimationName(animatorEnum.player_idle, 1, direction)))
                    {
                        if (stateInfo.normalizedTime >= 1.0f)
                        {
                            GetComponent<playerAnimatorController>().playAnimation(animatorEnum.player_idle, 2, direction);
                        }
                        else
                        {
                            GetComponent<playerAnimatorController>().playAnimation(animatorEnum.player_idle, 1, direction);
                        }
                    }
                    else if (stateInfo.IsName(GetComponent<playerAnimatorController>().getAnimationName(animatorEnum.player_idle, 2, direction)))
                    {
                        GetComponent<playerAnimatorController>().playAnimation(animatorEnum.player_idle, 2, direction);
                    }
                    else if (GetComponent<collisionController>().getIsOnPlatform() &&
                        !stateInfo.IsName(GetComponent<playerAnimatorController>().getAnimationName(animatorEnum.player_death, direction)))
                    {
                        GetComponent<playerAnimatorController>().playAnimation(animatorEnum.player_idle, 0, direction);
                    }
                }

                if (stateInfo.IsName(GetComponent<playerAnimatorController>().getAnimationName(animatorEnum.player_get_up, direction)))
                {
                    if (stateInfo.normalizedTime >= 1.0f)
                    {
                        GetComponent<playerAnimatorController>().playAnimation(animatorEnum.player_idle, 0, direction);
                    }
                    else
                    {
                        GetComponent<playerAnimatorController>().playAnimation(animatorEnum.player_get_up, direction);
                    }
                }

                if (!_isDodging && !_idle && GetComponent<collisionController>().getIsOnPlatform() && !_isJumping)
                {
                    GetComponent<playerAnimatorController>().playAnimation(animatorEnum.player_run, direction);
                }

                if (_isJumping && !_isDodging)
                {
                    if (stateInfo.IsName(GetComponent<playerAnimatorController>().getAnimationName(animatorEnum.player_jump, direction)))
                    {
                        if (stateInfo.normalizedTime >= 1.0f)
                        {
                            GetComponent<playerAnimatorController>().playAnimation(animatorEnum.player_fall, direction);
                        }
                    }
                    else if (stateInfo.IsName(GetComponent<playerAnimatorController>().getAnimationName(animatorEnum.player_fall, direction)))
                    {
                        GetComponent<playerAnimatorController>().playAnimation(animatorEnum.player_fall, direction);
                    }
                    else
                    {
                        GetComponent<playerAnimatorController>().playAnimation(animatorEnum.player_jump, direction);
                    }
                }

                if (!_isDodging && !GetComponent<collisionController>().getIsOnPlatform() && !_isJumping &&
                    GetComponent<Animator>().GetCurrentAnimatorClipInfo(0)[0].clip.name != GetComponent<playerAnimatorController>().getAnimationName(animatorEnum.player_climb, animatorEnum.up) &&
                    GetComponent<Animator>().GetCurrentAnimatorClipInfo(0)[0].clip.name != GetComponent<playerAnimatorController>().getAnimationName(animatorEnum.player_climb, animatorEnum.down) &&
                    GetComponent<Animator>().GetCurrentAnimatorClipInfo(0)[0].clip.name != GetComponent<playerAnimatorController>().getAnimationName(animatorEnum.player_get_up, direction) &&
                    GetComponent<Rigidbody2D>().velocity.y < 0)
                {
                    GetComponent<playerAnimatorController>().playAnimation(animatorEnum.player_fall, direction);
                }

                if (_canClimb)
                {
                    if (!GetComponent<collisionController>().getIsOnPlatform())
                    {
                        if (inputManager.GetKey(inputEnum.up))
                        {
                            GetComponent<Animator>().speed = 1;
                            if (!stateInfo.IsName(GetComponent<playerAnimatorController>().getAnimationName(animatorEnum.player_climb, animatorEnum.up)))
                            {
                                GetComponent<playerAnimatorController>().playAnimation(animatorEnum.player_climb, animatorEnum.up);
                            }
                        }
                        if (inputManager.GetKeyUp(inputEnum.up))
                        {
                            GetComponent<Animator>().speed = 0;
                        }

                        if (inputManager.GetKey(inputEnum.down))
                        {
                            GetComponent<Animator>().speed = 1;
                            if (!stateInfo.IsName(GetComponent<playerAnimatorController>().getAnimationName(animatorEnum.player_climb, animatorEnum.down)))
                            {
                                GetComponent<playerAnimatorController>().playAnimation(animatorEnum.player_climb, animatorEnum.down);
                            }
                        }
                        if (inputManager.GetKeyUp(inputEnum.down))
                        {
                            GetComponent<Animator>().speed = 0;
                        }
                    }
                }

                //Calculamos la velocidad vertical para las escaleras
                if (Input.GetAxisRaw("Vertical") < 0)
                {
                    _direction = -1;
                }
                else if (Input.GetAxisRaw("Vertical") > 0)
                {
                    _direction = 1;
                }
                else
                {
                    _direction = 0;
                }
                _VSpeed = _direction * _climbingSpeed;
            }
            

            //Podemos esquivar si no hemos esquivado y además no estamos tocando plataforma
            if (!_hasRolled && !GetComponent<collisionController>().getIsOnPlatform())
            {
                _canRoll = true;
            }

            //Si estamos en algo donde podemos saltar y no estamos esquivando
            if (!_isDodging && (GetComponent<collisionController>().getIsOnPlatform()))
            {
                //Si no estamos saltando
                if (!inputManager.GetKey(inputEnum.jump))
                {
                    _wasJumping = false;
                    _jumpsDone = 0;
                    if (!_isDodging)
                    {
                        _canJump = true;
                    }
                }

                //Modificamos variables
                _canRoll = true;
                _couldRoll = true;
                _wasClimbing = false;
                _hasRolled = false;

                //Si estamos en una de las plataformas especiales
                if (GetComponent<collisionController>().getIsOnOneWay() || GetComponent<collisionController>().getIsOnLadderTop())
                {
                    if (_distanceJumped == _JUMP_HEIGHT)
                    {
                        _isJumping = false;
                        _distanceJumped = 0f;
                    }
                }
                else
                {
                    if (!_wasJumping)
                    {
                        _isJumping = false;
                        _distanceJumped = 0f;
                    }
                }

                //Configuramos que podamos atacar
                GetComponent<combatController>().setCanAttack(true);

                if (GetComponent<combatController>().getPrimaryWeapon() != null)
                {
                    GetComponent<combatController>().getPrimaryWeapon().GetComponent<weapon>().setCanAttack(true);
                }
                if (GetComponent<combatController>().getSecundaryWeapon() != null)
                {
                    GetComponent<combatController>().getSecundaryWeapon().GetComponent<weapon>().setCanAttack(true);
                }
            }
            else 
            {
                _cameraTimer = 0f;

                //Si no estamos saltando
                if (!_isJumping)
                {
                    if (!_wasClimbing || _VSpeed != 0)
                    {
                        //Miramos que podamos usar el salto doble
                        if (_canDoubleJump && _jumpsDone == 1 && _wasJumping)
                        {
                            _canJump = true;
                        }
                        else
                        {
                            _canJump = false;
                        }
                    }
                    if (!_wasClimbing && !_isDodging && !GetComponent<combatController>().getIsAttacking())
                    {
                        _rb.gravityScale = _g0 * _fallSpeed;
                    }
                }
                if (_canClimb && _wasClimbing)
                {
                    GetComponent<combatController>().setCanAttack(false);
                    if (GetComponent<combatController>().getPrimaryWeapon() != null)
                    {
                        GetComponent<combatController>().getPrimaryWeapon().GetComponent<weapon>().setCanAttack(false);
                    }
                    if (GetComponent<combatController>().getSecundaryWeapon() != null)
                    {
                        GetComponent<combatController>().getSecundaryWeapon().GetComponent<weapon>().setCanAttack(false);
                    }
                }
            }

            if (!_isDodging && !GetComponent<combatController>().getIsAttacking() && !_wasClimbing)
            {
                _canMove = true;
            }

            if (GetComponent<collisionController>().getHithead() && !_canClimb)
            {
                _canJump = false;
                _isJumping = false;
                _distanceJumped = 0f;
                _rb.gravityScale = _g0;
            }

            //Manejamos el movimiento de la cámara hacia abajo o arriba
            if (GetComponent<collisionController>().getIsOnPlatform() && !GetComponent<collisionController>().getIsOnLadderTop())
            {
                manageLook();
            }

            //Manejamos el movimiento
            if (_canMove)
            {
                manageWalk();
            }

            //Manejamos el salto
            if (!GetComponent<combatController>().getIsAttacking())
            {
                manageJump();
            }

            if (_canClimb && _rb.velocity.y == 0 && !_isDodging && !_isJumping && inputManager.GetKeyUp(inputEnum.jump))
            {
                _rb.gravityScale = _g0 * _fallSpeed;
            }

            //Manejamos escalada
            if (_canClimb && !_isDodging)
            {
                manageClimb();
            }

            //Manejamos esquiva
            if (_canRoll && inputManager.GetKeyDown(inputEnum.roll))
            {
                int rollType = _idle ? 1 : 0;
                GetComponent<playerAnimatorController>().playAnimation(animatorEnum.player_dash, rollType, direction);
                StartCoroutine(manageRoll());
            }
        }
        if (bonfireBehaviour.getIsInBonfireMenu())
        {
            if (_facingLeft)
            {
                if (stateInfo.IsName(GetComponent<playerAnimatorController>().getAnimationName(animatorEnum.player_idle, 1, animatorEnum.front)))
                {
                    if (stateInfo.normalizedTime >= 1.0f)
                    {
                        GetComponent<playerAnimatorController>().playAnimation(animatorEnum.player_idle, 2, animatorEnum.front);
                    }
                    else
                    {
                        GetComponent<playerAnimatorController>().playAnimation(animatorEnum.player_idle, 1, animatorEnum.front);
                    }
                }
                else if (stateInfo.IsName(GetComponent<playerAnimatorController>().getAnimationName(animatorEnum.player_idle, 2, animatorEnum.front)))
                {
                    GetComponent<playerAnimatorController>().playAnimation(animatorEnum.player_idle, 2, animatorEnum.front);
                }
            }
            else
            {
                if (stateInfo.IsName(GetComponent<playerAnimatorController>().getAnimationName(animatorEnum.player_idle, 1, animatorEnum.back)))
                {
                    if (stateInfo.normalizedTime >= 1.0f)
                    {
                        GetComponent<playerAnimatorController>().playAnimation(animatorEnum.player_idle, 2, animatorEnum.back);
                    }
                    else
                    {
                        GetComponent<playerAnimatorController>().playAnimation(animatorEnum.player_idle, 1, animatorEnum.back);
                    }
                }
                else if (stateInfo.IsName(GetComponent<playerAnimatorController>().getAnimationName(animatorEnum.player_idle, 2, animatorEnum.back)))
                {
                    GetComponent<playerAnimatorController>().playAnimation(animatorEnum.player_idle, 2, animatorEnum.back);
                }
            }
        }
    }

    /// <summary>
    /// Método que se encarga de manejar subir y bajar en una escalera.
    /// </summary>
    void manageClimb()
    {
        //Se comprueba que no estemos saltando mientras escalamos
        if ((inputManager.GetKey(inputEnum.up) || inputManager.GetKey(inputEnum.down)) && !inputManager.GetKey(inputEnum.jump))
        {
            if (_VSpeed != 0)
            {
                _canJump = true;
                _wasClimbing = true;
                if (_VSpeed > 0) //Estamos subiendo
                {
                    //_canJump = false;
                    setDistanceJumped(0);
                    _rb.gravityScale = 0f;
                    _rb.velocity = new Vector2(0f, 0f);
                    gameObject.transform.position = new Vector3(gameObject.transform.position.x, 
                                                                gameObject.transform.position.y + _VSpeed * Time.deltaTime, 
                                                                gameObject.transform.position.z);
                }

                //Estamos bajando y además no estamos tocando suelo
                if (_VSpeed < 0 && (!GetComponent<collisionController>().getIsGrounded() && 
                                    !GetComponent<collisionController>().getIsOnOneWay()) && 
                                    (!GetComponent<collisionController>().getIsOnLadderTop()))
                {
                    gameObject.transform.position = new Vector3(gameObject.transform.position.x, 
                                                                gameObject.transform.position.y + _VSpeed * Time.deltaTime, 
                                                                gameObject.transform.position.z);
                }
            }
            _canMove = false;
            _canRoll = true;
            _hasRolled = false;
        }

        //Si estamos saltando
        if (_isJumping)
        {
            //Volvemos a hacer que podamos atacar
            GetComponent<combatController>().setCanAttack(true);
            if (GetComponent<combatController>().getPrimaryWeapon() != null)
            {
                GetComponent<combatController>().getPrimaryWeapon().GetComponent<weapon>().setCanAttack(true);
            }
            if (GetComponent<combatController>().getSecundaryWeapon() != null)
            {
                GetComponent<combatController>().getSecundaryWeapon().GetComponent<weapon>().setCanAttack(true);
            }
            _canMove = true;
        }
    }

    /// <summary>
    /// Método responsable de manejar el salto
    /// </summary>
    void manageJump()
    {
        //Sumamos salto realizado
        if (inputManager.GetKeyDown(inputEnum.jump))
        {
            _jumpsDone++;
        }
        
        //Si aún podemos saltar
        if ((inputManager.GetKey(inputEnum.jump) && !_isDodging && _canJump) && _distanceJumped < _JUMP_HEIGHT)
        {
            GetComponent<collisionController>().setCanCheckSlope(false);
            GetComponent<collisionController>().setIsOnSlope(false);
            _wasJumping = true;
            _isJumping = true;
            _camera.GetComponent<cameraController>().setOffset(0f);
            _rb.gravityScale = 0f;
            _rb.velocity = new Vector2(0f, 0f);
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, 
                                                        gameObject.transform.position.y + _jump * Time.deltaTime, 
                                                        gameObject.transform.position.z);

            _distanceJumped += _jump * Time.deltaTime;
        }

        //Hemos soltado el botón o bien hemos alcanzado la distancia de salto máxima
        if (_wasJumping && ((inputManager.GetKeyUp(inputEnum.jump)) || _distanceJumped >= _JUMP_HEIGHT))
        {
            if (_hasRolled || (_canDoubleJump && _jumpsDone < 2 && inputManager.GetKeyUp(inputEnum.jump)))
            {
                _canJump = true;
                _distanceJumped = 0;
            }
            else
            {
                _canJump = false;
                _distanceJumped = _JUMP_HEIGHT;
            }
            _isJumping = false;
            GetComponent<collisionController>().setCanCheckSlope(true);

            if (!_isDodging)
            {
                _rb.gravityScale = _g0 * _fallSpeed;
            }

            if (_canClimb && !inputManager.GetKey(inputEnum.up))
            {
                if (!_isDodging)
                {
                    _rb.gravityScale = _g0 * _fallSpeed;
                }
            }

            //Hemos comenzado a escalar una escalera
            if (_canClimb && inputManager.GetKey(inputEnum.up))
            {
                _rb.gravityScale = 0f;
                _rb.velocity = new Vector2(0f, 0f);
            }

        }
    }
    /// <summary>
    /// Método responsable de mover la cámara hacia arriba o abajo para mirar alrededor
    /// </summary>
    void manageLook()
    {
        //Queremos mirar hacia arriba
        if (inputManager.GetKey(inputEnum.up))
        {
            _canRoll = false;
            _cameraTimer += Time.deltaTime;
            _couldClimb = _canClimb;

            if (_cameraTimer >= _TIMELIMIT)
            {
                _camera.GetComponent<cameraController>().setOffset(2f);
            }
        }

        //Queremos mirar hacia abajo
        if (inputManager.GetKey(inputEnum.down))
        {
            _canRoll = false;
            _cameraTimer += Time.deltaTime;
            _canMove = false;
            if (_canClimb)
            {
                _couldClimb = _canClimb;
            }
            _canClimb = false;
            _isLookingDown = true;
            _canJump = false;

            //Utilizamos la plataforma en la que nos encontramos
            if (GetComponent<collisionController>().getIsOnOneWay() && inputManager.GetKey(inputEnum.up))
            {
                _canJump = false;
                _cameraTimer = 0;
                _canMove = true;
                _canRoll = true;
                _isLookingDown = false;
            }
            else
            {
                if (_cameraTimer >= _TIMELIMIT)
                {
                    _camera.GetComponent<cameraController>().setOffset(-2f);
                }
            }

        }

        //Dejamos de mirar hacia arriba
        if (inputManager.GetKeyUp(inputEnum.up) )
        {
            _canRoll = true;
            _canClimb = _couldClimb;
            _couldClimb = false;
            _cameraTimer = 0;
            _camera.GetComponent<cameraController>().setOffset(0f);
        }

        //Dejamos de mirar hacia abajo
        if (inputManager.GetKeyUp(inputEnum.down))
        {
            _canRoll = true;
            _cameraTimer = 0;
            _camera.GetComponent<cameraController>().setOffset(0f);
            _canMove = true;
            _isLookingDown = false;
            _canClimb = _couldClimb;
            _couldClimb = false;
            _canJump = true;
        }

    }

    /// <summary>
    /// Método responsable de mover al jugador.
    /// </summary>
    void manageWalk()
    {
        //Estamos presionando hacia la izquierda o derecha
        if (_HSpeed != 0)
        {
            //Si no estamos chocando con una pared
            if (!GetComponent<collisionController>().getSide())
            {
                if (GetComponent<collisionController>().getIsOnSlope() && !_isJumping)
                {
                    if (GetComponent<collisionController>().getSlopeNormalPerpendicular().x == 0) //Estamos entrando  a una rampa
                    {
                        gameObject.transform.position = new Vector3(gameObject.transform.position.x + (_HSpeed * Time.deltaTime), 
                                                                   gameObject.transform.position.y, 
                                                                   gameObject.transform.position.z);
                    }
                    else //Estamos en la rampa
                    {
                        float xOffset = (GetComponent<collisionController>().getSlopeNormalPerpendicular().x * -_HSpeed * Time.deltaTime);
                        float yOffset = (GetComponent<collisionController>().getSlopeNormalPerpendicular().y * -_HSpeed * Time.deltaTime);
                        gameObject.transform.position = new Vector3(gameObject.transform.position.x + xOffset, 
                                                                   gameObject.transform.position.y + yOffset,
                                                                   gameObject.transform.position.z);
                    }
                }
                else //El suelo es plano
                {
                    gameObject.transform.position = new Vector3(gameObject.transform.position.x + _HSpeed * Time.deltaTime, 
                                                                gameObject.transform.position.y, 
                                                                gameObject.transform.position.z);
                }
            }
            _camera.GetComponent<cameraController>().setOffset(0f);
        }
    }

    /// <summary>
    /// Método auxiliar que gira el sprite del personaje.
    /// </summary>
    void flip()
    {
        //Vector3 currentScale = gameObject.transform.localScale;
        //currentScale.x *= -1;
        //gameObject.transform.localScale = currentScale;
        _facingLeft = !_facingLeft;

        //if (_facingLeft) GetComponent<SpriteRenderer>().flipX = false;
        //else GetComponent<SpriteRenderer>().flipX = true;
    }

    /// <summary>
    /// Método responsable de realizar la esquiva.
    /// </summary>
    /// <returns>Un <see cref="IEnumerator"/> que contiene el tiempo que dura la esquiva.</returns>
    public IEnumerator manageRoll()
    {
        //Cambiamos variables
        _canMove = false;
        _canJump = false;
        _canRoll = false;
        _isDodging = true;
        _couldRoll = false;
        _canClimb = false;
        //Para que no caigamos
        _rb.gravityScale = 0f;

        AnimationClip clipToCalculate;

        //Modificamos el tiempo y distancia de esquiva según si estamos o no quietos
        if (_idle)
        {
            clipToCalculate = _backDashAnim;
            if (!_facingLeft)
            {
                _rollDistance = -1.0f * _roll;
            }
            else
            {
                _rollDistance = _roll;
            }
            _rollTime = _timeLimit;
        }
        else
        {
            clipToCalculate = _rollAnim;
            if (!_facingLeft)
            {
                _rollDistance = 2.0f * _roll;
            }
            else
            {
                _rollDistance = -2.0f * _roll;
            }
            _rollTime = 2.0f * _timeLimit;
        }

        _idle = false;
        //Para no caer
        _rb.velocity = new Vector2(_rollDistance, 0);
        
        //Utilizamos la stamina correspondiente
        GetComponent<statsController>().useStamina(GetComponent<combatController>().getDashStaminaUse());
        
        //Impedimos atacar
        GetComponent<combatController>().setCanAttack(false);
        if (GetComponent<combatController>().getPrimaryWeapon() != null)
        {
            GetComponent<combatController>().getPrimaryWeapon().GetComponent<weapon>().setCanAttack(false);
        }
        if (GetComponent<combatController>().getSecundaryWeapon() != null)
        {
            GetComponent<combatController>().getSecundaryWeapon().GetComponent<weapon>().setCanAttack(false);
        }

        //Desactivamos hurtbox para evitar daño
        GetComponent<combatController>().getHurtbox().GetComponent<BoxCollider2D>().enabled = false;

        //Modificamos el tiempo según el nivel de agilidad que tengamos
        float newRollTime = _rollTime;
        newRollTime -= newRollTime * ((_dashTimeMultiplier / 100f) * 
                                       statSystem.getAgility().getLevel() / GetComponent<combatController>().getLevelThreshold());
        GetComponent<Animator>().SetFloat("rollSpeed", clipToCalculate.length / newRollTime);
        yield return new WaitForSeconds(newRollTime);
        GetComponent<Animator>().SetFloat("rollSpeed", 0f);
        //Activamos hurtbox para poder recibir daño
        GetComponent<combatController>().getHurtbox().GetComponent<BoxCollider2D>().enabled = true;
        
        //Permitimos atacar
        GetComponent<combatController>().setCanAttack(true);

        if (GetComponent<combatController>().getPrimaryWeapon() != null)
        {
            GetComponent<combatController>().getPrimaryWeapon().GetComponent<weapon>().setCanAttack(true);
        }
        if (GetComponent<combatController>().getSecundaryWeapon() != null)
        {
            GetComponent<combatController>().getSecundaryWeapon().GetComponent<weapon>().setCanAttack(true);
        }

        //Para no poder saltar por si estamos en medio del aire
        setDistanceJumped(_JUMP_HEIGHT);

        //Para no salir despedidos
        _rb.velocity = new Vector2(0, 0);

        //Cambiamos distintas variables
        _canJump = true;
        _rb.gravityScale = _g0;
        _isDodging = false;
        _canMove = true;
        _isJumping = false;
        _hasRolled = true;
        _canRoll = false;
    }

    /// <summary>
    /// Setter que modifica <see cref="_canMove"/>.
    /// </summary>
    /// <param name="value">El valor a asignar.</param>
    public void setCanMove(bool value)
    {
        _canMove = value;
    }

    /// <summary>
    /// Setter que modifica <see cref="_canClimb"/>.
    /// </summary>
    /// <param name="climb">El valor a asignar.</param>
    public void setCanClimb(bool climb)
    {
        _canClimb = climb;
    }

    /// <summary>
    /// Setter que modifica <see cref="_facingLeft"/>.
    /// </summary>
    /// <param name="value">El valor a asignar.</param>
    public void setFacingLeft(bool value)
    {
        _facingLeft = value;
    }

    /// <summary>
    /// Setter que modifica <see cref="_couldClimb"/>.
    /// </summary>
    /// <param name="couldClimb">El valor a asignar.</param>
    public void setCouldClimb(bool couldClimb)
    {
        _couldClimb = couldClimb;
    }

    /// <summary>
    /// Setter que modifica <see cref="_canDoubleJump"/>.
    /// </summary>
    /// <param name="doubleJump">El valor a asignar.</param>
    public void setCanDoubleJump(bool doubleJump)
    {
        _canDoubleJump = doubleJump;
    }

    /// <summary>
    /// Setter que modifica <see cref="_rb.gravityScale"/>.
    /// </summary>
    /// <param name="gravity">El valor a asignar.</param>
    public void setGravity(float gravity)
    {
        _rb.gravityScale = gravity;
    }

    /// <summary>
    /// Setter que modifica <see cref="_rb.velocity"/>.
    /// </summary>
    /// <param name="newVelocity">El valor a asignar.</param>
    public void setRigidBodyVelocity(Vector2 newVelocity)
    {
        _rb.velocity = newVelocity;
    }

    /// <summary>
    /// Setter que modifica <see cref="_distanceJumped"/>.
    /// </summary>
    /// <param name="distance">El valor a asignar.</param>
    public void setDistanceJumped(float distance)
    {
        _distanceJumped = distance;
    }

    /// <summary>
    /// Setter que modifica <see cref="_canRoll"/>.
    /// </summary>
    /// <param name="canRoll">El valor a asignar.</param>
    public void setCanRoll(bool canRoll)
    {
        _canRoll = canRoll;
    }

    /// <summary>
    /// Setter que modifica <see cref="_canJump"/>.
    /// </summary>
    /// <param name="canJump">El valor a asignar.</param>
    public void setCanJump(bool canJump)
    {
        _canJump = canJump;
    }

    /// <summary>
    /// Setter que modifica <see cref="_couldRoll"/>.
    /// </summary>
    /// <param name="value">El valor a asignar.</param>
    public void setCouldRoll(bool value)
    {
        _couldRoll = value;
    }

    /// <summary>
    /// Setter que modifica <see cref="_isLookingDown"/>.
    /// </summary>
    /// <param name="value">El valor a asignar.</param>
    public void setIsLookingDown(bool value)
    {
        _isLookingDown = value;
    }

    /// <summary>
    /// Setter que modifica <see cref="_jumpsDone"/>.
    /// </summary>
    /// <param name="jumps">El valor a asignar.</param>
    public void setJumpsDone(int jumps)
    {
        _jumpsDone = jumps;
    }

    /// <summary>
    /// Getter que devuelve <see cref="_camera"/>
    /// </summary>
    /// <returns>Un GameObject que contiene la referencia a la cámara del jugador.</returns>
    public GameObject getCamera()
    {
        return _camera;
    }

    /// <summary>
    /// Getter que devuelve <see cref="_bc"/>
    /// </summary>
    /// <returns>El BoxCollider del personaje.</returns>
    public BoxCollider2D getBoxCollider()
    {
        return _bc;
    }

    /// <summary>
    /// Getter que devuelve un flag booleano para saber cuándo se puede reproducir el SFX de caminar.
    /// </summary>
    /// <returns>Flag booleano que indica si se puede o no reproducir el SFX de caminar.</returns>
    public bool getPlayWalkingSFX()
    {
        return !_idle && config.getPlayer().GetComponent<collisionController>().getIsOnPlatform() && !_isDodging;
    }

    /// <summary>
    /// Getter que devuelve <see cref="_isDodging"/>
    /// </summary>
    /// <returns>Un flag booleano que indica si podemos esquivar.</returns>
    public bool getIsDodging()
    {
        return _isDodging;
    }

    /// <summary>
    /// Getter que devuelve <see cref="_hasRolled"/>
    /// </summary>
    /// <returns>Un flag booleano que indice si hemos esquivado.</returns>
    public bool getHasRolled()
    {
        return _hasRolled;
    }

    /// <summary>
    /// Getter que devuelve <see cref="_couldRoll"/>
    /// </summary>
    /// <returns>Un flag booleano que indica si podíamos esquivar.</returns>
    public bool getCouldRoll()
    {
        return _couldRoll;
    }

    /// <summary>
    /// Getter que devuelve <see cref="_JUMP_HEIGHT"/>
    /// </summary>
    /// <returns>Un valor en coma flotante que contiene la altura de salto máxima.</returns>
    public float getJumpHeight()
    {
        return _JUMP_HEIGHT;
    }

    /// <summary>
    /// Getter que devuelve <see cref="_couldClimb"/>
    /// </summary>
    /// <returns>Un flag booleano que indica si podíamos escalar.</returns>
    public bool getCouldClimb()
    {
        return _couldClimb;
    }

    /// <summary>
    /// Getter que devuelve <see cref="_rb"/>
    /// </summary>
    /// <returns>El RigidBody del personaje.</returns>
    public Rigidbody2D getRigidBody()
    {
        return _rb;
    }

    /// <summary>
    /// Getter que devuelve <see cref="_facingLeft"/>
    /// </summary>
    /// <returns>Un flag booleano que indica a qué lado estamos mirando.</returns>
    public bool getIsFacingLeft()
    {
        return _facingLeft;
    }

    /// <summary>
    /// Getter que devuelve <see cref="_isLookingDown"/>
    /// </summary>
    /// <returns>Un flag booleano que indica si estamos mirando hacia abajo.</returns>
    public bool getIsLookingDown()
    {
        return _isLookingDown;
    }

    /// <summary>
    /// Getter que devuelve <see cref="_canRoll"/>
    /// </summary>
    /// <returns>Un flag booleano que indica si podemos esquivar.</returns>
    public bool getCanRoll()
    {
        return _canRoll;
    }

    /// <summary>
    /// Getter que devuelve <see cref="_g0"/>
    /// </summary>
    /// <returns>Un valor en coma flotante que contiene la gravedad inicial.</returns>
    public float getGravity()
    {
        return _g0;
    }

    /// <summary>
    /// Getter que devuelve <see cref="_rb.velocity"/>
    /// </summary>
    /// <returns>Un <see cref="Vector2"/> que contiene la velocidad del RigidBody del personaje.</returns>
    public Vector2 getRigidBodyVelocity()
    {
        return _rb.velocity;
    }

    /// <summary>
    /// Getter que devuelve <see cref="_canJump"/>
    /// </summary>
    /// <returns>Un flag booleano que indica si podemos esquivar.</returns>
    public bool getCanJump()
    {
        return _canJump;
    }

    /// <summary>
    /// Getter que devuelve <see cref="_isJumping"/>
    /// </summary>
    /// <returns>Un GameObject que cotiene la referencia a la cámara del jugador.</returns>
    public bool getIsJumping()
    {
        return _isJumping;
    }

    /// <summary>
    /// Getter que devuelve <see cref="_canClimb"/>
    /// </summary>
    /// <returns>Un flag booleano que indica si podemos escalar.</returns>
    public bool getCanClimb()
    {
        return _canClimb;
    }

    /// <summary>
    /// Getter que devuelve <see cref="_canMove"/>
    /// </summary>
    /// <returns>Un flag booleano que indica si podemos movernos.</returns>
    public bool getCanMove()
    {
        return _canMove;
    }
}
