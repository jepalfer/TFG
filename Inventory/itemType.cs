using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum itemType
{
    [SerializeField] keyItem,
    [SerializeField] consumable,
    [SerializeField] weapon,
    [SerializeField] upgrade_material
}
