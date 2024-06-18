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
    /// El punto de la izquierda para el movimiento del enemigo.
    /// </summary>
    [SerializeField] private GameObject _pointA;

    /// <summary>
    /// El punto de la derecha para el movimiento del enemigo.
    /// </summary>
    [SerializeField] private GameObject _pointB;

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
    /// Flag booleano para saber cuando se puede dar la vuelta al llegar a <see cref="_pointA"/> o <see cref="_pointB"/>.
    /// </summary>
    [SerializeField] private bool _isFlipping;

    /// <summary>
    /// Flag booleano para controlar la muerte del enemigo.
    /// </summary>
    private bool _isDying = false;

    /// <summary>
    /// Referencia al clip de idle para calcular la velocidad.
    /// </summary>
    [SerializeField] private AnimationClip _idleAnim;

    /// <summary>
    /// Capa de la hurtbox a atacar.
    /// </summary>
    [SerializeField] private LayerMask _hurtboxLayer;
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

        config.addEnemy(gameObject);
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
    /// Getter que devuelve <see cref="baseEnemy.getIsArmored()"/>.
    /// </summary>
    /// <returns>Flag booleano que indica si un enemigo tiene o no armadura.</returns>
    public bool getIsArmored()
    {
        return _enemy.getIsArmored();
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
    /// Getter que devuelve un objeto nulo o <see cref="_pointA"/> dependiendo de si este existe.
    /// </summary>
    /// <returns>GameObject que contiene un objeto nulo o el punto izquierdo.</returns>
    public GameObject getPointA()
    {
        GameObject point = null;
        if (_pointA != null)
        {
            point = _pointA;
        }
        return point;
    }

    /// <summary>
    /// Getter que devuelve un objeto nulo o <see cref="_pointB"/> dependiendo de si este existe.
    /// </summary>
    /// <returns>GameObject que contiene un objeto nulo o el punto derecho.</returns>
    public GameObject getPointB()
    {
        GameObject point = null;
        if (_pointB != null)
        {
            point = _pointB;
        }
        return point;
    }

    /// <summary>
    /// Getter que devuelve <see cref="_isFlipping"/>.
    /// </summary>
    /// <returns>Booleano que indica si el enemigo está dándose la vuelta.</returns>
    public bool getIsFlipping()
    {
        return _isFlipping;
    }

    /// <summary>
    /// Getter que devuelve <see cref="_idleAnim"/>.
    /// </summary>
    /// <returns><see cref="_idleAnim"/>.</returns>
    public AnimationClip getIdleAnim()
    {
        return _idleAnim;
    }

    /// <summary>
    /// Getter que devuelve <see cref="_hurtboxLayer"/>.
    /// </summary>
    /// <returns><see cref="_hurtboxLayer"/>.</returns>
    public LayerMask getHurtboxLayer()
    {
        return _hurtboxLayer;
    }

    /// <summary>
    /// Método que voltea el sprite del enemigo.
    /// </summary>
    public void flip()
    {
        //Vector3 currentScale = gameObject.transform.localScale;
        //currentScale.x *= -1;
        //gameObject.transform.localScale = currentScale;
        _isLookingRight = !_isLookingRight;
        _isFlipping = false;
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

        float critProbability = config.getPlayer().GetComponent<combatController>().getCritProbability() + config.getPlayer().GetComponent<combatController>().getExtraCritProbability();
        int critValue = Random.Range(1, 101);
        float critDealt = 0;

        config.getPlayer().GetComponent<combatController>().calculateSkillCritDamageProbability(ref critProbability);

        //Debug.Log(critProbability);
        if ((float)critValue <= critProbability)
        {
            critDealt = critDamage;
        }

        received = (dmg + (dmg * critDealt)) - (armor);

        int bleedValue = Random.Range(1, 101);
        float bleedProbability = config.getPlayer().GetComponent<combatController>().getBleedProbability() + config.getPlayer().GetComponent<combatController>().getExtraBleedingProbability();

        config.getPlayer().GetComponent<combatController>().calculateSkillBleedingProbability(ref bleedProbability);
        if ((float)bleedValue <= bleedProbability)
        {
            received += (_enemy.getHealth() * bleedingDamage);
        }

        //Debug.Log(received);
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
        if (damageDealt >= 0) //puede pasar que la armadura del enemigo sea mayor a nuestro daño y hagamos daño negativo y le curemos
        {
            _health -= damageDealt;

            if (config.getPlayer().GetComponent<combatController>().getSecundaryWeapon() != null &&
                config.getPlayer().GetComponent<combatController>().getSecundaryWeapon().GetComponent<weapon>().getIsAttacking())
            {
                if (config.getPlayer().GetComponent<combatController>().getLifeSteal() != 0)
                {
                    config.getPlayer().GetComponent<statsController>().healHP(damageDealt * config.getPlayer().GetComponent<combatController>().getLifeSteal());
                }
            }
        }
        animatorEnum direction = _isLookingRight ? animatorEnum.back : animatorEnum.front;
        if (_health <= 0 && !_isDying && GetComponent<enemyController>().enabled)
        {
            _isDying = true;
            GetComponent<enemyController>().enabled = false;
            GetComponent<enemyStateMachine>().enabled = false;
            GetComponent<enemyAnimatorController>().playAnimation(animatorEnum.enemy_death, getEnemyName(), direction);
        }
        
    }

    /// <summary>
    /// Método que maneja la lógica de muerte del enemigo.
    /// </summary>
    public void die()
    {
        Debug.Log("activo?");
        //Si tiene loot
        if (_loot.Length > 0)
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
        long soulsToGive = _souls;
        float soulModifier = 0f;
        List<GameObject> equippedSkills = config.getPlayer().GetComponent<skillManager>().getEquippedSkills();
        for (int i = 0; i < equippedSkills.Count; i++)
        {
            if (equippedSkills[i] != null && equippedSkills[i].GetComponent<skill>().getType() == skillTypeEnum.souls)
            {
                soulModifier += equippedSkills[i].GetComponent<skill>().getSkillValues()[skillValuesEnum.soulIncrease];
            }
        }
        config.getPlayer().GetComponent<combatController>().receiveSouls(soulsToGive + (int)(soulsToGive * soulModifier));

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

    private void OnDrawGizmos()
    {
        if (_pointA)
        {
            Gizmos.DrawWireSphere(_pointA.transform.position, 0.5f);
        }

        if (_pointB)
        {
            Gizmos.DrawWireSphere(_pointB.transform.position, 0.5f);
        }
    }
    /// <summary>
    /// Método auxiliar para dar la vuelta al enemigo en un tiempo random.
    /// </summary>
    /// <param name="time">El tiempo necesario para dar la vuelta.</param>
    public void flipInTime(float time)
    {
        _isFlipping = true;
        Invoke("flip", time);
    }

    /// <summary>
    /// Método que se ejecuta cada frame para actualizar la lógica.
    /// </summary>
    private void Update()
    {
        animatorEnum direction = _isLookingRight ? animatorEnum.back : animatorEnum.front;
        AnimatorStateInfo stateInfo = GetComponent<Animator>().GetCurrentAnimatorStateInfo(0);
        //Debug.Log(_health);

        if (stateInfo.IsName(GetComponent<enemyAnimatorController>().getAnimationName(animatorEnum.enemy_death, getEnemyName(), direction)))
        {
            if (stateInfo.normalizedTime >= 1.0f)
            {
                die();
            }
        }
    }
}
