using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// sceneLootableItem es una clase auxiliar usada para guardar la información de los objetos looteables de una escena
/// concreta.
/// </summary>
[System.Serializable]
public class sceneLootableItem
{
    /// <summary>
    /// ID de la escena en la que se encuentra el objeto.
    /// </summary>
    [SerializeField] private int _sceneID;

    /// <summary>
    /// ID del objeto.
    /// </summary>
    [SerializeField] private int _objectID;
    
    /// <summary>
    /// Flag que indica si ya ha sido recogido o no.
    /// </summary>
    [SerializeField] private int _isLooted;

    /// <summary>
    /// Constructor de la clase.
    /// </summary>
    /// <param name="sceneID">ID interno de la escena donde se encuentra el objeto.</param>
    /// <param name="objectID">ID interno del objeto a serializar.</param>
    /// <param name="isLooted">Flag que indica si ha sido recogido o no.</param>
    public sceneLootableItem(int sceneID, int objectID, int isLooted)
    {
        _sceneID = sceneID;
        _objectID = objectID;
        _isLooted = isLooted;
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
    /// Getter que devuelve <see cref="_objectID"/>.
    /// </summary>
    /// <returns><see cref="_objectID"/>.</returns>
    public int getObjectID()
    {
        return _objectID;
    }

    /// <summary>
    /// Getter que devuelve <see cref="_isLooted"/>.
    /// </summary>
    /// <returns><see cref="_isLooted"/>.</returns>
    public int getIsLooted()
    {
        return _isLooted;
    }

    /// <summary>
    /// Setter que modifica <see cref="_isLooted"/>.
    /// </summary>
    /// <param name="isLooted">Valor a asignar.</param>
    public void setIsLooted(int isLooted)
    {
        _isLooted = isLooted;
    }

}
