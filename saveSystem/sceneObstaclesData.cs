using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// sceneObstaclesData es una clase auxiliar que se usa para almacenar la información de los obstáculos de
/// una escena concreta.
/// </summary>
[System.Serializable]
public class sceneObstaclesData
{
    /// <summary>
    /// Flag booleano que indica si el obstáculo ha sido o no activado.
    /// </summary>
    [SerializeField] private bool _isActivated;

    /// <summary>
    /// ID interno del obstáculo.
    /// </summary>
    [SerializeField] private int _obstacleID;
    
    /// <summary>
    /// ID interno de la escena en la que se encuentra el obstáculo.
    /// </summary>
    [SerializeField] private int _sceneID;

    /// <summary>
    /// Getter que devuelve <see cref="_isActivated"/>.
    /// </summary>
    /// <returns><see cref="_isActivated"/>.</returns>
    public bool getIsActivated()
    {
        return _isActivated;
    }

    /// <summary>
    /// Getter que devuelve <see cref="_obstacleID"/>.
    /// </summary>
    /// <returns><see cref="_obstacleID"/>.</returns>
    public int getObstacleID()
    {
        return _obstacleID;
    }

    /// <summary>
    /// Getter que devuelve <see cref="_sceneID"/>.
    /// </summary>
    /// <returns><see cref="_sceneID"/>.</returns>
    public int getSceneID()
    {
        return _sceneID;
    }

    /// <summary>
    /// Setter que modifica <see cref="_isActivated"/>.
    /// </summary>
    /// <param name="active">Nuevo valor a asignar.</param>
    public void setIsAcivated(bool active)
    {
        _isActivated = active;
    }

    /// <summary>
    /// Constructor de la clase.
    /// </summary>
    /// <param name="active">Si el obstáculo ha sido o no activado.</param>
    /// <param name="obID">ID interno del obstáculo a serializar.</param>
    /// <param name="scID">ID interno de la escena en la que se encuentra el obstáculo.</param>
    public sceneObstaclesData(bool active, int obID, int scID)
    {
        _isActivated = active;
        _obstacleID = obID;
        _sceneID = scID;
    }
}
