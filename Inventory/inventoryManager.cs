using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;
[System.Serializable]
public class inventoryManager : MonoBehaviour
{
    [SerializeField] private int _maximumItemsInventory = 99;
    [SerializeField] private int _maximumItemsBackUp = 999;
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

            loadedInventory = inventoryData.getBackup();

            for (int i = 0; i < loadedInventory.Count; ++i)
            {
                serializedItem = ScriptableObject.CreateInstance<item>();
                serializedItem.setItemData(loadedInventory[i].getData());
                _backup.Add(new lootItem(serializedItem, loadedInventory[i].getQuantity()));
            }
        }
        else
        {
            _inventory = new List<lootItem>();
            _backup = new List<lootItem>();
        }
    }

    public void addItemToInventory(lootItem item)
    {
        int index = findInventoryIndex(item.getItem());
        if (item.getTipo() == itemType.weapon)
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

                addItemToBackup(itemBackUp);
                
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
                addItemToBackup(backUpItem);
            }
            else
            {
                lootItem addedItem = new lootItem(item.getInstance(), item.getQuantity());
                _inventory.Add(addedItem);
            }


        }
    }

    public void removeItemFromInventory(lootItem item)
    {
        int index = findInventoryIndex(item.getItem());
        if (index != -1)
        {
            if (_inventory[index].getQuantity() - item.getQuantity() > 0)
            {
                lootItem removedItem = new lootItem(item.getItem().getInstance(), _inventory[index].getQuantity() - item.getQuantity());
                _inventory[index] = removedItem;
            }
            else
            {
                _inventory.RemoveAt(index);
            }
        }
    }

    public void addItemToBackup(lootItem item)
    {
        //Comprobamos que el objeto esté metido en la reserva
        int index = findBackupIndex(item.getItem());

        if (index != -1)
        {
            if (_backup[index].getQuantity() + item.getQuantity() <= _maximumItemsBackUp)
            {
                lootItem addedItem = new lootItem(item.getItem().getInstance(), _backup[index].getQuantity() + item.getQuantity());
                _backup[index] = addedItem;
            }
        }
        else
        {
            lootItem addedItem = new lootItem(item.getItem().getInstance(), item.getQuantity());
            _backup.Add(addedItem);
        }
    }
    public void removeItemFromBackup(lootItem item)
    {
        int index = findBackupIndex(item.getItem());
        if (index != -1)
        {
            if (_backup[index].getQuantity() - item.getQuantity() > 0)
            {
                lootItem removedItem = new lootItem(item.getInstance(), _backup[index].getQuantity() - item.getQuantity());
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
