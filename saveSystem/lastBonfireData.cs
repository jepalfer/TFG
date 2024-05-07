using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// lastBonfireData es una clase que se usa para guardar la información relativa a la
/// última hoguera en la que se descansó.
/// </summary>
[System.Serializable]
public class lastBonfireData
{
    /// <summary>
    /// Coordenadas donde se encontraba la hoguera.
    /// </summary>
    [SerializeField] private float[] _bonfireCoordinates;

    /// <summary>
    /// ID de la escena a la que pertenece la hoguera.
    /// </summary>
    [SerializeField] private int _sceneID;

    /// <summary>
    /// Constructor de la clase.
    /// </summary>
    /// <param name="coordinates">Coordenadas de la hoguera.</param>
    /// <param name="scene">ID de la escena.</param>
    public lastBonfireData(float[] coordinates, int scene)
    {
        _bonfireCoordinates = coordinates;
        _sceneID = scene;
    }

    /// <summary>
    /// Getter que devuelve <see cref="_bonfireCoordinates"/>.
    /// </summary>
    /// <returns><see cref="_bonfireCoordinates"/>.</returns>
    public float[] getBonfireCoordinates()
    {
        return _bonfireCoordinates;
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
