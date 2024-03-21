using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// consumableItem es una clase que se usa para usar los objetos consumibles.
/// </summary>
public class consumableItem : generalItem
{
    /// <summary>
    /// Referencia a los datos internos del objeto.
    /// </summary>
    [SerializeField] protected consumableItemData _consumableData;

    /// <summary>
    /// Método que se ejecuta al utilizar el objeto.
    /// </summary>
    public virtual void onUse() { }
    /*{

        //Calculamos los valores reales
        float actualHeal = config.getPlayer().GetComponent<statsController>().getMaxHP() * (_consumableData.getRestore()) / 100f;

        float actualRestore = config.getPlayer().GetComponent<statsController>().getMaxStamina() * (_consumableData.getRestore()) / 100f;
        //Comprobamos qué tipo de consumible es
        switch (_consumableData.getTypeOfConsumable())
        {
            //Restauramos vida en el tiempo o al instante
            case upgradeTypeEnum.HP:
                if (_consumableData.getTimeRestoring() > 0)
                {
                    config.getPlayer().GetComponent<statsController>().healHP(actualHeal, _consumableData.getTimeRestoring());
                }
                else
                {
                    config.getPlayer().GetComponent<statsController>().healHP(actualHeal);
                }
            break;

            //Restauramos stamina en el tiempo o al instante
            case upgradeTypeEnum.stamina:
                if (_consumableData.getTimeRestoring() > 0)
                {
                    config.getPlayer().GetComponent<statsController>().restoreStamina(actualRestore, _consumableData.getTimeRestoring());
                }
                else
                {
                    config.getPlayer().GetComponent<statsController>().restoreStamina(actualRestore);
                }
            break;
        }
    }*/
}
