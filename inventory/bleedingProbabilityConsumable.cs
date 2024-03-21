using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bleedingProbabilityConsumable : consumableItem
{
    public override void onUse()
    {
        config.getPlayer().GetComponent<combatController>().useBleedProbabilityConsumable(_consumableData.getValue(), _consumableData.getEffectiveTime());
    }
}
