using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// baseEnemy es una clase que representa internamente al enemigo.
/// </summary>
[CreateAssetMenu (fileName = "New enemy", menuName = "Enemies/Create new enemy")]
public class baseEnemy : ScriptableObject
{
    /// <summary>
    /// El nombre del enemigo.
    /// </summary>
    [SerializeField] private string _enemyName;

    /// <summary>
    /// La descripción del enemigo.
    /// </summary>
    [TextArea]
    [SerializeField] private string _enemyDesc;

    /// <summary>
    /// El sprite del enemigo.
    /// </summary>
    [SerializeField] private Sprite _enemySprite;

    /// <summary>
    /// La vida del enemigo.
    /// </summary>
    [SerializeField] private float _health;

    /// <summary>
    /// El daño del enemigo.
    /// </summary>
    [SerializeField] private float _damage;

    /// <summary>
    /// La armadura del enemigo.
    /// </summary>
    [SerializeField] private float _armor;

    /// <summary>
    /// La velocidad del enemigo.
    /// </summary>
    [SerializeField] private int _speed;

    /// <summary>
    /// El rango de detección del enemigo.
    /// </summary>
    [SerializeField] private float _detectionRange;

    /// <summary>
    /// El rango de ataque del enemigo.
    /// </summary>
    [SerializeField] private float _attackRange;

    /// <summary>
    /// Las almas que proporciona el enemigo.
    /// </summary>
    [SerializeField] private long _souls;

    /// <summary>
    /// El loot que proporciona el enemigo.
    /// </summary>
    [SerializeField] private lootItem[] _loot;

    /// <summary>
    /// La probabilidad de que el enemigo suelte el loot.
    /// </summary>
    [SerializeField] private float _dropRate;

    /// <summary>
    /// Los tiempos de ataque del enemigo.
    /// </summary>
    [SerializeField] private List<float> _attacksTimes = new List<float>();


    /// <summary>
    /// Getter que devuelve <see cref="_enemyName"/>.
    /// </summary>
    /// <returns>Un string que contiene el nombre del enemigo.</returns>
    public string getName()
    {
        return _enemyName;
    }

    /// <summary>
    /// Getter que devuelve <see cref="_enemyDesc"/>.
    /// </summary>
    /// <returns>Un string que contiene la descripción del enemigo.</returns>
    public string getDesc()
    {
        return _enemyDesc;
    }

    /// <summary>
    /// Getter que devuelve <see cref="_enemySprite"/>.
    /// </summary>
    /// <returns>El sprite asociado al enemigo.</returns>
    public Sprite getSprite()
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
    public float getDMG()
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
    /// <returns>Un long que representa la cantidad de almas del enemigo.</returns>
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
    /// <returns>Una lista de enteros que representa los tiempos de ataque del enemigo.</returns>
    public List<float> getTimes()
    {
        return _attacksTimes;
    }

}
