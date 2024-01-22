using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class lastPath
{
    private string _path;
    private string _name;

    public lastPath(string path, string name)
    {
        _path = path;
        _name = name;
    }

    public string getPath()
    {
        return _path;
    }
    public string getName()
    {
        return _name;
    }
}
