using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class skillUIController : MonoBehaviour
{
    [SerializeField] private int _currentID;
    [SerializeField] private List<GameObject> _slots;
    [SerializeField] private List<GameObject> _sprites;
    // Start is called before the first frame update
    void Start()
    {
        _currentID = 0;
    }

    public void setID(int id)
    {
        _currentID = id;
    }

    public int getCurrentID()
    {
        return _currentID;
    }

    public List<GameObject> getSprites()
    {
        return _sprites;
    }

    public List<GameObject> getSlots()
    {
        return _slots;
    }

    public void equipSkill(skillData skill)
    {
        List<GameObject> equippedSkills = config.getPlayer().GetComponent<skillManager>().getEquippedSkills();
        int index = -1;

        checkIfEquipped(equippedSkills, ref index, skill);
        equippedSkillData data = saveSystem.loadEquippedSkillsState();
        int[] IDs;
        skillType[] types;

        if (data == null)
        {
            IDs = new int[3];
            types = new skillType[3];
            for (int i = 0; i < equippedSkills.Count; ++i)
            {
                IDs[i] = -1;
            }
        }
        else
        {
            IDs = data.getIDs();
            types = data.getTypes();
        }

        if (index != -1)
        {
            config.getPlayer().GetComponent<skillManager>().setEquippedValue(null, index);
            _sprites[index].GetComponent<Image>().sprite = null;
            _sprites[index].SetActive(false);

            IDs[index] = -1;

        }

        config.getPlayer().GetComponent<skillManager>().equipSkill(skill, _currentID);

        _sprites[_currentID].SetActive(true);
        _sprites[_currentID].GetComponent<Image>().sprite = skill.getSkillSprite();

        IDs[_currentID] = skill.getSkillID();
        types[_currentID] = skill.getType();

        saveSystem.saveEquippedSkillsState(IDs, types);
    }


    public void checkIfEquipped(List<GameObject> skills, ref int index, skillData data)
    {
        for (int i = 0; i < skills.Count; ++i)
        {
            if (skills[i] != null)
            {
                if (skills[i].GetComponent<comboIncreaseSkill>() != null)
                {
                    if (skills[i].GetComponent<comboIncreaseSkill>().getData().getSkillID() == data.getSkillID() && i != _currentID)
                    {
                        index = i;
                        break;
                    }
                }
                else if (skills[i].GetComponent<statUpgradeSkill>() != null)
                {
                    if (skills[i].GetComponent<statUpgradeSkill>().getData().getSkillID() == data.getSkillID() && i != _currentID)
                    {
                        index = i;
                        break;
                    }
                }
                else if (skills[i].GetComponent<statusSkill>() != null)
                {
                    if (skills[i].GetComponent<statusSkill>().getData().getSkillID() == data.getSkillID() && i != _currentID)
                    {
                        index = i;
                        break;
                    }
                }
                else
                {
                    if (skills[i].GetComponent<functionalitySkill>().getData().getSkillID() == data.getSkillID() && i != _currentID)
                    {
                        index = i;
                        break;
                    }
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!UIController.getIsEquippingSkill())
        {
            if (inputManager.getKeyDown(inputEnum.Left.ToString()) && _currentID > 0)
            {
                _slots[_currentID].SetActive(false);
                _currentID--;
                _slots[_currentID].SetActive(true);
            }
            if (inputManager.getKeyDown(inputEnum.Right.ToString()) && _currentID < _slots.Count)
            {
                _slots[_currentID].SetActive(false);
                _currentID++;
                _slots[_currentID].SetActive(true);
            }

            if (inputManager.getKeyDown(inputEnum.Enter_equip.ToString()))
            {
                UIConfig.getController().useSelectSkillUI();
            }
        }
        else
        {
            if (inputManager.getKeyDown(inputEnum.Enter_equip.ToString()))
            {
                UIConfig.getController().useSelectSkillUI();
            }
        }
    }
}