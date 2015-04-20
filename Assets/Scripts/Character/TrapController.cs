using UnityEngine;
using System.Collections;

public class TrapController : MonoBehaviour {
    #region STATIC_ENUM_CONSTANTS
    #endregion

    #region FIELDS
    public float angleVisibility = 45;
    public ParticleSystem activePS;
    public GameObject markGO;
    public Transform transformCharacter;
    #endregion

    #region ACCESSORS
    #endregion

    #region METHODS_UNITY
    void Start()
    {
        activePS.Stop();
        markGO.SetActive(false);
    }

    void Update()
    {
        Vector3 direction = transformCharacter.position - this.transform.position;
        markGO.SetActive((180-angleVisibility) <= Vector3.Angle(direction, transformCharacter.forward));
    }
    #endregion

    #region METHODS_CUSTOM
    #endregion

    #region EVENTS
    #endregion
}
