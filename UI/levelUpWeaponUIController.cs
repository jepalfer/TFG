using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

/// <summary>
/// levelUpWeaponUIController es una clase que controla la lógica de la UI para subir de nivel las armas.
/// </summary>
public class levelUpWeaponUIController : MonoBehaviour
{
    /// <summary>
    /// Referencia a la UI que se instancia por cada arma.
    /// </summary>
    [SerializeField] private GameObject _slotPrefab;

    /// <summary>
    /// Referencia a la UI donde se van insertando los distintos <see cref="_slotPrefab"/>.
    /// </summary>
    [SerializeField] private Transform _slotHolder;

    /// <summary>
    /// Lista de <see cref="_slotPrefab"/> para poder destruirlos cuando salimos de la UI.
    /// </summary>
    private List<GameObject> _slots = new List<GameObject>();

    /// <summary>
    /// Lista de las armas instanciadas temporalmente para poder destruirlas.
    /// </summary>
    private List<GameObject> _instantiatedWeapons = new List<GameObject>();

    /// <summary>
    /// Método que se ejecuta cuando entramos en la UI.
    /// </summary>
    public void initializeUI()
    {
        //Buscamos las armas en el inventario
        List<lootItem> weapons = config.getInventory().GetComponent<inventoryManager>().getInventory().FindAll(item => item.getTipo() == itemTypeEnum.weapon);
        for (int i = 0; i < weapons.Count; ++i)
        {
            //Variables locales para cada iteración
            int weaponIndex;
            GameObject clickedWeapon; 
            GameObject instantiatedPrefab;
            GameObject instantiatedWeapon;

            //Instanciamos los prefabs
            instantiatedPrefab = Instantiate(_slotPrefab);
            instantiatedWeapon = Instantiate(config.getInventory().GetComponent<weaponInventoryManagement>().getWeaponList().Find(weapon => weapon.getWeapon().GetComponent<weapon>().getID() == weapons[i].getID()).getWeapon());

            //Modificamos el prefab de la UI para que se adapte al arma correspondiente
            instantiatedPrefab.GetComponent<weaponSlotLevelUpController>().setSprite(weapons[i].getIcon());
            weaponIndex = config.getInventory().GetComponent<weaponInventoryManagement>().getWeaponList().FindIndex(weapon => weapon.getID() == weapons[i].getID());
            instantiatedWeapon.GetComponent<weapon>().setWeaponLevel(saveSystem.loadWeaponsState().getWeaponsLevels()[weaponIndex]);
            string extraLevelUp = (saveSystem.loadWeaponsState().getWeaponsLevels()[weaponIndex] - 1) == (weapon.getMaxLVL() - 1) ? "" : " > " + "+" + (saveSystem.loadWeaponsState().getWeaponsLevels()[weaponIndex]);
            instantiatedPrefab.GetComponent<weaponSlotLevelUpController>().setName(weapons[i].getName() + " +" + (saveSystem.loadWeaponsState().getWeaponsLevels()[weaponIndex] - 1) + extraLevelUp);

            if (extraLevelUp != "")
            {
                instantiatedPrefab.GetComponent<weaponSlotLevelUpController>().setSouls(weapon.calculateXpNextLevel(saveSystem.loadWeaponsState().getWeaponsLevels()[weaponIndex]));
                instantiatedPrefab.GetComponent<weaponSlotLevelUpController>().setMaterialQuantity(config.getInventory().GetComponent<weaponInventoryManagement>().getMaterialQuantity(weaponIndex));
                instantiatedPrefab.GetComponent<weaponSlotLevelUpController>().setMaterialSprite(config.getInventory().GetComponent<weaponInventoryManagement>().getMaterialSprite(weaponIndex));

                if (config.getPlayer().GetComponent<combatController>().getSouls() < weapon.calculateXpNextLevel(saveSystem.loadWeaponsState().getWeaponsLevels()[weaponIndex]))
                {
                    instantiatedPrefab.GetComponent<weaponSlotLevelUpController>().changeSoulsColor(Color.red);
                }
                else
                {
                    instantiatedPrefab.GetComponent<weaponSlotLevelUpController>().changeSoulsColor(Color.white);
                }

                lootItem searchedItem = config.getInventory().GetComponent<inventoryManager>().getMaterial(weaponIndex);

                if (searchedItem != null)
                {
                    if (searchedItem.getQuantity() >= config.getInventory().GetComponent<weaponInventoryManagement>().getQuantityOf(weaponIndex))
                    {
                        instantiatedPrefab.GetComponent<weaponSlotLevelUpController>().changeMaterialColor(Color.white);
                    }
                    else
                    {
                        instantiatedPrefab.GetComponent<weaponSlotLevelUpController>().changeMaterialColor(Color.red);
                    }
                }
                else
                {
                    instantiatedPrefab.GetComponent<weaponSlotLevelUpController>().changeMaterialColor(Color.red);
                }

            }
            else
            {
                instantiatedPrefab.GetComponent<weaponSlotLevelUpController>().deactivateSouls();
                instantiatedPrefab.GetComponent<weaponSlotLevelUpController>().deactivateMaterials();
            }

            //Obtenemos el arma asociada a esta iteración
            clickedWeapon = config.getInventory().GetComponent<weaponInventoryManagement>().getWeaponList().Find(weapon => weapon.getWeapon().GetComponent<weapon>().getID() == weapons[i].getID()).getWeapon();
            //Añadimos un evento de click al prefab de la UI
            instantiatedPrefab.GetComponent<weaponSlotLevelUpController>().getButton().onClick.AddListener(() => {
                
                //Si podemos subir de nivel el arma instanciada
                if (instantiatedWeapon.GetComponent<weapon>().levelUp())
                {
                    if (instantiatedWeapon.GetComponent<weapon>().getHand() == handEnum.primary)
                    {
                        //Si ya la teníamos equipada
                        if (weaponConfig.getPrimaryWeapon() != null && weaponConfig.getPrimaryWeapon().GetComponent<weapon>().getID() == instantiatedWeapon.GetComponent<weapon>().getID())
                        {
                            weaponConfig.getPrimaryWeapon().GetComponent<weapon>().levelUp();
                            //levelUPUIConfiguration.getPrimaryDMGValue().text = weaponConfig.getPrimaryWeapon().GetComponent<weapon>().getTotalDMG().ToString();

                        }
                    }
                    else
                    {
                        //Si ya la teníamos instanciada
                        if (weaponConfig.getSecundaryWeapon() != null && weaponConfig.getSecundaryWeapon().GetComponent<weapon>().getID() == instantiatedWeapon.GetComponent<weapon>().getID())
                        {
                            weaponConfig.getSecundaryWeapon().GetComponent<weapon>().levelUp();
                            //levelUPUIConfiguration.getSecundaryDMGValue().text = weaponConfig.getSecundaryWeapon().GetComponent<weapon>().getTotalDMG().ToString();
                        }
                    }

                    //Gastamos las almas
                    config.getPlayer().GetComponent<combatController>().useSouls(weapon.calculateXpNextLevel(instantiatedWeapon.GetComponent<weapon>().getWeaponLevel() - 1));
                    
                    //Cargamos los datos de las armas, modificamos y guardamos
                    equippedWeaponsData data = saveSystem.loadWeaponsState();
                    data.getWeaponsLevels()[weaponIndex] = instantiatedWeapon.GetComponent<weapon>().getWeaponLevel();
                    saveSystem.saveWeaponsState(data.getPrimaryIndex(), data.getSecundaryIndex(), data.getWeaponsLevels());
                    extraLevelUp = (saveSystem.loadWeaponsState().getWeaponsLevels()[weaponIndex] - 1) == (weapon.getMaxLVL() - 1) ? "" : " > " + "+" + (saveSystem.loadWeaponsState().getWeaponsLevels()[weaponIndex]);
                    instantiatedPrefab.GetComponent<weaponSlotLevelUpController>().setName(clickedWeapon.GetComponent<weapon>().getName() + " +" + (saveSystem.loadWeaponsState().getWeaponsLevels()[weaponIndex] - 1) + extraLevelUp);
                    if (extraLevelUp != "")
                    {
                        instantiatedPrefab.GetComponent<weaponSlotLevelUpController>().setSouls(weapon.calculateXpNextLevel(saveSystem.loadWeaponsState().getWeaponsLevels()[weaponIndex]));
                        instantiatedPrefab.GetComponent<weaponSlotLevelUpController>().setMaterialQuantity(config.getInventory().GetComponent<weaponInventoryManagement>().getMaterialQuantity(weaponIndex));
                        instantiatedPrefab.GetComponent<weaponSlotLevelUpController>().setMaterialSprite(config.getInventory().GetComponent<weaponInventoryManagement>().getMaterialSprite(weaponIndex));

                        if (config.getPlayer().GetComponent<combatController>().getSouls() < weapon.calculateXpNextLevel(saveSystem.loadWeaponsState().getWeaponsLevels()[weaponIndex]))
                        {
                            instantiatedPrefab.GetComponent<weaponSlotLevelUpController>().changeSoulsColor(Color.red);
                        }
                        else
                        {
                            instantiatedPrefab.GetComponent<weaponSlotLevelUpController>().changeSoulsColor(Color.white);
                        }

                        lootItem searchedItem = config.getInventory().GetComponent<inventoryManager>().getMaterial(weaponIndex);

                        if (searchedItem != null)
                        {
                            if (searchedItem.getQuantity() >= config.getInventory().GetComponent<weaponInventoryManagement>().GetComponent<weaponInventoryManagement>().getQuantityOf(weaponIndex))
                            {
                                instantiatedPrefab.GetComponent<weaponSlotLevelUpController>().changeMaterialColor(Color.white);
                            }
                            else
                            {
                                instantiatedPrefab.GetComponent<weaponSlotLevelUpController>().changeMaterialColor(Color.red);
                            }
                        }
                        else
                        {
                            instantiatedPrefab.GetComponent<weaponSlotLevelUpController>().changeMaterialColor(Color.red);
                        }


                    }
                    else
                    {
                        instantiatedPrefab.GetComponent<weaponSlotLevelUpController>().deactivateSouls();
                        instantiatedPrefab.GetComponent<weaponSlotLevelUpController>().deactivateMaterials();
                    }

                }
            });
            //Añadimos la UI a la lista
            _slots.Add(instantiatedPrefab);
            _instantiatedWeapons.Add(instantiatedWeapon);

            //Añadimos la UI a su contenedor
            instantiatedPrefab.transform.SetParent(_slotHolder, false);
            instantiatedWeapon.transform.SetParent(_slotHolder, false);
        }

        calculateNavigation();
    }

