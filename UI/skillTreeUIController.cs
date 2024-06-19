using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// skillTreeUIController es una clase que se usa para controlar la UI de los árboles de habilidad.
/// </summary>
public class skillTreeUIController : MonoBehaviour
{
    /// <summary>
    /// Referencia al panel derecho (de información)
    /// </summary>
    [SerializeField] private GameObject _rightPanel;

    /// <summary>
    /// Referencia al arma de la que estamos obteniendo la habilidad.
    /// </summary>
    private GameObject _instantiatedWeapon;

    /// <summary>
    /// Entero que representa el índice dentro de la lista de árboles.
    /// </summary>
    private int _indexInList;

    /// <summary>
    /// Referencia al arma al cual se le va a otorgar la habilidad.
    /// </summary>
    private GameObject _weaponToGiveSkill;

    /// <summary>
    /// Lista con todas las armas instanciadas.
    /// </summary>
    [SerializeField] private List<GameObject> _prefabs;

    /// <summary>
    /// Lista con todos los árboles de habilidad instanciados.
    /// </summary>
    [SerializeField] private List<GameObject> _instantiatedTreesPrefabs;

    /// <summary>
    /// Lista con los nombres de los árboles de habilidad.
    /// </summary>
    [SerializeField] private List<TextMeshProUGUI> _menusNames;

    /// <summary>
    /// Referencia al panel de la UI donde aparecen los árboles de habilidad.
    /// </summary>
    [SerializeField] private RectTransform _skillTreeContent;

    /// <summary>
    /// Referencia a la imagen del botón para navegar hacia la izquierda entre las armas.
    /// </summary>
    [SerializeField] private Image _leftButtonImage;

    /// <summary>
    /// Referencia a la imagen del botón para navegar hacia la derecha entre las armas.
    /// </summary>
    [SerializeField] private Image _rightButtonImage;

    /// <summary>
    /// Referencia a la imagen de la habilidad en el panel derecho.
    /// </summary>
    [SerializeField] private Image _skillSprite;

    /// <summary>
    /// Referencia al campo de texto del nombre de la habilidad en el panel derecho.
    /// </summary>
    [SerializeField] private TextMeshProUGUI _skillName;

    /// <summary>
    /// Referencia al campo de texto de la descripción de la habilidad en el panel derecho.
    /// </summary>
    [SerializeField] private TextMeshProUGUI _skillDesc;

    /// <summary>
    /// Referencia al campo de texto del precio de la habilidad en el panel derecho.
    /// </summary>
    [SerializeField] private TextMeshProUGUI _skillPrice;

    /// <summary>
    /// Referencia al último gameObject seleccionado.
    /// </summary>
    private GameObject _formerEventSystemSelected = null;

    /// <summary>
    /// Referencia a la imagen de fondo.
    /// </summary>
    [SerializeField] private Image _skillBackground;

    /// <summary>
    /// Referencia a la imagen de fondo cuando es una habilidad de status.
    /// </summary>
    [SerializeField] private Sprite _statusColor;

    /// <summary>
    /// Referencia a la imagen de fondo cuando es una habilidad de combo.
    /// </summary>
    [SerializeField] private Sprite _comboColor;

    /// <summary>
    /// Referencia a la imagen de fondo cuando es una habilidad pasiva.
    /// </summary>
    [SerializeField] private Sprite _passiveColor;
    /// <summary>
    /// Referencia a la imagen de fondo cuando es una habilidad de almas.
    /// </summary>
    [SerializeField] private Sprite _soulsColor;

    /// <summary>
    /// Método que se ejecuta cada frame para actualizar la lógica.
    /// </summary>
    void Update()
    {
        
        if (_indexInList < (_instantiatedTreesPrefabs.Count - 1) && inputManager.GetKeyDown(inputEnum.next))
        {
            config.getAudioManager().GetComponent<menuSFXController>().playTabSFX();
            _menusNames[_indexInList].color = Color.white;
            _instantiatedTreesPrefabs[_indexInList].SetActive(false);

            _indexInList++;

            _instantiatedTreesPrefabs[_indexInList].SetActive(true);
            _menusNames[_indexInList].color = Color.yellow;

            if (_instantiatedWeapon != null)
            {
                Destroy(_instantiatedWeapon);
                _instantiatedWeapon = null;
            }
            manageUpgradeSkillLogic();

            EventSystem.current.SetSelectedGameObject(_instantiatedTreesPrefabs[_indexInList].GetComponent<treeController>().getInitialSkill());
        }

        if (_indexInList > 0 && inputManager.GetKeyDown(inputEnum.previous))
        {
            config.getAudioManager().GetComponent<menuSFXController>().playTabSFX();
            _menusNames[_indexInList].color = Color.white;
            _instantiatedTreesPrefabs[_indexInList].SetActive(false);

            _indexInList--;

            _instantiatedTreesPrefabs[_indexInList].SetActive(true);
            _menusNames[_indexInList].color = Color.yellow;

            if (_instantiatedWeapon != null)
            {
                Destroy(_instantiatedWeapon);
                _instantiatedWeapon = null;
            }
            manageUpgradeSkillLogic();

            EventSystem.current.SetSelectedGameObject(_instantiatedTreesPrefabs[_indexInList].GetComponent<treeController>().getInitialSkill());
        }
        GameObject currentSelected = EventSystem.current.currentSelectedGameObject;

        if (currentSelected != _formerEventSystemSelected)
        {
            config.getAudioManager().GetComponent<menuSFXController>().playMenuNavigationSFX();
            _formerEventSystemSelected = currentSelected;
            modifyRightPanel();
        }

    }

