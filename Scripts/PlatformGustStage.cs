using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformGustStage : MonoBehaviour {

	
	public Transform targetA;
	public Transform targetB;
	private float velocity = 50;
	public bool actvade = true;
	private bool movingRight;
	private Animator anima;
	void Start () 
	{
		anima = GetComponent<Animator>();
	}
	
	void FixedUpdate () 
	{
		anima.SetBool("Actived", actvade);

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

	private void OnTriggerEnter2D(Collider2D other) 
	{
		if(other.CompareTag("PlatformTrigger"))
		{
			actvade = false;
		}
	}

	private void OnTriggerExit2D(Collider2D other)
	{
		if(other.CompareTag("PlatformTrigger"))
		{
			actvade = true;
		}
	}
}
