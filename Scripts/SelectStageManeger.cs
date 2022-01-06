using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectStageManeger : MonoBehaviour 
{

	private GameObject selectefect;
	public int bossselected;
	public Transform[] positionselect;
	public AudioClip song;
	private AudioSource somsource;

	void Start () 
	{
		somsource = GetComponent<AudioSource> ();
		selectefect = GameObject.Find ("StageSelectefect");
		bossselected = 1;
	}
	
	void FixedUpdate () 
	{
		select();
		selectmove ();
		enterstage ();
	}

	void select()
	{
		if (Input.GetButtonDown ("Left")) 
		{
			bossselected --;
			somsource.clip = song;
			somsource.PlayOneShot (somsource.clip);
		}
		if (Input.GetButtonDown ("Right")) 
		{
			bossselected ++;
			somsource.clip = song;
			somsource.PlayOneShot (somsource.clip);
		}

		if (bossselected < 1) 
		{
			bossselected = 6;
		}
		if (bossselected > 6) 
		{
			bossselected = 1;
		}
	}

	void selectmove()
	{
		if (bossselected == 1) {
			selectefect.transform.position = positionselect [0].position;
		} else if (bossselected == 2) {
			selectefect.transform.position = positionselect [1].position;
		} else if (bossselected == 3) {
			selectefect.transform.position = positionselect [2].position;
		} else if (bossselected == 4) {
			selectefect.transform.position = positionselect [3].position;
		} else if (bossselected == 5) {
			selectefect.transform.position = positionselect [4].position;
		} else if (bossselected == 6) {
			selectefect.transform.position = positionselect [5].position;
		} 
	}

	void enterstage()
	{
		if (Input.GetButtonDown ("Start")) 
		{
			if (bossselected == 1) 
			{
				SceneManager.LoadScene ("CutManStage");
			} else if(bossselected == 2) {
				SceneManager.LoadSceneAsync ("GutsManStage");
			} else if(bossselected == 3) {
				SceneManager.LoadScene ("IceManStage");
			} else if(bossselected == 4) {
				SceneManager.LoadScene ("BombManStage");
			} else if(bossselected == 5) {
				SceneManager.LoadScene ("FireManStage");
			} else if(bossselected == 6) {
				SceneManager.LoadScene ("ElecManStage");
			}
		}
	}

}