    /// <summary>
    /// Método que modifica la información mostrada en el panel de la derecha.
    /// </summary>
    private void modifyRightPanel()
    {
        _skillSprite.sprite = _formerEventSystemSelected.GetComponent<skill>().getSkillSprite();
        _skillName.text = _formerEventSystemSelected.GetComponent<skill>().getSkillName();
        _skillDesc.text = _formerEventSystemSelected.GetComponent<skill>().getSkillDescription();
        _skillPrice.text = _formerEventSystemSelected.GetComponent<skill>().getSkillPoints().ToString();

        if (!_formerEventSystemSelected.GetComponent<skill>().isUnlockable())
        {
            _skillPrice.color = Color.red;
            if (_formerEventSystemSelected.GetComponent<skill>().getIsUnlocked())
            {
                _skillPrice.text = "Adquirida.";
            }
        }
        else
        {
            _skillPrice.color = Color.black;
        }

        if (_formerEventSystemSelected.GetComponent<skill>().getData().getEquipType() == equipEnum.onWeapon)
        {
            _skillBackground.sprite = _passiveColor;
        }
        else
        {
            if (_formerEventSystemSelected.GetComponent<skill>().getData().getType() == skillTypeEnum.combo)
            {
                _skillBackground.sprite = _comboColor;
            }
            else if (_formerEventSystemSelected.GetComponent<skill>().getData().getType() == skillTypeEnum.status)
            {
                _skillBackground.sprite = _statusColor;
            }
            else if (_formerEventSystemSelected.GetComponent<skill>().getData().getType() == skillTypeEnum.souls)
            {
                _skillBackground.sprite = _soulsColor;
            }

        }
    }

    /// <summary>
    /// Método que se ejecuta al hacer visible la UI y que la inicializa.
    /// </summary>
    public void initializeUI()
    {
        _instantiatedTreesPrefabs = new List<GameObject>();
        inventoryData data = saveSystem.loadInventory();

        for (int i = 0; i < _menusNames.Count; ++i)
        {
            _menusNames[i].text = "";
        }

        _leftButtonImage.GetComponent<Image>().enabled = false;
        _rightButtonImage.GetComponent<Image>().enabled = false;
        if (data != null)
        {
            List<serializedItemData> unlockedWeapons = data.getInventory().FindAll(item => item.getData().getTipo() == itemTypeEnum.weapon);

            unlockedWeapons.Sort((item1, item2) => item1.getData().getID().CompareTo(item2.getData().getID()));
            if (unlockedWeapons.Count > 0)
            {
                _rightPanel.SetActive(true);
                if (unlockedWeapons.Count > 1)
                {
                    _leftButtonImage.GetComponent<Image>().enabled = true;
                    _rightButtonImage.GetComponent<Image>().enabled = true;
                }
            }

            for (int i = 0; i < unlockedWeapons.Count; ++i)
            {
                GameObject newPrefab = _prefabs.Find(prefab => prefab.GetComponent<generalItem>().getID() == unlockedWeapons[i].getData().getID());
                if (newPrefab != null)
                {
                    GameObject instantiatedPrefab = Instantiate(newPrefab);
                    instantiatedPrefab.transform.SetParent(_skillTreeContent, false);
                    _instantiatedTreesPrefabs.Add(instantiatedPrefab);
                    _menusNames[i].text = unlockedWeapons[i].getData().getName();
                }
            }
        }

        _indexInList = 0;

        if (_instantiatedTreesPrefabs.Count > 0)
        {
            _menusNames[_indexInList].color = Color.yellow;
            _instantiatedTreesPrefabs[_indexInList].SetActive(true);

            manageUpgradeSkillLogic();

            EventSystem.current.SetSelectedGameObject(_instantiatedTreesPrefabs[_indexInList].GetComponent<treeController>().getInitialSkill());
        }
    }

