using UnityEngine;
using System;
using System.Collections;

public class LoadingState : MonoBehaviour {

    #region STATIC_ENUM_CONSTANTS
    #endregion

    #region FIELDS
    public event Action onFinishIn = delegate { };

    protected bool animated = false;
    #endregion

    #region ACCESSORS
    public bool InAnimation
    {
        get { return animated; }
    }
    #endregion

    #region METHODS_UNITY
    void Awake()
    {
        InitLoadingState();
        DontDestroyOnLoad(this.gameObject);
    }
    #endregion

    #region METHODS_CUSTOM
    public virtual void InitLoadingState() { }
    public virtual void SetProgress(float progress){ }
    public virtual void In() { }
    public virtual void Out() { }
    #endregion
}
