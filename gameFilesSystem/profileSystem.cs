using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public static class profileSystem
{
    private static string _profileName;
    private static string _path = Application.persistentDataPath + "/profiles/";
    private static string _currentPath;
    public static bool createProfile(string name)
    {
        _profileName = name;
        bool created = false;

        string newPath = _path + _profileName + "/";
        if (!Directory.Exists(newPath))
        {
            _currentPath = newPath;
            created = true;
            Directory.CreateDirectory(newPath);
        }

        return created;
    }

    public static string getProfileName()
    {
        return _profileName;
    }

    public static string getCurrentPath()
    {
        return _currentPath;
    }

    public static string getPath()
    {
        return _path;
    }

    public static void setCurrentPath(string path)
    {
        _currentPath = path;
    }

    public static void setProfileName(string name)
    {
        _profileName = name;
    }
}
