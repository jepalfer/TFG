using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class profileData
{
    [SerializeField] private List<string> _userNames;
    public profileData()
    {
        _userNames = profileIndex.getUserNames();
    }

    public List<string> getUserNames()
    {
        return _userNames;
    }
}
