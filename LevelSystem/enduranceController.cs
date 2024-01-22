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
            if (inputManager.getKeyDown(inputEnum.Left.ToString()))
            {
                UIConfig.getController().getLevelUpUI().GetComponent<levelUpUI>().substractEnduranceLvl();
            }
            if (inputManager.getKeyDown(inputEnum.Right.ToString()))
            {
                UIConfig.getController().getLevelUpUI().GetComponent<levelUpUI>().addEnduranceLvl();
            }
            if (inputManager.getKeyDown(inputEnum.Accept.ToString()))
            {
                EventSystem.current.SetSelectedGameObject(UIConfig.getController().getLevelUpUI().GetComponent<levelUpUI>().getLevelUpButton().gameObject);
            }
        }
        else
        {
            _text.color = Color.white;
        }
    }
}
