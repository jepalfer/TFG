using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using TMPro;
using UnityEngine.EventSystems;

/// <summary>
/// selectSkillUIController es una clase que controla y maneja la lógica de la UI para seleccionar una habilidad a equipar.
/// </summary>
public class selectSkillUIController : MonoBehaviour
{
    /// <summary>
    /// Referencia al panel de la derecha que contiene la información de la habilidad que estamos seleccionando.
    /// </summary>
    [SerializeField] private GameObject _rightPannel;

    /// <summary>
    /// Referencia al sprite de la habilidad en el panel.
    /// </summary>
    [SerializeField] private Image _skillSprite;

    /// <summary>
    /// Referencia al nombre de la habilidad en el panel.
    /// </summary>
    [SerializeField] private TextMeshProUGUI _skillName;

    /// <summary>
    /// Referencia a la descripción de la habilidad en el panel.
    /// </summary>
    [SerializeField] private TextMeshProUGUI _skillDescription;

    /// <summary>
    /// Referencia al prefab de la habilidad que se instancia en <see cref="_skillHolder"/>.
    /// </summary>
    [SerializeField] private GameObject _skillPrefab;

    /// <summary>
    /// Referencia a la parte de la UI donde aparecen las habilidades.
    /// </summary>
    [SerializeField] private Transform _skillHolder;

    /// <summary>
    /// Referencia al grid que contiene las habilidades.
    /// </summary>
    [SerializeField] private GridLayoutGroup _grid;

    /// <summary>
    /// Referencia a la última habilidad seleccionada.
    /// </summary>
    private GameObject _formerEventSystemSelected = null;

    /// <summary>
    /// Lista de todas las habilidades instanciadas.
    /// </summary>
    private List<GameObject> _skills;

    /// <summary>
    /// Método que instancia todas las habilidades que se ven en la UI.
    /// </summary>
    /// <param name="data">Objeto de tipo <see cref="unlockedSkillsData"/> que contiene una referencia a los datos cargados.</param>
    public void createSkillInventory(unlockedSkillsData data)
    {
        _skills = new List<GameObject>();
        data.getUnlockedSkills().Sort((skill1, skill2) => skill1.getAssociatedSkill().getSkillID().CompareTo(skill2.getAssociatedSkill().getSkillID()));
        //Si tenemos habilidades desbloqueadas
        if (data.getUnlockedSkills().FindAll(skill => skill.getAssociatedSkill().getEquipType() == equipEnum.equippable).Count > 0)
        {
            _rightPannel.SetActive(true);

            //Recorremos las habilidades
            for (int i = 0; i < data.getUnlockedSkills().Count; ++i)
            {
                //Si las podemos equipar
                if (data.getUnlockedSkills()[i].getAssociatedSkill().getEquipType() == equipEnum.equippable)
                {
                    createSkill(data, i);
                }
            }

            //Calculamos la navegación en la UI de los botones 
            calculateNavigation();

            //Asignamos el GameObject seleccionado
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

    /// <summary>
    /// Método auxiliar para instanciar la habilidad que corresponde.
    /// </summary>
    /// <param name="data">Los datos cargados sobre las habilidades.</param>
    /// <param name="index">Índice de la lista de habilidades sobre el que nos encontramos.</param>
    public void createSkill(unlockedSkillsData data, int index)
    {
        //Instanciamos y asignamos algunas variables y creamos un listener para el botón
        GameObject newSkill = Instantiate(_skillPrefab);
        skillData _skillData = data.getUnlockedSkills()[index].getAssociatedSkill();

        newSkill.GetComponent<Button>().onClick.RemoveAllListeners();
        newSkill.GetComponent<Button>().onClick.AddListener(() =>
        {
            UIConfig.getController().getEquipSkillsUI().GetComponent<skillUIController>().equipSkill(_skillData);
            // UIConfig.getController().useSelectSkillUI();
        });
        newSkill.GetComponent<skillSlot>().setData(_skillData);
        newSkill.GetComponent<skillSlotLogic>().setSkillSprite(_skillData.getSkillSprite());
        newSkill.transform.SetParent(_skillHolder, false);
        _skills.Add(newSkill);
    }

    /// <summary>
    /// Método auxiliar para calcular la navegación entre botones en la UI.
    /// </summary>
    public void calculateNavigation()
    {
        int counter = 0;
        Navigation currentSlotNavigation;
        Navigation modeNavigation = new Navigation();

        //Recorremos las habilidades instanciadas
        for (int index = 0; index < _skills.Count; ++index)
        {
            modeNavigation.mode = Navigation.Mode.None;
            _skills[index].GetComponent<Button>().navigation = modeNavigation;

            modeNavigation.mode = Navigation.Mode.Explicit;
            _skills[index].GetComponent<Button>().navigation = modeNavigation;
            
            //Obtenemos la navegación de la habilidad actual
            currentSlotNavigation = _skills[index].GetComponent<Button>().navigation;

            if (index == 0 && _skills.Count > 1)  //Primera habilidad y hay más
            {
                currentSlotNavigation.selectOnRight = _skills[index + 1].GetComponent<Button>();
                counter++;
            }
            else
            {
                if (index == _skills.Count - 1) // Estamos en la última habilidad
                {
                    if (index % (_grid.constraintCount) != 0) //No es la primera habilidad de una fila
                    {
                        currentSlotNavigation.selectOnLeft = _skills[index - 1].GetComponent<Button>();
                    }
                }
                else // No estamos en la última habilidad
                {
                    if (index % (_grid.constraintCount) != 0)  //No es la primera habilidad de una fila
                    {
                        currentSlotNavigation.selectOnLeft = _skills[index - 1].GetComponent<Button>();
                    }
                    if (counter < (_grid.constraintCount - 1))  //No es la última habilidad de una fila
                    {
                        currentSlotNavigation.selectOnRight = _skills[index + 1].GetComponent<Button>();
                        counter++;
                    }
                    else
                    {
                        counter = 0;
                    }
                }
            }

            if (index - (_grid.constraintCount) >= 0) //No estamos en la fila de arriba
            {
                currentSlotNavigation.selectOnUp = _skills[index - (_grid.constraintCount)].GetComponent<Button>();
            }

            if (index + (_grid.constraintCount) < _skills.Count) //No estamos en la fila de abajo
            {
                currentSlotNavigation.selectOnDown = _skills[index + (_grid.constraintCount)].GetComponent<Button>();
            }
            _skills[index].GetComponent<Button>().navigation = currentSlotNavigation;

        }
    }

    /// <summary>
    /// Método auxiliar para limpiar la UI.
    /// </summary>
    public void destroySkillInventory()
    {
        if (_skills != null)
        {
            if (_skills.Count > 0)
            {
                for (int i = 0; i < _skills.Count; ++i)
                {
                    //Destruimos las habilidades instanciadas
                    Destroy(_skills[i]);
                }
            }
            _skills.Clear();
        }
    }

    /// <summary>
    /// Método que se ejecuta cada frame para actualizar la lógica.
    /// Cambia la información del panel derecho.
    /// </summary>
    private void Update()
    {
        GameObject currentSelected = EventSystem.current.currentSelectedGameObject;
        
        if (currentSelected != _formerEventSystemSelected && currentSelected != null)
        {
            _formerEventSystemSelected = currentSelected;
            changeInformationPanel();
        }

    }

    /// <summary>
    /// Método auxiliar para cambiar la información del panel derecho.
    /// </summary>
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
