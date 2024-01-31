using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

/// <summary>
/// profileSystem es una clase que se usa para crear los archivos de guardado y cargar la partida correspondiente.
/// </summary>
public static class profileSystem
{
    /// <summary>
    /// El nombre del perfil.
    /// </summary>
    private static string _profileName;

    /// <summary>
    /// El path a la carpeta que contiene los perfiles.
    /// </summary>
    private static string _path = Application.persistentDataPath + "/profiles/";

    /// <summary>
    /// El path actual.
    /// </summary>
    private static string _currentPath;

    /// <summary>
    /// Método que sirve para crear un perfil dado un nombre.
    /// </summary>
    /// <param name="name">El nombre del perfil.</param>
    /// <returns>Un booleano que indica si se ha podido o no crear el perfil.</returns>
    public static bool createProfile(string name)
    {
        _profileName = name;
        bool created = false;

        string newPath = _path + _profileName + "/";
        //Creamos el perfil si no existe
        if (!Directory.Exists(newPath))
        {
            _currentPath = newPath;
            created = true;
            Directory.CreateDirectory(newPath);
        }

        return created;
    }
    /// <summary>
    /// Getter que devuelve <see cref="_profileName"/>.
    /// </summary>
    /// <returns>Un string que contiene el nombre del perfil.</returns>
    public static string getProfileName()
    {
        return _profileName;
    }

    /// <summary>
    /// Getter que devuelve <see cref="_currentPath"/>.
    /// </summary>
    /// <returns>Un string que contiene el path del perfil actual.</returns>
    public static string getCurrentPath()
    {
        return _currentPath;
    }

    /// <summary>
    /// Getter que devuelve <see cref="_path"/>.
    /// </summary>
    /// <returns>Un string que contiene el path general.</returns>
    public static string getPath()
    {
        return _path;
    }

    /// <summary>
    /// Setter que modifica <see cref="_currentPath"/>.
    /// </summary>
    /// <param name="path">El path que queremos cargar.</param>
    public static void setCurrentPath(string path)
    {
        _currentPath = path;
    }

    /// <summary>
    /// Setter que modifica <see cref="_profileName"/>.
    /// </summary>
    /// <param name="name">El nombre del perfil.</param>
    public static void setProfileName(string name)
    {
        _profileName = name;
    }
}
