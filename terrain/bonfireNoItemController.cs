using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// bonfireNoItemController es una clase que se usa para controlar la UI que informa de que no tenemos 
/// objeto clave para aumentar 1 carga.
/// </summary>
public class bonfireNoItemController : MonoBehaviour
{
    /// <summary>
    /// Método que se ejecuta cada frame para actualizar la lógica.
    /// </summary>
    void Update()
    {
        if (inputManager.GetKeyDown(inputEnum.cancel) || inputManager.GetKeyDown(inputEnum.accept))
        {
            bonfireBehaviour.setIsInNoItem(false);
            gameObject.SetActive(false);
        }
    }
}
