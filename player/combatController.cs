using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

/// <summary>
/// combatController es una clase que se usa para manejar la lógica del combate.
/// </summary>
public class combatController : MonoBehaviour
{
    /// <summary>
    /// Referencia al arma primaria.
    /// </summary>
    [SerializeField] private GameObject _primary;

    /// <summary>
    /// Referencia al arma secundaria
    /// </summary>
    [SerializeField] private GameObject _secundary;

    /// <summary>
    /// Booleano que indica si podemos atacar.
    /// </summary>
    private bool _canAttack = false;

    /// <summary>
    /// Booleano que indica si estamos atacando.
    /// </summary>
    private bool _isAttacking = false;

    /// <summary>
    /// La cantidad de almas de las que dispone el jugador.
    /// </summary>
    private long _souls;

    /// <summary>
    /// Referencia a la máquina de estados.
    /// </summary>
    private stateMachine _stateMachine;

    /// <summary>
    /// Referencia a la hurtbox del jugador.
    /// </summary>
    [SerializeField] private GameObject _hurtbox;

    /// <summary>
    /// Referencia a la hitbox del jugador.
    /// </summary>
    [SerializeField] private GameObject _hitbox;

    /// <summary>
    /// Velocidad a la que se restaura la stamina.
    /// </summary>
    private float _staminaRestore = 0.05f;

    /// <summary>
    /// Cantidad de estamina que gasta el dash.
    /// </summary>
    private float _dashStaminaUse = 10f;

    /// <summary>
    /// Cantidad de stamina que gasta atacar.
    /// </summary>
    private float _attackStaminaUse = 2f;

    /// <summary>
    /// Referencia al prefab de la bala.
    /// </summary>
    [SerializeField] private GameObject _bulletPrefab;

    /// <summary>
    /// Daño que hacemos de penetración.
    /// </summary>
    [SerializeField] private float _penetrationDamage;

    /// <summary>
    /// Daño que hacemos de sangrado.
    /// </summary>
    [SerializeField] private float _bleedingDamage;

    /// <summary>
    /// Daño crítico que hacemos.
    /// </summary>
    [SerializeField] private float _critDamage;

    /// <summary>
    /// Referencia a los datos serializados de los atributos.
    /// </summary>
    private attributesData _attributesData;

    /// <summary>
    /// Probabilidad base del daño de sangrado.
    /// </summary>
    private float _baseBleedProbability;

    /// <summary>
    /// Probabilidad base de hacer daño crítico.
    /// </summary>
    private float _critDamageProbability;
    /// <summary>
    /// Multiplicador con el que las estadísticas <see cref="statSystem._strength"/>, <see cref="statSystem._dexterity"/> y <see cref="statSystem._precision"/>
    /// dan daño y probabilidad extra.
    /// </summary>
    private float _levelUpMultiplier;

    /// <summary>
    /// Umbral de nivel con el que los stats adicionales aumentan.
    /// </summary>
    private int _levelThreshold;

    /// <summary>
    /// Datos sobre la última hoguera visitada.
    /// </summary>
    private lastBonfireData _bonfireData;

    /// <summary>
    /// Cantidad de vida que robamos.
    /// </summary>
    private float _lifeSteal;

    /// <summary>
    /// Método que se ejecuta al iniciar el script. Asigna varios valores.
    /// </summary>
    private void Start()
    {
        _stateMachine = GetComponent<stateMachine>();
        config.setPlayer(gameObject);
        _baseBleedProbability = 10;
        _critDamageProbability = 10;
        _levelThreshold = 10;
        _levelUpMultiplier = 5f;
    }

