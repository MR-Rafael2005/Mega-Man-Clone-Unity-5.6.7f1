using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MenuManeger : MonoBehaviour {


	private AudioSource startefect;
	public AudioClip startSong;
	private GameObject pressStart;
	private Image startImage;
	public float time;

	void Start () 
	{
		startefect = GetComponent<AudioSource> ();
		pressStart = GameObject.Find ("PressSTART");
		startImage = pressStart.GetComponent<Image> ();
	}
	
	void Update () 
	{
		quitgame ();
		startButton ();
	}

	public void quitgame()
	{
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			//UnityEditor.EditorApplication.isPlaying = false;
			//Descomentar apenas quando for compilar
			Application.Quit();
	
		}

	}

	public void startgame()
	{
		StartCoroutine (start ());
	}

	void startButton()
	{
		if (Input.GetButtonDown ("Start")) 
		{
			StartCoroutine (start ());
		}
	}

	IEnumerator start()
	{

		startefect.clip = startSong;
		startefect.PlayOneShot (startefect.clip);
		//yield return new WaitForSeconds (1f);

		for (float i = 0f; i < time; i += 0.2f) 
		{
			startImage.enabled = false;
			yield return new WaitForSeconds (0.1f);
			startImage.enabled = true;
			yield return new WaitForSeconds (0.1f);
		}
		SceneManager.LoadScene ("StageSelect");
		StopCoroutine (start ());
	}

}