using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// collisionController es una clase que se utiliza para controlar las colisiones del jugador con el entorno.
/// </summary>
public class collisionController : MonoBehaviour
{
    /// <summary>
    /// <see cref="Vector2"/> para el tamaño de la caja que comprueba la colisión con el techo/suelo.
    /// </summary>
    private Vector2 _ground;

    /// <summary>
    /// <see cref="Vector2"/> para el tamaño de la caja que comprueba la colisión con los lados.
    /// </summary>
    private Vector2 _side;

    /// <summary>
    /// Collider con el que calculamos la colisión con el suelo.
    /// </summary>
    [SerializeField] private Transform _groundCheckCollider;
    
    /// <summary>
    /// Collider con el que calculamos la colisión con el techo.
    /// </summary>
    [SerializeField] private Transform _headCheckCollider;

    /// <summary>
    /// Collider con el que calculamos la colisión por los lados.
    /// </summary>
    [SerializeField] private Transform _sideCheckCollider;

    /// <summary>
    /// Booleano que indica si hemos golpeado un techo.
    /// </summary>
    [SerializeField] private bool _hitHead;

    /// <summary>
    /// Booleano que indica si estamos tocando suelo.
    /// </summary>
    [SerializeField] private bool _isGrounded;

    /// <summary>
    /// Booleano que indica si estamos tocando terreno por el lado.
    /// </summary>
    [SerializeField] private bool _isTouchingSide;

    /// <summary>
    /// Booleano que indica si estamos en una plataforma "ladderTop".
    /// </summary>
    [SerializeField] private bool _isOnLadderTop;
    
    /// <summary>
    /// Booleano que indica si estamos en una plataforma "oneWay".
    /// </summary>
    [SerializeField] private bool _isOnOneWay;

    /// <summary>
    /// Booleano que indica si estamos en una rampa.
    /// </summary>
    [SerializeField] private bool _isOnSlope;

    /// <summary>
    /// Referencia a la capa de suelo.
    /// </summary>
    [SerializeField] private LayerMask _groundLayer;

    /// <summary>
    /// Referencia a la capa de plataformas "oneWay".
    /// </summary>
    [SerializeField] private LayerMask _oneWayGroundLayer;

    /// <summary>
    /// Referencia a la capa de escaleras.
    /// </summary>
    [SerializeField] private LayerMask _ladderLayer;

    /// <summary>
    /// Referencia a la capa de plataformas "ladderTop".
    /// </summary>
    [SerializeField] private LayerMask _ladderTopLayer;

    /// <summary>
    /// Referencia a la capa de rampas.
    /// </summary>
    [SerializeField] private LayerMask _slopeLayer;

    /// <summary>
    /// float que indica la distancia a la que trazamos el rayo para la colisión con las rampas.
    /// </summary>
    [SerializeField] private float _slopeDistance;

    /// <summary>
    /// Referencia al <see cref="BoxCollider2D"/> del jugador.
    /// </summary>
    private BoxCollider2D _bc;

    /// <summary>
    /// Referencia al tamaño de <see cref="_bc"/>.
    /// </summary>
    private Vector2 _colliderSize;

    /// <summary>
    /// <see cref="Vector2"/> para almacenar la perpendicular normalizada a la rampa.
    /// </summary>
    [SerializeField] private Vector2 _slopeNormalPerpendicular;

    /// <summary>
    /// float para almacenar el ángulo con el que el rayo trazado hacia abajo ha chocado con la rampa.
    /// </summary>
    [SerializeField] private float _slopeDownAngle;

    /// <summary>
    /// float que almacena el anterior ángulo con el que el rayo trazado hacia abajo ha chocado con la rampa.
    /// </summary>
    [SerializeField] private float _slopeDownAngleOld;
    private float _slopeSideAngle;

    /// <summary>
    /// Booleano para saber si podemos o no comprobar colisión con la rampa.
    /// </summary>
    [SerializeField] private bool _canCheckSlope = true;

    /// <summary>
    /// Método que se llama al iniciar el script.
    /// </summary>
    void Awake()
    {
        //Calculamos los vectores para el tamaño
        _ground = new Vector2(GetComponent<BoxCollider2D>().size.x, 0.1f);
        _side = new Vector2(0.1f, GetComponent<BoxCollider2D>().size.y - 0.2f);

        //Asignamos variables
        _bc = GetComponent<BoxCollider2D>();
        _colliderSize = _bc.size;
    }

