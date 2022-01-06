using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public int playerLife = 20;
	public int playerDamange = 1;
	
	public bool[] getPower;
	//public int[] powerCharge;
	public float fireRate = 0.1f;

	public static GameManager gameManeger;

	void Awake () 
	{
		if (gameManeger == null) 
		{
			gameManeger = this;
		} else {
			Destroy (gameObject);
		}

		DontDestroyOnLoad (gameObject);
		
		getPower[0] = false;
		getPower[1] = false;
		getPower[2] = false;
		getPower[3] = false;
		getPower[4] = false;
		getPower[5] = false;
		getPower[6] = false;
	}

	void Update () {

	}
}
