using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class sceneObstaclesData
{
    [SerializeField] private bool _isActivated;
    [SerializeField] private int _obstacleID;
    [SerializeField] private int _sceneID;

    public bool getIsActivated()
    {
        return _isActivated;
    }

    public int getObstacleID()
    {
        return _obstacleID;
    }

    public int getSceneID()
    {
        return _sceneID;
    }

    public void setIsAcivated(bool active)
    {
        _isActivated = active;
    }
    public void setObstacleID(int ID)
    {
        _obstacleID = ID;
    }
    public void setSceneID(int ID)
    {
        _sceneID = ID;
    }

    public sceneObstaclesData(bool active, int obID, int scID)
    {
        _isActivated = active;
        _obstacleID = obID;
        _sceneID = scID;
    }
}
