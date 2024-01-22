using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class agilityController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _text;
    void Update()
    {

        if (EventSystem.current.currentSelectedGameObject == gameObject)
        {
            _text.color = Color.cyan;
            if (inputManager.getKeyDown(inputEnum.Left.ToString()))
            {
                UIConfig.getController().getLevelUpUI().GetComponent<levelUpUI>().substractAgilityLvl();
            }
            if (inputManager.getKeyDown(inputEnum.Right.ToString()))
            {
                UIConfig.getController().getLevelUpUI().GetComponent<levelUpUI>().addAgilityLvl();
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
