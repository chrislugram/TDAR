using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Bullet : SpawnElement {
	#region STATIC_ENUM_CONSTANTS
	#endregion
	
	#region FIELDS
    public float radiusGranade = 10;
    public ParticleSystem normalPS;
    public ParticleSystem explosionPS;
    public ParticleSystem finishExplosionPS;

    private bool isGranade = false;
	#endregion
	
	#region ACCESSORS
	public float		bulletSpeed = 10;
	public int			damage = 1;
	public float		bulletDisabledTime = 5;

	private Rigidbody	bulletRigidbody;
	private float		currentDisabledTime = 0;
	#endregion
	
	#region METHODS_UNITY
	void Awake(){
		bulletRigidbody = this.GetComponent<Rigidbody> ();
        finishExplosionPS.transform.parent = null;
	}

	void Update(){
		currentDisabledTime += Time.deltaTime;

		if (currentDisabledTime > bulletDisabledTime) {
			Desactive();
		}
	}

	void OnCollisionEnter(Collision collision) {
        if (isGranade)
        {
            finishExplosionPS.transform.position = this.transform.position;
            finishExplosionPS.Play();
            Explode();
        }
        else
        {
            if (collision.gameObject.layer == AppLayers.LAYER_ENEMY) {
                collision.gameObject.GetComponent<EnemyController>().DamageEnemy();
		    }
        }

        normalPS.Stop();
        explosionPS.Stop();

		Desactive ();
	}
	#endregion
	
	#region METHODS_CUSTOM
    public void InitNormal()
    {
        isGranade = false;
        normalPS.Play();
    }

    public void InitGranade()
    {
        isGranade = true;
        explosionPS.Play();
    }

    public void Explode()
    {
        List<GameObject> currentListElementDetected = new List<GameObject>();
        Collider[] hitColliders = Physics.OverlapSphere(this.transform.position, radiusGranade);

        for (int i=0; i<hitColliders.Length; i++){
			if ((hitColliders[i].gameObject.layer == AppLayers.LAYER_ENEMY) && !(currentListElementDetected.Contains(hitColliders[i].gameObject))){
				currentListElementDetected.Add(hitColliders[i].gameObject);
                hitColliders[i].gameObject.GetComponent<EnemyController>().DamageEnemy();
			}
		}
    }

	public override void Initialized (Spawner spawner){
        AudioManager.Instance.PlayFXSound(AudioManager.SHOOT, false, 0.1f);

		base.Initialized (spawner);

		bulletRigidbody.velocity = this.transform.forward * bulletSpeed;
		currentDisabledTime = 0;
	}
	#endregion

	#region METHODS_EVENT
	#endregion
}
