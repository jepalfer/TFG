using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

/// <summary>
/// inventoryManager es una clase que se encarga de manejar la lógica del inventario.
/// </summary>
[System.Serializable]
public class inventoryManager : MonoBehaviour
{
    /// <summary>
    /// Máximo de objetos que podemos tener de un tipo (ID) en el inventario.
    /// </summary>
    [SerializeField] private int _maximumItemsInventory;

    /// <summary>
    /// Máximo de objetos que podemos tener de un tipo (ID) en el almacén.
    /// </summary>
    [SerializeField] private int _maximumItemsBackUp;

    /// <summary>
    /// Máximo de objetos recargables que podemos tener en un momento determinado de la partida.
    /// </summary>
    [SerializeField] private int _maximumRefillable;

    /// <summary>
    /// Lista con los objetos que conforman el inventario.
    /// </summary>
    [SerializeField] private List<lootItem> _inventory;

    /// <summary>
    /// Lista con los objetos que conforman el almacén.
    /// </summary>
    [SerializeField] private List<lootItem> _backup;

    /// <summary>
    /// Referencia a los datos serializados del inventario.
    /// </summary>
    private inventoryData _inventoryData;

    /// <summary>
    /// Primer método que se ejecuta al inicio del script.
    /// </summary>
    private void Awake()
    {
        //Modificamos la variable estática y obtenemos los datos
        config.setInventory(gameObject);
        _inventoryData = saveSystem.loadInventory();

        //Si ya hemos recogido algún objeto
        if (_inventoryData != null)
        {
            //Obtenemos el inventario serializado
            List<serializedItemData> loadedInventory = _inventoryData.getInventory();
            item serializedItem;

            //Vamos instanciando cada uno de los objetos serializados
            for (int i = 0; i < loadedInventory.Count; ++i)
            {
                serializedItem = ScriptableObject.CreateInstance<item>();
                serializedItem.setItemData(loadedInventory[i].getData());
                _inventory.Add(new lootItem(serializedItem, loadedInventory[i].getQuantity()));
            }

            //Obtenemos el backup (objetos sobrantes)
            List<serializedItemData> loadedBackup = _inventoryData.getBackup();

            //Instanciamos el backup del que disponemos
            for (int i = 0; i < loadedBackup.Count; ++i)
            {
                serializedItem = ScriptableObject.CreateInstance<item>();
                serializedItem.setItemData(loadedBackup[i].getData());
                _backup.Add(new lootItem(serializedItem, loadedBackup[i].getQuantity()));
            }

            //Movemos objetos del backup al inventario
            for (int i = 0; i < loadedBackup.Count; i++)
            {
                //Buscamos cada objeto del backup en el inventario
                lootItem searchedItem = _inventory.Find(item => item.getID() == loadedBackup[i].getData().getID());

                if (searchedItem == null)//No disponible en el inventario
                {
                    serializedItem = ScriptableObject.CreateInstance<item>();
                    serializedItem.setItemData(loadedBackup[i].getData());
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
                else //Disponible en el inventario
                {
                    if (searchedItem.getQuantity() < config.getInventory().GetComponent<inventoryManager>().getMaximumInventory())
                    {
                        serializedItem = ScriptableObject.CreateInstance<item>();
                        serializedItem.setItemData(loadedBackup[i].getData());
                        int quantityToInventory = 0;

                        if (loadedBackup[i].getQuantity() < (config.getInventory().GetComponent<inventoryManager>().getMaximumInventory() - searchedItem.getQuantity()))
                        {
                            quantityToInventory = loadedBackup[i].getQuantity();
                        }
                        else
                        {
                            quantityToInventory = config.getInventory().GetComponent<inventoryManager>().getMaximumInventory() - searchedItem.getQuantity();
                        }
                        removeItemFromBackup(new lootItem(serializedItem, loadedBackup[i].getQuantity()), quantityToInventory);
                    }
                }
            }
            _maximumRefillable = _inventoryData.getMaximumRefillable();

            //Guardamos los cambios
            saveSystem.saveInventory();
        }
        else //No hemos recogido ningún objeto
        {
            //Hacemos un inventario por defecto
            _inventory = new List<lootItem>();
            _backup = new List<lootItem>();
            _maximumRefillable = 5;
            saveSystem.saveInventory();
        }
    }

    /// <summary>
    /// Método para añadir un objeto recargable.
    /// </summary>
    public void addMaximumRefillable()
    {
        _maximumRefillable++;
        refill();
    }

    /// <summary>
    /// Método auxiliar para recargar el objeto recargable.
    /// </summary>
    public void refill()
    {
        //Obtenemos los objetos recargables
        List<lootItem> refillableItems = _inventory.FindAll(item => item.getTipo() == itemTypeEnum.refillable);
        equippedObjectData equippedData = saveSystem.loadEquippedObjectsData();
        
        //Modificamos la cantidad de la que disponemos
        for (int i = 0; i < refillableItems.Count; ++i)
        {
            _inventory[_inventory.FindIndex(item => item.getID() == refillableItems[i].getID())].setQuantity(_maximumRefillable);
        }

        //Si lo tenemos equipado modificamos el valor de la UI
        if (equippedData != null)
        {
            if (_inventory.Find(item => item.getID() == equippedData.getData()[equippedData.getIndexInEquipped()].getItemID()) != null)
            {
                config.getPlayer().GetComponent<equippedInventory>().modifyEquippedObject(equippedData.getIndexInEquipped());
            }
        }
        saveSystem.saveInventory();
    }

    /// <summary>
    /// Método para añadir un objeto al inventario cuando lo obtenemos.
    /// </summary>
    /// <param name="item">Objeto a añadir.</param>
    public void addItemToInventory(lootItem item)
    {
        //Mostramos el objeto obtenido
        UIConfig.getController().getGeneralUI().GetComponent<generalUIController>().showItemAdded(item);
        int index = findInventoryIndex(item.getItem());

        //Si es un arma
        if (item.getTipo() == itemTypeEnum.weapon)
        {
            equippedWeaponsData data = saveSystem.loadWeaponsState();

            if (data == null) //Si no hemos equipado ninguna
            {
                List<int> weaponsLevels = new List<int>();
                for (int i = 0; i < config.getInventory().GetComponent<weaponInventoryManagement>().getWeaponList().Count; ++i)
                {
                    //Inicializamos todos los niveles internos de las armas
                    weaponsLevels.Add(1);
                }
                saveSystem.saveWeaponsState(-1, -1, weaponsLevels);
            }
        }

        //Miramos si tenemos ya el objeto en el inventario o no
        if (index != -1)
        {
            //Nos hemos pasado del máximo de objetos que caben en el inventario
            if (_inventory[index].getQuantity() + item.getQuantity() > _maximumItemsInventory)
            {
                //Calculamos el número de objetos que van al inventario y lo añadimos a este
                int toInventory = _maximumItemsInventory - _inventory[index].getQuantity();
                lootItem itemInventory = new lootItem(item.getInstance(), _inventory[index].getQuantity() + toInventory);
                _inventory[index] = itemInventory;

                //Calculamos el número de objetos que van al backup y lo añadimos a este
                int toBackUp = item.getQuantity() - toInventory;
                lootItem itemBackUp = new lootItem(item.getInstance(), toBackUp);
                addItemToBackup(itemBackUp, itemBackUp.getQuantity());
                
            }
            else
            {
                //Directamente añadimos esa cantidad de objetos
                lootItem addedItem = new lootItem(item.getInstance(), _inventory[index].getQuantity() + item.getQuantity());
                _inventory[index] = addedItem;
                saveSystem.saveInventory();
            }
        }
        else
        {
            //Nos pasamos del límite
            if (item.getQuantity() > _maximumItemsInventory)
            {
                //Calculamos el número de objetos que van al backup, al inventario y los añadimos
                int toBackUp = item.getQuantity() - _maximumItemsInventory;
                int toInventory = _maximumItemsInventory;
                lootItem addedItem = new lootItem(item.getInstance(), toInventory);
                lootItem backUpItem = new lootItem(item.getInstance(), toBackUp);
                _inventory.Add(addedItem);
                addItemToBackup(backUpItem, backUpItem.getQuantity());
            }
            else
            {
                //Añadimos el objeto al inventario directamente
                lootItem addedItem = new lootItem(item.getInstance(), item.getQuantity());
                _inventory.Add(addedItem);
                saveSystem.saveInventory();
            }


        }
    }

    /// <summary>
    /// Método para eliminar objetos del inventario.
    /// </summary>
    /// <param name="item">Objeto a eliminar.</param>
    /// <param name="quantity">Cantidad a eliminar.</param>
    public void removeItemFromInventory(lootItem item, int quantity)
    {
        //Buscamos el índice del objeto
        int index = findInventoryIndex(item.getItem());

        //El objeto se encuentra en el inventario
        if (index != -1)
        {
            //Quedan objetos en el inventario
            if (_inventory[index].getQuantity() - quantity > 0)
            {
                lootItem removedItem = new lootItem(item.getItem().getInstance(), _inventory[index].getQuantity() - quantity);
                _inventory[index] = removedItem;
                saveSystem.saveInventory();
            }
            else //No quedan objetos en el inventario
            {
                //Si no es recargable entonces lo eliminamos completamente del inventario
                if (item.getTipo() != itemTypeEnum.refillable)
                {
                    _inventory.RemoveAt(index);
                    saveSystem.saveInventory();
                }
                else
                {
                    //Si es recargable entonces solamente modificamos la cantidad en el inventario
                    if (_inventory[index].getQuantity() - quantity == 0)
                    {
                        lootItem removedItem = new lootItem(item.getItem().getInstance(), _inventory[index].getQuantity() - quantity);
                        _inventory[index] = removedItem;
                        saveSystem.saveInventory();
                    }
                }
            }
        }
    }

    /// <summary>
    /// Método para añadir un objeto al backup.
    /// </summary>
    /// <param name="item">Objeto a añadir.</param>
    /// <param name="quantity">Cantidad a añadir.</param>
    public void addItemToBackup(lootItem item, int quantity)
    {
        //Comprobamos que el objeto esté metido en la reserva
        int index = findBackupIndex(item.getItem());

        //Si lo está
        if (index != -1)
        {
            //Comprobamos que no se pase del límite
            if (_backup[index].getQuantity() + quantity <= _maximumItemsBackUp)
            {
                lootItem addedItem = new lootItem(item.getItem().getInstance(), _backup[index].getQuantity() + quantity);
                _backup[index] = addedItem;
                saveSystem.saveInventory();
            }
        }
        else //Añadimos un nuevo objeto
        {
            lootItem addedItem = new lootItem(item.getItem().getInstance(), item.getQuantity());
            _backup.Add(addedItem);
            saveSystem.saveInventory();
        }
    }
    /// <summary>
    /// Método para eliminar un objeto del backup.
    /// </summary>
    /// <param name="item">Objeto a eliminar.</param>
    /// <param name="quantity">Cantidad a eliminar.</param>
    public void removeItemFromBackup(lootItem item, int quantity)
    {
        //Buscamos que el objeto esté en el backup
        int index = findBackupIndex(item.getItem());

        //Si está dentro del backup
        if (index != -1)
        {
            //Si quitando esta cantidad seguimos teniendo objetos en el backup
            if (_backup[index].getQuantity() - quantity > 0)
            {
                lootItem removedItem = new lootItem(item.getInstance(), _backup[index].getQuantity() - quantity);
                _backup[index] = removedItem;
                saveSystem.saveInventory();
            }
            else //Si no lo eliminamos del backup
            {
                _backup.RemoveAt(index);
                saveSystem.saveInventory();
            }
            lootItem addedItem = new lootItem(item.getInstance(), quantity);

            //Añadimos el objeto al inventario
            addItemToInventory(addedItem);
        }
    }
    /// <summary>
    /// Método auxiliar para encontrar el índice de un objeto por ID dado dentro del inventario.
    /// </summary>
    /// <param name="item">Objeto a buscar.</param>
    /// <returns>Índice dentro de <see cref="_inventory"/> del objeto buscado</returns>
    public int findInventoryIndex(generalItemSerializable item)
    {;
        return _inventory.FindIndex(entry => entry.getID() == item.getID());
    }
    /// <summary>
    /// Método auxiliar para encontrar el índice de un objeto por ID dado dentro del backup.
    /// </summary>
    /// <param name="item">Objeto a buscar.</param>
    /// <returns>Índice dentro de <see cref="_backup"/> del objeto buscado</returns>
    public int findBackupIndex(generalItemSerializable item)
    {
        return _backup.FindIndex(entry => entry.getID() == item.getID());
    }

    /// <summary>
    /// Método para obtener un material de mejora en el inventario según el índice de un arma.
    /// </summary>
    /// <param name="index">Índice del arma para el que necesitamos cierto material de mejora.</param>
    /// <returns><see cref="lootItem"/> que representa el objeto buscado.</returns>
    public lootItem getMaterial(int index)
    {
        return getInventory().Find(item => item.getTipo() == itemTypeEnum.upgradeMaterial && item.getID() == 
        config.getInventory().GetComponent<weaponInventoryManagement>().getWeaponList()[index].getWeapon().
        GetComponent<weapon>().getListOfMaterials()[(saveSystem.loadWeaponsState().getWeaponsLevels()[index] - 1)].
        getItemData().getID());
    }

    /// <summary>
    /// Getter que devuelve <see cref="_inventory"/>.
    /// </summary>
    /// <returns><see cref="_inventory"/></returns>
    public List<lootItem> getInventory()
    {
        return _inventory;
    }

    /// <summary>
    /// Getter que devuelve <see cref="_backup"/>.
    /// </summary>
    /// <returns><see cref="_backup"/></returns>
    public List<lootItem> getBackUp()
    {
        return _backup;
    }

    /// <summary>
    /// Getter que devuelve <see cref="_maximumRefillable"/>.
    /// </summary>
    /// <returns><see cref="_maximumRefillable"/></returns>
    public int getMaximumRefillable()
    {
        return _maximumRefillable;
    }

    /// <summary>
    /// Getter que devuelve <see cref="_maximumItemsInventory"/>.
    /// </summary>
    /// <returns><see cref="_maximumItemsInventory"/></returns>
    public int getMaximumInventory()
    {
        return _maximumItemsInventory;
    }
    /// <summary>
    /// Getter que devuelve <see cref="_maximumItemsBackUp"/>.
    /// </summary>
    /// <returns><see cref="_maximumItemsBackUp"/></returns>
    public int getMaximumBackUp()
    {
        return _maximumItemsBackUp;
    }

}
