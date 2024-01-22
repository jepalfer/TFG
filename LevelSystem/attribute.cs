using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attribute : attributeBase
{
    public int getLevel()
    {
        return _level;
    }
    public int getMaxLevel()
    {
        return _maxlevel;
    }

    public void setLevel(int level)
    {
        _level = level;
    }
}
