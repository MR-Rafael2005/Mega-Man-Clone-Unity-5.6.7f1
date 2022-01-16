using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GigantEnemy : EnemyBase 
{

	void Start () {}
	
	// Update is called once per frame
	protected override void Update() 
	{
		base.Update();	
	}

	private void FixedUpdate()
	{
		if(Mathf.Abs(playerDistance) < atackDistance)
		{
			StartCoroutine(movi());
		} else {
			StopCoroutine(movi());
		}
	}

	IEnumerator movi()
	{	if(facingRight)
		{
			enemyBody.velocity = new Vector2(-enemySpeed / 2, enemySpeed);
		} else {
			enemyBody.velocity = new Vector2(enemySpeed / 2, enemySpeed);
		}
		yield return new WaitForSeconds(1f);
		if(facingRight)
		{
			enemyBody.velocity = new Vector2(-enemySpeed / 2, enemySpeed);
		} else {
			enemyBody.velocity = new Vector2(enemySpeed / 2, enemySpeed);
		}
		yield return new WaitForSeconds(1f);
		if(facingRight)
		{
			enemyBody.velocity = new Vector2(-enemySpeed / 2, enemySpeed);
		} else {
			enemyBody.velocity = new Vector2(enemySpeed / 2, enemySpeed * 1.5f);
		}
		yield return new WaitForSeconds(1.5f);
	}
	
}
