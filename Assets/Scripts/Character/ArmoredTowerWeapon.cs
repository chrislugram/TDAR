using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class ArmoredTowerWeapon : Spawner
{
    #region STATIC_ENUM_CONSTANTS
    #endregion

    #region FIELDS
    public float timeGranade = 5f;

    private bool granadeWeapon = false;
    private float currentTimeGranade = 0;
    #endregion

    #region ACCESSORS
    #endregion

    #region METHODS_UNITY
    void Update()
    {
        if (granadeWeapon)
        {
            Debug.Log("Granadas activadas");
            currentTimeGranade += Time.deltaTime;
            if (currentTimeGranade > timeGranade)
            {
                granadeWeapon = false;
            }
        }
    }
    #endregion

    #region METHODS_CUSTOM
    public void ActivateGranade()
    {
        granadeWeapon = true;
        currentTimeGranade = 0;
    }

    protected override void SpawnElementAction(GameObject spawnElementGO)
    {
        spawnElementGO.GetComponent<SpawnElement>().Initialized(this);
        Reload();
    }

    protected override GameObject GetSpawnElement()
    {
        GameObject spawnElementInstance = null;
        
        //Select de current canon
        Transform spawnPointSelected = spawnPoint[0];

        if (spawnElements.Count == 0)
        {
            spawnElementInstance = (GameObject)Instantiate(spawnElementsPrefab[0], spawnPointSelected.position, Quaternion.identity);
            if (!granadeWeapon)
            {
                spawnElementInstance.GetComponent<Bullet>().InitNormal();
            }
            else
            {
                spawnElementInstance.GetComponent<Bullet>().InitGranade();
            }
            spawnElementInstance.name = spawnElementInstance.name + "_" + totalSpawnerElementCreated;
            totalSpawnerElementCreated++;
        }
        else
        {
            spawnElementInstance = spawnElements.Dequeue();
            if (spawnElementInstance != null && spawnPointSelected != null)
            {
                if (!granadeWeapon)
                {
                    spawnElementInstance.GetComponent<Bullet>().InitNormal();
                }
                else
                {
                    spawnElementInstance.GetComponent<Bullet>().InitGranade();
                }
                spawnElementInstance.transform.position = spawnPointSelected.position;
                spawnElementInstance.transform.rotation = Quaternion.identity;
                spawnElementInstance.SetActive(true);
            }
        }

        return spawnElementInstance;
    }
    #endregion
}
