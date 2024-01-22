using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class weapon : generalItem
{
    public int _weaponLevel = 1;
    protected const int _MAXLEVEL = 10;
    protected int _numberOfAttacks;
    [SerializeField] protected int _currentAttack = 0;
    //[SerializeField] protected List<Skill> _unlockedSkills = new List<Skill>();
    [SerializeField] protected weaponInstance _scalings;
    protected scalingEnum _dexterityScale;
    protected scalingEnum _strengthScale;
    protected scalingEnum _precisionScale;
    [SerializeField] protected int _damage;
    protected int _baseDamage;
    [SerializeField] protected bool _isAttacking;
    [SerializeField] protected bool _canAttack;
    [SerializeField] private bool _piercesArmor;


    [SerializeField] private GameObject _player;
    [SerializeField] private handEnum _hand;

    protected float _timeBetweenAttacks = 1f;
    private void Awake()
    {
/*        _numberOfAttacks = _scalings.getScaling().getNumberOfAttacks();
        _dexterityScale = _scalings.getScaling().getDexterityScalings()[(_weaponLevel - 1)];
        _strengthScale = _scalings.getScaling().getStrengthScalings()[(_weaponLevel - 1)];
        _precisionScale = _scalings.getScaling().getPrecisionScalings()[(_weaponLevel - 1)];*/
        /*
        if (_hand == handEnum.primary)
        {

            TextMeshProUGUI _primaryDMG = GetComponent<LevelUpUI>().getPrimaryDMG();
            _primaryDMG.text = _damage.ToString();
            LevelUPUIConfiguration.setPrimaryDMGValue(_primaryDMG);
        }
        else
        {
            TextMeshProUGUI _secundaryDMG = GetComponent<LevelUpUI>().getSecundaryDMG();
            _secundaryDMG.text = _damage.ToString();
            LevelUPUIConfiguration.setSecundaryDMGValue(_secundaryDMG);
        }*/
    }

    

    public void createWeapon(int index, int level)
    {
        _weaponLevel = level;
        _numberOfAttacks = _scalings.getScaling().getNumberOfAttacks();
        _dexterityScale = _scalings.getScaling().getDexterityScalings()[(_weaponLevel - 1)];
        _strengthScale = _scalings.getScaling().getStrengthScalings()[(_weaponLevel - 1)];
        _precisionScale = _scalings.getScaling().getPrecisionScalings()[(_weaponLevel - 1)];

        _player = config.getPlayer();

        setBaseDMG(calculateBaseDMG());
        setTotalDMG(calculateDMG(statSystem.getStrength().getLevel(), statSystem.getDexterity().getLevel(), statSystem.getPrecision().getLevel()));

        if (_hand == handEnum.primary)
        {
            config.getPlayer().GetComponent<combatController>().assignPrimaryWeapon(gameObject);
            if (levelUPUIConfiguration.getPrimaryDMGValue() != null)
            {
                levelUPUIConfiguration.getPrimaryDMGValue().text = getTotalDMG().ToString();
            }

        }
        else
        {
            config.getPlayer().GetComponent<combatController>().assignSecundaryWeapon(gameObject);
            if (levelUPUIConfiguration.getSecundaryDMGValue() != null)
            {
                levelUPUIConfiguration.getSecundaryDMGValue().text = getTotalDMG().ToString();
            }
        }
    }

    public IEnumerator Attack()
    {
/*        _isAttacking = true;

        if (_player.GetComponent<CombatController>().getPrimaryWeapon() != null)
        {
            Debug.Log(_player.GetComponent<CombatController>().getPrimaryWeapon().GetComponent<Weapon>().getName());
            _player.GetComponent<CombatController>().getPrimaryWeapon().GetComponent<Weapon>().setCurrentAttack(_player.GetComponent<CombatController>().getPrimaryWeapon().GetComponent<Weapon>().getCurrentAttack() + 1);
        }
        if (_player.GetComponent<CombatController>().getSecundaryWeapon() != null)
        {
            Debug.Log(_player.GetComponent<CombatController>().getSecundaryWeapon().GetComponent<Weapon>().getName());
            _player.GetComponent<CombatController>().getSecundaryWeapon().GetComponent<Weapon>().setCurrentAttack(_player.GetComponent<CombatController>().getSecundaryWeapon().GetComponent<Weapon>().getCurrentAttack() + 1);
        }

        _player.GetComponent<PlayerMovement>().setCanMove(false);
        _player.GetComponent<PlayerMovement>().setCanRoll(false);
        _player.GetComponent<PlayerMovement>().setCanClimb(false);
        _player.GetComponent<CombatController>().setIsAttacking(true);
        _player.GetComponent<PlayerMovement>().getRigidBody().gravityScale = 0f;
        _player.GetComponent<PlayerMovement>().getRigidBody().velocity = Vector2.zero;
        */yield return new WaitForSeconds(_timeBetweenAttacks);
        /*
        _isAttacking = false;
        _player.GetComponent<PlayerMovement>().setCanMove(true);
        _player.GetComponent<PlayerMovement>().setCanRoll(true);
        _player.GetComponent<CombatController>().setIsAttacking(false);
        _player.GetComponent<CombatController>().setCanAttack(false);
        _player.GetComponent<PlayerMovement>().setDistanceJumped(_player.GetComponent<PlayerMovement>().getJumpHeight());


        if (_player.GetComponent<CombatController>().getPrimaryWeapon() != null)
        {
            _player.GetComponent<CombatController>().getPrimaryWeapon().GetComponent<Weapon>().setCurrentAttack(0);
            _player.GetComponent<CombatController>().getPrimaryWeapon().GetComponent<Weapon>().setIsAttacking(false);
        }
        if (_player.GetComponent<CombatController>().getSecundaryWeapon() != null)
        {
            _player.GetComponent<CombatController>().getSecundaryWeapon().GetComponent<Weapon>().setCurrentAttack(0);
            _player.GetComponent<CombatController>().getSecundaryWeapon().GetComponent<Weapon>().setIsAttacking(false);
        }
        _player.GetComponent<PlayerMovement>().getRigidBody().gravityScale = _player.GetComponent<PlayerMovement>().getInitialGravity();*/
    }
    //GETTERS


    public int getNumberOfAttacks()
    {
        return _numberOfAttacks;
    }

    public int getWeaponLevel()
    {
        return _weaponLevel;
    }

    public int getCurrentAttack()
    {
        return _currentAttack;
    }

    public static long calculateXpNextLevel(int level)
    {
        return (level + 1) * 10000;
    }

    public bool getIsAttacking()
    {
        return _isAttacking;
    }

    public scalingEnum getDexterityScale()
    {
        return _dexterityScale;
    }
    public scalingEnum getStrengthScale()
    {
        return _strengthScale;
    }
    public scalingEnum getPrecisionScale()
    {
        return _precisionScale;
    }

    public int getBaseDMG()
    {
        return _baseDamage;
    }

    public int getTotalDMG()
    {
        return _damage;
    }

    public bool getCanAttack()
    {
        return _canAttack;
    }


    public int calculateDMG(int strengthLVL, int dexterityLVL, int precisionLVL)
    {
        return _baseDamage + calculateStrengthDMG(strengthLVL) + calculateDexterityDMG(dexterityLVL) + calculatePrecisionDMG(precisionLVL);
    }

    public int calculateBaseDMG()
    {
        return _weaponLevel * 100;
    }

    public int calculateStrengthDMG(int level)
    {
        return ((level * ((int)_strengthScale) * 2) + (2 * level));
    }
    public int calculateDexterityDMG(int level)
    {
        return ((level * ((int)_dexterityScale) * 2) + (2 * level));
    }

    public int calculatePrecisionDMG(int level)
    {
        return ((level * ((int)_precisionScale) * 2) + (2 * level));
    }
    /*
    public List<Skill> getUnlockedSkills()
    {
        return _unlockedSkills;
    }*/

    public handEnum getHand()
    {
        return _hand;
    }



    //SETTERS
    public void setIsAttacking(bool isAttacking)
    {
        _isAttacking = isAttacking;
    }

    public void setBaseDMG(int baseDMG)
    {
        _baseDamage = baseDMG;
    }

    public void setTotalDMG(int DMG)
    {
        _damage = DMG;
    }
    public void setWeaponLevel(int level)
    {
        _weaponLevel = level;
    }
    /*
    public void addSkill(Skill skill)
    {
        _unlockedSkills.Add(skill);
    }*/
    public void setCurrentAttack(int attack)
    {
        _currentAttack = Mathf.Min(attack, _numberOfAttacks - 1);
    }

    public void setCanAttack(bool canAttack)
    {
        _canAttack = canAttack;
    }

    //MODIFIERS
    /*
    public void unlockSkill(Skill skill)
    {
        _unlockedSkills.Add(skill);
    }
    */
    public bool checkLevelUp()
    {
        return config.getPlayer().GetComponent<combatController>().getSouls() >= calculateXpNextLevel(_weaponLevel) && _weaponLevel < _MAXLEVEL;
    }

    public void addLevel()
    {
        _weaponLevel++;
    }

    public bool levelUp()
    {
        bool canLevelUp = checkLevelUp();
        if (canLevelUp)
        {
            addLevel();
            _strengthScale = _scalings.getScaling().getStrengthScalings()[(_weaponLevel - 1)];
            _dexterityScale = _scalings.getScaling().getDexterityScalings()[(_weaponLevel - 1)];
            _precisionScale = _scalings.getScaling().getPrecisionScalings()[(_weaponLevel - 1)];
            _baseDamage = calculateBaseDMG();
            _damage = calculateDMG(statSystem.getStrength().getLevel(), statSystem.getDexterity().getLevel(), statSystem.getPrecision().getLevel());
        }
        return canLevelUp;
    }
}
