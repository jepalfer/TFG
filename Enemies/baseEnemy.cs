using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu (fileName = "New enemy", menuName = "Enemies/Create new enemy")]
public class baseEnemy : ScriptableObject
{
    [SerializeField] private string _enemyName;
    [SerializeField] private string _enemyDesc;
    [SerializeField] private Sprite _enemySprite;
    [SerializeField] private float _health;
    [SerializeField] private float _damage;
    [SerializeField] private float _armor;
    [SerializeField] private int _speed;
    [SerializeField] private float _detectionRange;
    [SerializeField] private float _attackRange;
    [SerializeField] private long _souls;
    [SerializeField] private lootItem[] _loot;
    [SerializeField] private float _dropRate;
    [SerializeField] private List<float> _attacksTimes = new List<float>();

    public string getName()
    {
        return _enemyName;
    }
    public string getDesc()
    {
        return _enemyDesc;
    }
    public Sprite getSprite()
    {
        return _enemySprite;
    }
    public float getHealth()
    {
        return _health;
    }
    public float getDMG()
    {
        return _damage;
    }
    public float getArmor()
    {
        return _armor;
    }
    public int getSpeed()
    {
        return _speed;
    }
    public float getDetectionRange()
    {
        return _detectionRange;
    }
    public float getAttackRange()
    {
        return _attackRange;
    }
    public long getSouls()
    {
        return _souls;
    }
    public lootItem[] getLoot()
    {
        return _loot;
    }
    public float getDropRate()
    {
        return _dropRate;
    }

    public List<float> getTimes()
    {
        return _attacksTimes;
    }
}
