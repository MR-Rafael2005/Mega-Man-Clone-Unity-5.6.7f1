using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//[RequireComponent(typeof(AudioSource))]
public class Player : MonoBehaviour 
{

	[Header("Variaveis gerais")]
	public Rigidbody2D playerBody;
	private Animator playerAnimator;
	public AudioSource somSource;
	private GameManager gameManeger;
	private SpriteRenderer sprite;
	private int life;
	private int maxLife;
	public GameObject playerDeath;
	public Image lifeBar;
	public Transform deathPosition;

	[Header("Variaveis de movimento")]
	private float inputAxis;
	private bool isRight;

	[Header("Variaveis do pulo")]
	public bool notJumping;
	private bool check;
	private bool checkTwo;
	public Transform groundCheckPosition;
	public Transform groundCheckPositionTwo;
	public LayerMask ground;
	private float raio = 3f;

	[Header("Variaveis do tiro")]
	//private GameObject spawShot;
	public GameObject shotObject;
	public Transform spawShotPosition;
	private float nextFire;
	private float fireRate = 0.2f;
	public AudioClip shotSoundEfect;

	[Header("Variaveis do dano")]
	public float imunityTime;
	public bool tookDamange = false;
	public int enemyDamange;
	public bool isDead = false;
	private bool j;
	private bool fallKill = false;
	public AudioClip damangeSong;
	public AudioClip deathSong;


	void Start () 
	{
		playerAnimator = GetComponent<Animator> ();
		somSource = GetComponent<AudioSource> ();
		gameManeger = GameManager.gameManeger;
		sprite = GetComponent<SpriteRenderer> ();
		PlayerStatus ();
		life = maxLife;
	}
	
	//void Update () {}

	void FixedUpdate ()
	{
		lifeBar.fillAmount = life / 20f;
		moviment ();
		jump ();
		shot ();
		exit();
	}

	void moviment()
	{
		playerAnimator.SetFloat ("Moving", Mathf.Abs (Input.GetAxisRaw ("Horizontal")));

		inputAxis = Input.GetAxis("Horizontal");

		if (inputAxis > 0f && !isRight) 
		{
			flip ();
		}
		if (inputAxis < 0f && isRight) 
		{
			flip ();
		}

		if (isRight) 
		{
			transform.eulerAngles = new Vector2 (0f, 0f);
		}
		if (!isRight) 
		{
			transform.eulerAngles = new Vector2 (0f, 180f);
		}

		
		playerAnimator.SetBool("Damange", tookDamange);
	}

	void flip()
	{
		isRight = !isRight;
	}

	void jump()
	{
		playerAnimator.SetBool ("Jumping", !notJumping);
		
		check = Physics2D.OverlapCircle (groundCheckPosition.position, raio, ground);
		checkTwo = Physics2D.OverlapCircle (groundCheckPositionTwo.position, raio, ground);

		if(check || checkTwo)
		{
			notJumping = true;
		} else {
			notJumping = false;
		}
	}

	void shot()
	{		
		if (Input.GetButtonDown ("Shot") && Time.time > nextFire && !tookDamange) 
		{
			playerAnimator.SetTrigger ("ShoootingT");
			somSource.clip = shotSoundEfect;
			somSource.PlayOneShot (somSource.clip);
			nextFire = Time.time + fireRate;
			GameObject tempShot = Instantiate (shotObject, spawShotPosition.position, spawShotPosition.rotation);
			if (!isRight) 
			{
				tempShot.transform.eulerAngles = new Vector3 (0f, 0f, 180f);
			}
		}
	}

	public void PlayerStatus()
	{
		fireRate = gameManeger.fireRate;
		maxLife = gameManeger.playerLife;
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag ("Enemy") && tookDamange == false) 
		{
			StartCoroutine (TookingDamange ());
		}

		if(other.CompareTag("FallKill") && tookDamange == false)
		{
			fallKill = true;
			life = 0;
			StartCoroutine(TookingDamange());
		}
	}

	private void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.CompareTag("Enemy") && tookDamange == false) 
		{
			StartCoroutine (TookingDamange ());
		}
		
		if(other.gameObject.CompareTag("FallKill") && tookDamange == false)
		{
			life = 0;
			fallKill = true;
			StartCoroutine(TookingDamange());
		}

		
	}

	private void OnTriggerStay2D(Collider2D other)
	{
		if (other.CompareTag ("Enemy") && tookDamange == false) 
		{
			StartCoroutine (TookingDamange ());
			
		}

		if(other.CompareTag("FallKill") && tookDamange == false)
		{
			life = 0;
			fallKill = true;
			StartCoroutine(TookingDamange());
		}
	}

	private void OnCollisionStay2D(Collision2D other)
	{
		if (other.gameObject.CompareTag("Enemy") && tookDamange == false) 
		{
			StartCoroutine (TookingDamange ());
		}

		if(other.gameObject.CompareTag("FallKill") && tookDamange == false)
		{
			life = 0;
			fallKill = true;
			StartCoroutine(TookingDamange());
		}
	}

	IEnumerator TookingDamange()
	{
		tookDamange = true;
		j = true;
		life = life - enemyDamange;

		if (life <= 0) 
		{
			isDead = true;
			//playerAnimator.SetTrigger ("Dead");
			sprite.enabled = false;
			somSource.clip = deathSong;
			somSource.PlayOneShot (somSource.clip);
			if(!fallKill)
			{	
				Instantiate(playerDeath, deathPosition.position, deathPosition.rotation);
			}
			Invoke ("Reload", 2f);
		} else {
			Physics2D.IgnoreLayerCollision (8, 12);
			Physics2D.IgnoreLayerCollision (8, 13);
			somSource.clip = damangeSong;
			somSource.PlayOneShot (somSource.clip);
			StartCoroutine(damangeEfect());
			
			if(isRight)
			{
				playerBody.velocity = new Vector2( -25f, playerBody.velocity.y );
			} else{
				playerBody.velocity = new Vector2( 25f, playerBody.velocity.y );
			}
			yield return new WaitForSeconds(imunityTime / 2);
			tookDamange = false;
			yield return new WaitForSeconds(imunityTime );
			j = false;
			//Physics2D.IgnoreLayerCollision (8, 12, false);
			//Physics2D.IgnoreLayerCollision (8, 13, false);

			//tookDamange = false;
		}

	}

	void Reload()
	{
		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
	}

	void exit()
	{
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			SceneManager.LoadScene("StageSelect");
		}
	}

	IEnumerator damangeEfect()
	{
		for (int i = 0; i < 2; i--)
		{
			if(j)
			{
				sprite.enabled = false;
				yield return new WaitForEndOfFrame();
				sprite.enabled = true;
				yield return new WaitForEndOfFrame();
			} else {
				i = 4;
			}
		}

		Physics2D.IgnoreLayerCollision (8, 12, false);
		Physics2D.IgnoreLayerCollision (8, 13, false);
		StopCoroutine(damangeEfect());
		
	}

}
