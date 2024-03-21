using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bonfireNoItemController : MonoBehaviour
{
    void Update()
    {
        if (inputManager.GetKeyDown(inputEnum.cancel) || inputManager.GetKeyDown(inputEnum.accept))
        {
            bonfireBehaviour.setIsInNoItem(false);
            gameObject.SetActive(false);
        }
    }
}
