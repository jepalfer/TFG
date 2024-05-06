using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// attribute es una clase usada para representar internamente los atributos.
/// </summary>
public class attribute : attributeBase
{
    /// <summary>
    /// Getter que devuelve <see cref="attributeBase._level"/>
    /// </summary>
    /// <returns><see cref="attributeBase._level"/></returns>
    public int getLevel()
    {
        return _level;
    }
    /// <summary>
    /// Getter que devuelve <see cref="attributeBase._maxlevel"/>
    /// </summary>
    /// <returns><see cref="attributeBase._maxlevel"/></returns>
    public int getMaxLevel()
    {
        return _maxlevel;
    }

    /// <summary>
    /// Setter que modifica <see cref="attributeBase._level"/>.
    /// </summary>
    /// <param name="level">Nivel a asignar.</param>
    public void setLevel(int level)
    {
        _level = level;
    }
}
