using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// doubleJumpSkill es una clase que se usa para establecer cuándo se puede hacer el doble salto.
/// </summary>
public class doubleJumpSkill : MonoBehaviour
{
    /// <summary>
    /// Método que se ejecuta al iniciar el script.
    /// </summary>
    void Start()
    {
        config.getPlayer().GetComponent<playerMovement>().setCanDoubleJump(true);   
    }
    
    /// <summary>
    /// Método que se ejecuta al destruir la habilidad (cambiar de escena o desequipar el arma).
    /// </summary>
    private void OnDestroy()
    {
        config.getPlayer().GetComponent<playerMovement>().setCanDoubleJump(false);
    }
}
