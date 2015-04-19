using UnityEngine;
using System.Collections;

public class GizmoSphere : MonoBehaviour {

    #region STATIC_ENUM_CONSTANTS
    #endregion

    #region FIELDS
    public Color colorGizmo;
    public float radius;
    #endregion

    #region ACCESSORS
    #endregion

    #region METHODS_UNITY
    void OnDrawGizmos()
    {
        Gizmos.color = colorGizmo;
        Gizmos.DrawWireSphere(this.transform.position, radius);
    }
    #endregion

    #region METHODS_CUSTOM
    #endregion
}
