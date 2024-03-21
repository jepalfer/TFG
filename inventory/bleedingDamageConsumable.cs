using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bleedingDamageConsumable : consumableItem
{
    public override void onUse()
    {
        config.getPlayer().GetComponent<combatController>().useBleedDamageConsumable(_consumableData.getValue(), _consumableData.getEffectiveTime());
    }
}
