using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class reviveEnemy : MonoBehaviour
{
    [SerializeField] private GameObject _enemy;
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private float[] _coordinates = new float[3];
    [SerializeField] private int _ID;

    private void Start()
    {
        _ID = _enemy.GetComponent<enemy>().getEnemyID();
        enemyStateData _data = saveSystem.loadEnemyData();

        sceneEnemiesState foundEnemy = _data.getEnemyStates().Find(enemy => enemy.getSceneID() == SceneManager.GetActiveScene().buildIndex && enemy.getEnemyID() == _ID);

        if (foundEnemy != null)
        {
            int index = _data.getEnemyStates().IndexOf(foundEnemy);
            if (foundEnemy.getIsAlive() == 0)
            {
                if (foundEnemy.getCanRevive() == 0)
                {
                    _enemy.SetActive(false);
                }
                else
                {
                    foundEnemy.setCanRevive(0);
                    foundEnemy.setIsAlive(1);

                    _data.getEnemyStates()[index] = foundEnemy;

                    saveSystem.saveEnemyData(_data.getEnemyStates());
                    _enemy.SetActive(true);
                }
            }
            else
            {
                _enemy.SetActive(true);
            }
        }
    }

    private GameObject getPrefab()
    {
        return _enemyPrefab;
    }

    private GameObject getEnemy()
    {
        return _enemy;
    }

    private void setCoordinates(float[] coords)
    {
        _coordinates[0] = coords[0];
        _coordinates[1] = coords[1];
        _coordinates[2] = coords[2];

        _enemy.transform.position = new Vector3(_coordinates[0], _coordinates[1], _coordinates[2]);
    }


    /*
    public void revive()
    {
        enemyStateData _data = SaveSystem.loadEnemyData();
        if (_data.getEnemyStates().Find(enemy => enemy.getSceneID() == SceneManager.GetActiveScene().buildIndex && enemy.getEnemyID() == _enemy.GetComponent<Enemy>().getEnemyID() && enemy.getIsAlive() == 0 && enemy.getCanRevive() == 1) != null)
        {
            GameObject _object = Instantiate(_enemyPrefab);
            Destroy(gameObject);
            _object.GetComponent<reviveEnemy>().setCoordinates(_coordinates);
            _object.GetComponent<reviveEnemy>().getEnemy().GetComponent<Enemy>().setID(_ID);
            _data.getEnemyStates().Find(enemy => enemy.getSceneID() == SceneManager.GetActiveScene().buildIndex && enemy.getEnemyID() == _object.GetComponent<reviveEnemy>().getEnemy().GetComponent<Enemy>().getEnemyID()).setIsAlive(1);
            _object.GetComponent<reviveEnemy>().getEnemy().SetActive(true);

            SaveSystem.saveEnemyData(_data.getEnemyStates());
        }
    }

    public void enterScene()
    {
        enemyStateData _data = SaveSystem.loadEnemyData();
        List<sceneEnemiesState> _sceneData = _data.getEnemyStates().FindAll(enemy => enemy.getSceneID() == SceneManager.GetActiveScene().buildIndex);

        for (int i = 0; i < _sceneData.Count; i++)
        {
            _sceneData[i].setCanRevive(0);
        }
        SaveSystem.saveEnemyData(_sceneData);
    }
    */
}
