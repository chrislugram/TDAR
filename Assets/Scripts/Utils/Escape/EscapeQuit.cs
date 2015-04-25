using UnityEngine;
using System.Collections;

public class EscapeQuit : MonoBehaviour {

    #region STATIC_ENUM_CONSTANTS
    #endregion

    #region FIELDS
    #endregion

    #region ACCESSORS
    #endregion

    #region METHODS_UNITY
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
    #endregion

    #region METHODS_CUSTOM
    #endregion

    #region EVENTS
    #endregion
}
