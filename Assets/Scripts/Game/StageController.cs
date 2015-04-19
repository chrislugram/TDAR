using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class StageController : MonoBehaviour
{
    #region STATIC_ENUM_CONSTANTS
    #endregion

    #region FIELDS
    public float maxPercGenerateEnemy;
    public float initTimeBetweenGeneration;
    public float stepTimeBetweenGeneration;
    public float minTimeBetweenGeneration;
    public float increasePercWithGeneration;
    [Range(0, 120)]
    public float rangePointGeneration;
    [SerializeField]
    public StageEnemyInfo[] stageEnemies;

    public bool generating = false;
    private float currentStepBetweenGeneration;
    private float currentStepGeneration;
    #endregion

    #region ACCESSORS
    #endregion

    #region METHODS_UNITY
    void Update()
    {
        if (generating)
        {
            currentStepGeneration += Time.deltaTime;
            if (currentStepGeneration >= currentStepBetweenGeneration)
            {
                GenerateEnemy();
                currentStepGeneration = 0;

                currentStepBetweenGeneration -= stepTimeBetweenGeneration;
                if (currentStepBetweenGeneration < minTimeBetweenGeneration)
                {
                    currentStepBetweenGeneration = minTimeBetweenGeneration;
                }
            }
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(this.transform.position, rangePointGeneration);
    }
    #endregion

    #region METHODS_CUSTOM
    public void StartStage()
    {
        currentStepBetweenGeneration = initTimeBetweenGeneration;
        currentStepGeneration = float.MaxValue;
        generating = true;

        GameManager.Instance.StartGame();
    }

    public void GenerateEnemy()
    {
        //Select type of Enemy
        StageEnemyInfo enemyInfoSelected = null;
        for (int i = 0; i < stageEnemies.Length && (enemyInfoSelected == null); i++)
        {
            if (stageEnemies[i].IsSelected())
            {
                enemyInfoSelected = stageEnemies[i];
            }
        }

        //Generate enemy
        float fRandom = UnityEngine.Random.Range(0, 90);
        Vector3 vPos = new Vector3(Mathf.Cos(fRandom) * rangePointGeneration, 0, Mathf.Sin(fRandom) * rangePointGeneration);
        enemyInfoSelected.GenerateEnemy(vPos, increasePercWithGeneration, maxPercGenerateEnemy);
    }
    #endregion

    #region EVENTS
    #endregion
}
