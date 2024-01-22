using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class profileList : MonoBehaviour
{
    [SerializeField] private Transform _profileHolder;
    [SerializeField] private GameObject _profilePrefab;
    [SerializeField] private EventSystem _eventSystem;
    [SerializeField] private Button _cancelButton;
    private List<profile> profiles = new List<profile>();
    private void Start()
    {
        _eventSystem.firstSelectedGameObject = _cancelButton.gameObject;
        foreach (string name in profileIndex.getUserNames())
        {
            GameObject _object = Instantiate(_profilePrefab);
            profile UI = _object.GetComponent<profile>();
            profiles.Add(UI);
            
            //Cambiamos de ruta para cargar los atributos de todas las rutas
            profileSystem.setCurrentPath(profileSystem.getPath() + name + "/");
            attributesData data = saveSystem.loadAttributes();

            if (data == null)
            {
                UI.setLevel(6);
                UI.setVitality(1);
                UI.setEndurance(1);
                UI.setStrength(1);
                UI.setDexterity(1);
                UI.setAgility(1);
                UI.setPrecision(1);
            }
            else
            {
                UI.setLevel(data.getLevel());
                UI.setVitality(data.getVitality());
                UI.setEndurance(data.getEndurance());
                UI.setStrength(data.getStrength());
                UI.setDexterity(data.getDexterity());
                UI.setAgility(data.getAgility());
                UI.setPrecision(data.getPrecision());
            }

            UI.setUserName(name);
            UI.getLoadBtn().onClick.AddListener(() =>
            {
                profileSystem.setProfileName(UI.getName());
                //Cambiamos de ruta para cargar la partida que toca
                profileSystem.setCurrentPath(profileSystem.getPath() + UI.getName() + "/");

                statSystem.setLevel(UI.getLevel());
                statSystem.getVitality().setLevel(UI.getVitality());
                statSystem.getEndurance().setLevel(UI.getEndurance());
                statSystem.getStrength().setLevel(UI.getStrength());
                statSystem.getDexterity().setLevel(UI.getDexterity());
                statSystem.getAgility().setLevel(UI.getAgility());
                statSystem.getPrecision().setLevel(UI.getPrecision());

                saveSystem.savePath(profileSystem.getCurrentPath(), UI.getName());
                SceneManager.LoadScene("ToL");
            });

            UI.getRemoveBtn().onClick.AddListener(() => {
                profileIndex.removeName(UI.getName());
                //Cambiamos de ruta para eliminar la partida que toca
                profileSystem.setCurrentPath(profileSystem.getPath() + UI.getName() + "/");

                string[] files = Directory.GetFiles(profileSystem.getPath() + UI.getName() + "/");
                foreach (string file in files)
                {
                    File.Delete(file);
                }

                Directory.Delete(profileSystem.getCurrentPath());
                Destroy(_object);

                Debug.Log(profiles.Count);

                profiles.Remove(UI);

                EventSystem.current.SetSelectedGameObject(_cancelButton.gameObject);

                if (profiles.Count > 0)
                {
                    EventSystem.current.SetSelectedGameObject(profiles[0].getLoadBtn().gameObject);
                }

                calculateNavigation();

            });
            _object.transform.SetParent(_profileHolder, false);
        }

        calculateNavigation();

    }

    public void calculateNavigation()
    {
        Navigation cancel_navigation = _cancelButton.navigation;

        for (int i = 0; i < profiles.Count; i++)
        {
            Navigation current_UI_load = profiles[i].getLoadBtn().navigation;
            Navigation current_UI_remove = profiles[i].getRemoveBtn().navigation;

            if (i == 0)                                     //Estamos en el primero
            {
                _eventSystem.firstSelectedGameObject = profiles[i].getLoadBtn().gameObject;

                if (profiles.Count > 1)
                {
                    current_UI_load.selectOnDown = profiles[i + 1].getLoadBtn();
                    current_UI_remove.selectOnDown = profiles[i + 1].getRemoveBtn();
                }
                else
                {
                    current_UI_load.selectOnDown = _cancelButton;
                    current_UI_remove.selectOnDown = _cancelButton;
                    cancel_navigation.selectOnUp = profiles[i].getLoadBtn();
                    _cancelButton.navigation = cancel_navigation;
                }
            }
            else if (i > 0 && i < profiles.Count - 1)      //Estamos en los del medio
            {
                current_UI_load.selectOnDown = profiles[i + 1].getLoadBtn();
                current_UI_remove.selectOnDown = profiles[i + 1].getRemoveBtn();

                current_UI_load.selectOnUp = profiles[i - 1].getLoadBtn();
                current_UI_remove.selectOnUp = profiles[i - 1].getRemoveBtn();
            }
            else                                            //Estamos en el ultimo
            {
                current_UI_load.selectOnDown = _cancelButton;
                current_UI_remove.selectOnDown = _cancelButton;

                current_UI_load.selectOnUp = profiles[i - 1].getLoadBtn();
                current_UI_remove.selectOnUp = profiles[i - 1].getRemoveBtn();

                cancel_navigation.selectOnUp = profiles[profiles.Count - 1].getLoadBtn();
                _cancelButton.navigation = cancel_navigation;
            }

            profiles[i].getLoadBtn().navigation = current_UI_load;
            profiles[i].getRemoveBtn().navigation = current_UI_remove;
        }
    }

    public Transform getProfileHolder()
    {
        return _profileHolder;
    }

    public GameObject getProfilePrefab()
    {
        return _profilePrefab;
    }
}
