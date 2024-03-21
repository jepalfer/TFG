using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class critProbabilityConsumable : consumableItem
{
    public override void onUse()
    {
        config.getPlayer().GetComponent<combatController>().useCritProbabilityConsumable(_consumableData.getValue(), _consumableData.getEffectiveTime());
    }
}
