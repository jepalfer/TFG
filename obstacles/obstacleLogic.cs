using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class obstacleLogic : MonoBehaviour
{
    [SerializeField] private GameObject _associatedWall;
    [SerializeField] private int _obstacleID;
    [SerializeField] private bool _hasBeenActivated = false;
    [SerializeField] private obstaclesData _data;
    public GameObject getAssociatedWall()
    {
        return _associatedWall;
    }

    public int getObstacleID()
    {
        return _obstacleID;
    }

    public bool getHasBeenActivated()
    {
        return _hasBeenActivated;
    }

    private void Awake()
    {
        _data = saveSystem.loadObstaclesData();
        sceneObstaclesData obstacle = new sceneObstaclesData(false, _obstacleID, SceneManager.GetActiveScene().buildIndex);
        if (_data == null)
        {
            obstaclesData state = new obstaclesData();
            state.incrementSize(obstacle);
            saveSystem.saveObstaclesData(state.getStoredData());
        }
        else
        {
            sceneObstaclesData searchedObstacle = _data.getStoredData().Find(obst => obst.getObstacleID() == _obstacleID && obst.getSceneID() == SceneManager.GetActiveScene().buildIndex);
        
            if (searchedObstacle == null)
            {
                _data.incrementSize(searchedObstacle);
                saveSystem.saveObstaclesData(_data.getStoredData());
            }
            else
            {
                if (!searchedObstacle.getIsActivated())
                {
                    //_associatedWall.GetComponent<BoxCollider2D>().enabled = false;
                    _hasBeenActivated = true;
                    GetComponent<BoxCollider2D>().enabled = false;
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _hasBeenActivated = true;
    }

    private void Update()
    {
        if (_hasBeenActivated && GetComponent<BoxCollider2D>().enabled)
        {
            GetComponent<BoxCollider2D>().enabled = false;
            GetComponent<Animator>().SetTrigger("pressed");
        }
    }
}
