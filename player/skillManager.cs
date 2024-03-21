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

        if (_equippedSkills[index] != null)
        {
            Destroy(_equippedSkills[index]);
        }
        _equippedSkills[index] = Instantiate(getSkillToEquip(skillToEquip.getType(), skillToEquip.getSkillID()));
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
        return skill;
    }
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
                    Destroy(_equippedSkills[i]);
                    _equippedSkills[i] = Instantiate(getSkillToEquip(equippedData.getTypes()[i], equippedData.getIDs()[i]));
                    if (_equippedSkills[i] != null)
                    {
                        UIConfig.getController().getEquipSkillsUI().GetComponent<skillUIController>().getSprites()[i].GetComponent<Image>().sprite = _equippedSkills[i].GetComponent<skill>().getSkillSprite();
                        
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
