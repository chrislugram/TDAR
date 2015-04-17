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
    [SerializeField]
    public ArmoredTowerGraphicsConf graphicConf;

    private ArmoredTowerMovement movement;
    private DetectorFOV detector;
    private ArmoredTowerAnimation animation;
    #endregion

    #region ACCESSORS
    #endregion

    #region METHODS_UNITY
    void Start()
    {
        animation = GetComponent<ArmoredTowerAnimation>();
        movement = GetComponent<ArmoredTowerMovement>();
        detector = GetComponentInChildren<DetectorFOV>();
        detector.onDetectElement += DetectEnemy;
        detector.onNothingDetected += NothingDetected; 

        InitArmoredTower();
    }

    void OnDestroy()
    {
        detector.onDetectElement -= DetectEnemy;
        detector.onNothingDetected -= NothingDetected;
        detector = null;
        movement = null;
    }
    #endregion

    #region METHODS_CUSTOM
    public void InitArmoredTower()
    {
        graphicConf.SetGraphicConf();
    }
    #endregion

    #region EVENTS
    private void DetectEnemy(GameObject elementDetected)
    {
        movement.SetTarget(elementDetected.transform);
        animation.Shooting = true;
    }

    private void NothingDetected()
    {
        movement.SetTarget(null);
        animation.Shooting = false;
    }
    #endregion
}