    /// <summary>
    /// Método que se ejecuta cada frame para actualizar la lógica.
    /// </summary>
    void Update()
    {
        //Si no estamos en ningún menú
        if (!UIController.getIsInPauseUI() && !UIController.getIsInEquippingSkillUI() && !UIController.getIsInLevelUpUI() && !UIController.getIsInAdquireSkillUI() && 
            !UIController.getIsInLevelUpWeaponUI() && !UIController.getIsSelectingSkillUI() && !UIController.getIsInShopUI())
        {
            //Si tenemos equipada algun arma
            if (GetComponent<combatController>().getPrimaryWeapon() != null || GetComponent<combatController>().getSecundaryWeapon() != null)
            {
                bool condicion = true;
                //Si tenemos equipada el arma primaria
                if (GetComponent<combatController>().getPrimaryWeapon() != null)
                {
                    //Comprobamos que NO estemos atacando
                    condicion = !GetComponent<combatController>().getPrimaryWeapon().GetComponent<weapon>().getIsAttacking();
                }
                //Si tenemos el arma secundaria
                if (GetComponent<combatController>().getSecundaryWeapon() != null)
                {
                    //Comprobamos que NO estemos atacando y hacemos un AND para no perder el valor anterior
                    condicion = !GetComponent<combatController>().getSecundaryWeapon().GetComponent<weapon>().getIsAttacking() && condicion;
                }
                //Si no estábamos atacando con cualquiera de las 2 armas comprobamos colisiones
                if (condicion)
                {
                    headCheck();
                    oneWayCheck();
                    SideCheck();
                    GroundCheck();
                    ladderTopCheck();
                    slopeCheck();
                }
                else //Si no entonces ponemos todo a false para evitar poder movernos, saltar, dashear...
                {
                    _isOnSlope = false;
                    _isOnOneWay = false;
                    _hitHead = false;
                    _isGrounded = false;
                    _isTouchingSide = false;
                    _isOnLadderTop = false;
                }
            }
            else //si no entonces calculamos colisiones
            {
                headCheck();
                oneWayCheck();
                SideCheck();
                GroundCheck();
                ladderTopCheck();
                slopeCheck();
            }
        }
           
    }


    //COLLIDERS CHECK

    /// <summary>
    /// Método que comprueba las colisiones con rampas.
    /// </summary>
    private void slopeCheck()
    {
        if (_canCheckSlope)
        {
            //Ponemos checkPos como la parte inferior de la boxCollider
            Vector2 checkPos = transform.position - new Vector3(0f, _colliderSize.y / 2 - _bc.offset.y);
            slopeCheckHorizontal(checkPos);
            slopeCheckVertical(checkPos);
        }
    }

    /// <summary>
    /// Método que hace un trazado de rayos vertical.
    /// </summary>
    /// <param name="checkPos">La posición desde la que se traza el rayo</param>
    private void slopeCheckVertical (Vector2 checkPos)
    {
        //Trazamos un rayo hacia abajo
        RaycastHit2D hit = Physics2D.Raycast(checkPos, Vector2.down, _slopeDistance / 2.5f, _slopeLayer);

        //Si ha chocado con una rampa
        if (hit)
        {
            //Calculamos la normal y el ángulo de la normal
            _slopeNormalPerpendicular = Vector2.Perpendicular(hit.normal).normalized;
            _slopeDownAngle = Vector2.Angle(hit.normal, Vector2.up);

            if (_slopeDownAngle != _slopeDownAngleOld || _slopeDownAngle == 0.0f) //Es completamente horizontal
            {
                _isOnSlope = true;
            }

            _slopeDownAngleOld = _slopeDownAngle;

            Debug.DrawRay(hit.point, _slopeNormalPerpendicular, Color.red);
            Debug.DrawRay(hit.point, hit.normal, Color.green);
        }
    }

    /// <summary>
    /// Método que hace un trazado de rayos horizontal.
    /// </summary>
    /// <param name="checkPos">La posición desde la que se traza el rayo</param>
    private void slopeCheckHorizontal (Vector2 checkPos)
    {
        //Trazamos 2 rayos, uno hacia la izquierda y otro hacia la derecha
        RaycastHit2D slopeHitFront = Physics2D.Raycast(checkPos, transform.right, _slopeDistance, _slopeLayer);
        RaycastHit2D slopeHitBack = Physics2D.Raycast(checkPos, -transform.right, _slopeDistance, _slopeLayer);

        //Si ha golpeado el de la derecha
        if (slopeHitFront)
        {
            _isOnSlope = true;
            _slopeSideAngle = Vector2.Angle(slopeHitFront.normal, Vector2.up);
        }
        else if (slopeHitBack) //Si ha golpeado el de la izquierda
        {
            _isOnSlope = true;
            _slopeSideAngle = Vector2.Angle(slopeHitBack.normal, Vector2.up);
        }
        else
        {
            _slopeSideAngle = 0.0f;
            _isOnSlope = false;
        }
    }

