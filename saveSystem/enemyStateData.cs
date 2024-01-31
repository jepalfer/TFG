using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class enemyStateData
{
    [SerializeField] private List<sceneEnemiesState> _enemyStates = new List<sceneEnemiesState>();
    
    public enemyStateData()
    {
        _enemyStates = new List<sceneEnemiesState>();
    }

    public enemyStateData(List<sceneEnemiesState> data)
    {
        _enemyStates = data;
    }

    public void modifyEnemyState(int sceneID, int enemyID, int isAlive, int canRevive)
    {
        sceneEnemiesState enemy = _enemyStates.Find(item => item.getSceneID() == sceneID && item.getEnemyID() == enemyID);
        enemy.setIsAlive(isAlive);
        enemy.setCanRevive(canRevive);
    }

    public void incrementSize(sceneEnemiesState sceneState)
    {
        _enemyStates.Add(sceneState);
    }

    public List<sceneEnemiesState> getEnemyStates()
    {
        return _enemyStates;
    }

}
