using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class combatController : MonoBehaviour
{
    [SerializeField] private GameObject _primary;
    [SerializeField] private GameObject _secundary;
    private bool _canAttack = false;
    private bool _isAttacking = false;
    private long _souls;
    private stateMachine _stateMachine;
    [SerializeField] private GameObject _hurtbox;
    [SerializeField] private GameObject _hitbox;
    private float _staminaRestore = 0.05f;
    private float _dashStaminaUse = 10f;
    private float _attackStaminaUse = 2f;
    [SerializeField] private GameObject _bulletPrefab;

    private void Start()
    {
        _stateMachine = GetComponent<stateMachine>();
        config.setPlayer(gameObject);
    }
    private void Awake()
    {
        attributesData data;
        data = saveSystem.loadAttributes();
        if (data == null)
        {
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
            statSystem.setLevel(data.getLevel());
            statSystem.getVitality().setLevel(data.getVitality());
            statSystem.getEndurance().setLevel(data.getEndurance());
            statSystem.getStrength().setLevel(data.getStrength());
            statSystem.getDexterity().setLevel(data.getDexterity());
            statSystem.getAgility().setLevel(data.getAgility());
            statSystem.getPrecision().setLevel(data.getPrecision());
        }
    }
    //SETTERS
    #region setterMethods
    public void assignPrimaryWeapon(GameObject weapon)
    {
        _primary = weapon;
        weaponConfig.setPrimaryWeapon(weapon);
    }

    public void assignSecundaryWeapon(GameObject weapon)
    {
        _secundary = weapon;
        weaponConfig.setSecundaryWeapon(weapon);
    }

    public void setCanAttack(bool canAttack)
    {
        _canAttack = canAttack;
    }
    public void setIsAttacking(bool isAttacking)
    {
        _isAttacking = isAttacking;
    }
    #endregion

    //GETTERS

    #region getterMethods
    public GameObject getPrimaryWeapon()
    {
        return _primary;
    }
    public GameObject getSecundaryWeapon()
    {
        return _secundary;
    }
    public bool getCanAttack()
    {
        return _canAttack;
    }
    public bool getIsAttacking()
    {
        return _isAttacking;
    }
    public long getSouls()
    {
        return _souls;
    }

    public GameObject getHitbox()
    {
        return _hitbox;
    }
    public GameObject getHurtbox()
    {
        return _hurtbox;
    }

    public float getDashStaminaUse()
    {
        return _dashStaminaUse;
    }

    public float getAttackStaminaUse()
    {
        return _attackStaminaUse;
    }

    #endregion

    public void createBullet()
    {
        Vector3 bulletPos;

        if (GetComponent<playerMovement>().getIsFacingRight())
        {
            bulletPos = new Vector2(transform.position.x + (GetComponent<BoxCollider2D>().size.x / 2) + 0.01f, transform.position.y);
        }
        else
        {
            bulletPos = new Vector2(transform.position.x - (GetComponent<BoxCollider2D>().size.x / 2) - 0.01f, transform.position.y);
        }

        Instantiate(_bulletPrefab, bulletPos, Quaternion.identity);
    }
    public void receiveSouls(long souls)
    {
        _souls += souls;
        changeUIs(_souls);
    }


    public void setSouls(long souls)
    {
        _souls = souls;
        changeUIs(_souls);
    }

    public void useSouls(long souls)
    {
        _souls -= souls;
        changeUIs(_souls);
    }

    private void changeUIs(long souls)
    {
        TextMeshProUGUI field = levelUpUIConfiguration.getSoulsValue();
        levelUpUIController.updateUI(ref field, _souls.ToString());

        field = generalUIConfiguration.getAlmas();
        generalUIController.setSouls(ref field, _souls.ToString());
        saveSystem.saveSouls();
    }

    public void receiveLoot(lootItem[] loot)
    {
        foreach (lootItem item in loot)
        {
            config.getInventory().GetComponent<inventoryManager>().addItemToInventory(item);
        }
        saveSystem.saveInventory();
    }

    public void changeStaminaRestore(float value)
    {
        _staminaRestore = value;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            receiveSouls(10000);
        }
        if (!UIController.getIsInPauseUI() && !UIController.getIsInLevelUpUI() && !UIController.getIsInAdquireSkillUI() && 
            !UIController.getIsInLevelUpWeaponUI() && !UIController.getIsInInventoryUI() &&
            !UIController.getIsInShopUI() && !bonfireBehaviour.getIsInBonfireMenu())
        {
            if (_primary != null)
            {
                if (_primary.GetComponent<weapon>().getCanAttack())
                {
                    if (inputManager.GetKeyDown(inputEnum.primaryAttack) && _stateMachine.getCurrentState().GetType() == typeof(idleCombatState))
                    {
                        _stateMachine.setNextState(new entryState(true));
                    }
                }
            }

            if (_secundary != null)
            {
                if (_secundary.GetComponent<weapon>().getCanAttack())
                {
                    if (inputManager.GetKeyDown(inputEnum.secundaryAttack) && _stateMachine.getCurrentState().GetType() == typeof(idleCombatState))
                    {
                        _stateMachine.setNextState(new entryState(false));
                    }
                }
            }

            if (!_isAttacking && !GetComponent<playerMovement>().getIsDodging())
            {
                GetComponent<statsController>().restoreStamina(_staminaRestore);
            }
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
