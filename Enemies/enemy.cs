using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class enemy : MonoBehaviour
{
    [SerializeField] protected baseEnemy _enemy;
    [SerializeField] private int _enemyID;
    protected string _enemyName;
    protected string _enemyDesc;
    protected Sprite _enemySprite;
    protected float _health;
    protected float _damage;
    protected float _armor;
    protected int _speed;
    [SerializeField] protected float _detectionRange;
    [SerializeField] protected float _attackRange;
    protected long _souls;
    [SerializeField] protected lootItem[] _loot;
    [SerializeField] protected List<float> _attacksTimes;
    [SerializeField] protected float _dropRate;
    protected float _maxArmor = 500;

    [SerializeField] private GameObject _hitbox;
    [SerializeField] private GameObject _hurtbox;
    [SerializeField] private bool _isLookingRight;
    // Start is called before the first frame update

    private void Awake()
    {
        _enemyName = _enemy.getName();
        _enemyDesc = _enemy.getDesc();
        _enemySprite = _enemy.getSprite();
        _health = _enemy.getHealth();
        _damage = _enemy.getDMG();
        _armor = _enemy.getArmor();
        _speed = _enemy.getSpeed();
        _attacksTimes = _enemy.getTimes();
        _detectionRange = _enemy.getDetectionRange();
        _attackRange = _enemy.getAttackRange();
        _souls = _enemy.getSouls();
        _loot = _enemy.getLoot();
        _dropRate = _enemy.getDropRate();
        _isLookingRight = true;

        int isBoss = 0;

        if (GetComponent<boss>() != null)
        {
            isBoss = 1;
        }
        sceneEnemiesState _enemyData = new sceneEnemiesState(SceneManager.GetActiveScene().buildIndex, _enemyID, 1, isBoss, 0);
        //Prueba.addEnemyState(SceneManager.GetActiveScene().buildIndex, _enemyID, 1, isBoss, 0);

        enemyStateData _data = saveSystem.loadEnemyData();

        if (_data != null)
        {
            if (_data.getEnemyStates().Find(item => item.getSceneID() == _enemyData.getSceneID() && item.getEnemyID() == _enemyData.getEnemyID()) == null)
            {
                _data.incrementSize(_enemyData);
                saveSystem.saveEnemyData(_data.getEnemyStates());
            }
        }
        else
        {
            enemyStateData state = new enemyStateData();
            state.incrementSize(_enemyData);
            saveSystem.saveEnemyData(state.getEnemyStates());
        }
    }

    public int getEnemyID()
    {
        return _enemyID;
    }

    public  string getEnemyName()
    {
        return _enemyName;
    }
    public  string getEnemyDesc()
    {
        return _enemyDesc;
    }
    public  Sprite getEnemySprite()
    {
        return _enemySprite;
    }
    public  float getHealth()
    {
        return _health;
    }
    public  float getDamage()
    {
        return _damage;
    }
    public  float getArmor()
    {
        return _armor;
    }
    public  int getSpeed()
    {
        return _speed;
    }
    public  float getDetectionRange()
    {
        return _detectionRange;
    }
    public  float getAttackRange()
    {
        return _attackRange;
    }
    public  long getSouls()
    {
        return _souls;
    }
    public  lootItem[] getLoot()
    {
        return _loot;
    }
    public  float getDropRate()
    {
        return _dropRate;
    }

    public List<float> getTimes()
    {
        return _attacksTimes;
    }

    public GameObject getHitbox()
    {
        return _hitbox;
    }
    public GameObject getHurtbox()
    {
        return _hurtbox;
    }
    public bool getIsLookingRight()
    {
        return _isLookingRight;
    }
    

    public void flip()
    {
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;
        _isLookingRight = !_isLookingRight;
    }

    public float calculateDMG(float dmg, bool isCrit, bool piercesArmor)
    {
        float received = 0;
        float armor = _armor;

        if (piercesArmor) //Con armas penetrantes (gladius, khopesh)
        {
            armor *= 0.8f;
        }

        if (isCrit)             //Con daga por detras
        {
            received = dmg * (2.0f - armor / _maxArmor);
        }
        else
        {
            received = dmg * (1.0f - armor / _maxArmor);
        }

        return received;
    }

    public virtual void receiveDMG(float dmg, bool isCrit, bool piercesArmor)
    {
        _health -= calculateDMG(dmg, isCrit, piercesArmor);
        if (_health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        if (_loot.Length > 1)
        {
            int _upperBound = (int)(1f / _dropRate);
            int _value = Random.Range(0, _upperBound);
            if (_value == 0)
            {
                config.getPlayer().GetComponent<combatController>().receiveLoot(_loot);
            }
        }

        config.getPlayer().GetComponent<combatController>().receiveSouls(_souls);

        enemyStateData _data = saveSystem.loadEnemyData();
        _data.modifyEnemyState(SceneManager.GetActiveScene().buildIndex, getEnemyID(), 0, 0);
        saveSystem.saveEnemyData(_data.getEnemyStates());

        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setID(int id)
    {
        _enemyID = id;
    }

}