    /// <summary>
    /// Método que comprueba la colisión por los lados.
    /// </summary>
    private void SideCheck()
    {
        _isTouchingSide = false;
        _isTouchingSide = Physics2D.OverlapBox(_sideCheckCollider.position, _side, 1f, _groundLayer);

    }

    /// <summary>
    /// Método que comprueba la colisión con el techo.
    /// </summary>
    private void headCheck()
    {
        _hitHead = false;
        _hitHead = Physics2D.OverlapBox(_headCheckCollider.position, _ground, 1f, _groundLayer);

    }

    /// <summary>
    /// Método que comprueba la colisión con el suelo.
    /// </summary>
    private void GroundCheck()
    {
        _isGrounded = false;
        _isGrounded = Physics2D.OverlapBox(_groundCheckCollider.position, _ground, 1f, _groundLayer);
    }

    /// <summary>
    /// Método que comprueba la colisión con las plataformas "oneWay".
    /// </summary>
    private void oneWayCheck()
    {
        _isOnOneWay = false;
        _isOnOneWay = Physics2D.OverlapBox(_groundCheckCollider.position, _ground, 0f, _oneWayGroundLayer);
    }

    /// <summary>
    /// Método que comprueba la colisión con las plataformas "ladderTop".
    /// </summary>
    private void ladderTopCheck()
    {
        _isOnLadderTop = false;
        _isOnLadderTop = Physics2D.OverlapBox(_groundCheckCollider.position, _ground, 0f, _ladderTopLayer);
    }


    /// <summary>
    /// Setter que modifica <see cref="_canCheckSlope"/>.
    /// </summary>
    /// <param name="value">El valor a asignar.</param>
    public void setCanCheckSlope(bool value)
    {
        _canCheckSlope = value;
    }

    /// <summary>
    /// Setter que modifica <see cref="_isOnSlope"/>.
    /// </summary>
    /// <param name="value">El valor a asignar.</param>
    public void setIsOnSlope(bool value)
    {
        _isOnSlope = value;
    }

    /// <summary>
    /// Getter que devuelve <see cref="_isGrounded"/>.
    /// </summary>
    /// <returns>Booleano que indica si estamos tocando suelo.</returns>
    public bool getIsGrounded()
    {
        return _isGrounded;
    }

    /// <summary>
    /// Getter que devuelve <see cref="_isOnLadderTop"/>.
    /// </summary>
    /// <returns>Booleano que indica si estamos tocando una plataforma "ladderTop".</returns>
    public bool getIsOnLadderTop()
    {
        return _isOnLadderTop;
    }

    /// <summary>
    /// Getter que devuelve <see cref="_isOnOneWay"/>.
    /// </summary>
    /// <returns>Booleano que indica si estamos tocando una plataforma "oneWay".</returns>
    public bool getIsOnOneWay()
    {
        return _isOnOneWay;
    }

    /// <summary>
    /// Getter que devuelve <see cref="_hitHead"/>.
    /// </summary>
    /// <returns>Booleano que indica si estamos tocando techo.</returns>
    public bool getHithead()
    {
        return _hitHead;
    }

    /// <summary>
    /// Getter que devuelve <see cref="_isTouchingSide"/>.
    /// </summary>
    /// <returns>Booleano que indica si estamos tocando por los lados.</returns>
    public bool getSide()
    {
        return _isTouchingSide;
    }

    /// <summary>
    /// Getter que devuelve <see cref="_isOnSlope"/>.
    /// </summary>
    /// <returns>Booleano que indica si estamos tocando una rampa.</returns>
    public bool getIsOnSlope()
    {
        return _isOnSlope;
    }

    /// <summary>
    /// Getter que devuelve <see cref="_slopeNormalPerpendicular"/>.
    /// </summary>
    /// <returns>Un <see cref="Vector2"/> que contiene la perpendicular al rayo.</returns>
    public Vector2 getSlopeNormalPerpendicular()
    {
        return _slopeNormalPerpendicular;
    }

    /// <summary>
    /// Getter que devuelve <see cref="_slopeDownAngle"/>.
    /// </summary>
    /// <returns>Un float que representa el ángulo del rayo vertical con la rampa.</returns>
    public float getSlopeDownAngle()
    {
        return _slopeDownAngle;
    }

    /// <summary>
    /// Getter que devuelve <see cref="_slopeDownAngleOld"/>.
    /// </summary>
    /// <returns>Un float que representa el último ángulo del rayo vertical con la rampa.</returns>
    public float getSlopeDownAngleOld()
    {
        return _slopeDownAngleOld;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(_groundCheckCollider.position, _ground);
        Gizmos.DrawWireCube(_headCheckCollider.position, _ground);
        Gizmos.DrawWireCube(_sideCheckCollider.position, _side);
    }
}
