using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skillSlot : MonoBehaviour
{
    private skillData _data;

    public void setData(skillData data)
    {
        _data = data;
    }

    public skillData getData()
    {
        return _data;
    }
}
