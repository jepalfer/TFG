using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// weaponSlotLevelUpController es una clase que se usa para gestionar la información que aparece en cada
/// slot de la UI de subida de nivel de armas.
/// </summary>
public class weaponSlotLevelUpController : MonoBehaviour
{
    /// <summary>
    /// Referencia al sprite del arma.
    /// </summary>
    [SerializeField] private GameObject _weaponSprite;

    /// <summary>
    /// Referencia al campo de texto para el nombre del arma.
    /// </summary>
    [SerializeField] private TextMeshProUGUI _weaponName;

    /// <summary>
    /// Referencia al campo de texto para las almas requeridas para subir 1 nivel.
    /// </summary>
    [SerializeField] private TextMeshProUGUI _soulsRequired;

    /// <summary>
    /// Referencia al botón de subida de nivel.
    /// </summary>
    [SerializeField] private Button _levelUpButton;

    /// <summary>
    /// Referencia a la imagen del material requerido para subir el próximo nivel.
    /// </summary>
    [SerializeField] private Image _materialSprite;

    /// <summary>
    /// Referencia al campo de texto para la cantidad de material necesario para subir el arma de nivel.
    /// </summary>
    [SerializeField] private TextMeshProUGUI _materialQuantity;
    //private int _weaponID;

    /// <summary>
    /// Método que desactiva <see cref="_soulsRequired"/> en caso de que no se puedan subir más niveles.
    /// </summary>
    public void deactivateSouls()
    {
        _soulsRequired.enabled = false;
    }

    /// <summary>
    /// Método que desactiva <see cref="_materialSprite"/> y <see cref="_materialQuantity"/> en caso de que no se puedan 
    /// subir más niveles.
    /// </summary>
    public void deactivateMaterials()
    {
        _materialSprite.enabled = false;
        _materialQuantity.enabled = false;
    }

    //public void setWeaponID(int id)
    //{
    //    _weaponID = id;
    //}

    /// <summary>
    /// Setter que modifica el sprite de <see cref="_weaponSprite"/>.
    /// </summary>
    /// <param name="sprite">Sprite a asignar.</param>
    public void setSprite(Sprite sprite)
    {
        _weaponSprite.GetComponent<Image>().sprite = sprite;
    }

    /// <summary>
    /// Setter que modifica el texto del campo <see cref="_weaponName"/>.
    /// </summary>
    /// <param name="name">Nombre a asignar.</param>
    public void setName(string name)
    {
        _weaponName.text = name;
    }

    //public void setID(int id)
    //{
    //    _weaponID = id;
    //}

    /// <summary>
    /// Setter que modifica el texto del campo <see cref="_soulsRequired"/>.
    /// </summary>
    /// <param name="souls">Cantidad de almas a asignar.</param>
    public void setSouls(long souls)
    {
        _soulsRequired.text = souls.ToString();
    }

    /// <summary>
    /// Setter que modifica el color del campo <see cref="_soulsRequired"/>.
    /// </summary>
    /// <param name="color">Color a asignar.</param>
    public void changeSoulsColor(Color color)
    {
        _soulsRequired.color = color;
    }
    /// <summary>
    /// Setter que modifica el color de <see cref="_materialQuantity"/>.
    /// </summary>
    /// <param name="color">Color a asignar.</param>
    public void changeMaterialColor(Color color)
    {
        _materialQuantity.color = color;
    }


    /// <summary>
    /// Setter que modifica el sprite de <see cref="_materialSprite"/>.
    /// </summary>
    /// <param name="sprite">Sprite a asignar.</param>
    public void setMaterialSprite(Sprite sprite)
    {
        _materialSprite.sprite = sprite;
    }

    /// <summary>
    /// Setter que modifica el texto del campo <see cref="_materialQuantity"/>.
    /// </summary>
    /// <param name="quantity">Cantidad a asignar.</param>
    public void setMaterialQuantity(int quantity)
    {
        _materialQuantity.text = quantity.ToString();
    }

    /// <summary>
    /// Getter que devuelve <see cref="_levelUpButton"/>.
    /// </summary>
    /// <returns><see cref="_levelUpButton"/>.</returns>
    public Button getButton()
    {
        return _levelUpButton;
    }
}
