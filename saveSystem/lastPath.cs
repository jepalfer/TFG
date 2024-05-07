using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// lastPath es una clase que se usa para almacenar la última ruta utilizada.
/// </summary>
[System.Serializable]
public class lastPath
{
    /// <summary>
    /// Ruta utilizada.
    /// </summary>
    private string _path;

    /// <summary>
    /// Nombre del perfil.
    /// </summary>
    private string _name;

    /// <summary>
    /// Constructor de la clase.
    /// </summary>
    /// <param name="path">Ruta a serializar.</param>
    /// <param name="name">Nombre del perfil a serializar.</param>
    public lastPath(string path, string name)
    {
        _path = path;
        _name = name;
    }

    /// <summary>
    /// Getter que devuelve <see cref="_path"/>.
    /// </summary>
    /// <returns><see cref="_path"/>.</returns>
    public string getPath()
    {
        return _path;
    }

    /// <summary>
    /// Getter que devuelve <see cref="_name"/>.
    /// </summary>
    /// <returns><see cref="_name"/>.</returns>
    public string getName()
    {
        return _name;
    }
}
