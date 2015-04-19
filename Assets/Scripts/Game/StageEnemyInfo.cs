using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public class StageEnemyInfo
{
    #region STATIC_ENUM_CONSTANTS
    #endregion

    #region FIELDS
    public GameObject prefabEnemy;
    public float initPercEnemy;
    public bool increasePercInTime;

    private Queue<GameObject> cache = new Queue<GameObject>();
    #endregion

    #region ACCESSORS
    #endregion

    #region METHODS_CUSTOM
    public bool IsSelected()
    {
        float fRandom = UnityEngine.Random.Range(0, 1);
        return (fRandom <= initPercEnemy);
    }

    public void GenerateEnemy(Vector3 pos, float percIncreaseValue, float maxPerc)
    {
        GameObject enemyInstance = null;

        if (cache.Count == 0)
        {//Create new Enemy
            enemyInstance = (GameObject)GameObject.Instantiate(prefabEnemy, pos, Quaternion.identity);
        }
        else
        {//Take from cache
            enemyInstance = cache.Dequeue();
            enemyInstance.transform.position = pos;
            enemyInstance.transform.rotation = Quaternion.identity;
            enemyInstance.SetActive(true);
        }

        enemyInstance.GetComponent<EnemyController>().StartEnemy(this);
    }

    public void AddToCache(GameObject instanceGO)
    {
        cache.Enqueue(instanceGO);
    }
    #endregion

    #region EVENTS
    #endregion
}
