using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// lastSceneData es una clase que se usa para guardar el ID de la última escena visitada.
/// </summary>
[System.Serializable]
public class lastSceneData
{
    /// <summary>
    /// ID interno de la escena.
    /// </summary>
    [SerializeField] private int _sceneID;

    /// <summary>
    /// Constructor de la clase.
    /// </summary>
    /// <param name="sceneID">ID de la escena visitada.</param>
    public lastSceneData(int sceneID)
    {
        _sceneID = sceneID;
    }

    /// <summary>
    /// Getter que devuelve <see cref="_sceneID"/>.
    /// </summary>
    /// <returns><see cref="_sceneID"/>.</returns>
    public int getSceneID()
    {
        return _sceneID;
    }
}
