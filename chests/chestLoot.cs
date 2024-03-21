using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// lootSystem es una clase que se usa para guardar el loot que tiene un cofre.
/// </summary>
public class chestLoot : MonoBehaviour
{
    /// <summary>
    /// Es el loot que contiene el cofre.
    /// </summary>
    [SerializeField] private List<lootItem> _loot;

    /// <summary>
    /// Es el método que da el loot al jugador y después se guarda con el sistema de guardado. 
    /// Consulta <see cref="config"/>, <see cref="inventoryManager"/> y <see cref="saveSystem.saveInventory"/> para más información 
    /// </summary>
    public void giveLoot()
    {
        //Añadimos el loot
        foreach (lootItem item in _loot)
        {
            config.getInventory().GetComponent<inventoryManager>().addItemToInventory(item);

            //Guardamos el inventario
            saveSystem.saveInventory();
        }
    }
}
