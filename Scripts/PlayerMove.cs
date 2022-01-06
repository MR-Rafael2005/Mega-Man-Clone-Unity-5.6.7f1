using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour 
{
	[Header("Variaveis Gerais")]
	private Rigidbody2D playerBody;

	[Header("Variaveis do movimento")]
	private float speed = 100f;

	[Header("Variaveis do pulo")]
	public bool notJumping;
	private bool check;
	private bool checkTwo;
	public Transform groundCheckPosition;
	public Transform groundCheckPositionTwo;
	public LayerMask ground;
	private float raio = 0.5f;
	public float jumpForce;
	private bool reJump;
	public Player playerTrue;

	void Start ()
	{
		playerBody = GetComponent<Rigidbody2D> ();
		reJump = true;
	}
	
	void FixedUpdate () 
	{
		move();
		jumping ();
	}

	void move()
	{
		/*Vector3 movi = new Vector3 (Input.GetAxis ("Horizontal"), 0f, 0f); 
		transform.position += movi * Time.deltaTime * speed;*/
		if(playerTrue.isDead)
		{
			speed = 0f;
		}

		float axis = Input.GetAxis ("Horizontal");

		if (Input.GetButton ("Left") && Input.GetButton ("Right")) 
		{
			speed = 0f;
		} else {
			speed = 100f;
		}
		if(playerTrue.tookDamange == false)
		{
			playerBody.velocity = new Vector2 (axis * speed, playerBody.velocity.y);
		}
	}

	void jumping()
	{
		check = Physics2D.OverlapCircle (groundCheckPosition.position, raio, ground);
		checkTwo = Physics2D.OverlapCircle (groundCheckPositionTwo.position, raio, ground);

		if(check || checkTwo)
		{
			notJumping = true;
		} else {
			notJumping = false;
		}

		if (Input.GetButton ("Jump") && notJumping && reJump && playerTrue.tookDamange == false) 
		{
			playerBody.AddForce (new Vector2 (0f, jumpForce));
			reJump = false;
		} else if (Input.GetButton ("Jump") == false && reJump == false) 
		{
			reJump = true;
		}
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if(other.gameObject.CompareTag("Platform"))
		{
			this.transform.parent = other.transform;
		}
	}

	private void OnCollisionEnter2D(Collision2D other) 
	{
		if(other.gameObject.CompareTag("Platform"))
		{
			this.transform.parent = other.transform;
		}	
	}

	private void OnCollisionStay2D(Collision2D other)
	{
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
	}

	private void OnTriggerExit2D(Collider2D other)
	{
		if(other.gameObject.CompareTag("Platform"))
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
