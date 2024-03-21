using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class breakableWallBehaviour : MonoBehaviour
{
    [SerializeField] private int _wallID;
    private obstaclesData _obstaclesData;
    private void Start()
    {
        _obstaclesData = saveSystem.loadObstaclesData();

        if (_obstaclesData != null)
        {
            sceneObstaclesData currentWall = _obstaclesData.getStoredData().Find(wall => wall.getSceneID() == SceneManager.GetActiveScene().buildIndex && wall.getObstacleID() == _wallID);
            if (currentWall == null)
            {
                _obstaclesData.incrementSize(new sceneObstaclesData(true, _wallID, SceneManager.GetActiveScene().buildIndex));
            }
            else
            {
                if (!currentWall.getIsActivated())
                {
                    Destroy(gameObject);
                }
            }
            saveSystem.saveObstaclesData(_obstaclesData.getStoredData());
        }
        else
        {
            List<sceneObstaclesData> newData = new List<sceneObstaclesData>();
            newData.Add(new sceneObstaclesData(true, _wallID, SceneManager.GetActiveScene().buildIndex));

            saveSystem.saveObstaclesData(newData);
        }
    }

    public void destroyWall()
    {
        _obstaclesData = saveSystem.loadObstaclesData();

        _obstaclesData.modifyObstacle(false, _wallID, SceneManager.GetActiveScene().buildIndex);
        Destroy(gameObject);
        saveSystem.saveObstaclesData(_obstaclesData.getStoredData());
    }
}
