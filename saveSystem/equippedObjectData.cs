using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// equippedObjectData es una clase que se usa para almacenar la información de los objetos
/// consumibles equipados.
/// </summary>
[System.Serializable]
public class equippedObjectData
{
    /// <summary>
    /// Lista de los objetos equipados.
    /// </summary>
    [SerializeField] private List<newEquippedObjectData> _data;

    /// <summary>
    /// Índice en la lista del objeto que está seleccionado para utilizar.
    /// </summary>
    [SerializeField] private int _indexInEquipped;

    /// <summary>
    /// Getter que devuelve <see cref="_data"/>.
    /// </summary>
    /// <returns><see cref="_data"/>.</returns>
    public List<newEquippedObjectData> getData()
    {
        return _data;
    }

    /// <summary>
    /// Getter que devuelve <see cref="_indexInEquipped"/>.
    /// </summary>
    /// <returns><see cref="_indexInEquipped"/>.</returns>
    public int getIndexInEquipped()
    {
        return _indexInEquipped;
    }

    /// <summary>
    /// Setter que modifica un elemento concreto de <see cref="_data"/>.
    /// </summary>
    /// <param name="index">Índice a modificar.</param>
    /// <param name="obj">Valor a asignar en la posición de memoria.</param>
    public void setEquippedObject(int index, newEquippedObjectData obj)
    {
        _data[index] = obj;
    }

    /// <summary>
    /// Constructor de la clase.
    /// </summary>
    /// <param name="data">Datos a serializar.</param>
    /// <param name="id">Índice del objeto seleccionado.</param>
    public equippedObjectData(List<newEquippedObjectData> data, int id)
    {
        _data = data;
        _indexInEquipped = id;
    }
}
