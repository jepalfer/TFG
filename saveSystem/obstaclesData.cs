using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// obstaclesData es una clase que se usa para serializar la información de los obstáculos.
/// </summary>
[System.Serializable]
public class obstaclesData
{
    /// <summary>
    /// Lista con los datos de los obstáculos de cada escena.
    /// </summary>
    [SerializeField] private List<sceneObstaclesData> _storedData;

    /// <summary>
    /// Getter que devuelve <see cref="_storedData"/>.
    /// </summary>
    /// <returns><see cref="_storedData"/>.</returns>
    public List<sceneObstaclesData> getStoredData()
    {
        return _storedData;
    }

    /// <summary>
    /// Método que incrementa en 1 el tamaño de <see cref="_storedData"/>.
    /// </summary>
    /// <param name="obstacleData">Nuevo estado a añadir a la lista.</param>
    public void incrementSize(sceneObstaclesData obstacleData)
    {
        _storedData.Add(obstacleData);
    }

    /// <summary>
    /// Método usado para modificar un estado concreto de <see cref="_storedData"/>.
    /// </summary>
    /// <param name="active">Booleano que indica si el obstáculo está o no activo.</param>
    /// <param name="obID">ID interno del obstáculo.</param>
    /// <param name="scID">ID interno de la escena.</param>
    public void modifyObstacle(bool active, int obID, int scID)
    {
        sceneObstaclesData obstacle = _storedData.Find(obst => obst.getSceneID() == scID && obst.getObstacleID() == obID);
        obstacle.setIsAcivated(active);
    }

    /// <summary>
    /// Constructor por defecto de la clase. Crea una lista vacía.
    /// </summary>
    public obstaclesData()
    {
        _storedData = new List<sceneObstaclesData>();
    }

    /// <summary>
    /// Constructor con parámetros de la clase. Asigna una lista de estados a <see cref="_storedData"/>.
    /// </summary>
    /// <param name="data">Lista a asignar.</param>
    public obstaclesData(List<sceneObstaclesData> data)
    {
        _storedData = data;
    }
}
