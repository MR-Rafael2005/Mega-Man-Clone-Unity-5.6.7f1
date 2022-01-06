using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour {

	private Rigidbody2D bulletBody;
	public float velocityX;
	public float velocityY;
	private int bulletDamange = 1;

	void Awake () 
	{
		//bulletBody = GetComponent<Rigidbody2D> ();	
	}
	
	void FixedUpdate () 
	{
		//bulletBody.velocity = new Vector2 (velocityX, velocityY);
		Vector3 vecBullet = new Vector3 (velocityX /30, velocityY/30, 0f);
		transform.Translate(vecBullet);
		Destroy (gameObject, 4f);
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		Player playerDamange = other.GetComponent<Player> ();
		if (playerDamange != null) 
		{
			playerDamange.enemyDamange = bulletDamange;
		}
	}


}
