using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public class ArmoredTowerGraphicsConf {
    #region STATIC_ENUM_CONSTANTS
    #endregion

    #region FIELDS
    public GameObject[] damageLevelsConf;
    public GameObject[] speedLevelsConf;
    #endregion

    #region ACCESSORS
    #endregion

    #region METHODS_CUSTOM
    public void SetGraphicConf()
    {
        //Show correct number of plasma condensators
        for (int i = 0; i < damageLevelsConf.Length; i++)
		{
            damageLevelsConf[i].SetActive(i <= UserManager.Instance.UserConfiguration.lifeArmoredTower);
		}

        //Show correct number of canons
        for (int i = 0; i < speedLevelsConf.Length; i++)
		{
            speedLevelsConf[i].SetActive(i == UserManager.Instance.UserConfiguration.speedArmoredTower);
		}
    }
    #endregion
}
