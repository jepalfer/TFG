using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// enemy es una clase que representa el enemigo instanciado.
/// </summary>
public class enemy : MonoBehaviour
{
    /// <summary>
    /// Los datos internos del enemigo.
    /// </summary>
    [SerializeField] protected baseEnemy _enemy;

    /// <summary>
    /// El ID interno del enemigo.
    /// </summary>
    [SerializeField] protected int _enemyID;

    /// <summary>
    /// El nombre del enemigo.
    /// </summary>
    protected string _enemyName;

    /// <summary>
    /// La descripción del enemigo.
    /// </summary>
    protected string _enemyDesc;

    /// <summary>
    /// El sprite del enemigo.
    /// </summary>
    protected Sprite _enemySprite;

    /// <summary>
    /// La vida del enemigo.
    /// </summary>
    protected float _health;

    /// <summary>
    /// El daño del enemigo.
    /// </summary>
    protected float _damage;

    /// <summary>
    /// La armadura del enemigo.
    /// </summary>
    protected float _armor;

    /// <summary>
    /// La velocidad del enemigo.
    /// </summary>
    protected int _speed;

    /// <summary>
    /// El rango de detección del enemigo.
    /// </summary>
    [SerializeField] protected float _detectionRange;

    /// <summary>
    /// El rango de ataque del enemigo.
    /// </summary>
    [SerializeField] protected float _attackRange;

    /// <summary>
    /// Las almas del enemigo.
    /// </summary>
    protected long _souls;

    /// <summary>
    /// El loot del enemigo.
    /// </summary>
    [SerializeField] protected lootItem[] _loot;

    /// <summary>
    /// Los tiempos de cada ataque del enemigo.
    /// </summary>
    [SerializeField] protected List<float> _attacksTimes;

    /// <summary>
    /// La probabilidad de soltar <see cref="_loot"/> del enemigo.
    /// </summary>
    [SerializeField] protected float _dropRate;

    /// <summary>
    /// La armadura máxima del enemigo.
    /// </summary>
    protected float _maxArmor = 500;

    /// <summary>
    /// Referencia a la hitbox.
    /// </summary>
    [SerializeField] private GameObject _hitbox;

    /// <summary>
    /// Referencia a la hurtbox.
    /// </summary>
    [SerializeField] private GameObject _hurtbox;

    /// <summary>
    /// Si el enemigo está o no mirando hacia la derecha.
    /// </summary>
    [SerializeField] private bool _isLookingRight;

    /// <summary>
    /// Método que se ejecuta al iniciar el script y que asigna todas las variables y carga la información acerca de los enemigos.
    /// </summary>
    private void Awake()
    {
        //Asignación de variables
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

        //Manejo de si es boss o no
        int isBoss = 0;

        if (GetComponent<boss>() != null)
        {
            isBoss = 1;
        }
        //Creamos un enemigo
        sceneEnemiesState _enemyData = new sceneEnemiesState(SceneManager.GetActiveScene().buildIndex, _enemyID, 1, isBoss, 0);
        //Prueba.addEnemyState(SceneManager.GetActiveScene().buildIndex, _enemyID, 1, isBoss, 0);

        //Cargamos los datos
        enemyStateData _data = saveSystem.loadEnemyData();

        //Si hay datos guardados
        if (_data != null)
        {
            //Si el enemigo no se encontraba (primera vez que entramos a la escena) lo guardamos.
            if (_data.getEnemyStates().Find(item => item.getSceneID() == _enemyData.getSceneID() && item.getEnemyID() == _enemyData.getEnemyID()) == null)
            {
                _data.incrementSize(_enemyData);
                saveSystem.saveEnemyData(_data.getEnemyStates());
            }
        }
        else //Si es la primera escena con enemigos
        {
            //Guardamos los datos de este enemigo en la lista
            enemyStateData state = new enemyStateData();
            state.incrementSize(_enemyData);
            saveSystem.saveEnemyData(state.getEnemyStates());
        }
    }

    /// <summary>
    /// Getter que devuelve <see cref="_enemyID"/>.
    /// </summary>
    /// <returns>Un int que representa el ID del enemigo.</returns>
    public int getEnemyID()
    {
        return _enemyID;
    }

