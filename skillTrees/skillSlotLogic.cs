using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class skillSlotLogic : MonoBehaviour
{
    [SerializeField] private Image _skillSprite;

    public void setSkillSprite(Sprite sprite)
    {
        _skillSprite.sprite = sprite;
    }
}
