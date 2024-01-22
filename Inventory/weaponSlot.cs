using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class weaponSlot
{
    [SerializeField] private GameObject _weapon;
    public weaponSlot(GameObject weapon)
    {
        _weapon = weapon;
    }

    public GameObject getWeapon()
    {
        return _weapon;
    }


    private void setWeapon(GameObject weapon)
    {
        _weapon = weapon;
    }


    public int getID()
    {
        return getWeapon().GetComponent<weapon>().getID();
    }

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
    /*
    public List<Skill> getUnlockedSkills()
    {
        return getWeapon().GetComponent<Weapon>().getUnlockedSkills();
    }*/

    public handEnum getHand()
    {
        return getWeapon().GetComponent<weapon>().getHand();
    }
}
