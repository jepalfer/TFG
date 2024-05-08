using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// skillSlot es una clase que se usa para representar internamente las habilidades de la UI que se modifica en 
/// <see cref="selectSkillUIController"/>.
/// </summary>
public class skillSlot : MonoBehaviour
{
    /// <summary>
    /// Referencia a los datos internos de la habilidad.
    /// </summary>
    private skillData _data;

    /// <summary>
    /// Setter que modifica <see cref="_data"/>.
    /// </summary>
    /// <param name="data">Los datos a asignar.</param>
    public void setData(skillData data)
    {
        _data = data;
    }

    /// <summary>
    /// Getter que devuelve <see cref="_data"/>.
    /// </summary>
    /// <returns>Un objeto de tipo <see cref="skillData"/> que contiene la información interna de la habilidad.</returns>
    public skillData getData()
    {
        return _data;
    }
}
