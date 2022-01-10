using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineEnemy : EnemyBase 
{

	private bool atacking = false;
	public Transform SpawPosition;
	private float fireRateEnemy = 0.5f;
	private float nextFire;
	public GameObject bullet;

	private Animator animator;

	void Start () 
	{
		animator = GetComponent<Animator>();
	}

	
	protected override void Update()
	{
		base.Update();	
	}

	private void FixedUpdate()
	{
		animator.SetBool("Atacking", atacking);
		direction();
		atack();
	}

	private void direction()
	{
		if (facingRight) 
		{
			transform.eulerAngles = new Vector2 (0f, 180f);
		} else {
			transform.eulerAngles = new Vector2 (0f, 0f);
		}

		if (playerDistance < 0) 
		{
			//enemyBody.velocity = new Vector2 (enemySpeed, enemyBody.velocity.y);

			if (!facingRight) 
			{
				fliped ();
			}

		} else {
			//enemyBody.velocity = new Vector2 (-enemySpeed, enemyBody.velocity.y);

			if (facingRight) 
			{
				fliped ();
			}
		}
	}

	private void atack()
	{
		if(Mathf.Abs(playerDistance) < atackDistance)
		{
			atacking = true;
			blocking = false;
		} else {
			blocking = true;
			atacking = false;
		}

		if(atacking && Time.time > nextFire)
		{
			nextFire = Time.time + fireRateEnemy;
			Instantiate(bullet, SpawPosition.position, SpawPosition.rotation);
		}
	}

	private void resetAtack()
	{
		atacking = false;
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		Player playerDamange = other.GetComponent<Player> ();
		if (playerDamange != null) 
		{
			playerDamange.enemyDamange = enemyDamange;
		}
	}
}
