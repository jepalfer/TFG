using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// weapon es un script que representa internamente las armas.
/// </summary>
public class weapon : generalItem
{
    /// <summary>
    /// Es el nivel actual del arma.
    /// </summary>
    private int _weaponLevel = 1;

    /// <summary>
    /// Es el nivel m�ximo al que el arma puede subir.
    /// </summary>
    private const int _MAXLEVEL = 11;

    /// <summary>
    /// Es el n�mero de ataques del arma.
    /// </summary>
    private int _numberOfAttacks;

    /// <summary>
    /// Es el ataque actual del combo.
    /// </summary>
    [SerializeField] private int _currentAttack = 0;

    /// <summary>
    /// Son los datos espec�ficos del arma.
    /// </summary>
    [SerializeField] private weaponData _weaponData;

    /// <summary>
    /// Es el escalado por destreza actual del arma.
    /// </summary>
    private scalingEnum _dexterityScale;

    /// <summary>
    /// Es el escalado por fuerza actual del arma.
    /// </summary>
    private scalingEnum _strengthScale;

    /// <summary>
    /// Es el escalado por precisi�n actual del arma.
    /// </summary>
    private scalingEnum _precisionScale;

    /// <summary>
    /// Es el da�o que inflige el arma.
    /// </summary>
    [SerializeField] private int _damage;

    /// <summary>
    /// Es el da�o base (sin escalados) del arma.
    /// </summary>
    private int _baseDamage;

    /// <summary>
    /// Booleano que indica si estamos atacando con el arma.
    /// </summary>
    [SerializeField] private bool _isAttacking;

    /// <summary>
    /// Booleano que indica si podemos atacar con el arma.
    /// </summary>
    [SerializeField] private bool _canAttack;

    /// <summary>
    /// Referencia a la mano en la que se equipa el arma.
    /// </summary>
    [SerializeField] private handEnum _hand;

    /// <summary>
    /// Es una lista con las habilidades que se equipan en el arma.
    /// </summary>
    [SerializeField] private List<GameObject> _skills;

    /// <summary>
    /// Es una lista con los <see cref="upgradeMaterial"/> que se necesitan para subir de nivel el arma.
    /// </summary>
    [SerializeField] private List<upgradeMaterial> _listOfMaterials;

    /// <summary>
    /// Es una lista con las cantidades de <see cref="upgradeMaterial"/> que se necesitan para subir de nivel el arma.
    /// </summary>
    [SerializeField] private List<int> _quantities;

    /// <summary>
    /// Es el tiempo entre ataques.
    /// </summary>
    protected float _timeBetweenAttacks = 1f;

    /// <summary>
    /// Referencia a los datos de las habilidades desbloqueadas.
    /// </summary>
    private unlockedSkillsData _skillsData;

    /// <summary>
    /// M�todo usado para instanciar un arma al equiparla o al cargar partida.
    /// </summary>
    /// <param name="level">Nivel al que se encuentra el arma instanciada.</param>
    public void createWeapon(int level)
    {
        //Asignamos los valores que corresponda
        _weaponLevel = level;
        _numberOfAttacks = _weaponData.getNumberOfAttacks();
        _dexterityScale = _weaponData.getDexterityScalings()[(_weaponLevel - 1)];
        _strengthScale = _weaponData.getStrengthScalings()[(_weaponLevel - 1)];
        _precisionScale = _weaponData.getPrecisionScalings()[(_weaponLevel - 1)];

        //Calculamos el da�o del arma
        setBaseDMG(calculateBaseDMG());
        setTotalDMG(calculateDMG(statSystem.getStrength().getLevel(), statSystem.getDexterity().getLevel(), statSystem.getPrecision().getLevel()));

        //Asignamos variables est�ticas
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

        //Cargamos la informaci�n sobre las habilidades desbloqueadas
        _skillsData = saveSystem.loadSkillsState();

        if (_skillsData != null)
        {
            List<sceneSkillsState> weaponSkills = _skillsData.getUnlockedSkills().FindAll(skill => skill.getWeaponID() == getID() && skill.getAssociatedSkill().getEquipType() == equipEnum.onWeapon);

            _skills = new List<GameObject>();
            //Recorremos las habilidades del arma y a�adimos a su lista si es habilidad no equipable
            for (int i = 0; i < weaponSkills.Count; ++i)
            {
                GameObject searchedSkill = config.getPlayer().GetComponent<skillManager>().getAllSkills().Find(skill => skill.GetComponent<skill>().getSkillID() == weaponSkills[i].getAssociatedSkill().getSkillID());
                _skills.Add(Instantiate(searchedSkill));
            }
        }
    }

