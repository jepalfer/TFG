using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class equipItemsUIController : MonoBehaviour
{
    private List<GameObject> _objectList;
    [SerializeField] private GameObject _objectSlot;
    [SerializeField] private Transform _slotHolder;
    public void initializeUI()
    {
        _objectList = new List<GameObject>();
        for (int i = 0; i < config.getPlayer().GetComponent<equippedInventory>().getEquippedItems().Count; i++)
        {
            GameObject newSlot = Instantiate(_objectSlot);
            newSlot.GetComponent<objectSlot>().setID(i);

            if (config.getPlayer().GetComponent<equippedInventory>().getEquippedItems()[i] != null)
            {
                newSlot.GetComponent<objectSlot>().getAssociatedButton().GetComponent<Image>().sprite = config.getPlayer().GetComponent<equippedInventory>().getEquippedItems()[i].GetComponent<generalItem>().getIcon();
            }

            //Poner el sprite correspondiente al botón
            newSlot.transform.SetParent(_slotHolder, false);
            _objectList.Add(newSlot);
        }
        calculateNavigation();
        EventSystem.current.SetSelectedGameObject(_objectList[0].GetComponent<objectSlot>().getAssociatedButton().gameObject);
    }

    public void calculateNavigation()
    {

        Navigation current_slot_navigation;
        Navigation modeNavigation = new Navigation();
        for (int index = 0; index < _objectList.Count; ++index)
        {
            modeNavigation.mode = Navigation.Mode.None;
            _objectList[index].GetComponent<objectSlot>().getAssociatedButton().navigation = modeNavigation;
            modeNavigation.mode = Navigation.Mode.Explicit;
            _objectList[index].GetComponent<objectSlot>().getAssociatedButton().navigation = modeNavigation;
            current_slot_navigation = _objectList[index].GetComponent<objectSlot>().getAssociatedButton().navigation;
            if (index == 0 && _objectList.Count > 1)
            {
                current_slot_navigation.selectOnRight = _objectList[index + 1].GetComponent<objectSlot>().getAssociatedButton();
            }
            else
            {
                if (index == _objectList.Count - 1)
                {
                    current_slot_navigation.selectOnLeft = _objectList[index - 1].GetComponent<objectSlot>().getAssociatedButton();
                }
                else
                {
                    current_slot_navigation.selectOnLeft = _objectList[index - 1].GetComponent<objectSlot>().getAssociatedButton();
                    current_slot_navigation.selectOnRight = _objectList[index + 1].GetComponent<objectSlot>().getAssociatedButton();
                }
            }

            _objectList[index].GetComponent<objectSlot>().getAssociatedButton().navigation = current_slot_navigation;

        }
    }

    public void setUIOff()
    {
        for (int i = 0; i < _objectList.Count; ++i)
        {
            Destroy(_objectList[i]);
        }
        _objectList.Clear();
    }
}
