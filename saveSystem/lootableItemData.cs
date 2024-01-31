using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class lootableItemData
{
    [SerializeField] private List<sceneLootableItem> _objectsStates = new List<sceneLootableItem>();

    public lootableItemData()
    {
        _objectsStates = new List<sceneLootableItem>();
    }

    public lootableItemData(List<sceneLootableItem> data)
    {
        _objectsStates = data;
    }

    public void modifyEnemyState(int sceneID, int objectID, int isLooted)
    {
        sceneLootableItem enemy = _objectsStates.Find(item => item.getSceneID() == sceneID && item.getObjectID() == objectID);
        enemy.setIsLooted(isLooted);
    }

    public void incrementSize(sceneLootableItem sceneState)
    {
        _objectsStates.Add(sceneState);
    }

    public List<sceneLootableItem> getObjectsStates()
    {
        return _objectsStates;
    }
}
