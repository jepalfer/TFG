using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// inventoryData es una clase que se usa para serializar la información del inventario.
/// </summary>
[System.Serializable]
public class inventoryData
{
    /// <summary>
    /// Lista de objetos del inventario.
    /// </summary>
    [SerializeField] private List<serializedItemData> _inventory;

    /// <summary>
    /// Lista de objetos del backup.
    /// </summary>
    [SerializeField] private List<serializedItemData> _backup;

    /// <summary>
    /// El número máximo de objetos que puede tener un recargable/rellenable.
    /// </summary>
    [SerializeField] private int _maximumRefillable;

    /// <summary>
    /// Constructor con parámetros de la clase.
    /// Asigna las variables correspondientes.
    /// </summary>
    /// <param name="inventory">Lista de objetos del inventario.</param>
    /// <param name="backup">Lista de objetos del backup.</param>
    /// <param name="refillable">Número máximo de objetos a recargar.</param>
    public inventoryData(List<lootItem> inventory, List<lootItem> backup, int refillable)
    {
        //Inicializamos el inventario y el backup
        _inventory = new List<serializedItemData>();
        _backup = new List<serializedItemData>();

        //Creamos el inventario
        for (int i = 0; i < inventory.Count; ++i)
        {
            _inventory.Add(new serializedItemData(inventory[i].getItemData(), inventory[i].getQuantity()));
        }
        //Creamos el backup
        for (int i = 0; i < backup.Count; ++i)
        {
            _backup.Add(new serializedItemData(backup[i].getItemData(), backup[i].getQuantity()));
        }

        //Ponemos un número máximo de objetos recargables
        _maximumRefillable = refillable;
    }

    /// <summary>
    /// Getter que devuelve <see cref="_inventory"/>.
    /// </summary>
    /// <returns>Una lista de objetos tipo <see cref="serializedItemData"/> que representa el inventario del jugador.</returns>
    public List<serializedItemData> getInventory()
    {
        return _inventory;
    }

    /// <summary>
    /// Getter que devuelve <see cref="_backup"/>.
    /// </summary>
    /// <returns>Una lista de objetos tipo <see cref="serializedItemData"/> que representa el backup del jugador.</returns>
    public List<serializedItemData> getBackup()
    {
        return _backup;
    }

    /// <summary>
    /// Getter que devuelve <see cref="_maximumRefillable"/>.
    /// </summary>
    /// <returns>Un entero que representa el número máximo de objetos recargables.</returns>
    public int getMaximumRefillable()
    {
        return _maximumRefillable;
    }

}
