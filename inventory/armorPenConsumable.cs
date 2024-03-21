using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class armorPenConsumable : consumableItem
{
    public override void onUse()
    {
        config.getPlayer().GetComponent<combatController>().useArmorPenConsumable(_consumableData.getValue(), _consumableData.getEffectiveTime());
    }
}