    /// <summary>
    /// Getter que devuelve <see cref="_skills"/>.
    /// </summary>
    /// <returns>Una lista de GameObjects que contiene las habilidades del arma.</returns>
    public List<GameObject> getWeaponSkills()
    {
        return _skills;
    }

    /// <summary>
    /// Getter que devuelve <see cref="_numberOfAttacks"/>.
    /// </summary>
    /// <returns>Un int que representa el n�mero de ataques del arma.</returns>
    public int getNumberOfAttacks()
    {
        return _numberOfAttacks;
    }

    /// <summary>
    /// Getter que devuelve <see cref="_weaponLevel"/>.
    /// </summary>
    /// <returns>Un int que representa el nivel actual del arma.</returns>
    public int getWeaponLevel()
    {
        return _weaponLevel;
    }

    /// <summary>
    /// Getter que devuelve <see cref="_currentAttack"/>.
    /// </summary>
    /// <returns>Un int que representa el ataque actual del arma.</returns>
    public int getCurrentAttack()
    {
        return _currentAttack;
    }

    /// <summary>
    /// Getter que devuelve <see cref="_isAttacking"/>.
    /// </summary>
    /// <returns>Un booleano que indica si estamos atacando con el arma.</returns>
    public bool getIsAttacking()
    {
        return _isAttacking;
    }

    /// <summary>
    /// Getter que devuelve <see cref="_dexterityScale"/>.
    /// </summary>
    /// <returns>Un objeto de tipo <see cref="scalingEnum"/> que indica el escalado actual del arma con destreza.</returns>
    public scalingEnum getDexterityScale()
    {
        return _dexterityScale;
    }

    /// <summary>
    /// Getter que devuelve <see cref="_strengthScale"/>.
    /// </summary>
    /// <returns>Un objeto de tipo <see cref="scalingEnum"/> que indica el escalado actual del arma con fuerza.</returns>
    public scalingEnum getStrengthScale()
    {
        return _strengthScale;
    }

    /// <summary>
    /// Getter que devuelve <see cref="_precisionScale"/>.
    /// </summary>
    /// <returns>Un objeto de tipo <see cref="scalingEnum"/> que indica el escalado actual del arma con precisi�n.</returns>
    public scalingEnum getPrecisionScale()
    {
        return _precisionScale;
    }

    /// <summary>
    /// Getter que devuelve <see cref="_baseDamage"/>.
    /// </summary>
    /// <returns>Un int que representa el da�o base del arma.</returns>
    public int getBaseDMG()
    {
        return _baseDamage;
    }

    /// <summary>
    /// Getter que devuelve <see cref="_damage"/>.
    /// </summary>
    /// <returns>Un int que representa el da�o que inflige el arma.</returns>
    public int getTotalDMG()
    {
        return _damage;
    }

    /// <summary>
    /// Getter que devuelve <see cref="_canAttack"/>.
    /// </summary>
    /// <returns>Un booleano que indica si podemos atacar con el arma.</returns>
    public bool getCanAttack()
    {
        return _canAttack;
    }

    /// <summary>
    /// Getter que devuelve <see cref="_hand"/>.
    /// </summary>
    /// <returns>Un objeto de tipo <see cref="handEnum"/> que indica la mano en la que se equipa el arma.</returns>
    public handEnum getHand()
    {
        return _hand;
    }

    /// <summary>
    /// Getter que devuelve <see cref="_weaponData.getRange()"/>.
    /// </summary>
    /// <returns>Un objeto de tipo <see cref="rangeEnum"/> que indica si el arma es melee o a rango.</returns>
    public rangeEnum getRange()
    {
        return _weaponData.getRange();
    }

