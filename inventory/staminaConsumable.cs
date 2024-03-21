using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class staminaConsumable : consumableItem
{
    public override void onUse()
    {
        config.getPlayer().GetComponent<combatController>().useStaminaUpgradeConsumable(_consumableData.getValue(), _consumableData.getEffectiveTime());
    }
}
