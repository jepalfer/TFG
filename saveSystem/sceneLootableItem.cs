using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class sceneLootableItem
{
    [SerializeField] private int _sceneID;
    [SerializeField] private int _objectID;
    [SerializeField] private int _isLooted;

    public sceneLootableItem(int sceneID, int objectID, int isLooted)
    {
        _sceneID = sceneID;
        _objectID = objectID;
        _isLooted = isLooted;
    }

    public int getSceneID()
    {
        return _sceneID;
    }

    public int getObjectID()
    {
        return _objectID;
    }

    public int getIsLooted()
    {
        return _isLooted;
    }

    public void setSceneID(int sceneID)
    {
        _sceneID = sceneID;
    }

    public void setObjectID(int objectID)
    {
        _objectID = objectID;
    }

    public void setIsLooted(int isLooted)
    {
        _isLooted = isLooted;
    }

}
