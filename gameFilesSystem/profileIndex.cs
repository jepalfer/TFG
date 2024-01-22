using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class profileIndex
{
    private static List<string> _userNames = new List<string>();

    public static List<string> getUserNames()
    {
        return _userNames;
    }

    public static void addName(string name)
    {
        _userNames.Add(name);
    }

    public static void removeName(string name)
    {
        int index = _userNames.FindIndex(entry => entry.CompareTo(name) == 0);
        if (index != -1)
        {
            _userNames.RemoveAt(index);
            saveSystem.saveProfiles();
        }
    }

    public static void setNames(List<string> names)
    {
        _userNames = names;
    }
}
