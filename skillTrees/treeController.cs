using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class treeController : MonoBehaviour
{
    [SerializeField] private GameObject _initialSkill;

    public GameObject getInitialSkill()
    {
        return _initialSkill;
    }
}
