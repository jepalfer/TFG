using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class equipSkillController : MonoBehaviour
{
    [SerializeField] private GameObject _rightPannel;
    [SerializeField] private Image _skillSprite;
    [SerializeField] private TextMeshProUGUI _skillName;
    [SerializeField] private TextMeshProUGUI _skillDescription;
    [SerializeField] private GameObject _skillPrefab;
    [SerializeField] private Transform _skillHolder;
    [SerializeField] private GridLayoutGroup _grid;
    private GameObject _formerEventSystemSelected = null;
    private List<GameObject> _skills;


    public void createSkillInventory(unlockedSkillsData data)
    {
        _skills = new List<GameObject>(); 
        if (data.getUnlockedSkills().Count > 0)
        {
            _rightPannel.SetActive(true);

            for (int i = 0; i < data.getUnlockedSkills().Count; ++i)
            {
                if (data.getUnlockedSkills()[i].getAssociatedSkill().getEquipType() == equipEnum.equippable)
                {
                    GameObject newSkill = Instantiate(_skillPrefab);
                    skillData _skillData = data.getUnlockedSkills()[i].getAssociatedSkill();

                    newSkill.GetComponent<Button>().onClick.RemoveAllListeners();
                    newSkill.GetComponent<Button>().onClick.AddListener(() =>
                    {
                        UIConfig.getController().getEquipSkillsUI().GetComponent<skillUIController>().equipSkill(_skillData);
                        // UIConfig.getController().useSelectSkillUI();
                    });
                    newSkill.GetComponent<skillSlot>().setData(_skillData);
                    newSkill.transform.SetParent(_skillHolder, false);
                    _skills.Add(newSkill);
                }
            }

            calculateNavigation();
            if (_skills.Count > 0)
            {
                EventSystem.current.SetSelectedGameObject(_skills[0]);
            }
        }
        else
        {
            EventSystem.current.SetSelectedGameObject(null);
        }

    }

    public void calculateNavigation()
    {
        int counter = 0;
        Navigation current_slot_navigation;
        Navigation modeNavigation = new Navigation();
        for (int index = 0; index < _skills.Count; ++index)
        {
            modeNavigation.mode = Navigation.Mode.None;
            _skills[index].GetComponent<Button>().navigation = modeNavigation;
            modeNavigation.mode = Navigation.Mode.Explicit;
            _skills[index].GetComponent<Button>().navigation = modeNavigation;
            current_slot_navigation = _skills[index].GetComponent<Button>().navigation;
            if (index == 0 && _skills.Count > 1)
            {
                current_slot_navigation.selectOnRight = _skills[index + 1].GetComponent<Button>();
                counter++;
            }
            else
            {
                if (index == _skills.Count - 1)
                {
                    if (index % (_grid.constraintCount) != 0)
                    {
                        current_slot_navigation.selectOnLeft = _skills[index - 1].GetComponent<Button>();
                    }
                }
                else
                {
                    if (index % (_grid.constraintCount) != 0)
                    {
                        current_slot_navigation.selectOnLeft = _skills[index - 1].GetComponent<Button>();
                    }
                    if (counter < (_grid.constraintCount - 1))
                    {
                        current_slot_navigation.selectOnRight = _skills[index + 1].GetComponent<Button>();
                        counter++;
                    }
                    else
                    {
                        counter = 0;
                    }
                }
            }

            if (index - (_grid.constraintCount) >= 0)
            {
                current_slot_navigation.selectOnUp = _skills[index - (_grid.constraintCount)].GetComponent<Button>();
            }

            if (index + (_grid.constraintCount) < _skills.Count)
            {
                current_slot_navigation.selectOnDown = _skills[index + (_grid.constraintCount)].GetComponent<Button>();
            }
            _skills[index].GetComponent<Button>().navigation = current_slot_navigation;

        }
    }

    public void destroySkillInventory()
    {
        if (_skills != null)
        {
            if (_skills.Count > 0)
            {
                for (int i = 0; i < _skills.Count; ++i)
                {
                    Destroy(_skills[i]);
                }
            }
            _skills.Clear();
        }
    }

    private void Update()
    {
        GameObject currentSelected = EventSystem.current.currentSelectedGameObject;
        
        if (currentSelected != _formerEventSystemSelected)
        {
            _formerEventSystemSelected = currentSelected;
            changeInformationPanel();
        }

    }

    public void changeInformationPanel()
    {
        if (_rightPannel.activeSelf)
        {
            _skillSprite.sprite = EventSystem.current.currentSelectedGameObject.GetComponent<skillSlot>().getData().getSkillSprite();
            _skillName.text = EventSystem.current.currentSelectedGameObject.GetComponent<skillSlot>().getData().getSkillName();
            _skillDescription.text = EventSystem.current.currentSelectedGameObject.GetComponent<skillSlot>().getData().getSkillDescription();
        }
    }
}
