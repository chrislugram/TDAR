using UnityEngine;
using System.Collections;

public class ArmoredTowerMovement : MonoBehaviour {
    #region STATIC_ENUM_CONSTANTS
    #endregion

    #region FIELDS
    public Transform transformYRotation;
    public Transform transformXRotation;
    public float speedRotationY;
    public float speedRotationX;

    private Transform transformTarget;
    #endregion

    #region ACCESSORS
    #endregion

    #region METHODS_UNITY
    void Start()
    {
        InputGame.onMoveToLeft += InputGame_onMoveToLeft;
        InputGame.onMoveToRight += InputGame_onMoveToRight;
    }

    void Update()
    {
        //Rotation X
        if (transformTarget != null)
        {
            Vector3 relativePosition = transformTarget.position - transformXRotation.position;
            Quaternion rotation = Quaternion.LookRotation(relativePosition);
            transformXRotation.rotation = Quaternion.Euler(new Vector3(rotation.eulerAngles.x, transformXRotation.rotation.eulerAngles.y, transformXRotation.rotation.eulerAngles.z));
        }
    }
    #endregion

    #region METHODS_CUSTOM
    #endregion

    #region EVENTS
    private void InputGame_onMoveToLeft()
    {
        transformYRotation.Rotate(0, speedRotationY * Time.deltaTime, 0);
    }

    private void InputGame_onMoveToRight()
    {
        transformYRotation.Rotate(0, -speedRotationY * Time.deltaTime, 0);
    }
    #endregion
}
