using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// bonfireHasItemController es una clase que se usa para controlar la UI que informa de que tenemos 
/// objeto clave para aumentar 1 carga.
/// </summary>
public class bonfireHasItemController : MonoBehaviour
{
    /// <summary>
    /// Método que se ejecuta cada frame para actualizar la lógica.
    /// </summary>
    void Update()
    {
        if (inputManager.GetKeyDown(inputEnum.cancel) || inputManager.GetKeyDown(inputEnum.accept))
        {
            if (inputManager.GetKeyDown(inputEnum.accept))
            {
                bonfireBehaviour bonfire = gameObject.transform.parent.transform.parent.GetComponent<bonfireBehaviour>();

                //Eliminamos del inventario el objeto clave 
                config.getInventory().GetComponent<inventoryManager>().removeItemFromInventory(
                    config.getInventory().GetComponent<inventoryManager>().getInventory().Find(
                        item => item.getID() == bonfire.getChargeItem().GetComponent<generalItem>().getID()),
                    1);
                config.getInventory().GetComponent<inventoryManager>().addMaximumRefillable();

            }
            bonfireBehaviour.setIsInObtainCharge(false);
            gameObject.SetActive(false);
        }
    }
}
