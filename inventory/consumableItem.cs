using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class consumableItem : generalItem
{
    [SerializeField] private consumableItemData _consumableData;
    public void onUse()
    {
        float actualHeal = config.getPlayer().GetComponent<statsController>().getMaxHP() * (_consumableData.getRestore()) / 100f;
        float actualRestore = config.getPlayer().GetComponent<statsController>().getMaxStamina() * (_consumableData.getRestore()) / 100f;
        switch (_consumableData.getTypeOfConsumable())
        {
            case consumableTypeEnum.HP:
                if (_consumableData.getTimeRestoring() > 0)
                {
                    config.getPlayer().GetComponent<statsController>().healHP(actualHeal, _consumableData.getTimeRestoring());
                }
                else
                {
                    config.getPlayer().GetComponent<statsController>().healHP(actualHeal);
                }
            break;

            case consumableTypeEnum.stamina:
                if (_consumableData.getTimeRestoring() > 0)
                {
                    config.getPlayer().GetComponent<statsController>().restoreStamina(actualRestore, _consumableData.getTimeRestoring());
                }
                else
                {
                    config.getPlayer().GetComponent<statsController>().restoreStamina(actualRestore);
                }
            break;
        }
    }
}
