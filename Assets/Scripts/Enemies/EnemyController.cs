using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {
    #region STATIC_ENUM_CONSTANTS
    public static readonly string PARAM_END_GAME = "EndGame";
    public enum ENEMY_TYPE
    {
        SPIDER = 0,
        WASP = 1
    }
    #endregion

    #region FIELDS
    public int initHealth = 1;
    public int damage = 40;
    public int plasma = 2;
    public ParticleSystem deathPS;
    public Transform parentDeathPS;
    public ENEMY_TYPE enemyType;

    public Animator animatorEnemy;

    protected NavMeshAgent navMesh;
    protected int currentHealth = 0;
    protected StageEnemyInfo stageEnemyInfoRef;
    protected bool endGameFlag = false;
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
        GameManager.Instance.onEndGame += EndGame;
        deathPS.transform.parent = null;
    }

    void Update()
    {
        if (!IsDeath && !endGameFlag)
        {
            MoveEnemy();
        }
    }

    void OnDestroy()
    {
        GameManager.Instance.onEndGame -= EndGame;
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
        if (!IsDeath)
        {
            currentHealth--;
            if (IsDeath)
            {
                DeathEnemy();
            }
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
        GameManager.Instance.EnemyDestroyed(enemyType);
        GameManager.Instance.AddPlasma(plasma);
        stageEnemyInfoRef.AddToCache(this.gameObject);

        AudioManager.Instance.PlayFXSound(AudioManager.DEATH_ENEMY, false, 0.5f);

        deathPS.transform.position = parentDeathPS.transform.position;
        deathPS.Play();

        this.gameObject.SetActive(false);
    }

    protected virtual void EndGame()
    {
        endGameFlag = true;
        if (this.gameObject.activeSelf)
        {
            navMesh.SetDestination(this.transform.position);
            animatorEnemy.SetBool(PARAM_END_GAME, true);
        }       
    }
    #endregion

    #region EVENTS
    #endregion
}
