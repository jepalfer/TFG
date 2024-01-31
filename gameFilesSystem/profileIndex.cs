using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// profileIndex es una clase que se usa para guardar los nombres de los perfiles.
/// </summary>
public static class profileIndex
{
    /// <summary>
    /// La lista con los nombres de los perfiles.
    /// </summary>
    private static List<string> _userNames = new List<string>();

    /// <summary>
    /// Getter que devuelve <see cref="_userNames"/>.
    /// </summary>
    /// <returns>Una lista que contiene los strings con los nombres de los perfiles.</returns>
    public static List<string> getUserNames()
    {
        return _userNames;
    }

    /// <summary>
    /// Método que añade un nombre a <see cref="_userNames"/>.
    /// </summary>
    /// <param name="name">El nombre a añadir a la lista.</param>
    public static void addName(string name)
    {
        _userNames.Add(name);
    }

    /// <summary>
    /// Método que elimina un nombre de <see cref="_userNames"/>.
    /// </summary>
    /// <param name="name">El nombre a eliminar de la lista.</param>
    public static void removeName(string name)
    {
        int index = _userNames.FindIndex(entry => entry.CompareTo(name) == 0);
        if (index != -1)
        {
            _userNames.RemoveAt(index);
            saveSystem.saveProfiles();
        }
    }

    /// <summary>
    /// Setter que asigna una lista de nombres a <see cref="_userNames"/>.
    /// </summary>
    /// <param name="names">La lista de nombres a asignar a <see cref="_userNames"/>.</param>
    public static void setNames(List<string> names)
    {
        _userNames = names;
    }
}
