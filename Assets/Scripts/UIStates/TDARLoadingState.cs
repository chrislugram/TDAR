using UnityEngine;
using System.Collections;

public class TDARLoadingState : LoadingState{
    #region STATIC_ENUM_CONSTANTS
    public static readonly string PARAM_IN_ANIMATION = "in";
    public static readonly string PARAM_OUT_ANIMATION = "out";
    #endregion

    #region FIELDS
    private Animator animator;
    #endregion

    #region ACCESSORS
    #endregion

    #region METHODS_UNITY
    #endregion

    #region METHODS_CUSTOM
    public override void InitLoadingState()
    {
        animator = GetComponent<Animator>();
        animated = false;
    }

    public override void In()
    {
        animated = true;
        animator.SetTrigger(PARAM_IN_ANIMATION);
    }

    public override void Out()
    {
        animated = true;
        animator.SetTrigger(PARAM_OUT_ANIMATION);
    }

    public void FinishAnimation()
    {
        animated = false;
    }
    #endregion
}
