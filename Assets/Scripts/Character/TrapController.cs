using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TrapController : MonoBehaviour {
    #region STATIC_ENUM_CONSTANTS
    #endregion

    #region FIELDS
    public float radiusAttack = 4;
    public float angleVisibility = 45;
    public ParticleSystem activePS;
    public GameObject markGO;
    public Transform transformCharacter;
    
    private bool trapVisible = false;
    #endregion

    #region ACCESSORS
    #endregion

    #region METHODS_UNITY
    void Start()
    {
        GameState.onTrapActivate += Attack;

        activePS.Stop();
        markGO.SetActive(false);
        trapVisible = false;
    }

    void Update()
    {
        Vector3 direction = transformCharacter.position - this.transform.position;

        bool newVisibility = (180 - angleVisibility) <= Vector3.Angle(direction, transformCharacter.forward);

        if (newVisibility != trapVisible)
        {
            trapVisible = newVisibility;
            markGO.SetActive(trapVisible);
            if (trapVisible)
            {
                activePS.Play();
            }
            else
            {
                activePS.Stop();
            }
            
        }
    }

    void OnDestroy() 
    {
        GameState.onTrapActivate -= Attack;
    }
    #endregion

    #region METHODS_CUSTOM
    public void Attack()
    {
        List<GameObject> currentListElementDetected = new List<GameObject>();
        Collider[] hitColliders = Physics.OverlapSphere(this.transform.position, radiusAttack);

        for (int i = 0; i < hitColliders.Length; i++)
        {
            if ((hitColliders[i].gameObject.layer == AppLayers.LAYER_ENEMY) && !(currentListElementDetected.Contains(hitColliders[i].gameObject)))
            {
                currentListElementDetected.Add(hitColliders[i].gameObject);
                hitColliders[i].gameObject.GetComponent<EnemyController>().DamageEnemy();
            }
        }
    }
    #endregion

    #region EVENTS
    #endregion
}
