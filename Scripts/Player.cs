using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]

public class Player : MonoBehaviour 
{
	[Header("Variaveis Gerais")]
	private Animator playerAnimator;
	private Rigidbody2D playerBody;
	private SpriteRenderer playerSprite;
	private AudioSource somSource;
	private GameManager gameManager;
	private int maxLife;
	private int life;
	public Image lifeBar;
	public CameraFollow cameraScript;

	[Header("Variaveis do Movimento")]
	private bool isRight;
	private float inputAxis;
	public float playerSpeed;

	[Header("Variaveis do Pulo")]
	private bool notJumping;
	private float raio = 1.5f;
	private bool reJump;
	public float  jumpForce;
	public LayerMask ground;
	public Transform[] groundCheck;
	public float jumpTime;
	private bool jumpCheck;

	[Header("Variaveis do Tiro")]
	private float nextFire;
	private float fireRate;
	public AudioClip shotEfect;
	public GameObject[] shotObject;
	public Transform spawShot;
	private int powerSelect = 0;

	[Header("Variaveis do Dano")]
	public bool tookDamange;
	private bool tookingDamange;
	public int enemyDamange;
	public AudioClip damangEfect;
	public float imunityTime;
	private bool j;

	[Header("Variaveis da Morte")]
	private bool isDead = false;
	private bool fallKill;
	public AudioClip deathEfect;
	public GameObject playerDeath;


	
	void Start () 
	{
		playerAnimator = GetComponent<Animator>();
		somSource = GetComponent<AudioSource>();
		playerSprite = GetComponent<SpriteRenderer>();
		playerBody = GetComponent<Rigidbody2D>();
		gameManager = GameManager.gameManeger;
		maxLife = gameManager.playerLife;
		life = maxLife;
	}
	
	void FixedUpdate () 
	{
		playerAnimator.SetBool("Damange", tookingDamange);
		lifeBar.fillAmount = life / 20f ;
		moviment();
		jump();
		shot();
	//	damangeCheck();
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
		
		if(Input.GetButton("Left") && Input.GetButton("Right"))
		{
			playerSpeed = 0f;
		} else {
			playerSpeed = 100f;
		}
		
		if(!tookingDamange && !isDead)
		{
			playerBody.velocity = new Vector2(inputAxis * playerSpeed, playerBody.velocity.y);
		}
	}

	void jump()
	{
		bool checkOne = Physics2D.OverlapCircle(groundCheck[0].position, raio, ground);
		bool checkTwo = Physics2D.OverlapCircle(groundCheck[1].position, raio, ground);

		if(checkOne || checkTwo)
		{
			notJumping = true;
		} else {
			notJumping = false;
		}

		playerAnimator.SetBool("Jumping", !notJumping);

		if(Input.GetButton("Jump") && notJumping && reJump)
		{
			jumpCheck = true;
			reJump = false;
		}

		if(jumpCheck)
		{
			if(Input.GetButton("Jump"))
			{
				//playerBody.velocity = Vector2.up * jumpForce;
				playerBody.velocity = new Vector2(playerBody.velocity.x, jumpForce);
				jumpTime += Time.deltaTime;
			} else if(Input.GetButton("Jump") == false)
			{
				jumpCheck = false;
				jumpTime = 0f;
				reJump = true;
			}

			if(jumpTime >= 0.2f )
			{
				jumpCheck = false;
				jumpTime = 0f;
			}

		}

		if(Input.GetButton("Jump") == false && !reJump && notJumping)
		{
			reJump = true;
		}
	}

	void shot()
	{
		if(Input.GetButtonDown("Shot") && Time.time > nextFire && !tookingDamange)
		{
			playerAnimator.SetTrigger("ShoootingT");
			fireRate = gameManager.fireRate;
			somSource.clip = shotEfect;
			somSource.PlayOneShot(somSource.clip);
			nextFire = Time.time + fireRate;
			GameObject tempShot = Instantiate(shotObject[powerSelect], spawShot.position, spawShot.rotation);
			if(!isRight)
			{
				tempShot.transform.eulerAngles = new Vector3(0f, 0f, 180f);
			}
		}
	}	

	void damangeCheck()
	{
		if(tookDamange && !tookingDamange)
		{
			StartCoroutine(damange());
		}
	}

