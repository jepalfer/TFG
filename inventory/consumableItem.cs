using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// consumableItem es una clase que se usa para usar los objetos consumibles.
/// </summary>
public class consumableItem : generalItem
{
    /// <summary>
    /// Referencia a los datos internos del objeto.
    /// </summary>
    [SerializeField] protected consumableItemData _consumableData;

    /// <summary>
    /// Método que se ejecuta al utilizar el objeto.
    /// </summary>
    public virtual void onUse() { }
}