    /// <summary>
    /// Getter que devuelve <see cref="_MAXLEVEL"/>.
    /// </summary>
    /// <returns>Un int que representa el nivel m�ximo del arma.</returns>
    public static int getMaxLVL()
    {
        return _MAXLEVEL;
    }

    /// <summary>
    /// Getter que devuelve <see cref="_quantities"/>.
    /// </summary>
    /// <returns>Una lista de enteros que contiene las cantidades de materiales de mejora necesarios para subir de nivel el arma.</returns>
    public List<int> getQuantites()
    {
        return _quantities;
    }

    /// <summary>
    /// Getter que devuelve <see cref="_listOfMaterials"/>.
    /// </summary>
    /// <returns>Una lista de <see cref="upgradeMaterial"/> que contiene los materiales de mejora necesarios para subir de nivel el arma.</returns>
    public List<upgradeMaterial> getListOfMaterials()
    {
        return _listOfMaterials;
    }

    /// <summary>
    /// M�todo que calcula la experiencia hasta el pr�ximo nivel del arma.
    /// </summary>
    /// <param name="level">Nivel del que se quiere calcular la experiencia necesaria.</param>
    /// <returns>La experiencia necesaria para subir de nivel.</returns>
    public static long calculateXpNextLevel(int level)
    {
        return (level) * 10000;
    }

    /// <summary>
    /// M�todo para calcular el da�o total del arma.
    /// </summary>
    /// <param name="strengthLVL">Nivel de fuerza del jugador.</param>
    /// <param name="dexterityLVL">Nivel de destreza del jugador.</param>
    /// <param name="precisionLVL">Nivel de precisi�n del jugador.</param>
    /// <returns></returns>
    public int calculateDMG(int strengthLVL, int dexterityLVL, int precisionLVL)
    {
        int extraStrength = 0, extraDexterity = 0, extraPrecision = 0;
        config.getPlayer().GetComponent<combatController>().calculateAttributesLevelUp(ref extraStrength, ref extraDexterity, ref extraPrecision);
        Debug.Log(extraStrength + " " + extraDexterity + " " + extraPrecision);
        return _baseDamage + calculateStrengthDMG(strengthLVL + extraStrength) + calculateDexterityDMG(dexterityLVL + extraDexterity) + calculatePrecisionDMG(precisionLVL + extraPrecision);
    }

    /// <summary>
    /// M�todo que calcula el da�o base del arma.
    /// </summary>
    /// <returns>El da�o base que tiene el arma.</returns>
    public int calculateBaseDMG()
    {
        return _weaponLevel * 100;
    }

    /// <summary>
    /// M�todo auxiliar para calcular el da�o que aporta el escalado de fuerza.
    /// </summary>
    /// <param name="level">Nivel de fuerza del jugador.</param>
    /// <returns></returns>
    public int calculateStrengthDMG(int level)
    {
        return ((level * ((int)_strengthScale) * 2) + (2 * level));
    }

    /// <summary>
    /// M�todo auxiliar para calcular el da�o que aporta el escalado de destreza.
    /// </summary>
    /// <param name="level">Nivel de destreza del jugador.</param>
    /// <returns></returns>
    public int calculateDexterityDMG(int level)
    {
        return ((level * ((int)_dexterityScale) * 2) + (2 * level));
    }

    /// <summary>
    /// M�todo auxiliar para calcular el da�o que aporta el escalado de precisi�n.
    /// </summary>
    /// <param name="level">Nivel de precisi�n del jugador.</param>
    /// <returns></returns>
    public int calculatePrecisionDMG(int level)
    {
        return ((level * ((int)_precisionScale) * 2) + (2 * level));
    }

    /// <summary>
    /// Setter que modifica <see cref="_isAttacking"/>.
    /// </summary>
    /// <param name="isAttacking">El valor a asignar.</param>
    public void setIsAttacking(bool isAttacking)
    {
        _isAttacking = isAttacking;
    }

