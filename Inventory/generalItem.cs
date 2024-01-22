using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class generalItem : MonoBehaviour
{
    [SerializeField] protected itemInstance _data;


    public itemInstance getData()
    {
        return _data;
    }
    public itemType getTipo()
    {
        return _data.getData().getItemData().getTipo();
    }
    public int getID()
    {
        return _data.getData().getItemData().getId();
    }
    public Sprite getIcon()
    {
        return _data.getData().getItemData().getIcon();
    }
    public string getName()
    {
        return _data.getData().getItemData().getName();
    }
    public string getDesc()
    {
        return _data.getData().getItemData().getDesc();
    }


    public Sprite getRender()
    {
        return _data.getData().getItemData().getRender();
    }


}
