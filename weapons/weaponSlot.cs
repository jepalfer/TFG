using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// weaponSlot es una clase que representa las armas a instanciar.
/// </summary>
[System.Serializable]
public class weaponSlot
{
    /// <summary>
    /// Referencia al arma que se instancia.
    /// </summary>
    [SerializeField] private GameObject _weapon;

    /// <summary>
    /// Constructor con parámetros de la clase que asigna un arma a <see cref="_weapon"/>.
    /// </summary>
    /// <param name="weapon">El arma a asignar.</param>
    public weaponSlot(GameObject weapon)
    {
        _weapon = weapon;
    }

    /// <summary>
    /// Getter que devuelve <see cref="_weapon"/>.
    /// </summary>
    /// <returns>Un GameObject que representa el arma.</returns>
    public GameObject getWeapon()
    {
        return _weapon;
    }

    /*
    private void setWeapon(GameObject weapon)
    {
        _weapon = weapon;
    }
    */

    /// <summary>
    /// Getter que devuelve <see cref="generalItem.getID()"/>.
    /// </summary>
    /// <returns>Un valor entero que representa el ID del arma.</returns>
    public int getID()
    {
        return getWeapon().GetComponent<weapon>().getID();
    }

    /*
    public int getNumberOfAttacks()
    {
        return getWeapon().GetComponent<weapon>().getNumberOfAttacks();
    }

    public int getWeaponLevel()
    {
        return getWeapon().GetComponent<weapon>().getWeaponLevel();
    }

    public int getCurrentAttack()
    {
        return getWeapon().GetComponent<weapon>().getCurrentAttack();
    }

    public long calculateXpNextLevel(int level)
    {
        return weapon.calculateXpNextLevel(level);
    }

    public bool canLevelUp()
    {
        return true;
    }

    public bool getIsAttacking()
    {
        return getWeapon().GetComponent<weapon>().getIsAttacking();
    }

    public scalingEnum getDexterityScale()
    {
        return getWeapon().GetComponent<weapon>().getDexterityScale();
    }
    public scalingEnum getStrengthScale()
    {
        return getWeapon().GetComponent<weapon>().getStrengthScale();
    }
    public scalingEnum getPrecisionScale()
    {
        return getWeapon().GetComponent<weapon>().getPrecisionScale();
    }

    public int getBaseDMG()
    {
        return getWeapon().GetComponent<weapon>().getBaseDMG();
    }

    public int getTotalDMG()
    {
        return getWeapon().GetComponent<weapon>().getTotalDMG();
    }

    public bool getCanAttack()
    {
        return getWeapon().GetComponent<weapon>().getCanAttack();
    }

    public int calculateDMG(int strengthLVL, int dexterityLVL, int precisionLVL)
    {
        return getWeapon().GetComponent<weapon>().calculateDMG(strengthLVL, dexterityLVL, precisionLVL);
    }

    public int calculateBaseDMG()
    {
        return getWeapon().GetComponent<weapon>().calculateBaseDMG();
    }

    public int calculateStrengthDMG(int level)
    {
        return getWeapon().GetComponent<weapon>().calculateStrengthDMG(level);
    }
    public int calculateDexterityDMG(int level)
    {
        return getWeapon().GetComponent<weapon>().calculateDexterityDMG(level);
    }

    public int calculatePrecisionDMG(int level)
    {
        return getWeapon().GetComponent<weapon>().calculatePrecisionDMG(level);
    }
    
    */

    /// <summary>
    /// Getter que devuelve <see cref="weapon.getHand()"/>.
    /// </summary>
    /// <returns>Un valor de tipo <see cref="handEnum"/> que representa la mano en la que se equipa el arma.</returns>
    public handEnum getHand()
    {
        return getWeapon().GetComponent<weapon>().getHand();
    }
}
