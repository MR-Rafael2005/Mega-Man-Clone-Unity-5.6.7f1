using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineBullet : MonoBehaviour 
{
	public float velocityX;
	public float forceY;
	public float distance;
	public int bulletDamange;
	private Transform playerPosition;

	private Rigidbody2D rb;

	void Awake () 
	{
		playerPosition = GameObject.FindObjectOfType<Player>().transform;
		rb = GetComponent<Rigidbody2D>();
		distance = playerPosition.position.x - transform.position.x;
		rb.AddForce (new Vector2(0f, forceY));
	}
	
	void FixedUpdate () 
	{
		Vector3 movi = new Vector3(distance, 0f, 0f);
		transform.position += movi * Time.deltaTime * velocityX;
		Destroy(gameObject, 4f);	
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
