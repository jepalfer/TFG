using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healingConsumable : consumableItem
{
    public override void onUse()
    {
        float actualHeal = config.getPlayer().GetComponent<statsController>().getMaxHP() * _consumableData.getValue();

        if (_consumableData.getEffectiveTime() == 0)
        {
            config.getPlayer().GetComponent<statsController>().healHP(actualHeal);
        }
        else
        {
            config.getPlayer().GetComponent<statsController>().healHP(actualHeal, _consumableData.getEffectiveTime());
        }
    }
}