    /// <summary>
    /// Getter que devuelve <see cref="_enemyName"/>.
    /// </summary>
    /// <returns>Un string que contiene el nombre del enemigo.</returns>
    public string getEnemyName()
    {
        return _enemyName;
    }
    /// <summary>
    /// Getter que devuelve <see cref="_enemyDesc"/>.
    /// </summary>
    /// <returns>Un string que contiene la descripción del enemigo.</returns>
    public string getEnemyDesc()
    {
        return _enemyDesc;
    }
    /// <summary>
    /// Getter que devuelve <see cref="_enemySprite"/>.
    /// </summary>
    /// <returns>El sprite del enemigo.</returns>
    public Sprite getEnemySprite()
    {
        return _enemySprite;
    }
    /// <summary>
    /// Getter que devuelve <see cref="_health"/>.
    /// </summary>
    /// <returns>Un float que representa la vida del enemigo.</returns>
    public float getHealth()
    {
        return _health;
    }
    /// <summary>
    /// Getter que devuelve <see cref="_damage"/>.
    /// </summary>
    /// <returns>Un float que representa el daño del enemigo.</returns>
    public float getDamage()
    {
        return _damage;
    }
    /// <summary>
    /// Getter que devuelve <see cref="_armor"/>.
    /// </summary>
    /// <returns>Un float que representa la armadura del enemigo.</returns>
    public float getArmor()
    {
        return _armor;
    }
    /// <summary>
    /// Getter que devuelve <see cref="_speed"/>.
    /// </summary>
    /// <returns>Un int que representa la velocidad del enemigo.</returns>
    public int getSpeed()
    {
        return _speed;
    }
    /// <summary>
    /// Getter que devuelve <see cref="_detectionRange"/>.
    /// </summary>
    /// <returns>Un float que representa el rango de detección del enemigo.</returns>
    public float getDetectionRange()
    {
        return _detectionRange;
    }
    /// <summary>
    /// Getter que devuelve <see cref="_attackRange"/>.
    /// </summary>
    /// <returns>Un float que representa el rango de ataque del enemigo.</returns>
    public float getAttackRange()
    {
        return _attackRange;
    }
    /// <summary>
    /// Getter que devuelve <see cref="_souls"/>.
    /// </summary>
    /// <returns>Un long que representa las almas del enemigo.</returns>
    public long getSouls()
    {
        return _souls;
    }
    /// <summary>
    /// Getter que devuelve <see cref="_loot"/>.
    /// </summary>
    /// <returns>Un array de <see cref="lootItem"/> que representa el loot del enemigo.</returns>
    public lootItem[] getLoot()
    {
        return _loot;
    }
    /// <summary>
    /// Getter que devuelve <see cref="_dropRate"/>.
    /// </summary>
    /// <returns>Un float que representa la probabilidad de soltar el loot del enemigo.</returns>
    public float getDropRate()
    {
        return _dropRate;
    }
    /// <summary>
    /// Getter que devuelve <see cref="_attacksTimes"/>.
    /// </summary>
    /// <returns>Una lista de enteros que representa los tiempos de ataques del enemigo.</returns>

    public List<float> getTimes()
    {
        return _attacksTimes;
    }
    /// <summary>
    /// Getter que devuelve <see cref="_hitbox"/>.
    /// </summary>
    /// <returns>Un GameObject que representa la hitbox del enemigo.</returns>

    public GameObject getHitbox()
    {
        return _hitbox;
    }
    /// <summary>
    /// Getter que devuelve <see cref="_hurtbox"/>.
    /// </summary>
    /// <returns>Un GameObject que representa la hurtbox del enemigo.</returns>
    public GameObject getHurtbox()
    {
        return _hurtbox;
    }
    /// <summary>
    /// Getter que devuelve <see cref="_isLookingRight"/>.
    /// </summary>
    /// <returns>Un booleano que representa hacia qué lado está mirando el enemigo.</returns>
    public bool getIsLookingRight()
    {
        return _isLookingRight;
    }

    /// <summary>
    /// Getter que devuelve <see cref="_enemy"/>.
    /// </summary>
    /// <returns>Devuelve un objeto de tipo <see cref="baseEnemy"/> que contiene la información del enemigo base.</returns>
    public baseEnemy getEnemyData()
    {
        return _enemy;
    }
    
