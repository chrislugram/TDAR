using UnityEngine;
using System.Collections;

public class DronBlade : MonoBehaviour {
    #region STATIC_ENUM_CONSTANTS
    #endregion

    #region FIELDS
    public float speedBlade = 10;

    private Transform bladeTransform;
    #endregion

    #region ACCESSORS
    #endregion

    #region METHODS_UNITY
    void Start()
    {
        bladeTransform = this.transform;
    }

    void Update()
    {
        if (this.gameObject.activeSelf)
        {
            Vector3 localRotation = bladeTransform.localRotation.eulerAngles;
            bladeTransform.localRotation = Quaternion.EulerAngles(localRotation.x, localRotation.y + speedBlade, localRotation.z);
        }
    }
    #endregion

    #region METHODS_CUSTOM
    #endregion

    #region EVENTS
    #endregion
}
