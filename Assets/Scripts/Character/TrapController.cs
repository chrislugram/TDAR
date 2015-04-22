using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TrapController : MonoBehaviour {
    #region STATIC_ENUM_CONSTANTS
    #endregion

    #region FIELDS
    public float radiusAttack = 4;
    public ParticleSystem explosionPS;
    public GameObject markGO;
    #endregion

    #region ACCESSORS
    #endregion

    #region METHODS_UNITY
    void Start()
    {
        GameState.onTrapActivate += Attack;
    }

    void OnDestroy() 
    {
        GameState.onTrapActivate -= Attack;
    }
    #endregion

    #region METHODS_CUSTOM
    public void Attack()
    {
        explosionPS.Play();

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
