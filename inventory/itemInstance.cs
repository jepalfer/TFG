using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// itemInstance es una clase usada para guardar los datos internos de los objetos.
/// </summary>
[System.Serializable]
public class itemInstance
{
    /// <summary>
    /// Referencia a los datos internos.
    /// </summary>
    [SerializeField] private item _data;
   
    /// <summary>
    /// Constructor de la clase.
    /// </summary>
    /// <param name="data">Datos internos del objeto a crear.</param>
    public itemInstance(item data)
    {
        _data = data;
    }

    /// <summary>
    /// Getter que devuelve <see cref="_data"/>.
    /// </summary>
    /// <returns><see cref="_data"/></returns>
    public item getData()
    {
        return _data;
    }
}
