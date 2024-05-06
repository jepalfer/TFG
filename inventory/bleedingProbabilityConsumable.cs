using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// bleedingProbabilityConsumable es una clase que implementa el método <see cref="onUse"/> de <see cref="consumableItem"/> para
/// manejar el uso de objetos consumibles que dan probabilidad de sangrado.
/// </summary>
public class bleedingProbabilityConsumable : consumableItem
{
    /// <summary>
    /// Método que se ejecuta al usar un objeto de este tipo.
    /// </summary>
    public override void onUse()
    {
        config.getPlayer().GetComponent<combatController>().useBleedProbabilityConsumable(_consumableData.getValue(), _consumableData.getEffectiveTime());
    }
}
