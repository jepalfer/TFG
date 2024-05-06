using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// bleedingDamageConsumable es una clase que implementa el m�todo <see cref="onUse"/> de <see cref="consumableItem"/> para
/// manejar el uso de objetos consumibles que dan da�o de sangrado.
/// </summary>
public class bleedingDamageConsumable : consumableItem
{
    /// <summary>
    /// M�todo que se ejecuta al usar un objeto de este tipo.
    /// </summary>
    public override void onUse()
    {
        config.getPlayer().GetComponent<combatController>().useBleedDamageConsumable(_consumableData.getValue(), _consumableData.getEffectiveTime());
    }
}