    /// <summary>
    /// Método que se ejecuta al iniciar el script antes de start. Asigna los valores de <see cref="statSystem"/>.
    /// </summary>
    private void Awake()
    {
        //Cargamos los datos de los atributos
        _attributesData = saveSystem.loadAttributes();
        if (_attributesData == null)
        {
            //Primera vez que entramos al juego
            statSystem.setLevel(6);
            statSystem.getVitality().setLevel(1);
            statSystem.getEndurance().setLevel(1);
            statSystem.getStrength().setLevel(1);
            statSystem.getDexterity().setLevel(1);
            statSystem.getAgility().setLevel(1);
            statSystem.getPrecision().setLevel(1);
        }
        else
        {
            //Asignamos los valores correspondientes
            statSystem.setLevel(_attributesData.getLevel());
            statSystem.getVitality().setLevel(_attributesData.getVitality());
            statSystem.getEndurance().setLevel(_attributesData.getEndurance());
            statSystem.getStrength().setLevel(_attributesData.getStrength());
            statSystem.getDexterity().setLevel(_attributesData.getDexterity());
            statSystem.getAgility().setLevel(_attributesData.getAgility());
            statSystem.getPrecision().setLevel(_attributesData.getPrecision());
        }
    }
    //SETTERS

    /// <summary>
    /// Método auxiliar que asigna un arma a <see cref="_primary"/> y asigna la variable estática de <see cref="weaponConfig"/> correspondiente.
    /// </summary>
    /// <param name="weapon">Arma a asignar.</param>
    public void assignPrimaryWeapon(GameObject weapon)
    {
        _primary = weapon;
        weaponConfig.setPrimaryWeapon(weapon);
    }

