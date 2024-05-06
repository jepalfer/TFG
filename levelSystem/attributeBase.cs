using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// attributeBase es una clase de la cual hereda <see cref="attribute"/>.
/// </summary>
public class attributeBase
{
    /// <summary>
    /// Nivel en el cual se encuentra el atributo.
    /// </summary>
    protected int _level = 1;

    /// <summary>
    /// Nivel máximo al cual puede llegar un atributo.
    /// </summary>
    protected int _maxlevel = 99;

}
