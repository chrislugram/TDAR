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
        InputGame.onMoveToLeft += MoveToLeft;
        InputGame.onMoveToRight += MoveToRight;
    }

    void Update()
    {
        //Rotation X
        Quaternion newRotation = Quaternion.identity;

        if (transformTarget != null)
        {
            Debug.Log("Target: " + transformTarget.name);
            Vector3 relativePosition = transformTarget.position - transformXRotation.position;
            newRotation = Quaternion.LookRotation(relativePosition);
            newRotation = Quaternion.Euler(new Vector3(newRotation.eulerAngles.x, 0, 0));
        }

        transformXRotation.localRotation = Quaternion.Lerp(transformXRotation.localRotation, newRotation, Time.deltaTime * speedRotationX);
    }
    #endregion

    #region METHODS_CUSTOM
    public void SetTarget(Transform t)
    {
        transformTarget = t;
    }
    #endregion

    #region EVENTS
    private void MoveToLeft()
    {
        transformYRotation.Rotate(0, -speedRotationY * Time.deltaTime, 0);
    }

    private void MoveToRight()
    {
        transformYRotation.Rotate(0, speedRotationY * Time.deltaTime, 0);
    }
    #endregion
}
