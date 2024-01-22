using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class buttonModifier : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    private bool _isSelected;
    [SerializeField] private TextMeshProUGUI _buttonText;
    private static Button _selected;

    public void OnSelect(BaseEventData eventData)
    {
        setColor(Color.yellow);
        _isSelected = true;
        _selected = gameObject.transform.GetComponent<Button>();
    }

    public void OnDeselect(BaseEventData eventData)
    {
        setColor(Color.white);
        _isSelected = false;

    }

    public static Button getSelected()
    {
        return _selected;
    }

    private void setColor(Color color)
    {
        _buttonText.color = color;
    }
    public bool getIsSelected()
    {
        return _isSelected;
    }

}
