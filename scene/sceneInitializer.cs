using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneInitializer : MonoBehaviour
{
    [SerializeField] private GameObject _UIPrefab;
    [SerializeField] private GameObject _playerPrefab;
    [SerializeField] private GameObject _inputControllerPrefab;
    [SerializeField] private GameObject _eventSystemPrefab;
    [SerializeField] private GameObject _inventoryPrefab;
    // Start is called before the first frame update
    private void Awake()
    {
        Instantiate(_eventSystemPrefab);
        Instantiate(_inventoryPrefab);
        Instantiate(_inputControllerPrefab);
        Instantiate(_playerPrefab);
        Instantiate(_UIPrefab);
    }

    private void Start()
    {
        playerData data = saveSystem.loadPlayer();

        if (data != null) //Hemos cargado partida
        {
            if (saveSystem.loadLastSceneData().getSceneID() == SceneManager.GetActiveScene().buildIndex)
            {
                Debug.Log("hola2");
                Vector3 pos = new Vector3(data.getX(), data.getY(), data.getZ());
                config.getPlayer().GetComponent<playerMovement>().setFacingRight(data.getIsFacingRight());

                if (!config.getPlayer().GetComponent<playerMovement>().getIsFacingRight())
                {
                    Vector3 currentScale = config.getPlayer().gameObject.transform.localScale;
                    currentScale.x *= -1;
                    config.getPlayer().gameObject.transform.localScale = currentScale;
                }

                config.getPlayer().transform.position = pos;
            }
            else
            {
                if (saveSystem.loadLastSceneData().getSceneID() == GetComponent<sceneSwitch>().getTopID())
                {
                    List<float> coordinates = GetComponent<sceneSwitch>().getTopCoordinates();

                    Vector3 pos = new Vector3(coordinates[0], coordinates[1], coordinates[2]);
                    config.getPlayer().GetComponent<playerMovement>().setFacingRight(true);
                    config.getPlayer().transform.position = pos;
                }
                else if (saveSystem.loadLastSceneData().getSceneID() == GetComponent<sceneSwitch>().getRightID())
                {
                    List<float> coordinates = GetComponent<sceneSwitch>().getRightCoordinates();

                    Vector3 pos = new Vector3(coordinates[0], coordinates[1], coordinates[2]);
                    config.getPlayer().GetComponent<playerMovement>().setFacingRight(false);

                    Vector3 currentScale = config.getPlayer().gameObject.transform.localScale;
                    currentScale.x *= -1;
                    config.getPlayer().gameObject.transform.localScale = currentScale;

                    config.getPlayer().transform.position = pos;
                }
                else if (saveSystem.loadLastSceneData().getSceneID() == GetComponent<sceneSwitch>().getBottomID())
                {
                    List<float> coordinates = GetComponent<sceneSwitch>().getBottomCoordinates();

                    Vector3 pos = new Vector3(coordinates[0], coordinates[1], coordinates[2]);
                    config.getPlayer().GetComponent<playerMovement>().setFacingRight(true);
                    config.getPlayer().transform.position = pos;
                }
                else
                {
                    List<float> coordinates = GetComponent<sceneSwitch>().getLeftCoordinates();

                    Vector3 pos = new Vector3(coordinates[0], coordinates[1], coordinates[2]);
                    config.getPlayer().GetComponent<playerMovement>().setFacingRight(true);
                    config.getPlayer().transform.position = pos;
                }
            }
        }
        else //Hemos empezado partida
        {
            if (GetComponent<sceneSwitch>().getSpawnPoint() != null)
            {
                Vector3 spawnPos = new Vector3(GetComponent<sceneSwitch>().getSpawnPoint()[0], GetComponent<sceneSwitch>().getSpawnPoint()[1], GetComponent<sceneSwitch>().getSpawnPoint()[2]);
                config.getPlayer().transform.position = spawnPos;
                saveSystem.saveLastScene();
            }
        }
        saveSystem.saveLastScene();
        saveSystem.savePlayer();
    }
}
