using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// playerData es una clase que se usa para almacenar la posición del jugador.
/// </summary>
[System.Serializable]
public class playerData
{
    /// <summary>
    /// Coordenadas del jugador.
    /// </summary>
    private float[] _position;

    /// <summary>
    /// Flag booleano que indica si el jugador estaba o no mirando a la derecha.
    /// </summary>
    private bool _isFacingRight;

    /// <summary>
    /// Constructor de la clase.
    /// </summary>
    public playerData()
    {
        //Asignamos las coordenadas en las que se encuentra el jugador
        _position = new float[3];

        _position[0] = config.getPlayer().transform.position.x;
        _position[1] = config.getPlayer().transform.position.y;
        _position[2] = config.getPlayer().transform.position.z;

        //Asigamos si el jugador miraba hacia la derecha
        _isFacingRight = config.getPlayer().GetComponent<playerMovement>().getIsFacingRight();
    }

    /// <summary>
    /// Getter que devuelve la primera posición de <see cref="_position"/>.
    /// </summary>
    /// <returns>Primera posición de <see cref="_position"/>.</returns>
    public float getX()
    {
        return _position[0];
    }
    /// <summary>
    /// Getter que devuelve la segunda posición de <see cref="_position"/>.
    /// </summary>
    /// <returns>Segunda posición de <see cref="_position"/>.</returns>
    public float getY()
    {
        return _position[1];
    }
    /// <summary>
    /// Getter que devuelve la tercera posición de <see cref="_position"/>.
    /// </summary>
    /// <returns>Tercera posición de <see cref="_position"/>.</returns>
    public float getZ()
    {
        return _position[2];
    }

    /// <summary>
    /// Getter que devuelve <see cref="_isFacingRight"/>.
    /// </summary>
    /// <returns><see cref="_isFacingRight"/>.</returns>
    public bool getIsFacingRight()
    {
        return _isFacingRight;
    }
}
