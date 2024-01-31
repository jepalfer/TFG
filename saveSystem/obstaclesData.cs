using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class obstaclesData
{

    [SerializeField] private List<sceneObstaclesData> _storedData;

    public List<sceneObstaclesData> getStoredData()
    {
        return _storedData;
    }
    public void incrementSize(sceneObstaclesData obstacleData)
    {
        _storedData.Add(obstacleData);
    }

    public void modifyObstacle(bool active, int obID, int scID)
    {
        sceneObstaclesData obstacle = _storedData.Find(obst => obst.getSceneID() == scID && obst.getObstacleID() == obID);
        obstacle.setIsAcivated(active);
    }

    public obstaclesData()
    {
        _storedData = new List<sceneObstaclesData>();
    }


    public obstaclesData(List<sceneObstaclesData> data)
    {
        _storedData = data;
    }
}
