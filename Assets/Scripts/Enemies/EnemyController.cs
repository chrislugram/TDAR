using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {
    #region STATIC_ENUM_CONSTANTS
    #endregion

    #region FIELDS
    public int initHealth = 1;
    public int damage = 40;

    private NavMeshAgent navMesh;
    public int currentHealth = 0;
    private StageEnemyInfo stageEnemyInfoRef;
    #endregion

    #region ACCESSORS
    public bool IsDeath
    {
        get { return (currentHealth <= 0); }
    }
    #endregion

    #region METHODS_UNITY
    void Start()
    {
        navMesh = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (!IsDeath)
        {
            MoveEnemy();
        }
    }
    #endregion

    #region METHODS_CUSTOM
    public virtual void StartEnemy(StageEnemyInfo stageEnemyInfo)
    {
        stageEnemyInfoRef = stageEnemyInfo;
        currentHealth = initHealth;
    }

    public virtual void DamageEnemy()
    {
        currentHealth--;
        if (IsDeath)
        {
            DeathEnemy();
        }
    }

    public virtual void CollisionWithCharacter()
    {
        DeathEnemy();
    }

    protected virtual void MoveEnemy()
    {
        navMesh.SetDestination(GameManager.Instance.Character.transform.position);
    }

    protected virtual void DeathEnemy()
    {
        GameManager.Instance.EnemyDestroyed();
        stageEnemyInfoRef.AddToCache(this.gameObject);
        this.gameObject.SetActive(false);
    }
    #endregion

    #region EVENTS
    #endregion
}
