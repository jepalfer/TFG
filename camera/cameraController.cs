using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// cameraController es una clase que se usa para suavizar el movimiento de la cámara.
/// </summary>
public class cameraController : MonoBehaviour
{
    /// <summary>
    /// Es la velocidad de seguimiento de la cámara.
    /// </summary>
    [SerializeField] private float _followSpeed = 20f;

    /// <summary>
    /// Es el desplazamiento de la cámara.
    /// </summary>
    [SerializeField] private float _offset = 0f;

    /// <summary>
    /// Es el objetivo de la cámara.
    /// </summary>
    [SerializeField] private Transform _target;

    /// <summary>
    /// Booleano que indica si la cámara está libre o no.
    /// </summary>
    private bool _canMove;

    /// <summary>
    /// Booleano que indica si la cámara se puede mover solo hacia arriba o hacia abajo
    /// </summary>
    private bool _canMoveUpDown;

    /// <summary>
    /// Booleano que indica si la cámara se puede mover solo hacia izquierda o derecha
    /// </summary>
    private bool _canMoveLeftRight;

    /// <summary>
    /// Método se ejecuta al iniciar el script.
    /// </summary>
    private void Awake()
    {
        _canMove = true;
        _canMoveUpDown = false;
        _canMoveLeftRight = false;
    }


    /// <summary>
    /// Método llamado en cada fotograma para actualizar la lógica.
    /// </summary>
    void Update()
    {
        //No podemos moverla en x
        if (_canMove)
        {
            //Obtenemos la posición del target y le aplicamos un offset a la y
            Vector3 newPos = new Vector3(_target.transform.position.x, _target.transform.position.y + _offset, -10f);

            //Suavizamos con Slerp el movimiento
            transform.position = Vector3.Slerp(transform.position, newPos, _followSpeed * Time.deltaTime);
        }
        else
        {
            if (_canMoveLeftRight)
            {
                //Aplicamos un offset a la y
                Vector3 newPos = new Vector3(_target.transform.position.x, transform.position.y + _offset, -10f);

                //Suavizamos con Slerp el movimiento
                transform.position = Vector3.Slerp(transform.position, newPos, _followSpeed * Time.deltaTime);
            }

            if (_canMoveUpDown)
            {
                //Aplicamos un offset a la y
                Vector3 newPos = new Vector3(transform.position.x, _target.transform.position.y + _offset, -10f);

                //Suavizamos con Slerp el movimiento
                transform.position = Vector3.Slerp(transform.position, newPos, _followSpeed * Time.deltaTime);
            }
        }
        
    }

    /// <summary>
    /// Setter que modifica <see cref="_canMove"/>.
    /// </summary>
    /// <param name="val">Nuevo valor a asignar al booleano.</param>
    public void setCanMove(bool val)
    {
        _canMove = val;
    }

    /// <summary>
    /// Setter que modifica <see cref="_canMoveUpDown"/>.
    /// </summary>
    /// <param name="val">Nuevo valor a asignar al booleano.</param>
    public void setCanMoveUpDown(bool val)
    {
        _canMoveUpDown = val;
    }

    /// <summary>
    /// Setter que modifica <see cref="_canMoveLeftRight"/>.
    /// </summary>
    /// <param name="val">Nuevo valor a asignar al booleano.</param>
    public void setCanMoveLeftRight(bool val)
    {
        _canMoveLeftRight = val;
    }

    /// <summary>
    /// Getter que devuelve el offset de la cámara.
    /// </summary>
    /// <returns> Es el offset de la cámara </returns>
    public float getOffset()
    {
        return _offset;
    }
    /// <summary>
    /// Setter que modifica _offset
    /// </summary>
    /// <param name="value"> Es el valor que se le asigna a _offset </param>
    public void setOffset(float value)
    {
        _offset = value;
    }
}
