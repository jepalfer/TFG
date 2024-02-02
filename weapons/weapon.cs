using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// weapon es un script que representa internamente las armas.
/// </summary>
public class weapon : generalItem
{
    public int _weaponLevel = 1;
    protected const int _MAXLEVEL = 11;
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

    [SerializeField] private List<GameObject> _skills;
    [SerializeField] private List<upgradeMaterial> _listOfMaterials;
    [SerializeField] private List<int> _quantities;

    protected float _timeBetweenAttacks = 1f;

    

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
            if (levelUpUIConfiguration.getPrimaryDMGValue() != null)
            {
                levelUpUIConfiguration.getPrimaryDMGValue().text = getTotalDMG().ToString();
            }

        }
        else
        {
            config.getPlayer().GetComponent<combatController>().assignSecundaryWeapon(gameObject);
            if (levelUpUIConfiguration.getSecundaryDMGValue() != null)
            {
                levelUpUIConfiguration.getSecundaryDMGValue().text = getTotalDMG().ToString();
            }
        }

        unlockedSkillsData skillsData = saveSystem.loadSkillsState();

        if (skillsData != null)
        {
            List<sceneSkillsState> weaponSkills = skillsData.getUnlockedSkills().FindAll(skill => skill.getWeaponID() == getID() && skill.getAssociatedSkill().getEquipType() == equipEnum.onWeapon);

            _skills = new List<GameObject>();
            for (int i = 0; i < weaponSkills.Count; ++i)
            {
                GameObject searchedSkill;

                switch (weaponSkills[i].getAssociatedSkill().getType())
                {
                    case skillTypeEnum.combo:
                        searchedSkill = config.getPlayer().GetComponent<skillManager>().getAllSkills().Find(skill => skill.GetComponent<comboIncreaseSkill>().getSkillID() == weaponSkills[i].getAssociatedSkill().getSkillID());
                        _skills.Add(searchedSkill);
                    break;
                    case skillTypeEnum.stat:
                        searchedSkill = config.getPlayer().GetComponent<skillManager>().getAllSkills().Find(skill => skill.GetComponent<statUpgradeSkill>().getSkillID() == weaponSkills[i].getAssociatedSkill().getSkillID());
                        _skills.Add(searchedSkill); 
                    break;

                    case skillTypeEnum.status:
                        searchedSkill = config.getPlayer().GetComponent<skillManager>().getAllSkills().Find(skill => skill.GetComponent<statusSkill>().getSkillID() == weaponSkills[i].getAssociatedSkill().getSkillID());
                        _skills.Add(searchedSkill); 
                    break;

                    case skillTypeEnum.functionality:
                        searchedSkill = config.getPlayer().GetComponent<skillManager>().getAllSkills().Find(searchedSkill =>
                        {
                            functionalitySkill function = searchedSkill.GetComponent<functionalitySkill>();
                            return function != null && function.getData()?.getSkillID() == weaponSkills[i].getAssociatedSkill().getSkillID();
                        });

                        _skills.Add(Instantiate(searchedSkill)); 
                    break;
                }
            }
        }
    }

    //GETTERS

    public List<GameObject> getWeaponSkills()
    {
        return _skills;
    }

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
        return (level) * 10000;
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

    public rangeEnum getRange()
    {
        return _scalings.getScaling().getRange();
    }

    public List<GameObject> getSkills()
    {
        return _skills;
    }

    public static int getMaxLVL()
    {
        return _MAXLEVEL;
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
        bool xpCondition = config.getPlayer().GetComponent<combatController>().getSouls() >= calculateXpNextLevel(_weaponLevel) && _weaponLevel < _MAXLEVEL;
        lootItem material = config.getInventory().GetComponent<inventoryManager>().getInventory().Find(item => item.getTipo() == itemTypeEnum.upgradeMaterial && item.getID() == getListOfMaterials()[_weaponLevel - 1].getItemData().getID());
        bool materialCondition = material != null && material.getQuantity() >= getQuantites()[_weaponLevel - 1];

        return xpCondition && materialCondition;
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
            lootItem material = config.getInventory().GetComponent<inventoryManager>().getInventory().Find(item => item.getTipo() == itemTypeEnum.upgradeMaterial && item.getID() == getListOfMaterials()[_weaponLevel - 1].getItemData().getID());
            config.getInventory().GetComponent<inventoryManager>().removeItemFromInventory(material, getQuantites()[_weaponLevel - 2]);
        }
        return canLevelUp;
    }
    public List<int> getQuantites()
    {
        return _quantities;
    }

    public List<upgradeMaterial> getListOfMaterials()
    {
        return _listOfMaterials;
    }
}
