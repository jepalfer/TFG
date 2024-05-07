using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// lootableItemData es una clase que se usa para guardar la información de los objetos looteables.
/// </summary>
[System.Serializable]
public class lootableItemData
{
    /// <summary>
    /// Lista de los estados de todos los objetos.
    /// </summary>
    [SerializeField] private List<sceneLootableItem> _objectsStates = new List<sceneLootableItem>();

    /// <summary>
    /// Constructor por defecto de la clase. Crea una lista vacía.
    /// </summary>
    public lootableItemData()
    {
        _objectsStates = new List<sceneLootableItem>();
    }

    /// <summary>
    /// Constructor con parámetros de la clase.
    /// </summary>
    /// <param name="data">Lista de estados a serializar.</param>
    public lootableItemData(List<sceneLootableItem> data)
    {
        _objectsStates = data;
    }

    /// <summary>
    /// Método usado para modificar el estado de un objeto.
    /// </summary>
    /// <param name="sceneID">ID de la escena en la que se encuentra el objeto.</param>
    /// <param name="objectID">ID del objeto.</param>
    /// <param name="isLooted">Booleano que indica si ya ha sido recogido o no.</param>
    public void modifyItemState(int sceneID, int objectID, int isLooted)
    {
        sceneLootableItem item = _objectsStates.Find(item => item.getSceneID() == sceneID && item.getObjectID() == objectID);
        item.setIsLooted(isLooted);
    }

    /// <summary>
    /// Método para incrementar en 1 la lista de estados.
    /// </summary>
    /// <param name="sceneState">Estado a añadir.</param>
    public void incrementSize(sceneLootableItem sceneState)
    {
        _objectsStates.Add(sceneState);
    }

    /// <summary>
    /// Getter que devuelve <see cref="_objectsStates"/>.
    /// </summary>
    /// <returns><see cref="_objectsStates"/>.</returns>
    public List<sceneLootableItem> getObjectsStates()
    {
        return _objectsStates;
    }
}