	void flip()
	{
		isRight = !isRight;
	}

	void reload()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	IEnumerator damange()
	{
		
		tookingDamange = true;
		j = true;
		life -= enemyDamange;

		if(life <= 0)
		{
			isDead = true;
			playerSprite.enabled = false;
			somSource.clip = deathEfect;
			somSource.PlayOneShot(somSource.clip);
			if(!fallKill)
			{
				Instantiate(playerDeath, transform.position, transform.rotation);
			}
			Invoke("reload", 2f);
		} else {
			Physics2D.IgnoreLayerCollision(8, 12);
			Physics2D.IgnoreLayerCollision(8, 13);
			somSource.clip = damangEfect;
			somSource.PlayOneShot(somSource.clip);

			StartCoroutine(damangeVisualEfect());

			if(isRight)
			{
				playerBody.velocity = new Vector2( -25, playerBody.velocity.y);
			} else {
				playerBody.velocity = new Vector2( 25, playerBody.velocity.y);
			}

			yield return new WaitForSeconds(imunityTime / 2);
			tookingDamange = false;
			yield return new WaitForSeconds(imunityTime);
			j = false;
		}


	}

	IEnumerator damangeVisualEfect()
	{
		for (int i = 0; i < 1; i--)
		{
			if(j)
			{
				playerSprite.enabled = false;
				yield return new WaitForEndOfFrame();
				playerSprite.enabled = true;
				yield return new WaitForEndOfFrame(); 
			
			} else {
				i = 4;
				playerSprite.enabled = true;
			}
		}

		Physics2D.IgnoreLayerCollision(8, 12, false);
		Physics2D.IgnoreLayerCollision(8, 13, false);
		StopCoroutine(damangeVisualEfect());
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if(other.CompareTag("Enemy") && !tookDamange)
		{
			StartCoroutine(damange());
		}

		if(other.CompareTag("FallKill"))
		{
			life = 0;
			fallKill = true;
			StartCoroutine(damange());
		}

		if(other.CompareTag("InstaKill") && !j)
		{
			life = 0;
			StartCoroutine(damange());
		}

		if(other.CompareTag("Platform"))
		{
			this.transform.parent = other.transform;
		}
	}

	private void OnCollisionEnter2D(Collision2D other)
	{
		if(other.gameObject.CompareTag("Enemy") && !tookDamange)
		{
			StartCoroutine(damange());
		}

		if(other.gameObject.CompareTag("FallKill"))
		{
			life = 0;
			fallKill = true;
			StartCoroutine(damange());
		}

		if(other.gameObject.CompareTag("InstaKill") && !j)
		{
			life = 0;
			//tookDamange = true;
			StartCoroutine(damange());
		}

		if(other.gameObject.CompareTag("Platform"))
		{
			this.transform.parent = other.transform;
		}
	}


	private void OnTriggerStay2D(Collider2D other)
	{
		if(other.CompareTag("Platform"))
		{
			this.transform.parent = other.transform;
		}

		if(other.gameObject.CompareTag("InstaKill") && !j)
		{
			life = 0;
			//tookDamange = true;
			StartCoroutine(damange());
		}

		if(other.CompareTag("ChangeCamera0"))
		{
			cameraScript.changePosition[0] = true;
		}

		if(other.CompareTag("ChangeCamera1"))
		{
			cameraScript.changePosition[0] = false;
			cameraScript.changePosition[1] = true;
		}

		if(other.CompareTag("ChangeCamera2"))
		{
			cameraScript.changePosition[1] = false;
			cameraScript.changePosition[2] = true;
		}
	}

	private void OnCollisionStay2D(Collision2D other)
	{
		if(other.gameObject.CompareTag("Platform"))
		{
			this.transform.parent = other.transform;
		}

		if(other.gameObject.CompareTag("InstaKill") && !j)
		{
			life = 0;
			//tookDamange = true;
			StartCoroutine(damange());
		}
	}


	private void OnTriggerExit2D(Collider2D other)
	{
		if(other.CompareTag("Platform"))
		{
			this.transform.parent = null;
		}
	}

	private void OnCollisionExit2D(Collision2D other)
	{
		if(other.gameObject.CompareTag("Platform"))
		{
			this.transform.parent = null;
		}
	}
}
