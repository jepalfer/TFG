using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sceneSwitch : MonoBehaviour
{
    [SerializeField] private int _topAccessID;
    [SerializeField] private List<float> _topAccessCoordinates;

    [SerializeField] private int _rightAccessID;
    [SerializeField] private List<float> _rightAccessCoordinates;

    [SerializeField] private int _bottomAccessID;
    [SerializeField] private List<float> _bottomAccessCoordinates;
    
    [SerializeField] private int _leftAccessID;
    [SerializeField] private List<float> _leftAccessCoordinates;

    [SerializeField] private List<float> _spawnPointCoordinates;

    public List<float> getSpawnPoint()
    {
        return _spawnPointCoordinates;
    }

    public List<float> getTopCoordinates()
    {
        return _topAccessCoordinates;
    }
    public List<float> getRightCoordinates()
    {
        return _rightAccessCoordinates;
    }
    public List<float> getBottomCoordinates()
    {
        return _bottomAccessCoordinates;
    }
    public List<float> getLeftCoordinates()
    {
        return _leftAccessCoordinates;
    }

    public int getTopID()
    {
        return _topAccessID;
    }
    public int getRightID()
    {
        return _rightAccessID;
    }
    public int getBottomID()
    {
        return _bottomAccessID;
    }
    public int getLeftID()
    {
        return _leftAccessID;
    }
}
