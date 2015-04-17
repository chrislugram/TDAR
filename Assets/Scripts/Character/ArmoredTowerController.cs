using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// This class have all control about AI of ArmoredTower
/// </summary>
public class ArmoredTowerController : MonoBehaviour {
    #region STATIC_ENUM_CONSTANTS
    #endregion

    #region FIELDS
    public Transform transformTarget;

    [SerializeField]
    public ArmoredTowerGraphicsConf graphicConf;

    #endregion

    #region ACCESSORS
    #endregion

    #region METHODS_UNITY
    void Start()
    {
        InitArmoredTower();
    }
    #endregion

    #region METHODS_CUSTOM
    public void InitArmoredTower()
    {
        graphicConf.SetGraphicConf();
    }
    #endregion
}
