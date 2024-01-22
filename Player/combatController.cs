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

    private void Start()
    {
        _stateMachine = GetComponent<stateMachine>();
        config.setPlayer(gameObject);
    }
    private void Awake()
    {
        /*
        if (_primary != null)
        {
            _primary.GetComponent<Weapon>().setBaseDMG(_primary.GetComponent<Weapon>().calculateBaseDMG());
            _primary.GetComponent<Weapon>().setTotalDMG(_primary.GetComponent<Weapon>().calculateDMG(statSystem.getStrength().getLevel(), statSystem.getDexterity().getLevel(), statSystem.getPrecision().getLevel()));
            weaponConfig.setPrimaryWeapon(_primary);
        }
        if (_secundary != null)
        {
            _secundary.GetComponent<Weapon>().setBaseDMG(_secundary.GetComponent<Weapon>().calculateBaseDMG());
            _secundary.GetComponent<Weapon>().setTotalDMG(_secundary.GetComponent<Weapon>().calculateDMG(statSystem.getStrength().getLevel(), statSystem.getDexterity().getLevel(), statSystem.getPrecision().getLevel()));
            weaponConfig.setSecundaryWeapon(_secundary);
        }*/
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
        TextMeshProUGUI field = levelUPUIConfiguration.getSoulsValue();
        levelUpUI.updateUI(ref field, _souls.ToString());

        field = generalUIConfiguration.getAlmas();
        UIManager.setSouls(ref field, _souls.ToString());
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
        if (!UIController.getIsPaused() && !UIController.getIsLevelingUp() && !UIController.getIsAdquiringSkills() && !UIController.getIsLevelingUpWeapon() && !UIController.getIsInInventory() && !bonfireBehaviour.getIsInBonfireMenu())
        {
            if (_primary != null)
            {
                if (_primary.GetComponent<weapon>().getCanAttack())
                {
                    if (inputManager.getKeyDown(inputEnum.PrimaryAttack.ToString()) && _stateMachine.getCurrentState().GetType() == typeof(idleCombatState))
                    {
                        _stateMachine.SetNextState(new groundMeleeEntryState(true));
                    }
                }
            }

            if (_secundary != null)
            {
                if (_secundary.GetComponent<weapon>().getCanAttack())
                {
                    if (inputManager.getKeyDown(inputEnum.SecundaryAttack.ToString()) && _stateMachine.getCurrentState().GetType() == typeof(idleCombatState))
                    {
                        _stateMachine.SetNextState(new groundMeleeEntryState(false));
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
