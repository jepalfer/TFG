using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class boss : enemy
{
    private void Start()
    {
        enemyStateData data = saveSystem.loadEnemyData();

        if (data != null)
        {
            if (data.getEnemyStates().Find(enemy => enemy.getSceneID() == SceneManager.GetActiveScene().buildIndex && enemy.getEnemyID() == getEnemyID()).getIsAlive() == 0)
            {
                gameObject.SetActive(false);
            }
        }
    }
    public override void receiveDMG(float dmg, bool isCrit, bool piercesArmor)
    {
        GetComponent<bossUIController>().recalculateHPBar(dmg);
        base.receiveDMG(dmg, isCrit, piercesArmor);
    }

    /*
    public override string getEnemyName()
    {
        return _enemyName;
    }
    public override string getEnemyDesc()
    {
        return _enemyDesc;
    }
    public override Sprite getEnemySprite()
    {
        return _enemySprite;
    }
    public override float getHealth()
    {
        return _health;
    }
    public override float getDamage()
    {
        return _damage;
    }
    public override float getArmor()
    {
        return _armor;
    }
    public override int getSpeed()
    {
        return _speed;
    }
    public override float getDetectionRange()
    {
        return _detectionRange;
    }
    public override float getAttackRange()
    {
        return _attackRange;
    }
    public override long getSouls()
    {
        return _souls;
    }
    public override lootItem[] getLoot()
    {
        return _loot;
    }
    public override float getDropRate()
    {
        return _dropRate;
    }*/

    
}
