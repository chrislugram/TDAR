using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// This class have all control about AI of ArmoredTower
/// </summary>
public class ArmoredTowerController : MonoBehaviour {
    #region STATIC_ENUM_CONSTANTS
    #endregion

    #region FIELDS
    [SerializeField]
    public ArmoredTowerGraphicsConf graphicConf;

    public ParticleSystem pulsePS;
    public ParticleSystem deathPS;
    public float radiusPulse = 25;

    private ArmoredTowerMovement movement;
    private DetectorFOV detector;
    private ArmoredTowerAnimation animation;
    private ArmoredTowerWeapon weapon;
    private Health health;
    #endregion

    #region ACCESSORS
    #endregion

    #region METHODS_UNITY
    void Awake()
    {
        GameManager.Instance.Character = this.gameObject;
    }

    void Start()
    {
        animation = GetComponent<ArmoredTowerAnimation>();
        animation.SetShootAction(ShootAction);
        movement = GetComponent<ArmoredTowerMovement>();
        weapon = GetComponent<ArmoredTowerWeapon>();
        detector = GetComponentInChildren<DetectorFOV>();
        detector.onDetectElement += DetectEnemy;
        detector.onNothingDetected += NothingDetected;
        health = GetComponent<Health>();
        health.onReciveDamage += ReciveDamage;
        health.onDeath += Death;

        pulsePS.Stop();

        InitArmoredTower();
    }

    void OnDestroy()
    {
        detector.onDetectElement -= DetectEnemy;
        detector.onNothingDetected -= NothingDetected;
        detector = null;
        movement = null;
        animation = null;
        weapon = null;
        health.onDeath -= Death;
        health.onReciveDamage -= ReciveDamage;
        health = null;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == AppLayers.LAYER_ENEMY)
        {
            EnemyController enemy = collision.gameObject.GetComponent<EnemyController>();
            health.Damage(enemy.damage);
            enemy.CollisionWithCharacter();
        }
    }
    #endregion

    #region METHODS_CUSTOM
    public void InitArmoredTower()
    {
        graphicConf.SetGraphicConf();
        health.SetInitHealth(100 * (UserManager.Instance.UserConfiguration.lifeArmoredTower + 1));
    }

    public void ActivePulseWeapon()
    {
        pulsePS.Play();
        List<GameObject> currentListElementDetected = new List<GameObject>();
        Collider[] hitColliders = Physics.OverlapSphere(this.transform.position, radiusPulse);

        for (int i = 0; i < hitColliders.Length; i++)
        {
            if ((hitColliders[i].gameObject.layer == AppLayers.LAYER_ENEMY) && !(currentListElementDetected.Contains(hitColliders[i].gameObject)))
            {
                currentListElementDetected.Add(hitColliders[i].gameObject);
                hitColliders[i].gameObject.GetComponent<EnemyController>().DamageEnemy();
            }
        }
    }

    public void ActiveGranadeWeapon()
    {
        Debug.Log("Activamos las grandas");
        weapon.ActivateGranade();
    }
    #endregion

    #region EVENTS 
    private void ShootAction()
    {
        weapon.SpawnElement(movement.CanonDirection);
    }

    private void DetectEnemy(GameObject elementDetected)
    {
        movement.SetTarget(elementDetected.transform);
        animation.Shooting = true;
    }

    private void NothingDetected()
    {
        movement.SetTarget(null);
        animation.Shooting = false;
    }

    private void ReciveDamage(float life)
    {
        AudioManager.Instance.PlayFXSound(AudioManager.DAMAGE_TOWER, false, 0.5f);
    }

    private void Death()
    {
        deathPS.Play();
        AudioManager.Instance.PlayFXSound(AudioManager.EXPLOTION, false);
        AudioManager.Instance.PlayFXSound(AudioManager.FIRE, true);
        GameManager.Instance.GameFail();
    }
    #endregion
}