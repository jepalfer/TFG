using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class precisionController : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI _text;
    // Update is called once per frame
    void Update()
    {

        if (EventSystem.current.currentSelectedGameObject == gameObject)
        {
            _text.color = Color.cyan;
            if (inputManager.getKeyDown(inputEnum.Left.ToString()))
            {
                UIConfig.getController().getLevelUpUI().GetComponent<levelUpUI>().substractPrecisionLvl();
            }
            if (inputManager.getKeyDown(inputEnum.Right.ToString()))
            {
                UIConfig.getController().getLevelUpUI().GetComponent<levelUpUI>().addPrecisionLvl();
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
