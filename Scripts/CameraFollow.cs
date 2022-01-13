using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour 
{
	public float xMargin;		// Distance in the x axis the player can move before the camera follows.
	public float yMargin;		// Distance in the y axis the player can move before the camera follows.
	public float xSmooth;		// How smoothly the camera catches up with it's target movement in the x axis.
	public float ySmooth;		// How smoothly the camera catches up with it's target movement in the y axis.
	public Vector2[] maxXAndY;		// The maximum x and y coordinates the camera can have.
	public Vector2[] minXAndY;		// The minimum x and y coordinates the camera can have.
	public bool[] changePosition;

	private Transform player;		// Reference to the player's transform.



	void Awake ()
	{
		// Setting up the reference.
		player = GameObject.FindGameObjectWithTag("Player").transform;
		changePosition[0] = true;
	}


	bool CheckXMargin()
	{
		// Returns true if the distance between the camera and the player in the x axis is greater than the x margin.
		return Mathf.Abs(transform.position.x - player.position.x) > xMargin;
	}


	bool CheckYMargin()
	{
		// Returns true if the distance between the camera and the player in the y axis is greater than the y margin.
		return Mathf.Abs(transform.position.y - player.position.y) > yMargin;
	}


	void FixedUpdate ()
	{
		TrackPlayer();
	}
	
	
	void TrackPlayer ()
	{
		// By default the target x and y coordinates of the camera are it's current x and y coordinates.
		float targetX = transform.position.x;
		float targetY = transform.position.y;

		// If the player has moved beyond the x margin...
		if(CheckXMargin())
			// ... the target x coordinate should be a Lerp between the camera's current x position and the player's current x position.
			targetX = Mathf.Lerp(transform.position.x, player.position.x, xSmooth * Time.deltaTime);

		// If the player has moved beyond the y margin...
		if(CheckYMargin())
			// ... the target y coordinate should be a Lerp between the camera's current y position and the player's current y position.
			targetY = Mathf.Lerp(transform.position.y, player.position.y, ySmooth * Time.deltaTime);

		// The target x and y coordinates should not be larger than the maximum or smaller than the minimum.
		if(changePosition[0])
		{
			targetX = Mathf.Clamp(targetX, minXAndY[0].x, maxXAndY[0].x);
			targetY = Mathf.Clamp(targetY, minXAndY[0].y, maxXAndY[0].y);
		} else if(changePosition[1]) 
		{
			targetX = Mathf.Clamp(targetX, minXAndY[1].x, maxXAndY[1].x);
			targetY = Mathf.Clamp(targetY, minXAndY[1].y, maxXAndY[1].y);
		} else if(changePosition[2])
		{
			targetX = Mathf.Clamp(targetX, minXAndY[2].x, maxXAndY[2].x);
			targetY = Mathf.Clamp(targetY, minXAndY[2].y, maxXAndY[2].y);
		}
		// Set the camera's position to the target position with the same z component.
		transform.position = new Vector3(targetX, targetY, transform.position.z);
	}

}
