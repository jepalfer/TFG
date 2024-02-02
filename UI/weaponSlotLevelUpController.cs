using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class weaponSlotLevelUpController : MonoBehaviour
{
    [SerializeField] private GameObject _weaponSprite;
    [SerializeField] private TextMeshProUGUI _weaponName;
    [SerializeField] private TextMeshProUGUI _soulsRequired;
    [SerializeField] private Button _levelUpButton;
    [SerializeField] private Image _materialSprite;
    [SerializeField] private TextMeshProUGUI _materialQuantity;
    private int _weaponID;

    public string getSouls()
    {
        return _soulsRequired.text;
    }

    public void deactivateSouls()
    {
        _soulsRequired.enabled = false;
    }

    public void deactivateMaterials()
    {
        _materialSprite.enabled = false;
        _materialQuantity.enabled = false;
    }

    public void setWeaponID(int id)
    {
        _weaponID = id;
    }

    public void setSprite(Sprite sprite)
    {
        _weaponSprite.GetComponent<Image>().sprite = sprite;
    }

    public void setName(string name)
    {
        _weaponName.text = name;
    }

    public void setID(int id)
    {
        _weaponID = id;
    }

    public void setSouls(long souls)
    {
        _soulsRequired.text = souls.ToString();
    }

    public void changeSoulsColor(Color color)
    {
        _soulsRequired.color = color;
    }
    public void changeMaterialColor(Color color)
    {
        _materialQuantity.color = color;
    }

    public Button getButton()
    {
        return _levelUpButton;
    }

    public void setMaterialSprite(Sprite sprite)
    {
        _materialSprite.sprite = sprite;
    }

    public void setMaterialQuantity(int quantity)
    {
        _materialQuantity.text = quantity.ToString();
    }

}
