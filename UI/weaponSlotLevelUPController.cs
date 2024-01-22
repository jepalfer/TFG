using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class weaponSlotLevelUPController : MonoBehaviour
{
    [SerializeField] private GameObject _weaponSprite;
    [SerializeField] private TextMeshProUGUI _weaponName;
    [SerializeField] private TextMeshProUGUI _soulsRequired;
    [SerializeField] private TextMeshProUGUI _idText;
    [SerializeField] private Button _levelUpButton;
    private int _weaponID;

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
        _idText.text = id.ToString();
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

    public Button getButton()
    {
        return _levelUpButton;
    }

}
