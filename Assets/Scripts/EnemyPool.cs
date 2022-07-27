using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : ObjectPool
{
    protected List<EnemyBehavior> enemies = new List<EnemyBehavior>();



    protected override void Awake()
    {
        base.Awake();

        foreach (GameObject obj in pooledObjects) enemies.Add(obj.GetComponent<EnemyBehavior>());
    }

    public new EnemyBehavior GetNext()
    {
        foreach (EnemyBehavior enemy in enemies)
        {
            if (!enemy.gameObject.activeSelf) return enemy;
        }

        if (dynamicSize)
        {
            EnemyBehavior newEnemy = null;
            if (poolAsChild) 
            {
                newEnemy = Instantiate(prefab, transform).GetComponent<EnemyBehavior>();
            }
            else 
            {
                newEnemy = Instantiate(prefab).GetComponent<EnemyBehavior>();
            }
            
            enemies.Add(newEnemy.GetComponent<EnemyBehavior>());
            return newEnemy;
        }

        return null;
    }

    public new List<EnemyBehavior> GetAll()
    {
        return enemies;
    }
}