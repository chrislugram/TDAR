using UnityEngine;
using System;
using System.Collections;

public class ArmoredTowerAnimation : MonoBehaviour {
    #region STATIC_ENUM_CONSTANTS
    public static readonly string PARAM_TOTAL_CANON = "TotalCanons";
    public static readonly string PARAM_SHOOTING = "Shooting";
    #endregion

    #region FIELDS
    public Animator animator;

    private EventAnimation eventAnimation;
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
        eventAnimation = animator.GetComponent<EventAnimation>();
        animator.SetInteger(PARAM_TOTAL_CANON, UserManager.Instance.UserConfiguration.speedArmoredTower + 1);
    }
    #endregion

    #region METHODS_CUSTOM
    public void SetShootAction(Action action)
    {
        eventAnimation.eventAnimationAction = action;
    }
    #endregion

    #region EVENTS
    #endregion
}
