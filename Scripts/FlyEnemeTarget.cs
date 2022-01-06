using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyEnemeTarget : MonoBehaviour {

	
	
	[Header("Variaveis do movimento natural")]
	public Transform targetA;
	public Transform targetB;
	private float velocity = 50;
	public bool actvade = true;
	private bool movingRight;
	
	//void Start () {}
	
	void Update () 
	{
		if(transform.position.x < targetA.position.x)
		{
			movingRight = true;
		} 
		if(transform.position.x > targetB.position.x) {
			movingRight = false;
		}

		if(movingRight)
		{
			transform.position = new Vector2(transform.position.x + velocity * Time.deltaTime, transform.position.y);
		} else{
			transform.position = new Vector2(transform.position.x - velocity * Time.deltaTime, transform.position.y);	
		}
	}
}
