using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class playerMovement : MonoBehaviour
{
    //Camera
    [SerializeField] private GameObject _camera;
    //------------------------------------------- PLAYER COMPONENTS --------------------------------------------------------
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private BoxCollider2D _bc;

    [Header("Animation")]
    [SerializeField] private Animator _animator;
    //----------------------------------------------------------------------------------------------------------------------
    [Header("Crouching")]
    [SerializeField] private bool _isLookingDown = false;
    [SerializeField] private float _g0;

    [Header("Jump")]
    [SerializeField] private const float _JUMP_HEIGHT = 3f;
    [SerializeField] private float _jump = 5f;
    [SerializeField] private float _distanceJumped = 0f;

    [SerializeField] private bool _canJump = true;
    [SerializeField] private bool _isJumping = false;
    [SerializeField] private bool _wasJumping = false;
    [SerializeField] private bool _canDoubleJump = false;
    [SerializeField] private int _jumpsDone = 0;

    [Header("Dodge")]
    [SerializeField] private float _rollTime;
    [SerializeField] private float _rollDistance = 20f;
    [SerializeField] private bool _couldRoll;
    [SerializeField] private bool _isDodging = false;
    [SerializeField] private float _timeLimit = 0.2f;
    [SerializeField] private float _roll = 5f;
    [SerializeField] private bool _canRoll = true;
    [SerializeField] private bool _canMove = true;
    [SerializeField] private bool _hasRolled = false;

    [Header("Movement")]
    [SerializeField] private float _HSpeed;
    [SerializeField] private float _VSpeed;
    [SerializeField] private float _movementSpeed = 6f;
    [SerializeField] private float _climbingSpeed = 4.5f;
    [SerializeField] private float _fallSpeed = 1.5f;

    [SerializeField] private bool _facingRight = true;
    [SerializeField] private bool _idle = true;
    [SerializeField] private bool _passedThrough = false;
    [SerializeField] private const float _TIMELIMIT = 0.75f;
    private int _direction = 0;

    [SerializeField] private float _cameraTimer = 0f;
    [SerializeField] private float _gravity = 0f;
    [SerializeField] private float _ySpeed = 0f;

    [SerializeField] private bool _canClimb = false;
    [SerializeField] private bool _couldClimb = false;
    [SerializeField] private bool _wasClimbing = false;

    [SerializeField] private float _dashTimeMultiplier;

    // Start is called before the first frame update
    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();

        BoxCollider2D[] colliders = GetComponentsInChildren<BoxCollider2D>();

        _bc =  colliders[0];
        _g0 = _rb.gravityScale;
        _dashTimeMultiplier = 5f;
        config.setPlayer(gameObject);
        //saveSystem.savePlayer();
    }    


    // Update is called once per frame
    void Update()
    {
        if (UIController.getIsInPauseUI())
        {
            _distanceJumped = _JUMP_HEIGHT;
        }
        if (!UIController.getIsInPauseUI() && !UIController.getIsInEquippingSkillUI() && !UIController.getIsInLevelUpUI() && !UIController.getIsInAdquireSkillUI() && 
            !UIController.getIsInLevelUpWeaponUI() && !UIController.getIsInInventoryUI() && !UIController.getIsInShopUI() && !bonfireBehaviour.getIsInBonfireMenu() &&
            !GetComponent<downWardBlowController>().getIsInDownWardBlow() && !GetComponent<combatController>().getIsAttacking())
        { 
       
            if ((!inputManager.GetKey(inputEnum.right) && !inputManager.GetKey(inputEnum.left)))
            {
                _ySpeed = _rb.velocity.y;
                _gravity = _rb.gravityScale;
                
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

                _HSpeed = _direction * _movementSpeed;
                _idle = _HSpeed == 0;


                if (((_HSpeed > 0 && !_facingRight) || (_HSpeed < 0 && _facingRight)) && _canMove)
                {
                    flip();
                }

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

            if (!_hasRolled && !GetComponent<collisionController>().getIsOnPlatform())
            {
                _canRoll = true;
            }

            //Si estamos en algo donde podemos saltar
            if (!_isDodging && (GetComponent<collisionController>().getIsOnPlatform()))
            {
                if (!inputManager.GetKey(inputEnum.jump))
                {
                    _wasJumping = false;
                    _jumpsDone = 0;
                    if (!_isDodging)
                    {
                        _canJump = true;
                    }
                }
                _canRoll = true;
                _couldRoll = true;
                _wasClimbing = false;
                _hasRolled = false;

                /*_isJumping = false;
                _distanceJumped = 0f;*/

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
            else   //Estamos cayendo 
            {
                _cameraTimer = 0f;
                if (!_isJumping)
                {
                    if (!_wasClimbing || _VSpeed != 0)
                    {
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

            if (GetComponent<collisionController>().getIsOnPlatform() && !GetComponent<collisionController>().getIsOnLadderTop())
            {
                manageLook();
            }

            if (_canMove)
            {
                manageWalk();
            }

            if (!GetComponent<combatController>().getIsAttacking())
            {
                manageJump();
            }

            if (_canClimb && _rb.velocity.y == 0 && !_isDodging && !_isJumping && inputManager.GetKeyUp(inputEnum.jump))
            {
                _rb.gravityScale = _g0 * _fallSpeed;
                //_rb.velocity = new Vector2(0, 0);
            }
            if (_canClimb && !_isDodging)
            {
                manageClimb();
            }
            if (_canRoll && inputManager.GetKeyDown(inputEnum.roll))
            {
                StartCoroutine(manageRoll());
            }
        }
    }

    void manageClimb()
    {
        if ((inputManager.GetKey(inputEnum.up) || inputManager.GetKey(inputEnum.down)) && !inputManager.GetKey(inputEnum.jump))
        {
            if (_VSpeed != 0)
            {
                _canJump = true;
                _wasClimbing = true;
                if (_VSpeed > 0)
                {
                    //_canJump = false;
                    setDistanceJumped(0);
                    _rb.gravityScale = 0f;
                    _rb.velocity = new Vector2(0f, 0f);
                    gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + _VSpeed * Time.deltaTime, gameObject.transform.position.z);
                }

                if (_VSpeed < 0 && (!GetComponent<collisionController>().getIsGrounded() && !GetComponent<collisionController>().getIsOnOneWay()) && (!GetComponent<collisionController>().getIsOnLadderTop()))
                {
                    gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + _VSpeed * Time.deltaTime, gameObject.transform.position.z);
                }
            }
            _canMove = false;
            _canRoll = true;
            _hasRolled = false;
        }

        if (_isJumping)
        {
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
    /// M�todo responsable de manejar el salto
    /// </summary>
    void manageJump()
    {
        if (inputManager.GetKeyDown(inputEnum.jump))
        {
            _jumpsDone++;
        }

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

            if (_canClimb && inputManager.GetKey(inputEnum.up))
            {
                _rb.gravityScale = 0f;
                _rb.velocity = new Vector2(0f, 0f);
            }

        }
    }
    #region updateMethods
    void manageLook()
    {
        if (inputManager.GetKey(inputEnum.up))                //Move camera up
        {
            _canRoll = false;
            _cameraTimer += Time.deltaTime;
            _couldClimb = _canClimb;

            if (_cameraTimer >= _TIMELIMIT)
            {
                _camera.GetComponent<cameraController>().setOffset(2f);
            }
        }

        if (inputManager.GetKey(inputEnum.down) )                //Move camera down and crouch
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

            if (GetComponent<collisionController>().getIsOnOneWay() && inputManager.GetKey(inputEnum.up))
            {
                _passedThrough = true;
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

        if (inputManager.GetKeyUp(inputEnum.up) )
        {
            _canRoll = true;
            _canClimb = _couldClimb;
            _couldClimb = false;
            _cameraTimer = 0;
            _camera.GetComponent<cameraController>().setOffset(0f);
        }

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
    /// M�todo responsable de mover al jugador.
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

    void flip()
    {
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;
        _facingRight = !_facingRight;
    }

    public IEnumerator manageRoll()
    {
        _canMove = false;
        _canJump = false;
        _canRoll = false;
        _isDodging = true;
        _couldRoll = false;
        _canClimb = false;
        

        _rb.gravityScale = 0f;

        if (_idle)
        {
            if (_facingRight)
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
            if (_facingRight)
            {
                _rollDistance = 2.0f * _roll;
            }
            else
            {
                _rollDistance = -2.0f * _roll;
            }
            _rollTime = 2.0f * _timeLimit;
        }

        _rb.velocity = new Vector2(_rollDistance, 0);
        GetComponent<statsController>().useStamina(GetComponent<combatController>().getDashStaminaUse());
        
        GetComponent<combatController>().setCanAttack(false);
        if (GetComponent<combatController>().getPrimaryWeapon() != null)
        {
            GetComponent<combatController>().getPrimaryWeapon().GetComponent<weapon>().setCanAttack(false);
        }
        if (GetComponent<combatController>().getSecundaryWeapon() != null)
        {
            GetComponent<combatController>().getSecundaryWeapon().GetComponent<weapon>().setCanAttack(false);
        }

        GetComponent<combatController>().getHurtbox().GetComponent<BoxCollider2D>().enabled = false;

        float newRollTime = _rollTime;

        newRollTime -= newRollTime * ((_dashTimeMultiplier / 100f) * statSystem.getAgility().getLevel() / GetComponent<combatController>().getLevelThreshold());

        yield return new WaitForSeconds(newRollTime);

        GetComponent<combatController>().getHurtbox().GetComponent<BoxCollider2D>().enabled = true;
        GetComponent<combatController>().setCanAttack(true);

        if (GetComponent<combatController>().getPrimaryWeapon() != null)
        {
            GetComponent<combatController>().getPrimaryWeapon().GetComponent<weapon>().setCanAttack(true);
        }
        if (GetComponent<combatController>().getSecundaryWeapon() != null)
        {
            GetComponent<combatController>().getSecundaryWeapon().GetComponent<weapon>().setCanAttack(true);
        }
        setDistanceJumped(_JUMP_HEIGHT);
        _rb.velocity = new Vector2(0, 0);
        _canJump = true;
        _rb.gravityScale = _g0;
        _isDodging = false;
        _canMove = true;
        _isJumping = false;
        _hasRolled = true;
        _canRoll = false;
    }

    #endregion

    //SETTERS
    #region settersMethods
    public void setCanMove(bool value)
    {
        _canMove = value;
    }
    public void setCanClimb(bool climb)
    {
        _canClimb = climb;
    }

    public void setFacingRight(bool value)
    {
        _facingRight = value;
    }

    public void setCouldClimb(bool couldClimb)
    {
        _couldClimb = couldClimb;
    }

    public void setCanDoubleJump(bool doubleJump)
    {
        _canDoubleJump = doubleJump;
    }
    public void setGravity(float gravity)
    {
        _rb.gravityScale = gravity;
    }
    public void setRigidBodyVelocity(Vector2 newVelocity)
    {
        _rb.velocity = newVelocity;
    }

    public void setPassedThrough(bool passed)
    {
        _passedThrough = passed;
    }

    public void setDistanceJumped(float distance)
    {
        _distanceJumped = distance;
    }

    public void setCanRoll(bool canRoll)
    {
        _canRoll = canRoll;
    }

    public void setCanJump(bool canJump)
    {
        _canJump = canJump;
    }
    public void setCouldRoll(bool value)
    {
        _couldRoll = value;
    }

    public void setIsLookingDown(bool value)
    {
        _isLookingDown = value;
    }

    public void setJumpsDone(int jumps)
    {
        _jumpsDone = jumps;
    }

    #endregion

    //GETTERS

    #region gettersMethods

    public GameObject getCamera()
    {
        return _camera;
    }

    public BoxCollider2D getBoxCollider()
    {
        return _bc;
    }

    public bool getIsDodging()
    {
        return _isDodging;
    }
    public bool getHasRolled()
    {
        return _hasRolled;
    }
    public bool getCouldRoll()
    {
        return _couldRoll;
    }
    public float getJumpHeight()
    {
        return _JUMP_HEIGHT;
    }
    public bool getCouldClimb()
    {
        return _couldClimb;
    }
    public Rigidbody2D getRigidBody()
    {
        return _rb;
    }

    public bool getIsFacingRight()
    {
        return _facingRight;
    }
    public bool getIsLookingDown()
    {
        return _isLookingDown;
    }

    public float getInitialGravity()
    {
        return _g0;
    }
    public bool getCanRoll()
    {
        return _canRoll;
    }
    public float getGravity()
    {
        return _g0;
    }
    public bool getPassedThrough()
    {
        return _passedThrough;
    }
    public Vector2 getRigidBodyVelocity()
    {
        return _rb.velocity;
    }

    public bool getCanJump()
    {
        return _canJump;
    }

    public bool getIsJumping()
    {
        return _isJumping;
    }

    public bool getCanClimb()
    {
        return _canClimb;
    }

    public bool getCanMove()
    {
        return _canMove;
    }
    #endregion
}