    /// <summary>
    /// Método que voltea el sprite del enemigo.
    /// </summary>
    public void flip()
    {
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;
        _isLookingRight = !_isLookingRight;
    }

    /// <summary>
    /// Método que calcula el daño que se le inflige al enemigo.
    /// </summary>
    /// <param name="dmg">El daño plano entrante.</param>
    /// <param name="critDamage">El daño crítico.</param>
    /// <param name="penetrationDamage">La armadura que eliminamos.</param>
    /// <param name="bleedingDamage">El daño por sangrado.</param>
    /// <returns></returns>
    public float calculateDMG(float dmg, float critDamage, float penetrationDamage, float bleedingDamage)
    {
        float received;
        float armor = _armor;

        //Disminuimos armadura si es penetrante
        penetrationDamage /= 100;
        bleedingDamage /= 100;
        critDamage /= 100;
        armor -= armor * (penetrationDamage);

        float critProbability = config.getPlayer().GetComponent<combatController>().getCritProbability();
        int critValue = Random.Range(1, 101);
        float critDealt = 0;
        config.getPlayer().GetComponent<combatController>().calculateExtraCritDamageProbability(ref critProbability);

        Debug.Log(critProbability);
        if ((float)critValue <= critProbability)
        {
            critDealt = critDamage;
        }

        received = (dmg + (dmg * critDealt)) - (armor);

        int bleedValue = Random.Range(1, 101);
        float bleedProbability = config.getPlayer().GetComponent<combatController>().getBleedProbability();

        Debug.Log(bleedProbability);
        config.getPlayer().GetComponent<combatController>().calculateExtraBleedingProbability(ref bleedProbability);
        if ((float)bleedValue <= bleedProbability)
        {
            received += (_enemy.getHealth() * bleedingDamage);
        }

        Debug.Log(received);
        return received;
    }

    /// <summary>
    /// Método que inflige el daño al enemigo.
    /// </summary>
    /// <param name="dmg">Daño plano entrante.</param>
    /// <param name="critDamage">El daño crítico.</param>
    /// <param name="penetrationDamage">La armadura que eliminamos.</param>
    /// <param name="bleedingDamage">El daño por sangrado.</param>
    public virtual void receiveDMG(float dmg, float critDamage, float penetrationDamage, float bleedingDamage)
    {
        int damageDealt = (int)calculateDMG(dmg, critDamage, penetrationDamage, bleedingDamage);
        _health -= damageDealt;

        if (config.getPlayer().GetComponent<combatController>().getSecundaryWeapon() != null && 
            config.getPlayer().GetComponent<combatController>().getSecundaryWeapon().GetComponent<weapon>().getIsAttacking())
        {
            if (config.getPlayer().GetComponent<combatController>().getLifeSteal() != 0)
            {
                config.getPlayer().GetComponent<statsController>().healHP(damageDealt * config.getPlayer().GetComponent<combatController>().getLifeSteal());
            }
        }

        if (_health <= 0)
        {
            die();
        }
    }

    /// <summary>
    /// Método que maneja la lógica de muerte del enemigo.
    /// </summary>
    public void die()
    {
        //Si tiene loot
        if (_loot.Length > 0 && _dropRate > 0f)
        {
            //Pasamos _dropRate que está en un rango [0, 1] a un valor entre [1, 100] para cuando usemos Random.Range comprobemos si
            //el valor 
            int upperBound = (int)(_dropRate * 100);
            int value = Random.Range(1, 101);
            if (value <= upperBound)
            {
                config.getPlayer().GetComponent<combatController>().receiveLoot(_loot);
            }
        }
        //Le damos las almas al jugador
        config.getPlayer().GetComponent<combatController>().receiveSouls(_souls);

        //Guardamos la información del enemigo y lo desactivamos
        enemyStateData _data = saveSystem.loadEnemyData();
        _data.modifyEnemyState(SceneManager.GetActiveScene().buildIndex, getEnemyID(), 0, 0);
        saveSystem.saveEnemyData(_data.getEnemyStates());

        gameObject.SetActive(false);
    }

    /// <summary>
    /// Setter que asigna un valor a <see cref="_enemyID"/>.
    /// </summary>
    /// <param name="id">El valor que se le asigna.</param>
    public void setID(int id)
    {
        _enemyID = id;
    }

}
