using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// equipItemsUIControllers es una clase que se usa para la UI de equipar un objeto consumible.
/// </summary>
public class equipItemsUIController : MonoBehaviour
{
    /// <summary>
    /// Referencia a la lista de objetos.
    /// </summary>
    private List<GameObject> _objectList;

    /// <summary>
    /// Referencia al prefab de los slots.
    /// </summary>
    [SerializeField] private GameObject _objectSlot;

    /// <summary>
    /// Referencia al panel de la UI donde se instancian los slots.
    /// </summary>
    [SerializeField] private Transform _slotHolder;

    /// <summary>
    /// Referencia al último objeto seleccionado.
    /// </summary>
    private GameObject _formerSelectedGameObject = null;

    /// <summary>
    /// Método que se ejecuta al hacer visible la UI y la inicializa.
    /// </summary>
    public void initializeUI()
    {
        Debug.Log("adios");
        _objectList = new List<GameObject>();
        for (int i = 0; i < config.getPlayer().GetComponent<equippedInventory>().getEquippedItems().Count; i++)
        {
            //Instanciamos un slot y le asignamos un ID
            GameObject newSlot = Instantiate(_objectSlot);
            newSlot.GetComponent<objectSlot>().setID(i);

            //Si a ese slot le corresponde un objeto
            if (config.getPlayer().GetComponent<equippedInventory>().getEquippedItems()[i] != null)
            {
                newSlot.GetComponent<objectSlot>().getAssociatedButton().GetComponent<Image>().sprite = config.getPlayer().GetComponent<equippedInventory>().getEquippedItems()[i].GetComponent<generalItem>().getIcon();
            }

            //Poner el sprite correspondiente al botón
            newSlot.transform.SetParent(_slotHolder, false);
            _objectList.Add(newSlot);
        }
        //Calculamos la navegación
        calculateNavigation();
        EventSystem.current.SetSelectedGameObject(_objectList[0].GetComponent<objectSlot>().getAssociatedButton().gameObject);
    }

    /// <summary>
    /// Método auxiliar para calcular la navegación entre slots.
    /// </summary>
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
            
            //Obtenemos la navegación del slot actual
            current_slot_navigation = _objectList[index].GetComponent<objectSlot>().getAssociatedButton().navigation;
            if (index == 0 && _objectList.Count > 1) //Primer slot
            {
                current_slot_navigation.selectOnRight = _objectList[index + 1].GetComponent<objectSlot>().getAssociatedButton();
            }
            else
            {
                if (index == _objectList.Count - 1) //Último slot
                {
                    current_slot_navigation.selectOnLeft = _objectList[index - 1].GetComponent<objectSlot>().getAssociatedButton();
                }
                else //Slots intermedios
                {
                    current_slot_navigation.selectOnLeft = _objectList[index - 1].GetComponent<objectSlot>().getAssociatedButton();
                    current_slot_navigation.selectOnRight = _objectList[index + 1].GetComponent<objectSlot>().getAssociatedButton();
                }
            }
            //Asociamos la navegación del slot
            _objectList[index].GetComponent<objectSlot>().getAssociatedButton().navigation = current_slot_navigation;

        }
    }

    /// <summary>
    /// Método que se ejecuta cada frame para actualizar la lógica.
    /// </summary>
    private void Update()
    {
        GameObject currentSelected = EventSystem.current.currentSelectedGameObject;
        //Debug.Log(currentSelected.transform.parent);
        if (currentSelected != _formerSelectedGameObject)
        {
            if (_formerSelectedGameObject != null)
            {
                _formerSelectedGameObject.transform.parent.GetComponent<objectSlot>().getOverlayImage().SetActive(false);
            }
            _formerSelectedGameObject = currentSelected;
        }
        else
        {
            currentSelected.transform.parent.GetComponent<objectSlot>().getOverlayImage().SetActive(true);
        }
    }

    /// <summary>
    /// Método que se ejecuta al ocultar la UI.
    /// </summary>
    public void setUIOff()
    {
        for (int i = 0; i < _objectList.Count; ++i)
        {
            Destroy(_objectList[i]);
        }
        _objectList.Clear();
    }
}
