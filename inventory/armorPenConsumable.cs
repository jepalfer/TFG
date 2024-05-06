using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// armorPenConsumable es una clase que implementa el m�todo <see cref="onUse"/> de <see cref="consumableItem"/> para
/// manejar el uso de objetos consumibles que dan penetraci�n de armadura.
/// </summary>
public class armorPenConsumable : consumableItem
{
    /// <summary>
    /// M�todo que se ejecuta al usar un objeto de este tipo.
    /// </summary>
    public override void onUse()
    {
        config.getPlayer().GetComponent<combatController>().useArmorPenConsumable(_consumableData.getValue(), _consumableData.getEffectiveTime());
    }
}
