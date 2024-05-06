using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// critDamageConsumable es una clase que implementa el método <see cref="onUse"/> de <see cref="consumableItem"/> para
/// manejar el uso de objetos consumibles que dan daño crítico.
/// </summary>
public class critDamageConsumable : consumableItem
{
    /// <summary>
    /// Método que se ejecuta al usar un objeto de este tipo.
    /// </summary>
    public override void onUse()
    {
        config.getPlayer().GetComponent<combatController>().useCritDamageConsumable(_consumableData.getValue(), _consumableData.getEffectiveTime());
    }
}
