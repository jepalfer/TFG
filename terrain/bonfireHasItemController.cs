using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bonfireHasItemController : MonoBehaviour
{
    void Update()
    {
        if (inputManager.GetKeyDown(inputEnum.cancel) || inputManager.GetKeyDown(inputEnum.accept))
        {
            if (inputManager.GetKeyDown(inputEnum.accept))
            {
                bonfireBehaviour bonfire = gameObject.transform.parent.transform.parent.GetComponent<bonfireBehaviour>();

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
