using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class ArmoredTowerWeapon : Spawner
{
    #region STATIC_ENUM_CONSTANTS
    #endregion

    #region FIELDS
    public int damageWeapon;
    #endregion

    #region ACCESSORS
    #endregion

    #region METHODS_UNITY
    #endregion

    #region METHODS_CUSTOM
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
            Debug.Log("Damage: " + damageWeapon);
            spawnElementInstance = (GameObject)Instantiate(spawnElementsPrefab[damageWeapon], spawnPointSelected.position, Quaternion.identity);
            spawnElementInstance.name = spawnElementInstance.name + "_" + totalSpawnerElementCreated;
            totalSpawnerElementCreated++;
        }
        else
        {
            spawnElementInstance = spawnElements.Dequeue();
            if (spawnElementInstance != null && spawnPointSelected != null)
            {
                spawnElementInstance.transform.position = spawnPointSelected.position;
                spawnElementInstance.transform.rotation = Quaternion.identity;
                spawnElementInstance.SetActive(true);
            }
        }

        return spawnElementInstance;
    }
    #endregion
}
