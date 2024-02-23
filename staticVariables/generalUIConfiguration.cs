using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// generalUIConfiguration es una clase que se usa para tener una referencia al campo de las almas en la UI general.
/// </summary>
public static class generalUIConfiguration
{
    /// <summary>
    /// Referencia al campo de texto de la UI.
    /// </summary>
    private static TextMeshProUGUI _souls;

    /// <summary>
    /// Setter que modifica la referencia a <see cref="_souls"/>.
    /// </summary>
    /// <param name="field">La referencia a asignar.</param>
    public static void setSouls(TextMeshProUGUI field)
    {
        _souls = field;
    }

    /// <summary>
    /// Getter que devuelve <see cref="_souls"/>.
    /// </summary>
    /// <returns>Un objeto de tipo TextMeshProUGUI que contiene la referencia al campo de texto.</returns>
    public static TextMeshProUGUI getSouls()
    {
        return _souls;
    }
}
