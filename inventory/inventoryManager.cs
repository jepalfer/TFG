using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;
[System.Serializable]
public class inventoryManager : MonoBehaviour
{
    [SerializeField] private int _maximumItemsInventory;
    [SerializeField] private int _maximumItemsBackUp;
    [SerializeField] private List<lootItem> _inventory;
    [SerializeField] private List<lootItem> _backup;

    private void Awake()
    {
        config.setInventory(gameObject);

        inventoryData inventoryData = saveSystem.loadInventory();

        if (inventoryData != null)
        {
            List<serializedItemData> loadedInventory = inventoryData.getInventory();
            item serializedItem;
            for (int i = 0; i < loadedInventory.Count; ++i)
            {
                serializedItem = ScriptableObject.CreateInstance<item>();
                serializedItem.setItemData(loadedInventory[i].getData());
                _inventory.Add(new lootItem(serializedItem, loadedInventory[i].getQuantity()));
            }

            List<serializedItemData> loadedBackup = inventoryData.getBackup();
            bool addedFromBackup = false;

            for (int i = 0; i < loadedBackup.Count; ++i)
            {
                serializedItem = ScriptableObject.CreateInstance<item>();
                serializedItem.setItemData(loadedBackup[i].getData());
                _backup.Add(new lootItem(serializedItem, loadedBackup[i].getQuantity()));
            }

            for (int i = 0; i < loadedBackup.Count; i++)
            {
                if ((_inventory.Find(item => item.getID() == loadedBackup[i].getData().getID()) == null))//No disponible en el inventario
                {
                    serializedItem = ScriptableObject.CreateInstance<item>();
                    serializedItem.setItemData(loadedBackup[i].getData());
                    addedFromBackup = true;
                    int quantityToInventory;
                    if (loadedBackup[i].getQuantity() > _maximumItemsInventory)
                    {
                        quantityToInventory = 15;
                    }
                    else
                    {
                        quantityToInventory = loadedBackup[i].getQuantity();
                    }
                    removeItemFromBackup(new lootItem(serializedItem, loadedBackup[i].getQuantity()), quantityToInventory);
                }
            }
            saveSystem.saveInventory();
        }
        else
        {
            _inventory = new List<lootItem>();
            _backup = new List<lootItem>();
        }
    }

    public void addItemToInventory(lootItem item)
    {
        UIConfig.getController().gameObject.GetComponent<UIManager>().showItemAdded(item);
        int index = findInventoryIndex(item.getItem());

        if (item.getTipo() == itemTypeEnum.weapon)
        {

            equippedWeaponsData data = saveSystem.loadWeaponsState();

            if (data == null)
            {
                List<int> weaponsLevels = new List<int>();
                for (int i = 0; i < config.getInventory().GetComponent<weaponInventoryManagement>().getWeaponList().Count; ++i)
                {
                    weaponsLevels.Add(1);
                }
                saveSystem.saveWeaponsState(-1, -1, weaponsLevels);
            }
        }

        //Miramos si tenemos ya el objeto en el inventario o no
        if (index != -1)
        {
            if (_inventory[index].getQuantity() + item.getQuantity() > _maximumItemsInventory)
            {
                //Usamos la reserva si sobrepasamos el límite
                int toInventory = _maximumItemsInventory - _inventory[index].getQuantity();

                lootItem itemInventory = new lootItem(item.getInstance(), _inventory[index].getQuantity() + toInventory);

                _inventory[index] = itemInventory;

                int toBackUp = item.getQuantity() - toInventory;

                lootItem itemBackUp = new lootItem(item.getInstance(), toBackUp);

                addItemToBackup(itemBackUp, itemBackUp.getQuantity());
                
            }
            else
            {
                lootItem addedItem = new lootItem(item.getInstance(), _inventory[index].getQuantity() + item.getQuantity());
                _inventory[index] = addedItem;
            }
        }
        else
        {
            if (item.getQuantity() > _maximumItemsInventory)
            {
                int toBackUp = item.getQuantity() - _maximumItemsInventory;
                int toInventory = _maximumItemsInventory;
                lootItem addedItem = new lootItem(item.getInstance(), toInventory);
                lootItem backUpItem = new lootItem(item.getInstance(), toBackUp);
                _inventory.Add(addedItem);
                addItemToBackup(backUpItem, backUpItem.getQuantity());
            }
            else
            {
                lootItem addedItem = new lootItem(item.getInstance(), item.getQuantity());
                _inventory.Add(addedItem);
            }


        }
    }

    public void removeItemFromInventory(lootItem item, int quantity)
    {
        int index = findInventoryIndex(item.getItem());
        if (index != -1)
        {
            if (_inventory[index].getQuantity() - quantity > 0)
            {
                lootItem removedItem = new lootItem(item.getItem().getInstance(), _inventory[index].getQuantity() - quantity);
                _inventory[index] = removedItem;
            }
            else
            {
                _inventory.RemoveAt(index);
                saveSystem.saveInventory();
            }
        }
    }

    public void addItemToBackup(lootItem item, int quantity)
    {
        //Comprobamos que el objeto esté metido en la reserva
        int index = findBackupIndex(item.getItem());

        if (index != -1)
        {
            if (_backup[index].getQuantity() + quantity <= _maximumItemsBackUp)
            {
                lootItem addedItem = new lootItem(item.getItem().getInstance(), _backup[index].getQuantity() + quantity);
                _backup[index] = addedItem;
            }
        }
        else
        {
            lootItem addedItem = new lootItem(item.getItem().getInstance(), item.getQuantity());
            _backup.Add(addedItem);
        }
    }
    public void removeItemFromBackup(lootItem item, int quantity)
    {
        int index = findBackupIndex(item.getItem());
        if (index != -1)
        {
            if (_backup[index].getQuantity() - quantity > 0)
            {
                lootItem removedItem = new lootItem(item.getInstance(), _backup[index].getQuantity() - quantity);
                _backup[index] = removedItem;
            }
            else
            {
                _backup.RemoveAt(index);
            }
            lootItem addedItem = new lootItem(item.getInstance(), item.getQuantity());

            addItemToInventory(addedItem);
        }
    }
    public int findInventoryIndex(generalItemSerializable item)
    {
        return _inventory.FindIndex(entry => entry.getID() == item.getID());
    }

    public int findBackupIndex(generalItemSerializable item)
    {
        return _backup.FindIndex(entry => entry.getID() == item.getID());
    }

    public int getInventoryStock(generalItemSerializable item)
    {
        int stock = 0, index = findInventoryIndex(item);
        if (index != -1)
        {
            stock = _inventory[index].getQuantity();
        }

        return stock;
    }
    public int getBackupStock(generalItemSerializable item)
    {
        int stock = 0, index = findBackupIndex(item);
        if (index != -1)
        {
            Debug.Log("No deberia entrar");
            stock = _backup[index].getQuantity();
        }

        return stock;
    }

    public List<lootItem> getInventory()
    {
        return _inventory;
    }
    public List<lootItem> getBackUp()
    {
        return _backup;
    }

    public int getMaximumInventory()
    {
        return _maximumItemsInventory;
    }
    public int getMaximumBackUp()
    {
        return _maximumItemsBackUp;
    }

}
