using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class levelUpWeaponUIController : MonoBehaviour
{
    [SerializeField] private GameObject _slotPrefab;
    [SerializeField] private Transform _slotHolder;
    private List<GameObject> _slots = new List<GameObject>();
    private List<GameObject> _instantiatedWeapons = new List<GameObject>();

    public void enterUI()
    {
        List<lootItem> weapons = config.getInventory().GetComponent<inventoryManager>().getInventory().FindAll(item => item.getTipo() == itemType.weapon);
        for (int i = 0; i < weapons.Count; ++i)
        {
            int weaponIndex;
            GameObject clickedWeapon; // Crear una variable local
            GameObject instantiatedPrefab;
            GameObject instantiatedWeapon;

            instantiatedPrefab = Instantiate(_slotPrefab);
            instantiatedWeapon = Instantiate(config.getInventory().GetComponent<weaponInventoryManagement>().getWeaponList().Find(weapon => weapon.getWeapon().GetComponent<weapon>().getID() == weapons[i].getID()).getWeapon());

            instantiatedPrefab.GetComponent<weaponSlotLevelUPController>().setID(weapons[i].getID());
            instantiatedPrefab.GetComponent<weaponSlotLevelUPController>().setSprite(weapons[i].getIcon());
            weaponIndex = config.getInventory().GetComponent<weaponInventoryManagement>().getWeaponList().FindIndex(weapon => weapon.getID() == weapons[i].getID());
            instantiatedWeapon.GetComponent<weapon>().setWeaponLevel(saveSystem.loadWeaponsState().getWeaponsLevels()[weaponIndex]);
            instantiatedPrefab.GetComponent<weaponSlotLevelUPController>().setName(weapons[i].getName() + " +" + saveSystem.loadWeaponsState().getWeaponsLevels()[weaponIndex]);
            instantiatedPrefab.GetComponent<weaponSlotLevelUPController>().setSouls(weapon.calculateXpNextLevel(saveSystem.loadWeaponsState().getWeaponsLevels()[weaponIndex]));

            clickedWeapon = config.getInventory().GetComponent<weaponInventoryManagement>().getWeaponList().Find(weapon => weapon.getWeapon().GetComponent<weapon>().getID() == weapons[i].getID()).getWeapon();
            instantiatedPrefab.GetComponent<weaponSlotLevelUPController>().getButton().onClick.AddListener(() => {
                
                if (instantiatedWeapon.GetComponent<weapon>().levelUp())
                {
                    if (instantiatedWeapon.GetComponent<weapon>().getHand() == handEnum.primary)
                    {
                        if (weaponConfig.getPrimaryWeapon() != null && weaponConfig.getPrimaryWeapon().GetComponent<weapon>().getID() == instantiatedWeapon.GetComponent<weapon>().getID())
                        {
                            weaponConfig.getPrimaryWeapon().GetComponent<weapon>().levelUp();
                            levelUPUIConfiguration.getPrimaryDMGValue().text = weaponConfig.getPrimaryWeapon().GetComponent<weapon>().getTotalDMG().ToString();

                        }
                    }
                    else
                    {
                        if (weaponConfig.getSecundaryWeapon() != null && weaponConfig.getSecundaryWeapon().GetComponent<weapon>().getID() == instantiatedWeapon.GetComponent<weapon>().getID())
                        {
                            weaponConfig.getSecundaryWeapon().GetComponent<weapon>().levelUp();
                            levelUPUIConfiguration.getSecundaryDMGValue().text = weaponConfig.getSecundaryWeapon().GetComponent<weapon>().getTotalDMG().ToString();
                        }
                    }


                    config.getPlayer().GetComponent<combatController>().useSouls(weapon.calculateXpNextLevel(instantiatedWeapon.GetComponent<weapon>().getWeaponLevel()));
                    equippedWeaponsData data = saveSystem.loadWeaponsState();
                    data.getWeaponsLevels()[weaponIndex] = instantiatedWeapon.GetComponent<weapon>().getWeaponLevel();
                    saveSystem.saveWeaponsState(data.getPrimaryIndex(), data.getSecundaryIndex(), data.getWeaponsLevels());
                    instantiatedPrefab.GetComponent<weaponSlotLevelUPController>().setName(clickedWeapon.GetComponent<weapon>().getName() + " +" + saveSystem.loadWeaponsState().getWeaponsLevels()[weaponIndex]);
                    instantiatedPrefab.GetComponent<weaponSlotLevelUPController>().setSouls(weapon.calculateXpNextLevel(saveSystem.loadWeaponsState().getWeaponsLevels()[weaponIndex]));
                }
            });
            _slots.Add(instantiatedPrefab);
            _instantiatedWeapons.Add(instantiatedWeapon);
            instantiatedPrefab.transform.SetParent(_slotHolder, false);
            instantiatedWeapon.transform.SetParent(_slotHolder, false);
        }

        calculateNavigation();
    }

    public void exitUI()
    {
        for (int i = 0; i < _slots.Count; ++i)
        {
            Destroy(_slots[i]);
        }
        for (int i = 0; i < _instantiatedWeapons.Count; ++i)
        {
            Destroy(_instantiatedWeapons[i]);
        }
        _slots.Clear();
        _instantiatedWeapons.Clear();
        EventSystem.current.SetSelectedGameObject(null);
    }

    public void calculateNavigation()
    {
        Navigation current_nav;
        weaponSlotLevelUPController currentSlot;
        for (int i = 0; i < _slots.Count; ++i)
        {
            currentSlot = _slots[i].GetComponent<weaponSlotLevelUPController>();
            current_nav = currentSlot.getButton().navigation;
            if (i == 0)
            {
                EventSystem.current.SetSelectedGameObject(currentSlot.getButton().gameObject);

                if (_slots.Count > 1)
                {
                    current_nav.selectOnDown = _slots[i + 1].GetComponent<weaponSlotLevelUPController>().getButton();
                }
                else
                {
                    //Boton de cancelar
                }
            }
            else if (i > 0 && i < _slots.Count - 1)
            {
                current_nav.selectOnDown = _slots[i + 1].GetComponent<weaponSlotLevelUPController>().getButton();
                current_nav.selectOnUp = _slots[i - 1].GetComponent<weaponSlotLevelUPController>().getButton();
            }
            else
            {
                current_nav.selectOnUp = _slots[i - 1].GetComponent<weaponSlotLevelUPController>().getButton();
            }

            currentSlot.getButton().navigation = current_nav;
        }
    }

    private void levelUp()
    {

    }
}
