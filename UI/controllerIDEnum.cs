using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// controllerIDEnum es un enum que se usa para guardar en un <see cref="Dictionary"/> las distintas imágenes que dan
/// feedback visual sobre el mando (des)conectado.
/// </summary>
public enum controllerIDEnum
{
    /// <summary>
    /// El mando es de PlayStation/default.
    /// </summary>
    PS,

    /// <summary>
    /// El mando es de xbox.
    /// </summary>
    XBOX,

    /// <summary>
    /// El mando es de nintendo.
    /// </summary>
    NINTENDO
}