using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// playerHitController es una clase que controla cuando la hitbox del jugador ha golpeado a un enemigo.
/// </summary>
public class playerHitController : MonoBehaviour
{
    /// <summary>
    /// Booleano que indica si el ataque ha sido del arma primaria.
    /// </summary>
    private bool _primaryAttack;

    /// <summary>
    /// Booleano que indica si el ataque ha sido del arma secundaria.
    /// </summary>
    private bool _secundaryAttack;

    /// <summary>
    /// M�todo para comprobar colisiones.
    /// Ver <see cref="weapon"/> y <see cref="enemy"/> para m�s informaci�n.
    /// </summary>
    /// <param name="collision">El collider con el que la hitbox ha chocado.</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Si es la hurtbox enemiga
        if (collision.CompareTag("enemyHurtbox"))
        {
            float bleed = 0f, penetration = 0f, crit = 0f;
            config.getPlayer().GetComponent<combatController>().calculateExtraDamages(ref penetration, ref bleed, ref crit);

            if (_primaryAttack)
            {
                //Si es ataque primario el enemigo recibe el da�o del arma primaria.
                collision.gameObject.transform.parent.gameObject.GetComponent<enemy>().receiveDMG(config.getPlayer().GetComponent<combatController>().getPrimaryWeapon().GetComponent<weapon>().getTotalDMG(), crit, penetration, bleed);
                
                if (collision.gameObject.transform.parent.gameObject.GetComponent<enemy>().getIsArmored())
                {
                    weaponConfig.getPrimaryWeapon().GetComponent<weaponSFXController>().playHit1SFX();
                }
                else
                {
                    weaponConfig.getPrimaryWeapon().GetComponent<weaponSFXController>().playHit2SFX();
                }
            }
            else if (_secundaryAttack)
            {
                //Si es ataque secundario el enemigo recibe el da�o del arma secudaria
                collision.gameObject.transform.parent.gameObject.GetComponent<enemy>().receiveDMG(config.getPlayer().GetComponent<combatController>().getSecundaryWeapon().GetComponent<weapon>().getTotalDMG(), crit, penetration, bleed);
                if (collision.gameObject.transform.parent.gameObject.GetComponent<enemy>().getIsArmored())
                {
                    weaponConfig.getSecundaryWeapon().GetComponent<weaponSFXController>().playHit1SFX();
                }
                else
                {
                    weaponConfig.getSecundaryWeapon().GetComponent<weaponSFXController>().playHit2SFX();
                }
            }
        }
    }
    /// <summary>
    /// Setter para cambiar el valor de <see cref="_primaryAttack"/> y <see cref="_secundaryAttack"/>.
    /// </summary>
    /// <param name="value">El nuevo valor de <see cref="_primaryAttack"/>.</param>
    public void setPrimary(bool value)
    {
        _primaryAttack = value;
        _secundaryAttack = false;
    }
    /// <summary>
    /// Setter para cambiar el valor de <see cref="_primaryAttack"/> y <see cref="_secundaryAttack"/>.
    /// </summary>
    /// <param name="value">El nuevo valor de <see cref="_secundaryAttack"/>.</param>
    public void setSecundary(bool value)
    {
        _primaryAttack = false;
        _secundaryAttack = value;
    }
}
