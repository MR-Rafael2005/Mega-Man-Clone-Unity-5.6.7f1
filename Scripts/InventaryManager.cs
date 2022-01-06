using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InventaryManager : MonoBehaviour 
{

	GameManager gameManager;
	public GameObject inventory;
	public GameObject[] powers;
	public GameObject selectPower;
	public Transform selectPowerTransform;
	public Transform[] selectPositions;
	public SpriteRenderer selectSprite;
	public int selected;

	public bool activate = false;
	
	void Start () 
	{
		gameManager = GameManager.gameManeger;
		Time.timeScale = 1f;
		powers[0] = GameObject.Find("M");
		powers[1] = GameObject.Find("F");
		powers[2] = GameObject.Find("C");
		powers[3] = GameObject.Find("I");
		powers[4] = GameObject.Find("G");
		powers[5] = GameObject.Find("E");
		powers[6] = GameObject.Find("B");
		selectSprite = selectPower.GetComponent<SpriteRenderer>();
		inventory = GameObject.Find("Inventory");
	}

	private void Update()
	{
		activateInventory()	;
		inventory.SetActive(activate);
		selectedPower();
		actived();
	}

	void activateInventory()
	{
		if(Input.GetButtonDown("Start"))
		{
			if(activate)
			{
				Time.timeScale = 1f;
				activate = false;
				StopCoroutine(selectPowerEfect());
			} else {
				Time.timeScale = 0f;
				activate = true;
				StartCoroutine(selectPowerEfect());
			}
		} 

		if(Input.GetButtonDown("Up"))
		{
			selected ++;
		}
		if(Input.GetButtonDown("Down"))
		{
			selected --;
		}

		if(selected > 7)
		{
			selected = 0;
		}
		if(selected < 0)
		{
			selected = 7;
		}
	}

	IEnumerator selectPowerEfect()
	{
		for (int i = 0; i < 1; i--)
		{
			selectSprite.enabled = false;
			yield return new WaitForSecondsRealtime(0.1f);
			selectSprite.enabled = true;
			yield return new WaitForSecondsRealtime(0.1f);
		}
	}

	void selectedPower()
	{

		if(selected == 0)
		{
			selectPower.transform.position = selectPositions[0].position;
		} else if ( selected == 1)
		{
			if(gameManager.getPower[0])
			{
				selectPower.transform.position = selectPositions[1].position;
			} else {
				selected ++;
			}
		}else if ( selected == 2)
		{
			if(gameManager.getPower[1])
			{
				selectPower.transform.position = selectPositions[2].position;
			} else {
				selected ++;
			}
		}else if ( selected == 3)
		{
			if(gameManager.getPower[2])
			{
				selectPower.transform.position = selectPositions[3].position;
			} else {
				selected ++;
			}
		}else if ( selected == 4)
		{
			if(gameManager.getPower[3])
			{
				selectPower.transform.position = selectPositions[4].position;
			} else {
				selected ++;
			}
		}else if ( selected == 5)
		{
			if(gameManager.getPower[4])
			{
				selectPower.transform.position = selectPositions[5].position;
			} else {
				selected ++;
			}
		}else if ( selected == 6)
		{
			if(gameManager.getPower[5])
			{
				selectPower.transform.position = selectPositions[6].position;
			} else {
				selected ++;
			}
		}else if ( selected == 7)
		{
			if(gameManager.getPower[6])
			{
				selectPower.transform.position = selectPositions[7].position;
			} else {
				selected ++;
			}
		}
	}

	void actived()
	{
		powers[0].SetActive(gameManager.getPower[0]);
		powers[1].SetActive(gameManager.getPower[1]);
		powers[2].SetActive(gameManager.getPower[2]);
		powers[3].SetActive(gameManager.getPower[3]);
		powers[4].SetActive(gameManager.getPower[4]);
		powers[5].SetActive(gameManager.getPower[5]);
		powers[6].SetActive(gameManager.getPower[6]);
	}

}
