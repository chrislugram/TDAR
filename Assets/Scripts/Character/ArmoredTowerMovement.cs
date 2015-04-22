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
    public float offsetXRotation = 0;

    private Transform transformTarget;
    #endregion

    #region ACCESSORS
    public Vector3 CanonDirection
    {
        get { return transformXRotation.forward; }
    }
    #endregion

    #region METHODS_UNITY
    void Start()
    {
        InputGame.onMoveToLeft += MoveToLeft;
        InputGame.onMoveToRight += MoveToRight;
        InputGame.onNothing += Nothing;
    }

    void Update()
    {
        //Rotation X
        if (!GameManager.Instance.FinishedGame)
        {
            Quaternion newRotation = Quaternion.identity;

            if (transformTarget != null && transformTarget.gameObject.activeSelf)
            {
                Vector3 relativePosition = transformTarget.position - transformXRotation.position;
                newRotation = Quaternion.LookRotation(relativePosition);
                newRotation = Quaternion.Euler(new Vector3(newRotation.eulerAngles.x + offsetXRotation, 0, 0));
            }

            transformXRotation.localRotation = Quaternion.Lerp(transformXRotation.localRotation, newRotation, Time.deltaTime * speedRotationX);
        }
       
    }

    void OnDestroy()
    {
        InputGame.onMoveToLeft -= MoveToLeft;
        InputGame.onMoveToRight -= MoveToRight;
        InputGame.onNothing -= Nothing;
    }
    #endregion

    #region METHODS_CUSTOM
    public void SetTarget(Transform t)
    {
        if (transformTarget != null && t != null)
        {
            float distance = Vector3.Distance(this.transform.position, t.position);
            if (!transformTarget.gameObject.activeSelf)
            {
                transformTarget = t;
            }
            else if (distance < Vector3.Distance(this.transform.position, transformTarget.position))
            {
                transformTarget = t;
            }
        }
        else
        {
            transformTarget = t;
        }
        
    }
    #endregion

    #region EVENTS
    private void Nothing()
    {
        if (!GameManager.Instance.FinishedGame)
        {
            AudioManager.Instance.StopFXSound(AudioManager.MOVE_TOWER);
        }
    }

    private void MoveToLeft()
    {
        if (!GameManager.Instance.FinishedGame)
        {
            AudioManager.Instance.PlayFXSound(AudioManager.MOVE_TOWER, true, 0.2f, true);
            transformYRotation.Rotate(0, -speedRotationY * Time.deltaTime, 0);
        }
    }

    private void MoveToRight()
    {
        if (!GameManager.Instance.FinishedGame)
        {
            AudioManager.Instance.PlayFXSound(AudioManager.MOVE_TOWER, true, 0.2f, true);
            transformYRotation.Rotate(0, speedRotationY * Time.deltaTime, 0);
        }
    }
    #endregion
}
