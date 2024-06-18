using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// soulContainerData es una clase que se usa para guardar la información respectiva a la entidad que nos da una cantidad de almas
/// tras morir.
/// </summary>

[System.Serializable]
public class soulContainerData
{
    /// <summary>
    /// Cantidad de almas a devolver.
    /// </summary>
    [SerializeField] private long _souls;

    /// <summary>
    /// Posición en la que se instancia.
    /// </summary>
    [SerializeField] private float[] _position;

    /// <summary>
    /// ID interno de la escena en la que se instancia.
    /// </summary>
    [SerializeField] private int _sceneID;

    /// <summary>
    /// Booleano para saber si el personaje ha muerto para instanciar o no el objeto.
    /// </summary>
    [SerializeField] private bool _playerDied;

    /// <summary>
    /// Constructor de la clase.
    /// </summary>
    /// <param name="souls">Cantidad de almas que devuelve.</param>
    /// <param name="position">Posición en la que aparece el objeto.</param>
    /// <param name="sceneID">ID interno de la escena en la que el objeto aparece.</param>
    /// <param name="died">booleano para controlar que el objeto aparece.</param>
    public soulContainerData(long souls, float[] position, int sceneID, bool died)
    {
        //Debug.Log(souls);
        _souls = souls;
        _position = position;
        _sceneID = sceneID;
        _playerDied = died;
    }

    /// <summary>
    /// Getter que devuelve <see cref="_souls"/>.
    /// </summary>
    /// <returns>Un long que contiene las almas a devolver.</returns>
    public long getSouls()
    {
        return _souls;
    }

    /// <summary>
    /// Getter que devuelve <see cref="_position"/>.
    /// </summary>
    /// <returns>Un array de floats que contiene la posición del objeto.</returns>
    public float[] getPosition()
    {
        return _position;
    }

    /// <summary>
    /// Getter que devuelve <see cref="_sceneID"/>.
    /// </summary>
    /// <returns>Un int que contiene el ID interno de la escena.</returns>
    public int getSceneID()
    {
        return _sceneID;
    }

    /// <summary>
    /// Getter que devuelve <see cref="_playerDied"/>.
    /// </summary>
    /// <returns>Un bool que indica si el jugador ha muerto o no para instanciar el objeto.</returns>
    public bool getPlayerDied()
    {
        return _playerDied;
    }
}