    /// <summary>
    /// Método que maneja la lógica de desbloquear una habilidad.
    /// </summary>
    public void manageUpgradeSkillLogic()
    {
        lootItem selectedWeapon = config.getInventory().GetComponent<inventoryManager>().getInventory().Find(item => item.getTipo() == itemTypeEnum.weapon && item.getID() == _instantiatedTreesPrefabs[_indexInList].GetComponent<generalItem>().getID());

        weaponSlot unlockedWeapon = config.getInventory().GetComponent<weaponInventoryManagement>().getWeaponList().Find(weapon => weapon.getID() == selectedWeapon.getID());
        if (unlockedWeapon.getHand() == handEnum.primary)
        {

            if (saveSystem.loadWeaponsState().getPrimaryIndex() == -1 || saveSystem.loadWeaponsState().getPrimaryIndex() != config.getInventory().GetComponent<weaponInventoryManagement>().getWeaponList().IndexOf(unlockedWeapon))
            {
                _instantiatedWeapon = Instantiate(unlockedWeapon.getWeapon());
                _weaponToGiveSkill = _instantiatedWeapon;
            }
            else
            {
                _weaponToGiveSkill = weaponConfig.getPrimaryWeapon();
            }
        }
        else
        {

            if (saveSystem.loadWeaponsState().getSecundaryIndex() == -1 || saveSystem.loadWeaponsState().getSecundaryIndex() != config.getInventory().GetComponent<weaponInventoryManagement>().getWeaponList().IndexOf(unlockedWeapon))
            {
                _instantiatedWeapon = Instantiate(unlockedWeapon.getWeapon());
                _weaponToGiveSkill = _instantiatedWeapon;
            }
            else
            {
                _weaponToGiveSkill = weaponConfig.getSecundaryWeapon();
            }
        }
    }

    /// <summary>
    /// Método que desbloquea una habilidad dada.
    /// </summary>
    /// <param name="skill">Habilidad a desbloquear.</param>
    public void unlockSkill(skill skill)
    {
       //_weaponToGiveSkill.GetComponent<Weapon>().addSkill(skill);
        unlockedSkillsData data = saveSystem.loadSkillsState();
        

        sceneSkillsState newSkill = new sceneSkillsState(_weaponToGiveSkill.GetComponent<weapon>().getID(), skill);
        if (data == null)
        {
            unlockedSkillsData aux = new unlockedSkillsData();
            aux.getUnlockedSkills().Add(newSkill);
            data = aux;
        }
        else
        {
            data.getUnlockedSkills().Add(newSkill);
            for(int i = 0; i < data.getUnlockedSkills().Count; ++i)
            {
                Debug.Log(data.getUnlockedSkills()[i].getAssociatedSkill().getSkillID());
                Debug.Log("ID: " + data.getUnlockedSkills()[i].getWeaponID());
            }
        }

        if (skill.getData().getEquipType() == equipEnum.onWeapon)
        {
            if (weaponConfig.getPrimaryWeapon() != null)
            {
                if (data.getUnlockedSkills().Find(_skill => _skill.getWeaponID() == weaponConfig.getPrimaryWeapon().GetComponent<weapon>().getID() && _skill.getAssociatedSkill().getSkillID() == skill.getSkillID()) != null)
                {
                    GameObject searchedSkill = config.getPlayer().GetComponent<skillManager>().getAllSkills().Find(_skill => _skill.GetComponent<skill>().getSkillID() == skill.getSkillID());
                    weaponConfig.getPrimaryWeapon().GetComponent<weapon>().addSkill(Instantiate(searchedSkill));
                }
            }
            if (weaponConfig.getSecundaryWeapon() != null)
            {
                if (data.getUnlockedSkills().Find(_skill => _skill.getWeaponID() == weaponConfig.getSecundaryWeapon().GetComponent<weapon>().getID() && _skill.getAssociatedSkill().getSkillID() == skill.getSkillID()) != null)
                {
                    GameObject searchedSkill = config.getPlayer().GetComponent<skillManager>().getAllSkills().Find(_skill => _skill.GetComponent<skill>().getSkillID() == skill.getSkillID());
                    weaponConfig.getSecundaryWeapon().GetComponent<weapon>().addSkill(Instantiate(searchedSkill));
                }
            }
        }
        saveSystem.saveSkillsState(data.getUnlockedSkills());
        _skillPrice.color = Color.red;
        _skillPrice.text = "Adquirida.";
    }

    /// <summary>
    /// Método que se ejecuta al ocultar la UI.
    /// </summary>
    public void setUIOff()
    {
        _menusNames[_indexInList].color = Color.white;
        _indexInList = 0;
        if (_instantiatedWeapon != null)
        {
            Destroy(_instantiatedWeapon);
            _instantiatedWeapon = null;
        }

        for (int i = 0; i < _instantiatedTreesPrefabs.Count; ++i)
        {
            Destroy(_instantiatedTreesPrefabs[i]);
        }

        _instantiatedTreesPrefabs.Clear();
    }

    /// <summary>
    /// Getter que devuelve <see cref="_instantiatedTreesPrefabs"/>.
    /// </summary>
    /// <returns><see cref="_instantiatedTreesPrefabs"/>.</returns>
    public List<GameObject> getAllUIs()
    {
        return _instantiatedTreesPrefabs;
    }
}
