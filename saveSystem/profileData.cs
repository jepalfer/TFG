using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// profileData es una clase que se usa para guardar los nombres de los perfiles.
/// </summary>
[System.Serializable]
public class profileData
{
    /// <summary>
    /// Lista con todos los nombres/nicks de los perfiles.
    /// </summary>
    [SerializeField] private List<string> _userNames;

    /// <summary>
    /// Constructor de la clase.
    /// </summary>
    public profileData()
    {
        _userNames = profileIndex.getUserNames();
    }

    /// <summary>
    /// Getter que devuelve <see cref="_userNames"/>.
    /// </summary>
    /// <returns><see cref="_userNames"/>.</returns>
    public List<string> getUserNames()
    {
        return _userNames;
    }
}
