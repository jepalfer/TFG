using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class menu : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Button _newGameButton;
    [SerializeField] private Button _continueButton;
    [SerializeField] private Button _loadButton;

    private void Awake()
    {
        /*Cursor.visible = false;

        Cursor.lockState = CursorLockMode.Locked;*/
        profileData data = saveSystem.loadProfiles();

        if (data == null)
        {

        }
        else
        {
            profileIndex.setNames(data.getUserNames());

            if (profileIndex.getUserNames().Count > 0)
            {
                lastPath path = saveSystem.loadPath();

                if (Directory.Exists(path.getPath()))
                {
                    profileSystem.setProfileName(path.getName());
                    profileSystem.setCurrentPath(path.getPath());
                    _continueButton.interactable = true;
                }
                else
                {
                    _continueButton.interactable = false;
                }
            }
            else
            {
                _continueButton.interactable = false;
            }
        }
        Navigation newGameNavigation = _newGameButton.navigation;
        Navigation continueNavigation = _continueButton.navigation;
        Navigation loadGameNavigation = _loadButton.navigation;
        if (_continueButton.interactable)
        {
            newGameNavigation.selectOnDown = _continueButton;

            continueNavigation.selectOnUp = _newGameButton;
            continueNavigation.selectOnDown = _loadButton;

            loadGameNavigation.selectOnUp = _continueButton;
        }
        else
        {

            newGameNavigation.selectOnDown = _loadButton;

            loadGameNavigation.selectOnUp = _newGameButton;
        }

        _newGameButton.navigation = newGameNavigation;
        _continueButton.navigation = continueNavigation;
        _loadButton.navigation = loadGameNavigation;
    }

    public void createGame()
    {
        SceneManager.LoadScene("newGame");
    }

    public void loadGame()
    {
        SceneManager.LoadScene("loadGame");
    }

    public void playGame()
    {
        SceneManager.LoadScene("ToL");
    }

    public void quitGame()
    {
        Application.Quit();
    }

    public void continueGame()
    {
        lastSceneData data = saveSystem.loadLastSceneData();
        SceneManager.LoadScene(data.getSceneID());
    }

    public Button getContinueButton()
    {
        return _continueButton;
    }

}
