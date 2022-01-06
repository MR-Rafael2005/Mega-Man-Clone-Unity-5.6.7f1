using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectEfect : MonoBehaviour {

	private SpriteRenderer sprite;

	void Start () 
	{
		sprite = GetComponent<SpriteRenderer> ();
		sprite.enabled = false;
	}
	
	void FixedUpdate ()
	{
		StartCoroutine (pisca ());
	}

	IEnumerator pisca()
	{
		sprite.enabled = false;
		yield return new WaitForSeconds (0.5f);
		sprite.enabled = true;
		yield return new WaitForSeconds (0.5f);
	}
}
