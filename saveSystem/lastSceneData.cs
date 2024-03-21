using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class lastSceneData
{
    [SerializeField] private int _sceneID;

    public lastSceneData(int sceneID)
    {
        _sceneID = sceneID;
    }

    public int getSceneID()
    {
        return _sceneID;
    }
}
