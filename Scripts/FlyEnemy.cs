using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyEnemy : EnemyBase 
{

	public GameObject[] targets;
	public bool atacking = false;
	public float moveDistance;
	public float tempoUm;
	public float tempoDois;
	public bool rountine;
	public float atackSpeed;
	public bool extraR;
	public float atackTime;
	public bool actvade;
	private bool movingRight;
	public float distanceY;

	
	void Start () 
	{
		enemySpeed = 50;	
		targets[0] = GameObject.Find("Player");
	}
	
	protected override void Update()
	{
		base.Update();
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
		//	enemyBody.velocity = new Vector2 (enemySpeed, enemyBody.velocity.y);

			if (!facingRight) 
			{
				fliped ();
			}

		} else {
		//	enemyBody.velocity = new Vector2 (-enemySpeed, enemyBody.velocity.y);

			if (facingRight) 
			{
				fliped ();
			}
		}	

		if( Mathf.Abs(playerDistance) < moveDistance)
		{
			moving();
		} 

		if(Mathf.Abs(playerDistance) < atackDistance && actvade == false)
		{
			actvade = true;
			rountine = true;
			enemySpeed = 100;
			distanceY = playerTarget.position.y - transform.position.y;
		}  
		if(Mathf.Abs(playerDistance) > atackDistance)
		{
			actvade = false;
			tempoUm = 0f;
			tempoDois = 0f;
		}

		ataq();
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		Player playerDamange = other.GetComponent<Player> ();
		if (playerDamange != null) 
		{
			playerDamange.enemyDamange = enemyDamange;
		}
	}

	private void moving()
	{
		if(transform.position.x < playerTarget.position.x - 50)
		{
			movingRight = true;
		} 
		if(transform.position.x > playerTarget.position.x + 50) 
		{
			movingRight = false;
		}

		if(movingRight)
		{
			transform.position = new Vector2(transform.position.x + enemySpeed * Time.deltaTime, transform.position.y);
		} else {
			transform.position = new Vector2(transform.position.x - enemySpeed * Time.deltaTime, transform.position.y);	
		}
	}

	private void ataq()
	{
		if(actvade)
		{
			if(rountine)
			{
				tempoUm = tempoUm + Time.deltaTime;
				Vector3 altura = new Vector3(0f, distanceY, 0f);
				transform.position += altura * Time.deltaTime * atackSpeed;
				if(tempoUm >= atackTime)
				{
					rountine = false;
					extraR = true;
				}
			} 
			if(extraR)
			{
				tempoDois = tempoDois + Time.deltaTime;
				Vector3 altura = new Vector3(0f, distanceY, 0f);
				transform.position -= altura * Time.deltaTime * atackSpeed;
				if(tempoDois >= atackTime)
				{
					extraR = false;
					enemySpeed = 50;
				}
			}
			
		}
	

	}
}
