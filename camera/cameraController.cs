using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// cameraFollow es una clase que se usa para suavizar el movimiento de la cámara.
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
    /// Método llamado en cada fotograma para actualizar la lógica.
    /// </summary>
    void Update()
    {
        //Obtenemos la posición del target y le aplicamos un offset a la y
        Vector3 newPos = new Vector3(_target.transform.position.x, _target.transform.position.y + _offset, -10f);
        
        //Suavizamos con Slerp el movimiento
        transform.position = Vector3.Slerp(transform.position, newPos, _followSpeed * Time.deltaTime);
        
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
