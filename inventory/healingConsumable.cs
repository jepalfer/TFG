using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// healingConsumable es una clase que implementa el método <see cref="onUse"/> de <see cref="consumableItem"/> para
/// manejar el uso de objetos consumibles que dan curación.
/// </summary>
public class healingConsumable : consumableItem
{
    /// <summary>
    /// Método que se ejecuta al usar un objeto de este tipo.
    /// </summary>
    public override void onUse()
    {
        //Obtenemos la cura del objeto en función a nuestra vida
        float actualHeal = config.getPlayer().GetComponent<statsController>().getMaxHP() * _consumableData.getValue();

        //Si es una cura instantánea
        if (_consumableData.getEffectiveTime() == 0)
        {
            config.getPlayer().GetComponent<statsController>().healHP(actualHeal);
        }
        else //Si es una cura durante un tiempo
        {
            config.getPlayer().GetComponent<statsController>().healHP(actualHeal, _consumableData.getEffectiveTime());
        }
    }
}
