using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// profileList es una clase que se encarga de crear la lista de perfiles en la pantalla de carga de partida.
/// </summary>
public class profileList : MonoBehaviour
{
    /// <summary>
    /// La parte de la UI que contiene los perfiles.
    /// </summary>
    [SerializeField] private Transform _profileHolder;

    /// <summary>
    /// Un perfil sin instanciar.
    /// </summary>
    [SerializeField] private GameObject _profilePrefab;

    /// <summary>
    /// Referencia al botón de cancelar.
    /// </summary>
    [SerializeField] private Button _cancelButton;

    /// <summary>
    /// Lista que contiene los perfiles instanciados.
    /// </summary>
    private List<profile> _profiles = new List<profile>();

    /// <summary>
    /// Método que se ejecuta al iniciar el script.
    /// Recorre los nombres, instancia cada perfil con ellos y asigna distintas variables.
    /// Ver <see cref="profile"/>, <see cref="profileSystem"/>, <see cref="profileIndex"/>, <see cref="saveSystem"/> y <see cref="statSystem"/> para más información.
    /// </summary>
    private void Start()
    {
        EventSystem.current.firstSelectedGameObject = _cancelButton.gameObject;
        //Recorremos los nombres guardados
        foreach (string name in profileIndex.getUserNames())
        {
            //Instanciamos un perfil
            GameObject _object = Instantiate(_profilePrefab);
            profile UI = _object.GetComponent<profile>();
            _profiles.Add(UI);
            
            //Cambiamos de ruta para cargar los atributos de todas las rutas
            profileSystem.setCurrentPath(profileSystem.getPath() + name + "/");
            //Cargamos los atributos del perfil
            attributesData data = saveSystem.loadAttributes();

            //Si no se ha guardado nada asignamos valores default
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
            else //Si no, asignamos según corresponda
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

            //Añadimos el evento de click
            UI.getLoadBtn().onClick.AddListener(() =>
            {
                //Cambiamos variables que corresponden
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

            //Añadimos el evento de click
            UI.getRemoveBtn().onClick.AddListener(() => {
                profileIndex.removeName(UI.getName());
                //Cambiamos de ruta para eliminar la partida que toca
                profileSystem.setCurrentPath(profileSystem.getPath() + UI.getName() + "/");

                string[] files = Directory.GetFiles(profileSystem.getPath() + UI.getName() + "/");
                //Borramos todos los archivos de guardado del perfil
                foreach (string file in files)
                {
                    File.Delete(file);
                }

                //Borramos el perfil y destruimos el objeto
                Directory.Delete(profileSystem.getCurrentPath());
                Destroy(_object);

                Debug.Log(_profiles.Count);

                //Lo quitamos de la lista de perfiles instaciados
                _profiles.Remove(UI);

                EventSystem.current.SetSelectedGameObject(_cancelButton.gameObject);

                if (_profiles.Count > 0)
                {
                    EventSystem.current.SetSelectedGameObject(_profiles[0].getLoadBtn().gameObject);
                }

                calculateNavigation();

            });
            _object.transform.SetParent(_profileHolder, false);
        }

        calculateNavigation();

    }
    /// <summary>
    /// Método para asignar la navegación a cada uno de los botones de los perfiles.
    /// </summary>
    public void calculateNavigation()
    {
        Navigation cancel_navigation = _cancelButton.navigation;

        //Recorremos los perfiles
        for (int i = 0; i < _profiles.Count; i++)
        {
            Navigation current_UI_load = _profiles[i].getLoadBtn().navigation;
            Navigation current_UI_remove = _profiles[i].getRemoveBtn().navigation;

            if (i == 0)                                     //Estamos en el primero
            {
                EventSystem.current.firstSelectedGameObject = _profiles[i].getLoadBtn().gameObject;

                if (_profiles.Count > 1)
                {
                    current_UI_load.selectOnDown = _profiles[i + 1].getLoadBtn();
                    current_UI_remove.selectOnDown = _profiles[i + 1].getRemoveBtn();
                }
                else
                {
                    current_UI_load.selectOnDown = _cancelButton;
                    current_UI_remove.selectOnDown = _cancelButton;
                    cancel_navigation.selectOnUp = _profiles[i].getLoadBtn();
                    _cancelButton.navigation = cancel_navigation;
                }
            }
            else if (i > 0 && i < _profiles.Count - 1)      //Estamos en los del medio
            {
                current_UI_load.selectOnDown = _profiles[i + 1].getLoadBtn();
                current_UI_remove.selectOnDown = _profiles[i + 1].getRemoveBtn();

                current_UI_load.selectOnUp = _profiles[i - 1].getLoadBtn();
                current_UI_remove.selectOnUp = _profiles[i - 1].getRemoveBtn();
            }
            else                                            //Estamos en el ultimo
            {
                current_UI_load.selectOnDown = _cancelButton;
                current_UI_remove.selectOnDown = _cancelButton;

                current_UI_load.selectOnUp = _profiles[i - 1].getLoadBtn();
                current_UI_remove.selectOnUp = _profiles[i - 1].getRemoveBtn();

                cancel_navigation.selectOnUp = _profiles[_profiles.Count - 1].getLoadBtn();
                _cancelButton.navigation = cancel_navigation;
            }

            _profiles[i].getLoadBtn().navigation = current_UI_load;
            _profiles[i].getRemoveBtn().navigation = current_UI_remove;
        }
    }
}
