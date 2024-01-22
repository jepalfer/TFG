using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class skillTreeUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _skillName;
    [SerializeField] private TextMeshProUGUI _skillDesc;
    [SerializeField] private TextMeshProUGUI _isUnlocked;
    [SerializeField] private TextMeshProUGUI _canBeUnlocked;
    [SerializeField] private TextMeshProUGUI _skillPRequired;

    //[SerializeField] private GameObject _unlockButton;
    //[SerializeField] private GameObject _informationPanel;
    private GameObject _formerSelectedSkill;
   
    /*
    public void showInformationPanel()
    {
        _informationPanel.SetActive(!_informationPanel.activeSelf);
    }*/

    public void setCanBeUnlocked(string text)
    {
        _canBeUnlocked.text = text;
    }
    public void setIsUnlocked(string text)
    {
        _isUnlocked.text = text;
    }

    public void writeSkillOnUI(skill skill)
    {

        _skillName.text = skill.getSkillName();
        _skillDesc.text = skill.getSkillDescription();
        _isUnlocked.text = skill.getIsUnlocked() ? "Is unlocked" : "Is not unlocked";

        skill.setCanBeUnlocked(skill.isUnlockable());
        _canBeUnlocked.text = skill.getCanBeUnlocked() ? "Can be unlocked" : "Cannot be unlocked";

        _skillPRequired.text = skill.getSkillPoints().ToString();

    }

    private void Update()
    {
        GameObject currentSelected = EventSystem.current.currentSelectedGameObject;


        if (EventSystem.current.currentSelectedGameObject != null)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                writeSkillOnUI(currentSelected.GetComponent<skill>());
                //_unlockButton.GetComponent<skillTreeButton>().assignSkill(currentSelected.GetComponent<Skill>());
            }
            /*if (inputManager.getKeyDown("Interact"))
            {
                writeSkillOnUI(currentSelected.GetComponent<Skill>());
                _unlockButton.GetComponent<skillTreeButton>().assignSkill(currentSelected.GetComponent<Skill>());
                showInformationPanel();
            }*/
        }
        if (currentSelected != _formerSelectedSkill)
        {
            _formerSelectedSkill = currentSelected;
            writeSkillOnUI(currentSelected.GetComponent<skill>());
        }
    }
}
