using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour {

	private float speedShot = 250;
	public int damangeShot = 1;
	private float destroyTime = 1.5f;
	//public AudioClip efectsong;
	//private AudioSource efectsource;

	void Start () 
	{
	//	efectsource = GetComponent<AudioSource> ();
	//	efectsource.clip = efectsong;
	//	efectsource.PlayOneShot (efectsource.clip);
		Destroy (gameObject, destroyTime);	

	}
	
	void Update ()
	{
		transform.Translate (Vector3.right * speedShot * Time.deltaTime);
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		EnemyBase otherEnemy = other.GetComponent<EnemyBase> ();
		if (otherEnemy != null) 
		{
			otherEnemy.EnemyTookDamange (damangeShot);
		}

		Destroy (gameObject);
	}

	private void OnColliderEnter2D(Collider other)
	{
		Destroy (gameObject);
	}
	
	


}