    /// <summary>
    /// Setter que modifica <see cref="_baseDamage"/>.
    /// </summary>
    /// <param name="baseDMG">El valor a asignar.</param>
    public void setBaseDMG(int baseDMG)
    {
        _baseDamage = baseDMG;
    }

    /// <summary>
    /// Setter que modifica <see cref="_damage"/>.
    /// </summary>
    /// <param name="DMG">El valor a asignar.</param>
    public void setTotalDMG(int DMG)
    {
        _damage = DMG;
    }
    /// <summary>
    /// Setter que modifica <see cref="_weaponLevel"/>.
    /// </summary>
    /// <param name="level">El valor a asignar.</param>
    public void setWeaponLevel(int level)
    {
        _weaponLevel = level;
    }

    /// <summary>
    /// Setter que modifica <see cref="_currentAttack"/>.
    /// </summary>
    /// <param name="attack">El valor a asignar.</param>
    public void setCurrentAttack(int attack)
    {
        _currentAttack = Mathf.Min(attack, _numberOfAttacks - 1);
    }

    /// <summary>
    /// Setter que modifica <see cref="_canAttack"/>.
    /// </summary>
    /// <param name="canAttack">El valor a asignar.</param>
    public void setCanAttack(bool canAttack)
    {
        _canAttack = canAttack;
    }

    /// <summary>
    /// M�todo que comprueba que podamos subir de nivel el arma.
    /// </summary>
    /// <returns>Un booleano indicando si podemos (true) o no (false).</returns>
    public bool checkLevelUp()
    {
        bool xpCondition = config.getPlayer().GetComponent<combatController>().getSouls() >= calculateXpNextLevel(_weaponLevel) && _weaponLevel < _MAXLEVEL;
        lootItem material = config.getInventory().GetComponent<inventoryManager>().getInventory().Find(item => item.getTipo() == itemTypeEnum.upgradeMaterial && item.getID() == getListOfMaterials()[_weaponLevel - 1].getItemData().getID());
        bool materialCondition = material != null && material.getQuantity() >= getQuantites()[_weaponLevel - 1];

        return xpCondition && materialCondition;
    }

    /// <summary>
    /// M�todo que suma un nivel al nivel actual del arma.
    /// </summary>
    public void addLevel()
    {
        _weaponLevel++;
    }

    /// <summary>
    /// M�todo que se encarga de subir de nivel el arma.
    /// </summary>
    /// <returns>Un booleano indicando si hemos podido subir de nivel (true) o no (false)</returns>
    public bool levelUp()
    {
        //Comprobamos que podamos subir de nivel
        bool canLevelUp = checkLevelUp();
        if (canLevelUp)
        {
            performLevelUp();
        }
        return canLevelUp;
    }

    public void addSkill(GameObject skill)
    {
        if (_skills == null)
        {
            _skills = new List<GameObject>();
        }
        _skills.Add(skill);
    }

    /// <summary>
    /// M�todo auxiliar que se encarga de ejecutar la l�gica de subir el arma de nivel en <see cref="levelUp()"/>
    /// </summary>
    private void performLevelUp()
    {
        //Subimos de nivel y asignamos variables
        addLevel();
        _strengthScale = _weaponData.getStrengthScalings()[(_weaponLevel - 1)];
        _dexterityScale = _weaponData.getDexterityScalings()[(_weaponLevel - 1)];
        _precisionScale = _weaponData.getPrecisionScalings()[(_weaponLevel - 1)];
        _baseDamage = calculateBaseDMG();
        _damage = calculateDMG(statSystem.getStrength().getLevel(), statSystem.getDexterity().getLevel(), statSystem.getPrecision().getLevel());
        lootItem material = config.getInventory().GetComponent<inventoryManager>().getInventory().Find(item => item.getTipo() == itemTypeEnum.upgradeMaterial && item.getID() == getListOfMaterials()[_weaponLevel - 1].getItemData().getID());
        config.getInventory().GetComponent<inventoryManager>().removeItemFromInventory(material, getQuantites()[_weaponLevel - 2]);
    }
}
