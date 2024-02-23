using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class lastBonfireData
{
    [SerializeField] private float[] _bonfireCoordinates;
    [SerializeField] private int _sceneID;

    public lastBonfireData(float[] coordinates, int scene)
    {
        _bonfireCoordinates = coordinates;
        _sceneID = scene;
    }

    public float[] getBonfireCoordinates()
    {
        return _bonfireCoordinates;
    }

    public int getSceneID()
    {
        return _sceneID;
    }
}
