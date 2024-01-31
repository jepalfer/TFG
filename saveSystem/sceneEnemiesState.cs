using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class sceneEnemiesState
{
    [SerializeField] private int _sceneID;
    [SerializeField] private int _enemyID;
    [SerializeField] private int _isAlive;
    [SerializeField] private int _isBoss;
    [SerializeField] private int _canRevive;

    public sceneEnemiesState(int sceneID, int enemyID, int enemyAlive, int isBoss, int enemyCanRevive)
    {
        _sceneID = sceneID;
        _enemyID = enemyID;
        _isAlive = enemyAlive;
        _isBoss = isBoss;
        _canRevive = enemyCanRevive;
    }

    public int getSceneID()
    {
        return _sceneID;
    }

    public int getEnemyID()
    {
        return _enemyID;
    }

    public int getIsAlive()
    {
        return _isAlive;
    }

    public int getIsBoss()
    {
        return _isBoss;
    }

    public int getCanRevive()
    {
        return _canRevive;
    }

    public void setSceneID(int sceneID)
    {
        _sceneID = sceneID;
    }

    public void setEnemyID(int enemyID)
    {
        _enemyID = enemyID;
    }

    public void setIsAlive(int isAlive)
    {
        _isAlive = isAlive;
    }

    public void setIsBoss(int isBoss)
    {
        _isBoss = isBoss;
    }

    public void setCanRevive(int enemyCanRevive)
    {
        if (_isBoss == 0)
        {
            _canRevive = enemyCanRevive;
        }
    }
}
