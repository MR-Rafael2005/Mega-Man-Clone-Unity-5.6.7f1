using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMet : EnemyBase 
{
	public float moveDistance;

	private bool isMoving;
	private bool isAtacking = false;
	private bool bulletCharge;

	public AudioClip bulletSong;
	private AudioSource metAudioSource;
	public GameObject bulletUM;
	public GameObject bulletDOIS;
	public GameObject bulletTRES;

	void Start () 
	{
		bulletCharge = true;
		metAudioSource = GetComponent<AudioSource> ();
		enemyDamange = 1;
	}

	protected override void Update()
	{
		base.Update ();


		/*if (Mathf.Abs (playerDistance) < moveDistance) 
		{
			isMoving = true;
		}*/
		if (Mathf.Abs (playerDistance) < atackDistance) 
		{
			isAtacking = true;
			isMoving = false;
		} else {
			isAtacking = false;
			isMoving = true;
		}

		if (isMoving) 
		{
			blocking = true;
		} else {
			blocking = false;
		}

	}

	private void FixedUpdate()
	{
		if (facingRight) 
		{
			transform.eulerAngles = new Vector2 (0f, 180f);
		} else {
			transform.eulerAngles = new Vector2 (0f, 0f);
		}

		if (playerDistance < 0) 
		{
			enemyBody.velocity = new Vector2 (enemySpeed, enemyBody.velocity.y);

			if (!facingRight) 
			{
				fliped ();
			}

		} else {
			enemyBody.velocity = new Vector2 (-enemySpeed, enemyBody.velocity.y);

			if (facingRight) 
			{
				fliped ();
			}
		}

		if (isAtacking) 
		{
			if(bulletCharge)
			{
				metAudioSource.clip = bulletSong;
				metAudioSource.PlayOneShot (metAudioSource.clip);
				bulletCharge = false;
				Instantiate (bulletUM, transform.position, transform.rotation);
				Instantiate(bulletDOIS, transform.position, transform.rotation);
				Instantiate(bulletTRES, transform.position, transform.rotation);
			}

		} else if (Mathf.Abs (playerDistance) < moveDistance) {
			isAtacking = false;
			bulletCharge = true;
			
		}


		enemyAnimator.SetBool ("Stop", isMoving);
		enemyAnimator.SetBool ("Atack", isAtacking);
	}

	public void ResetAtack()
	{
		isAtacking = false;
		bulletCharge = true;
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		Player playerDamange = other.GetComponent<Player> ();
		if (playerDamange != null) 
		{
			playerDamange.enemyDamange = enemyDamange;
		}
	}

	private void OnTriggerStay2D(Collider2D other)
	{
		Player playerDamange = other.GetComponent<Player> ();
		if (playerDamange != null) 
		{
			playerDamange.enemyDamange = enemyDamange;
		}
	}

	private void OnCollisionStay2D(Collision2D other)
	{
		Player playerDamange = other.gameObject.GetComponent<Player> ();
		if (playerDamange != null) 
		{
			playerDamange.enemyDamange = enemyDamange;
		}
	}
}