    /// <summary>
    /// Método que se ejecuta cuando salimos de la UI
    /// </summary>
    public void setUIOff()
    {
        //Destruimos todos los prefabs creados
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

    /// <summary>
    /// Método auxiliar para calcular la navegación de los GameObjects de la escena.
    /// </summary>
    public void calculateNavigation()
    {
        Navigation current_nav;
        weaponSlotLevelUpController currentSlot;
        for (int i = 0; i < _slots.Count; ++i)
        {
            currentSlot = _slots[i].GetComponent<weaponSlotLevelUpController>();
            current_nav = currentSlot.getButton().navigation;
            if (i == 0) //El primer objeto
            {
                EventSystem.current.SetSelectedGameObject(currentSlot.getButton().gameObject);

                if (_slots.Count > 1)
                {
                    current_nav.selectOnDown = _slots[i + 1].GetComponent<weaponSlotLevelUpController>().getButton();
                }
                else
                {
                    //Boton de cancelar
                }
            }
            else if (i > 0 && i < _slots.Count - 1) //Entre medias
            {
                current_nav.selectOnDown = _slots[i + 1].GetComponent<weaponSlotLevelUpController>().getButton();
                current_nav.selectOnUp = _slots[i - 1].GetComponent<weaponSlotLevelUpController>().getButton();
            }
            else //El último
            {
                current_nav.selectOnUp = _slots[i - 1].GetComponent<weaponSlotLevelUpController>().getButton();
            }

            currentSlot.getButton().navigation = current_nav;
        }
    }
}
