using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(AudioSource))]

public class EnemyBase : MonoBehaviour {

	public int enemyLife;
	public float enemySpeed;
	protected bool facingRight;
	protected float playerDistance;
	public float atackDistance;
	protected bool blocking;
	public int enemyDamange;

	protected Transform playerTarget;
	public GameObject deathAnimation;
	public GameObject dropEnemy;
	protected Rigidbody2D enemyBody;
	protected Animator enemyAnimator;
	protected SpriteRenderer enemySprite;
	protected AudioSource enemyAudiioSource;
	public AudioClip blockSong;
	public AudioClip damangeSong;

	void Awake () 
	{
		enemyBody = GetComponent<Rigidbody2D> ();
		enemyAnimator = GetComponent<Animator> ();
		playerTarget = FindObjectOfType<Player> ().transform;
		enemySprite = GetComponent<SpriteRenderer> ();
		enemyAudiioSource = GetComponent<AudioSource> ();
	}
	
	protected virtual void Update () 
	{
		playerDistance = transform.position.x - playerTarget.position.x;
	}

	protected void fliped()
	{
		facingRight = !facingRight;
	}

	public void EnemyTookDamange(int damange)
	{
		if(blocking == false)
		{
			enemyLife -= damange;
		}

		if (enemyLife <= 0) 
		{
			Instantiate (deathAnimation, transform.position, transform.rotation);
			gameObject.SetActive (false);
			//Descomentar quando for compilar ou se o jogo estiver muito pesado
			//Destroy(gameObject)
		} else if (blocking == false) {
			enemyAudiioSource.clip = damangeSong;
			enemyAudiioSource.volume = 0.7f;
			enemyAudiioSource.PlayOneShot (enemyAudiioSource.clip);
			//StartCoroutine (EnemyDemange ());
		} else {
			enemyAudiioSource.clip = blockSong;
			enemyAudiioSource.PlayOneShot (enemyAudiioSource.clip);
		}
	}

	/*IEnumerator EnemyDemange()
	{
			enemySprite.color = Color.red;
		yield return new WaitForSeconds (0.1f);
			enemySprite.color = Color.white;
	}*/

}
