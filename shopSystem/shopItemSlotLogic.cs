using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class shopItemSlotLogic : MonoBehaviour
{
    [SerializeField] private Button _slotButton;
    [SerializeField] private Image _slotImage;
    private int _slotID;


    public Button getSlotButton()
    {
        return _slotButton;
    }

    public void setSlotID(int ID)
    {
        _slotID = ID;
    }

    public int getSlotID()
    {
        return _slotID;
    }

    public void setSprite(Sprite sprite)
    {
        _slotImage.sprite = sprite;
    }
}
