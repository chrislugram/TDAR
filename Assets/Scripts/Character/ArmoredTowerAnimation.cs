using UnityEngine;
using System.Collections;

public class ArmoredTowerAnimation : MonoBehaviour {
    #region STATIC_ENUM_CONSTANTS
    public static readonly string PARAM_TOTAL_CANON = "TotalCanons";
    public static readonly string PARAM_SHOOTING = "Shooting";
    #endregion

    #region FIELDS
    public Animator animator;
    #endregion

    #region ACCESSORS
    public bool Shooting
    {
        set {
            animator.SetBool(PARAM_SHOOTING, value);
        }
    }
    #endregion

    #region METHODS_UNITY
    void Start()
    {
        animator.SetInteger(PARAM_TOTAL_CANON, UserManager.Instance.SpeedArmoredTower + 1);
    }
    #endregion

    #region METHODS_CUSTOM
    #endregion

    #region EVENTS
    #endregion
}
