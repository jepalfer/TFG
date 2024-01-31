using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class enduranceController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _text;
    void Update()
    {
        if (EventSystem.current.currentSelectedGameObject == gameObject)
        {
            _text.color = Color.cyan;
            if (inputManager.GetKeyDown(inputEnum.left))
            {
                UIConfig.getController().getLevelUpUI().GetComponent<levelUpUIController>().substractEnduranceLvl();
            }
            if (inputManager.GetKeyDown(inputEnum.right))
            {
                UIConfig.getController().getLevelUpUI().GetComponent<levelUpUIController>().addEnduranceLvl();
            }
            if (inputManager.GetKeyDown(inputEnum.accept))
            {
                EventSystem.current.SetSelectedGameObject(UIConfig.getController().getLevelUpUI().GetComponent<levelUpUIController>().getLevelUpButton().gameObject);
            }
        }
        else
        {
            _text.color = Color.white;
        }
    }
}
