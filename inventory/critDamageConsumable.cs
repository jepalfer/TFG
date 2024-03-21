using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class critDamageConsumable : consumableItem
{
    public override void onUse()
    {
        config.getPlayer().GetComponent<combatController>().useCritDamageConsumable(_consumableData.getValue(), _consumableData.getEffectiveTime());
    }
}