    /// <summary>
    /// Método auxiliar que asigna un arma a <see cref="_secundary"/> y asigna la variable estática de <see cref="weaponConfig"/> correspondiente.
    /// </summary>
    /// <param name="weapon">Arma a asignar.</param>
    public void assignSecundaryWeapon(GameObject weapon)
    {
        _secundary = weapon;
        weaponConfig.setSecundaryWeapon(weapon);
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
    /// Setter que modifica <see cref="_isAttacking"/>.
    /// </summary>
    /// <param name="isAttacking">El valor a asignar.</param>
    public void setIsAttacking(bool isAttacking)
    {
        _isAttacking = isAttacking;
    }

    //GETTERS

    /// <summary>
    /// Getter que devuelve <see cref="_primary"/>.
    /// </summary>
    /// <returns>Un GameObject que contiene una referencia al arma primaria.</returns>
    public GameObject getPrimaryWeapon()
    {
        return _primary;
    }
    /// <summary>
    /// Getter que devuelve <see cref="_secundary"/>.
    /// </summary>
    /// <returns>Un GameObject que contiene una referencia al arma secundaria.</returns>
    public GameObject getSecundaryWeapon()
    {
        return _secundary;
    }
    /// <summary>
    /// Getter que devuelve <see cref="_canAttack"/>.
    /// </summary>
    /// <returns>Un booleano que indica si podemos atacar.</returns>
    public bool getCanAttack()
    {
        return _canAttack;
    }
    /// <summary>
    /// Getter que devuelve <see cref="_isAttacking"/>.
    /// </summary>
    /// <returns>Un booleano que indica si estamos atacando.</returns>
    public bool getIsAttacking()
    {
        return _isAttacking;
    }
    /// <summary>
    /// Getter que devuelve <see cref="_souls"/>.
    /// </summary>
    /// <returns>Un long que contiene el número de almas del jugador.</returns>
    public long getSouls()
    {
        return _souls;
    }

    /// <summary>
    /// Getter que devuelve <see cref="_hitbox"/>.
    /// </summary>
    /// <returns>Un GameObject que contiene una referencia a la hitbox del jugador.</returns>
    public GameObject getHitbox()
    {
        return _hitbox;
    }
    /// <summary>
    /// Getter que devuelve <see cref="_hurtbox"/>.
    /// </summary>
    /// <returns>Un GameObject que contiene una referencia a la hurtbox del jugador.</returns>
    public GameObject getHurtbox()
    {
        return _hurtbox;
    }

    /// <summary>
    /// Getter que devuelve <see cref="_dashStaminaUse"/>.
    /// </summary>
    /// <returns>Un float que contiene el gasto de stamina al dashear.</returns>
    public float getDashStaminaUse()
    {
        return _dashStaminaUse;
    }

    /// <summary>
    /// Getter que devuelve <see cref="_attackStaminaUse"/>.
    /// </summary>
    /// <returns>Un float que contiene el gasto de stamina al atacar.</returns>
    public float getAttackStaminaUse()
    {
        return _attackStaminaUse;
    }

    /// <summary>
    /// Getter que devuelve <see cref="_baseBleedProbability"/>.
    /// </summary>
    /// <returns>Un float que contiene la probabilidad base de hacer daño de sangrado.</returns>
    public float getBleedProbability()
    {
        return _baseBleedProbability;
    }

    /// <summary>
    /// Getter que devuelve <see cref="_critDamageProbability"/>.
    /// </summary>
    /// <returns>Un float que contiene la probabilidad base de hacer un crítico.</returns>
    public float getCritProbability()
    {
        return _critDamageProbability;
    }

    /// <summary>
    /// Getter que devuelve <see cref="_levelUpMultiplier"/>.
    /// </summary>
    /// <returns>Un float que contiene el multiplicador con el que algunos stats extra aumentan al subir de nivel.</returns>
    public float getLevelMultiplier()
    {
        return _levelUpMultiplier;
    }

    /// <summary>
    /// Getter que devuelve <see cref="_levelThreshold"/>.
    /// </summary>
    /// <returns>Un int que contiene el umbral al que empiezan a aumentar los stats adicionales.</returns>
    /// 
    public int getLevelThreshold()
    {
        return _levelThreshold;
    }

    /// <summary>
    /// Getter que devuelve <see cref="_lifeSteal"/>.
    /// </summary>
    /// <returns>Un float que contiene el robo de vida.</returns>
    public float getLifeSteal()
    {
        return _lifeSteal;
    }
    /// <summary>
    /// Método que instancia una bala en una posición que depende de la dirección a la que miremos.
    /// </summary>
    public void createBullet()
    {
        Vector3 bulletPos;

        if (GetComponent<playerMovement>().getIsFacingRight())
        {
            //Situamos la bala a la derecha del jugador
            bulletPos = new Vector2(transform.position.x + (GetComponent<BoxCollider2D>().size.x / 2) + 0.01f, transform.position.y);
        }
        else
        {
            //Situamos la bala a la izquierda del jugador
            bulletPos = new Vector2(transform.position.x - (GetComponent<BoxCollider2D>().size.x / 2) - 0.01f, transform.position.y);
        }

        //Instanciamos la bala
        Instantiate(_bulletPrefab, bulletPos, Quaternion.identity);
    }

    /// <summary>
    /// Método que sirve para obtener una cantidad de almas.
    /// </summary>
    /// <param name="souls">La cantidad de almas a recibir.</param>
    public void receiveSouls(long souls)
    {
        _souls += souls;
        changeUIs();
    }

    /// <summary>
    /// Setter que modifica la cantidad de almas directamente.
    /// </summary>
    /// <param name="souls">La cantidad de almas a asignar.</param>
    public void setSouls(long souls)
    {
        _souls = souls;
        changeUIs();
    }

    /// <summary>
    /// Método auxiliar para gastar una cantidad de almas.
    /// </summary>
    /// <param name="souls">La cantidad de almas a gastar.</param>
    public void useSouls(long souls)
    {
        _souls -= souls;
        changeUIs();
    }

    /// <summary>
    /// Método auxiliar para modificar ciertas UIs.
    /// </summary>
    private void changeUIs()
    {
        //Obtenemos el campo estático y lo modificamos
        TextMeshProUGUI field = levelUpUIConfiguration.getSoulsValue();
        levelUpUIController.updateUI(ref field, _souls.ToString());

        //Obtenemos el campo estático y lo modificamos
        field = generalUIConfiguration.getSouls();
        generalUIController.updateSoulsUI(ref field, _souls.ToString());

        //Guardamos la información
        saveSystem.saveSouls();
    }

    /// <summary>
    /// Método para añadir loot al inventario. Ver <see cref="inventoryManager"/> para más información.
    /// </summary>
    /// <param name="loot">El loot a recibir.</param>
    public void receiveLoot(lootItem[] loot)
    {
        //Recorremos el array
        foreach (lootItem item in loot)
        {
            //Añadimos los objetos
            config.getInventory().GetComponent<inventoryManager>().addItemToInventory(item);
        }

        //Guardamos
        saveSystem.saveInventory();
    }

    /// <summary>
    /// Setter que modifica <see cref="_staminaRestore"/>.
    /// </summary>
    /// <param name="value">El valor a asignar.</param>
    public void setStaminaRestore(float value)
    {
        _staminaRestore = value;
    }

    /// <summary>
    /// Setter que modifica <see cref="_critDamage"/>.
    /// </summary>
    /// <param name="val">El valor a asignar.</param>
    public void setCritDamage(float val)
    {
        _critDamage = val;
    }
    /// <summary>
    /// Setter que modifica <see cref="_penetrationDamage"/>.
    /// </summary>
    /// <param name="val">El valor a asignar.</param>
    public void setPenetrationDamage(float val)
    {
        _penetrationDamage = val;
    }
    /// <summary>
    /// Setter que modifica <see cref="_bleedingDamage"/>.
    /// </summary>
    /// <param name="val">El valor a asignar.</param>
    public void setBleedingDamage(float val)
    {
        _bleedingDamage = val;
    }

    /// <summary>
    /// Setter que modifica <see cref="_lifeSteal"/>.
    /// </summary>
    /// <param name="val">El valor a asignar al robo de vida.</param>
    public void setLifeSteal(float val)
    {
        _lifeSteal = val;
    }

    /// <summary>
    /// Método auxiliar para calcular los golpes extras.
    /// </summary>
    /// <param name="primaryAttack">Paso de referencia de la variable donde se almacenan los golpes extras del arma primaria.</param>
    /// <param name="secundaryAttack">Paso de referencia de la variable donde se almacenan los golpes extras del arma secundaria.</param>
    public void calculateExtraComboHits(ref int primaryAttack, ref int secundaryAttack)
    {
        List<GameObject> equippedSkills = config.getPlayer().GetComponent<skillManager>().getEquippedSkills();
        for (int i = 0; i < equippedSkills.Count; ++i)
        {
            if (equippedSkills[i] != null && equippedSkills[i].GetComponent<skill>().getType() == skillTypeEnum.combo)
            {
                comboSkillData skillData = equippedSkills[i].GetComponent<skill>().getData() as comboSkillData;

                primaryAttack += skillData.getPrimaryIncrease();
                secundaryAttack += skillData.getSecundaryIncrease();
            }
        }
    }


    /// <summary>
    /// Método auxiliar para calcular la probabilidad extra de sangrado proporcionada por habilidades.
    /// </summary>
    /// <param name="probability">Paso por referencia de la probabilidad base para sumar y que se almacene.</param>
    public void calculateExtraBleedingProbability(ref float probability)
    {
        List<GameObject> equippedSkills = config.getPlayer().GetComponent<skillManager>().getEquippedSkills();
        for (int i = 0; i < equippedSkills.Count; i++)
        {
            if (equippedSkills[i] != null && equippedSkills[i].GetComponent<skill>().getType() == skillTypeEnum.probabilityAugment)
            {
                probabilityAugmentSkillData castedData = equippedSkills[i].GetComponent<skill>().getData() as probabilityAugmentSkillData;
                if (castedData.getAugmentType() == probabilityTypeEnum.bleeding)
                {
                    probability += (int)(castedData.getAugment() * 100);
                }
            }
        }
        int extraStrength = 0, extraDexterity = 0, extraPrecision = 0;
        calculateAttributesLevelUp(ref extraStrength, ref extraDexterity, ref extraPrecision);

        probability += (config.getPlayer().GetComponent<combatController>().getLevelMultiplier()) * ((statSystem.getDexterity().getLevel() + extraDexterity) / config.getPlayer().GetComponent<combatController>().getLevelThreshold());
    }

    /// <summary>
    /// Método auxiliar para calcular la probabilidad extra de daño crítico proporcionada por habilidades.
    /// </summary>
    /// <param name="probability">Paso por referencia de la probabilidad base para sumar y que se almacene.</param>
    public void calculateExtraCritDamageProbability(ref float probability)
    {
        List<GameObject> equippedSkills = config.getPlayer().GetComponent<skillManager>().getEquippedSkills();
        for (int i = 0; i < equippedSkills.Count; i++)
        {
            if (equippedSkills[i] != null && equippedSkills[i].GetComponent<skill>().getType() == skillTypeEnum.probabilityAugment)
            {
                probabilityAugmentSkillData castedData = equippedSkills[i].GetComponent<skill>().getData() as probabilityAugmentSkillData;
                if (castedData.getAugmentType() == probabilityTypeEnum.critDamage)
                {
                    probability += (int)(castedData.getAugment() * 100);
                }
            }
        }
        int extraStrength = 0, extraDexterity = 0, extraPrecision = 0;
        calculateAttributesLevelUp(ref extraStrength, ref extraDexterity, ref extraPrecision);

        probability += (config.getPlayer().GetComponent<combatController>().getLevelMultiplier()) * ((statSystem.getPrecision().getLevel() + extraPrecision) / config.getPlayer().GetComponent<combatController>().getLevelThreshold());        
    }
    
    /// <summary>
    /// Método auxiliar para calcular los atributos extra.
    /// </summary>
    /// <param name="strength">Paso por referencia del atributo de fuerza.</param>
    /// <param name="dexterity">Paso por referencia del atributo de destreza.</param>
    /// <param name="precision">Paso por referencia del atributo de precisión.</param>
    public void calculateAttributesLevelUp(ref int strength, ref int dexterity, ref int precision)
    {
        List<GameObject> equippedSkills = config.getPlayer().GetComponent<skillManager>().getEquippedSkills();
        for (int i = 0; i < equippedSkills.Count; ++i)
        {
            if (equippedSkills[i] != null && equippedSkills[i].GetComponent<skill>().getType() == skillTypeEnum.stat)
            {
                statSkillData skillData = equippedSkills[i].GetComponent<skill>().getData() as statSkillData;

                strength += skillData.getStrength();
                dexterity += skillData.getDexterity();
                precision += skillData.getPrecision();
            }
        }
    }
    /// <summary>
    /// Método auxiliar para calcular la regeneración extra de vida o stamina.
    /// </summary>
    /// <param name="HP">Paso por referencia de la regeneración de vida.</param>
    /// <param name="stamina">Paso por referencia de la regeneración de stamina.</param>
    public void calculateRegenUpgrade(ref float HP, ref float stamina)
    {
        List<GameObject> equippedSkills = config.getPlayer().GetComponent<skillManager>().getEquippedSkills();
        for (int i = 0; i < equippedSkills.Count; ++i)
        {
            if (equippedSkills[i] != null && equippedSkills[i].GetComponent<skill>().getType() == skillTypeEnum.regenUpgrade)
            {
                regenUpgradeSkillData skillData = equippedSkills[i].GetComponent<skill>().getData() as regenUpgradeSkillData;

                if (skillData.getUpgradeType() == upgradeTypeEnum.HP)
                {
                    HP += skillData.getUpgradeAmount();
                }
                else
                {
                    stamina += skillData.getUpgradeAmount();
                }
            }
        }
    }

    /// <summary>
    /// Método auxiliar para calcular el daño extra penetrante, de sangrado y crítico.
    /// </summary>
    /// <param name="penetrating">Paso por referencia al daño penetrante.</param>
    /// <param name="bleeding">Paso por referencia al daño de sangrado.</param>
    /// <param name="crit">Paso por referencia al daño crítico.</param>
    public void calculateExtraDamages(ref float penetrating, ref float bleeding, ref float crit)
    {

        List<GameObject> equipped = config.getPlayer().GetComponent<skillManager>().getEquippedSkills();
        for (int i = 0; i < equipped.Count; i++)
        {
            if (equipped[i] != null && equipped[i].GetComponent<statusSkill>() != null)
            {
                statusSkillData skillData = equipped[i].GetComponent<statusSkill>().getData() as statusSkillData;
                bleeding += skillData.getBleedingDamage() * 100;
                penetrating += skillData.getPenetrationDamage() * 100;
                crit += skillData.getCritDamage() * 100;
            }
        }
        int extraStrength = 0, extraDexterity = 0, extraPrecision = 0;
        calculateAttributesLevelUp(ref extraStrength, ref extraDexterity, ref extraPrecision);
        penetrating += (config.getPlayer().GetComponent<combatController>().getLevelMultiplier()) * ((statSystem.getStrength().getLevel() + extraStrength) / config.getPlayer().GetComponent<combatController>().getLevelThreshold());


    }

    /// <summary>
    /// Método que se ejecuta cada frame para modificar la lógica.
    /// </summary>
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            receiveSouls(10000);
        }
        //Si no estamos en ningún menú
        if (!UIController.getIsInPauseUI() && !UIController.getIsInLevelUpUI() && !UIController.getIsInAdquireSkillUI() && 
            !UIController.getIsInLevelUpWeaponUI() && !UIController.getIsInInventoryUI() &&
            !UIController.getIsInShopUI() && !bonfireBehaviour.getIsInBonfireMenu() && !inputManager.GetKey(inputEnum.down) && !inputManager.GetKey(inputEnum.up) &&
            !GetComponent<downWardBlowController>().getIsInDownWardBlow())
        {
            //Comprobamos arma primaria
            if (_primary != null)
            {
                if (_primary.GetComponent<weapon>().getCanAttack())
                {
                    //Golpeamos con el arma
                    if (inputManager.GetKeyDown(inputEnum.primaryAttack) && _stateMachine.getCurrentState().GetType() == typeof(idleCombatState))
                    {
                        _stateMachine.setNextState(new entryState(true));
                    }
                }
            }

            //Comprobamos arma secundaria
            if (_secundary != null)
            {
                if (_secundary.GetComponent<weapon>().getCanAttack())
                {
                    //Golpeamos con el arma
                    if (inputManager.GetKeyDown(inputEnum.secundaryAttack) && _stateMachine.getCurrentState().GetType() == typeof(idleCombatState))
                    {
                        _stateMachine.setNextState(new entryState(false));
                    }
                }
            }

            //Regeneramos stamina

            if (!_isAttacking && !GetComponent<playerMovement>().getIsDodging())
            {
                float HPUpgrade = 0, staminaUpgrade = 0;
                config.getPlayer().GetComponent<combatController>().calculateRegenUpgrade(ref HPUpgrade, ref staminaUpgrade);

                GetComponent<statsController>().restoreStamina(_staminaRestore + (_staminaRestore * staminaUpgrade));
            }
        }
        if (GetComponent<statsController>().getCurrentHP() <= 0)
        {
            die();
        }
    }

    /// <summary>
    /// Método auxiliar que maneja la muerte del jugador.
    /// </summary>
    private void die()
    {
        _bonfireData = saveSystem.loadLastBonfireData();

        if (_bonfireData != null)
        {
            Debug.Log(_bonfireData.getSceneID());
            SceneManager.LoadScene(_bonfireData.getSceneID());
            Vector3 pos = new Vector3(_bonfireData.getBonfireCoordinates()[0], _bonfireData.getBonfireCoordinates()[1], _bonfireData.getBonfireCoordinates()[2]);
            GetComponent<statsController>().healHP(GetComponent<statsController>().getMaxHP());
            GetComponent<statsController>().restoreStamina(GetComponent<statsController>().getMaxStamina());
            transform.position = pos;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        if (_hitbox.GetComponent<BoxCollider2D>().enabled)
        {
            Gizmos.DrawCube(_hitbox.transform.position, new Vector3(_hitbox.GetComponent<BoxCollider2D>().size.x, _hitbox.GetComponent<BoxCollider2D>().size.y, 1));

        }
    }
}
