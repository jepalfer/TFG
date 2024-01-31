using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class skillManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> _equippedSkills;
    [SerializeField] private List<sceneSkillsState> _unlockedSkills;
    [SerializeField] private List<GameObject> _allSkills;
    public void equipSkill(skillData skillToEquip, int index)
    {

        _equippedSkills[index] = getSkillToEquip(skillToEquip.getType(), skillToEquip.getSkillID());
    }

    public List<GameObject> getAllSkills()
    {
        return _allSkills;
    }

    public List<GameObject> getEquippedSkills()
    {
        return _equippedSkills;
    }

    public void setEquippedValue(GameObject val, int index)
    {
        _equippedSkills[index] = val;
    }

    public List<sceneSkillsState> getUnlockedSkills()
    {
        return _unlockedSkills;
    }

    public GameObject getSkillToEquip(skillTypeEnum type, int id)
    {
        GameObject skill = _allSkills.Find(searchedSkill => searchedSkill.GetComponent<skill>().getType() == type && searchedSkill.GetComponent<skill>().getSkillID() == id);

        /*
        if (type == skillTypeEnum.combo)
        {
            skill = _allSkills.Find(searchedSkill =>
            {
                comboIncreaseSkill combo = searchedSkill.GetComponent<comboIncreaseSkill>();
                return combo != null && combo.getData()?.getSkillID() == id;
            });
        }
        else if (type == skillTypeEnum.stat)
        {
            skill = _allSkills.Find(searchedSkill =>
            {
                statUpgradeSkill stat = searchedSkill.GetComponent<statUpgradeSkill>();
                return stat != null && stat.getData()?.getSkillID() == id;
            });
        }
        else if (type == skillTypeEnum.status)
        {
            skill = _allSkills.Find(searchedSkill =>
            {
                statusSkill status = searchedSkill.GetComponent<statusSkill>();
                return status != null && status.getData()?.getSkillID() == id;
            });
        }
        else
        {
            skill = _allSkills.Find(searchedSkill =>
            {
                functionalitySkill functionality = searchedSkill.GetComponent<functionalitySkill>();
                return functionality != null && functionality.getData()?.getSkillID() == id;
            });
        }*/

        return skill;
    }
    /*
    public List<int> getIndexesOfSkill(skillType type)
    {
        List<int> indexes = new List<int>();

        for (int i = 0; i < _equippedSkills.Count; ++i)
        {
            if (_equippedSkills[i] != null)
            {
                if (_equippedSkills[i].getType() == type)
                {
                    indexes.Add(i);
                }
            }
        }
        return indexes;
    }*/
    // Start is called before the first frame update
    void Start()
    {
        equippedSkillData equippedData = saveSystem.loadEquippedSkillsState();
        _equippedSkills = new List<GameObject>();

        for (int i = 0; i < 3; ++i)
        {
            _equippedSkills.Add(null);
        }

        if (equippedData == null)
        {
        }
        else
        {
            for (int i = 0; i < 3; ++i)
            {
                if (equippedData.getIDs()[i] != -1)
                {
                    _equippedSkills[i] = getSkillToEquip(equippedData.getTypes()[i], equippedData.getIDs()[i]);
                    if (_equippedSkills[i] != null)
                    {
                        UIConfig.getController().getEquipSkillsUI().GetComponent<skillUIController>().getSprites()[i].GetComponent<Image>().sprite = _equippedSkills[i].GetComponent<skill>().getSkillSprite();
                        /*
                        if (equippedData.getTypes()[i] == skillTypeEnum.combo)
                        {
                            UIConfig.getController().getEquipSkillsUI().GetComponent<skillUIController>().getSprites()[i].GetComponent<Image>().sprite = _equippedSkills[i].GetComponent<comboIncreaseSkill>().getSkillSprite();
                        }
                        else if (equippedData.getTypes()[i] == skillTypeEnum.stat)
                        {
                            UIConfig.getController().getEquipSkillsUI().GetComponent<skillUIController>().getSprites()[i].GetComponent<Image>().sprite = _equippedSkills[i].GetComponent<statUpgradeSkill>().getSkillSprite();
                        }
                        else if (equippedData.getTypes()[i] == skillTypeEnum.status)
                        {
                            UIConfig.getController().getEquipSkillsUI().GetComponent<skillUIController>().getSprites()[i].GetComponent<Image>().sprite = _equippedSkills[i].GetComponent<statusSkill>().getSkillSprite();
                        }
                        else
                        {
                            UIConfig.getController().getEquipSkillsUI().GetComponent<skillUIController>().getSprites()[i].GetComponent<Image>().sprite = _equippedSkills[i].GetComponent<functionalitySkill>().getSkillSprite();
                        }*/
                        UIConfig.getController().getEquipSkillsUI().GetComponent<skillUIController>().getSprites()[i].SetActive(true);
                    }
                }
            }
        }

        unlockedSkillsData data = saveSystem.loadSkillsState();
        if (data != null)
        {
            _unlockedSkills = data.getUnlockedSkills();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
